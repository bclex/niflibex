/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */


using System;
using System.Collections.Generic;

namespace Niflib
{
    /*!
     * Used by the ComplexShape::WeightedVertex strut to store a single
     * skin-weight/bone influence combination for a vertex.
     */
    public struct SkinInfluence
    {
        /*! Constructor */
        public SkinInfluence()
        {
            influenceIndex = ComplexShape.CS_NO_INDEX;
        }
        /*! 
         * Index into the ComplexShape::skinInfluences array of the bone
         * influence for this skin weight.
         */
        uint influenceIndex;
        /*! 
         * The amount of influence the indexed bone has on this vertex, between
         * 0.0 and 1.0
         */
        float weight;
    }

    /*!
     * Used by the ComplexShape class to store a single vertex and any
     * Associated skin weights
     */
    public struct WeightedVertex
    {
        /*! The 3D position of this vertex. */
        Vector3 position;
        /*! A list of weight/influence index pairs for this vertex. */
        List<SkinInfluence> weights;
    }

    /*!
     * Used by the ComplexShape::ComplexPoint struct to store a single texture
     * cooridinate set/texture coordinate pair of indices.
     */
    public struct TexCoordIndex
    {
        /*! Constructor */
        public TexCoordIndex()
        {
            texCoordSetIndex = ComplexShape.CS_NO_INDEX;
            texCoordIndex = ComplexShape.CS_NO_INDEX;
        }

        /*!
         * Index into the ComplexShape::texCoordSets array of texture
         * coordinate sets.
         */
        uint texCoordSetIndex;

        /*!
         * Index into the ComplexShape::TexCoordSet::texCoords array of the
         * texture coordinate set referenced by texCoordSetIndex.
         */
        uint texCoordIndex;
    }

    /*!
     * Used by ComplexShape::ComplexFace class to describe a single point in
     * the 3D model.  Points share their data in case of duplication, so all
     * information, such as position, normal vector, texture coordinates, etc.,
     * are stored as indices into the asociated data arrays.
     */
    public struct ComplexPoint
    {
        /*! Constructor */
        public ComplexPoint()
        {
            vertexIndex = ComplexShape.CS_NO_INDEX;
            normalIndex = ComplexShape.CS_NO_INDEX;
            colorIndex = ComplexShape.CS_NO_INDEX;
        }
        /*! 
         * Index into the ComplexShape::vertices array which stores the
         * position and any associated skin weights for this point.
         */
        uint vertexIndex;
        /*! 
         * Index into the ComplexShape::normals array which stores the normal
         * vector for this point.
         */
        uint normalIndex;
        /*!
         * Index into the ComplexShape::colors array which stores the vertex
         * color for this point
         */
        uint colorIndex;
        /*!
         * An array of texture coordinate set/texture coordinate index pairs
         * describing all the UV coordinates for this point.
         */
        List<TexCoordIndex> texCoordIndices;
    }

    /*! 
     * Used by ComplexShape to describe a single polygon.  Complex shape
     * polygons can have more than three points, unlike the triangels required
     * within the NIF format.  Each face may also be associated with a
     * different set of NiProperty classes, enabling each face to have unique
     * material settings.
     */
    public struct ComplexFace
    {
        /*! Constructor */
        public ComplexFace()
        {
            propGroupIndex = ComplexShape.CS_NO_INDEX;
        }

        /*! A list of points which make up this polygon */
        List<ComplexPoint> points;
        /*!
         * Index into the ComplexShape::propGroups array which specifies which
         * set of NiProperty classes to apply to this face.
         */
        uint propGroupIndex;
    }

    /*!
     * Used by ComplexShape to store texture coordinate data and the
     * associated type of texture, such as base, detail, or dark map.
     */
    public struct TexCoordSet
    {
        /*!
         * The type of the texture such as base, detail, bump, etc.
         */
        TexType texType;
        /*!
         * A list of all the texture cooridnates for this texture set.
         */
        List<TexCoord> texCoords;
    }

    /*!
     * This class is a helper object to ease the process of converting between the
     * 3D model format of a NIF file, which is optimized for real time display via
     * OpenGL or DirectX, and the more compact, complex format usually prefered by
     * 3D modeling software.
     * 
     * It is capable of mering multiple NiTriShape objects into one multi-material
     * object with indexed data, or taking such an object and splitting it up into
     * multiple NiTriShape objects.
     */
    public class ComplexShape
    {
        /*! Marks empty data indices */
        const uint CS_NO_INDEX = 0xFFFFFFFF;

        /*!
         * This function splits the contents of the ComplexShape into multiple
         * NiTriBasedGeom objects.
         * \param parent The parent NiNode that the resulting NiTriBasedGeom
         * objects will be attached to.
         * \param transform The transform for the resulting object or group of
         * objects
         * \param max_bones_per_partition The maximum number of bones to allow in
         * each skin partition.  Set to zero to skip creation of partition.
         * \param stripify Whether or not to generate efficient triangle strips.
         * \param tangent_space Whether or not to generate Oblivion tangent space
         * information.
         * \param min_vertex_weight Remove vertex weights bellow a given value
         * \param use_dismember_partitions Uses BSDismemberSkinInstance with custom partitions for dismember
         * \return A reference to the root NiAVObject that was created.
         */
        public Ref<NiAVObject> Split(NiNode parent, Matrix44 transform, int max_bones_per_partition = 4, bool stripify = false, bool tangent_space = false, float min_vertex_weight = 0.001f, byte tspace_flags = 0)
        {
            //Make sure parent is not NULL
            if (parent == null)
                throw new InvalidOperationException("A parent is necessary to split a complex shape.");

            bool use_dismember_partitions = false;
            if (DismemberPartitionsFaces.Count > 0)
            {
                if (DismemberPartitionsFaces.Count != Faces.Count)
                    throw new InvalidOperationException("The number of faces mapped to skin partitions is different from the actual face count.");
                if (DismemberPartitionsBodyParts.Count == 0)
                    throw new InvalidOperationException("The number of dismember partition body parts can't be 0.");
                use_dismember_partitions = true;
            }

            //There will be one NiTriShape per property group
            //with a minimum of 1
            uint num_shapes = (uint)propGroups.Count;
            if (num_shapes == 0)
                num_shapes = 1;

            vector<NiTriBasedGeomRef> shapes(num_shapes);
            //Loop through each shape slot and create a NiTriShape
            for (uint shape_num = 0; shape_num < Shapes.Count; ++shape_num)
                Shapes[shape_num] = stripify ? new NiTriStrips() : new NiTriShape();

            NiAVObjectRef root;
            //If there is just one shape, return it.  Otherwise
            //create a node, parent all shapes to it, and return
            // that
            if (Shapes.Count == 1)
            {
                //One shape
                Shapes[0].Name = Name;
                root = (NiAVObject)Shapes[0];
            }
            else
            {
                //Multiple shapes
                NiNodeRef niNode = new NiNode();
                niNode.Name(Name);
                for (uint i = 0; i < Shapes.Count; ++i)
                {
                    niNode.AddChild((NiAVObject)Shapes[i]);
                    //Set Shape Name
                    Shapes[i].Name = $"{ShapeName}{Name} {i}";
                }
                root = (NiAVObject)niNode;
            }
            parent.AddChild(root);

            //Set transform of root
            root.SetLocalTransform(transform);

            //Create NiTriShapeData and fill it out with all data that is relevant
            //to this shape based on the material.
            for (uint shape_num = 0; shape_num < Shapes.size(); ++shape_num)
            {
                NiTriBasedGeomDataRef niData = stripify ? new NiTriStripsData() : new NiTriShapeData();
                shapes[shape_num].SetData((NiGeometryData)niData);

                //Create a list of CompoundVertex to make it easier to
                //test for the need to clone a vertex
                List<CompoundVertex> compVerts = new List<CompoundVertex>();

                //List of triangles for the final shape to use
                List<Triangle> shapeTriangles = new List<Triangle>();

                //a vector that holds in what dismember groups or skin partition does each face belong
                List<BodyPartList> current_dismember_partitions = DismemberPartitionsBodyParts;

                //create a map betweem the faces and the dismember groups
                List<uint> current_dismember_partitions_faces = new List<uint>();

                //since we might have dismember partitions the face index is also required
                int current_face_index = 0;

                //Loop through all faces, and all points on each face
                //to set the vertices in the CompoundVertex list
                foreach (face in Faces)
                {
                    //Ignore faces with less than 3 vertices
                    if (face.Points.Count < 3)
                        continue;

                    //Skip this face if the material does not relate to this shape
                    if (face.PropGroupIndex != shape_num)
                        continue;

                    List<ushort> shapeFacePoints = new List<ushort>();
                    foreach (var point in face.Points)
                    {
                        //--Set up Compound vertex--//
                        CompoundVertex cv;
                        if (Vertices.Count > 0)
                        {
                            WeightedVertex wv = vertices[point.VertexIndex];
                            cv.Position = wv.Position;
                            if (SkinInfluences.Count > 0)
                                for (uint i = 0; i < wv.Weights.Count; ++i)
                                {
                                    SkinInfluence inf = wv.Weights[i];
                                    cv.Weights[skinInfluences[inf.InfluenceIndex]] = inf.Weight;
                                }
                        }
                        if (Normals.Count > 0)
                            cv.Normal = Normals[point.NormalIndex];
                        if (colors.Count > 0)
                            cv.Color = Colors[point.ColorIndex];
                        if (texCoordSets.size() > 0)
                            for (uint i = 0; i < point.TexCoordIndices.Count; ++i)
                            {
                                TexCoordSet set = texCoordSets[point.TexCoordIndices[i].TexCoordSetIndex];
                                cv.TexCoords[set.TexType] = set.TexCoords[point.TexCoordIndices[i].TexCoordIndex];
                            }

                        bool found_match = false;
                        //Search for an identical vertex in the list
                        for (ushort cv_index = 0; cv_index < compVerts.Count; ++cv_index)
                            if (compVerts[cv_index] == cv)
                            {
                                //We found a match, push its index into the face list
                                found_match = true;
                                shapeFacePoints.Add(cv_index);
                                break;
                            }

                        //If no match was found, append this vertex to the list
                        if (!found_match)
                        {
                            compVerts.Add(cv);
                            //put the new vertex into the face point list
                            shapeFacePoints.Add((uint)compVerts.Count - 1);
                        }
                        //Next Point
                    }

                    if (!use_dismember_partitions)
                    {
                        //Starting from vertex 0, create a fan of triangles to fill
                        //in non-triangle polygons
                        Triangle new_face;
                        for (uint i = 0; i < shapeFacePoints.Count - 2; ++i)
                        {
                            new_face[0] = shapeFacePoints[0];
                            new_face[1] = shapeFacePoints[i + 1];
                            new_face[2] = shapeFacePoints[i + 2];
                            //Push the face into the face list
                            shapeTriangles.Add(new_face);
                        }
                        //Next Face
                    }
                    else
                    {
                        //Starting from vertex 0, create a fan of triangles to fill
                        //in non-triangle polygons
                        Triangle new_face;
                        for (uint i = 0; i < shapeFacePoints.Count - 2; ++i)
                        {
                            new_face[0] = shapeFacePoints[0];
                            new_face[1] = shapeFacePoints[i + 1];
                            new_face[2] = shapeFacePoints[i + 2];

                            //Push the face into the face list
                            shapeTriangles.Add(new_face);

                            //all the resulting triangles belong in the the same dismember partition or better said skin partition
                            current_dismember_partitions_faces.Add(dismemberPartitionsFaces[current_face_index]);
                        }
                    }
                    current_face_index++;
                }

                //Clean up the dismember skin partitions
                //if no face points to a certain dismember partition then that dismember partition must be removed
                if (use_dismember_partitions)
                {
                    List<bool> used_dismember_groups = new List<bool>(current_dismember_partitions.Count); //: (, false);
                    for (uint x = 0; x < current_dismember_partitions_faces.Count; x++)
                        if (!used_dismember_groups[current_dismember_partitions_faces[x]])
                            used_dismember_groups[current_dismember_partitions_faces[x]] = true;
                    List<BodyPartList> cleaned_up_dismember_partitions = new List<BodyPartList>();
                    for (uint x = 0; x < current_dismember_partitions.Count; x++)
                        if (!used_dismember_groups[x])
                        {
                            for (uint y = 0; y < current_dismember_partitions_faces.Count; y++)
                                if (current_dismember_partitions_faces[y] > x)
                                    current_dismember_partitions_faces[y]--;
                        }
                        else
                            cleaned_up_dismember_partitions.Add(current_dismember_partitions[x]);
                    current_dismember_partitions = cleaned_up_dismember_partitions;
                }

                //Attatch properties if any
                //Check if the properties are skyrim specific in which case attach them in the 2 special slots called bs_properties
                if (propGroups.Count > 0)
                {
                    BSLightingShaderPropertyRef shader_property = null;

                    foreach (var prop in propGroups[shape_num])
                    {
                        NiPropertyRef current_property = prop;
                        if (current_property.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                        {
                            shader_property = (BSLightingShaderProperty)current_property;
                            break;
                        }
                    }

                    if (shader_property == null)
                    {
                        foreach (var prop in propGroups)
                            Shapes[shape_num]->AddProperty(prop);
                    }
                    else
                    {
                        NiAlphaPropertyRef alpha_property = null;
                        foreach (var prop in propGroups[shape_num])
                        {
                            if (prop.GetType().IsSameType(NiAlphaProperty.TYPE))
                                alpha_property = (NiAlphaProperty)prop;
                        }
                        NiPropertyRef[] bs_properties = new NiPropertyRef[2];
                        bs_properties[0] = shader_property;
                        bs_properties[1] = alpha_property;
                        Shapes[shape_num].SetBSProperties(bs_properties);
                    }
                }

                //--Set Shape Data--//

                //lists to hold data
                List<Vector3> shapeVerts(compVerts.Count);
                List<Vector3> shapeNorms(compVerts.Count );
                List<Color4> shapeColors(compVerts.Count );
                List<List<TexCoord>> shapeTCs;
                List<int> shapeTexCoordSets;
                Dictionary<NiNodeRef, List<SkinWeight>> shapeWeights;

                //Search for a NiTexturingProperty to build list of
                //texture coordinates sets to create
                NiPropertyRef niProp = Shapes[shape_num].GetPropertyByType(NiTexturingProperty.TYPE);
                NiTexturingPropertyRef niTexProp;
                if (niProp != null)
                    niTexProp = (NiTexturingProperty)niProp;
                if (niTexProp != null)
                {
                    for (int tex_num = 0; tex_num < 8; ++tex_num)
                        if (niTexProp.HasTexture(tex_num))
                        {
                            shapeTexCoordSets.Add(tex_num);
                            TexDesc td = niTexProp.GetTexture(tex_num);
                            td.uvSet = (int)shapeTexCoordSets.Count - 1;
                            niTexProp->SetTexture(tex_num, td);
                        }
                }
                else
                {
                    //Always include the base map if it's there, whether there's a
                    //texture or not
                    shapeTexCoordSets.Add(BASE_MAP);
                }
                shapeTCs.resize(shapeTexCoordSets.Count);
                foreach (var set in shapeTCs)
                    set.resize(compVerts.Count);

                //Loop through all compound vertices, adding the data
                //to the correct arrays.
                uint vert_index = 0;
                foreach (var cv in compVerts)
                {
                    shapeVerts[vert_index] = cv.position;
                    shapeColors[vert_index] = cv.color;
                    shapeNorms[vert_index] = cv.normal;
                    shapeNorms[vert_index] = cv.normal;
                    uint tex_index = 0;
                    foreach (var tex in shapeTexCoordSets)
                    {
                        if (cv.texCoords.find(TexType(tex)) != cv.texCoords.end())
                            shapeTCs[tex_index][vert_index] = cv->texCoords[TexType(tex)];
                        tex_index++;
                    }
                    SkinWeight sk;
                    foreach (var wt in weights)
                    {
                        //Only record influences that make a noticable contribution
                        if (wt.second > min_vertex_weight)
                        {
                            sk.index = vert_index;
                            sk.weight = wt->second;
                            if (shapeWeights.find(wt.first) == shapeWeights.end())
                                shapeWeights[wt->first] = new List<SkinWeight>();
                            shapeWeights[wt->first].Add(sk);
                        }
                    }
                    ++vert_index;
                }

                //Finally, set the data into the NiTriShapeData
                if (Vertices.Count > 0)
                {
                    niData.SetVertices(shapeVerts);
                    niData.SetTriangles(shapeTriangles);
                }
                if (Normals.Count > 0)
                    niData.SetNormals(shapeNorms);
                if (Colors.Count > 0)
                    niData.SetVertexColors(shapeColors);
                if (TexCoordSets.Count > 0)
                {
                    niData.SetUVSetCount((int)shapeTCs.Count);
                    for (uint tex_index = 0; tex_index < shapeTCs.Count; ++tex_index)
                        niData.SetUVSet(tex_index, shapeTCs[tex_index]);
                }

                //If there are any skin influences, bind the skin
                if (shapeWeights.Count > 0)
                {
                    List<NiNodeRef> shapeInfluences;
                    foreach (var inf in shapeWeights)
                        shapeInfluences.Add(inf.first);

                    if (!use_dismember_partitions)
                        shapes[shape_num].BindSkin(shapeInfluences);
                    else
                    {
                        shapes[shape_num].BindSkinWith(shapeInfluences, BSDismemberSkinInstance.Create);
                        BSDismemberSkinInstanceRef dismember_skin = (BSDismemberSkinInstance)shapes[shape_num].GetSkinInstance();
                        dismember_skin.SetPartitions(current_dismember_partitions);
                    }

                    for (uint inf = 0; inf < shapeInfluences.Count; ++inf)
                        shapes[shape_num].SetBoneWeights(inf, shapeWeights[shapeInfluences[inf]]);

                    shapes[shape_num].NormalizeSkinWeights();

                    if (use_dismember_partitions)
                    {
                        int face_map = new int[current_dismember_partitions_faces.Count];
                        for (uint x = 0; x < current_dismember_partitions_faces.Count; x++)
                            face_map[x] = current_dismember_partitions_faces[x];
                        shapes[shape_num].GenHardwareSkinInfo(max_bones_per_partition, 4, stripify, face_map);
                        //delete[] face_map;

                        BSDismemberSkinInstanceRef dismember_skin = (BSDismemberSkinInstance)shapes[shape_num].GetSkinInstance();
                        dismember_skin.SetPartitions(current_dismember_partitions);
                    }
                    else if (max_bones_per_partition > 0)
                    {
                        shapes[shape_num].GenHardwareSkinInfo(max_bones_per_partition, 4, stripify);
                    }

                    //NiSkinInstanceRef skinInst = shapes[shape_num].GetSkinInstance();

                    //if ( skinInst != null) {
                    //	NiSkinDataRef skinData = skinInst.GetSkinData();

                    //	if ( skinData != NULL ) {
                    //		for ( uint inf = 0; inf < shapeInfluences.Count; ++inf ) {
                    //			skinData.SetBoneWeights( inf, shapeWeights[ shapeInfluences[inf] ] );
                    //		}
                    //	}
                    //}
                }

                //If tangent space was requested, generate it
                if (tangent_space)
                {
                    if (tspace_flags == 0)
                        Shapes[shape_num].UpdateTangentSpace();
                    else if (Shapes[shape_num].GetData() != null)
                    {
                        Shapes[shape_num].GetData().SetUVSetCount(1);
                        Shapes[shape_num].GetData().SetTspaceFlag(tspace_flags);
                        Shapes[shape_num].UpdateTangentSpace(1);
                    }
                }

                //Next Shape
            }

            return root;
        }

        /* 
         * Merges together multiple NiTriBasedGeom objects and stores their data
         * in this ComplexShape object.
         * \param root The root NiAVObject to which all of the NiTriBasedGeom
         * objects to be merged are attached.  It could be a single NiTribasedGeom
         * or a NiNode that is a split mesh proxy.
         * \sa NiNode::IsSplitMeshProxy
         */
        public void Merge(NiAVObject root)
        {
            if (root == null)
                throw new ArgumentNullException("Called ComplexShape::Merge with a null root reference.");

            List<NiTriBasedGeomRef> shapes = new List<NiTriBasedGeomRef>();
            //Determine root type
            if (root.IsDerivedType(NiTriBasedGeom.TYPE))
                //The function was called on a single shape.
                //Add it to the list
                shapes.Add((NiTriBasedGeom)root);
            else if (root.IsDerivedType(NiNode.TYPE))
            {
                //The function was called on a NiNode.  Search for
                //shape children
                NiNodeRef nodeRoot = (NiNode)root;
                List<NiAVObjectRef> children = nodeRoot.GetChildren();
                for (uint child = 0; child < children.Count; ++child)
                    if (children[child]->IsDerivedType(NiTriBasedGeom.TYPE))
                        shapes.Add((NiTriBasedGeom)children[child]);
                if (shapes.Count == 0)
                    throw new InvalidOperationException("The NiNode passed to ComplexShape::Merge has no shape children.");
            }
            else
                throw new InvalidOperationException(" The ComplexShape::Merge function requies either a NiNode or a NiTriBasedGeom AVObject.");

            //The vector of VertNorm struts allows us to to refuse
            //to merge vertices that have different normals.
            List<VertNorm> vns = new List<VertNorm>();

            //Clear all existing data
            Clear();

            //Merge in data from each shape
            bool has_any_verts = false;
            bool has_any_norms = false;
            propGroups.resize(shapes.Count);
            uint prop_group_index = 0;
            foreach (var geom in shapes)
            {
                List<NiPropertyRef> current_property_group = geom.GetProperties();

                //Special code to handle the Bethesda Skyrim properties
                var bs_properties = geom.GetBSProperties();
                if (bs_properties[0] != null)
                    current_property_group.Add(bs_properties[0]);
                if (bs_properties[1] != null)
                    current_property_group.Add(bs_properties[1]);
                //Get properties of this shape
                propGroups[prop_group_index] = current_property_group;


                NiTriBasedGeomDataRef geomData = (NiTriBasedGeomData)geom.GetData();
                if (geomData == null)
                    throw new InvalidOperationException("One of the NiTriBasedGeom found by ComplexShape::Merge with a NiTriBasedGeom has no NiTriBasedGeomData attached.");

                //Get Data
                List<Vector3> shapeVerts;
                List<Vector3> shapeNorms;
                //If this is a skin influenced mesh, get vertices from niGeom
                if (geom.GetSkinInstance() != null)
                {
                    geom.GetSkinDeformation(shapeVerts, shapeNorms);
                    if (geom.GetSkinInstance().GetType().IsSameType(BSDismemberSkinInstance.TYPE))
                    {
                        BSDismemberSkinInstanceRef dismember_skin = (BSDismemberSkinInstance)geom->GetSkinInstance();
                        NiSkinPartitionRef skin_partition = dismember_skin.GetSkinPartition();
                    }
                }
                else
                {
                    shapeVerts = geomData->GetVertices();
                    shapeNorms = geomData->GetNormals();
                }

                List<Color4> shapeColors = geomData.GetColors();
                List<List<TexCoord>> shapeUVs(geomData.GetUVSetCount() );
                for (uint i = 0; i < shapeUVs.Count; ++i)
                    shapeUVs[i] = geomData.GetUVSet(i);
                List<Triangle> shapeTris = geomData.GetTriangles();

                //Lookup table
                List<MergeLookUp> lookUp = new List<MergeLookUp>(geomData.GetVertexCount());

                //Vertices and normals
                if (shapeVerts.Count != 0)
                    has_any_verts = true;

                bool shape_has_norms = (shapeNorms.Count == shapeVerts.Count);
                if (shape_has_norms)
                    has_any_norms = true;
                for (uint v = 0; v < shapeVerts.Count; ++v)
                {
                    VertNorm newVert;
                    newVert.position = shapeVerts[v];
                    if (shape_has_norms)
                        newVert.normal = shapeNorms[v];

                    //Search for matching vert/norm
                    bool match_found = false;
                    for (uint vn_index = 0; vn_index < vns.Count; ++vn_index)
                        if (vns[vn_index] == newVert)
                        {
                            //Match found, use existing index
                            lookUp[v].vertIndex = vn_index;
                            if (shapeNorms.Count != 0)
                                lookUp[v].normIndex = vn_index;
                            match_found = true;
                            //Stop searching
                            break;
                        }
                    if (!match_found)
                    {
                        //No match found, add this vert/norm to the list
                        vns.Add(newVert);
                        //Record new index
                        lookUp[v].vertIndex = (uint)vns.Count - 1;
                        if (shapeNorms.Count != 0)
                            lookUp[v].normIndex = (uint)vns.Count - 1;
                    }
                }

                //Colors
                for (uint c = 0; c < shapeColors.Count; ++c)
                {
                    Color4 newColor;

                    newColor = shapeColors[c];

                    //Search for matching color
                    bool match_found = false;
                    for (uint c_index = 0; c_index < colors.Count; ++c_index)
                    {
                        if (colors[c_index].r == newColor.r && colors[c_index].g == newColor.g && colors[c_index].b == newColor.b && colors[c_index].a == newColor.a)
                        {
                            //Match found, use existing index
                            lookUp[c].colorIndex = c_index;
                            match_found = true;
                            //Stop searching
                            break;
                        }
                    }

                    if (!match_found)
                    {
                        //No match found, add this color to the list
                        colors.Add(newColor);
                        //Record new index
                        lookUp[c].colorIndex = (uint)colors.Count - 1;
                    }
                }

                //Texture Coordinates

                //Create UV set list
                List<TexType> uvSetList = new List<TexType>(shapeUVs.Count);
                //Initialize to base
                for (uint tex = 0; tex < uvSetList.Count; ++tex)
                    uvSetList[tex] = BASE_MAP;
                NiPropertyRef niProp = geom.GetPropertyByType(NiTexturingProperty.TYPE);
                NiTexturingPropertyRef niTexingProp;
                if (niProp != null)
                    niTexingProp = (NiTexturingProperty)niProp;
                niProp = geom.GetPropertyByType(NiTextureProperty.TYPE);
                NiTexturePropertyRef niTexProp;
                if (niProp != null)
                    niTexProp = (NiTextureProperty)niProp;
                BSShaderTextureSetRef bsTexProp = null;
                niProp = geom.GetPropertyByType(BSShaderTextureSet.TYPE);
                if (niProp != null)
                    bsTexProp = (BSShaderTextureSet)niProp;
                niProp = geom.GetBSProperties()[0];
                if (niProp != null && niProp.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                {
                    BSLightingShaderPropertyRef bs_shader = (BSLightingShaderProperty)niProp;
                    if (bs_shader->GetTextureSet() != null)
                        bsTexProp = bs_shader.GetTextureSet();
                }
                niProp = geom.GetBSProperties()[1];
                if (niProp != null && niProp.GetType().IsSameType(BSLightingShaderProperty.TYPE))
                {
                    BSLightingShaderPropertyRef bs_shader = (BSLightingShaderProperty)niProp;
                    if (bs_shader.GetTextureSet() != null)
                        bsTexProp = bs_shader.GetTextureSet();
                }

                //Create a list of UV sets to check
                //pair.first = Texture Type
                //pair.second = UV Set ID
                List<Tuple<TexType, uint>> uvSets = new List<Tuple<TexType, uint>>();
                if (shapeUVs.Count != 0 && (niTexingProp != null || niTexProp != null || bsTexProp != null))
                {
                    if (niTexingProp != null)
                    {
                        //Add the UV set to the list for every type of texture slot that uses it
                        for (int tex = 0; tex < 8; ++tex)
                            if (niTexingProp.HasTexture(tex))
                            {
                                TexDesc td;
                                td = niTexingProp->GetTexture(tex);
                                uvSets.Add(new Tuple<TexType, uint>(TexType(tex), td.uvSet));
                            }
                    }
                    else if (niTexProp != null || bsTexProp != null)
                        //Add the base UV set to the list and just use zero.
                        uvSets.Add(new Tuple<TexType, uint>(BASE_MAP, 0));

                    //Add the UV set to the list for every type of texture slot that uses it
                    for (int i = 0; i < uvSets.Count; ++i)
                    {

                        TexType newType = uvSets[i].Item1;
                        uint set = uvSets[i].Item2;

                        //Search for matching UV set
                        bool match_found = false;
                        uint uvSetIndex;
                        for (uint set_index = 0; set_index < texCoordSets.Count; ++set_index)
                            if (texCoordSets[set_index].texType == newType)
                            {
                                //Match found, use existing index
                                uvSetIndex = set_index;
                                match_found = true;
                                //Stop searching
                                break;
                            }

                        if (!match_found)
                        {
                            //No match found, add this UV set to the list
                            TexCoordSet newTCS;
                            newTCS.texType = newType;
                            texCoordSets.Add(newTCS);
                            //Record new index
                            uvSetIndex = (uint)texCoordSets.Count - 1;
                        }

                        //Loop through texture coordinates in this set
                        if (set >= shapeUVs.Count || set < 0)
                            throw new InvalidOperationException("One of the UV sets specified in the NiTexturingProperty did not exist in the NiTriBasedGeomData.");
                        for (uint v = 0; v < shapeUVs[set].Count; ++v)
                        {
                            TexCoord newCoord;
                            newCoord = shapeUVs[set][v];

                            //Search for matching texture coordinate
                            bool match_found = false;
                            for (uint tc_index = 0; tc_index < texCoordSets[uvSetIndex].texCoords.Count; ++tc_index)
                            {
                                if (texCoordSets[uvSetIndex].texCoords[tc_index] == newCoord)
                                {
                                    //Match found, use existing index
                                    lookUp[v].uvIndices[uvSetIndex] = tc_index;
                                    match_found = true;
                                    //Stop searching
                                    break;
                                }
                            }

                            //Done with loop, check if match was found
                            if (!match_found)
                            {
                                //No match found, add this texture coordinate to the list
                                texCoordSets[uvSetIndex].texCoords.Add(newCoord);
                                //Record new index
                                lookUp[v].uvIndices[uvSetIndex] = (uint)texCoordSets[uvSetIndex].texCoords.Count - 1;
                            }
                        }
                    }
                }

                //Use look up table to build list of faces
                for (uint t = 0; t < shapeTris.Count; ++t)
                {
                    ComplexFace newFace;
                    newFace.propGroupIndex = prop_group_index;
                    newFace.points.resize(3);
                    Triangle tri = shapeTris[t];
                    for (uint p = 0; p < 3; ++p)
                    {
                        if (shapeVerts.Count != 0)
                            newFace.points[p].vertexIndex = lookUp[tri[p]].vertIndex;
                        if (shapeNorms.Count != 0)
                            newFace.points[p].normalIndex = lookUp[tri[p]].normIndex;
                        if (shapeColors.Count != 0)
                            newFace.points[p].colorIndex = lookUp[tri[p]].colorIndex;
                        foreach (var set in lookUp[tri[p]].uvIndices)
                        {
                            TexCoordIndex tci;
                            tci.texCoordSetIndex = set.Key;
                            tci.texCoordIndex = set.Value;
                            newFace.points[p].texCoordIndices.Add(tci);
                        }
                    }
                    faces.Add(newFace);
                }

                //Use look up table to set vertex weights, if any
                NiSkinInstanceRef skinInst = geom.GetSkinInstance();
                if (skinInst != null)
                {
                    NiSkinDataRef skinData = skinInst.GetSkinData();
                    if (skinData != null)
                    {
                        //Get influence list
                        List<NiNodeRef> shapeBones = skinInst.GetBones();
                        //Get weights
                        List<SkinWeight> shapeWeights;
                        for (uint b = 0; b < shapeBones.Count; ++b)
                        {
                            shapeWeights = skinData.GetBoneWeights(b);
                            for (uint w = 0; w < shapeWeights.Count; ++w)
                            {
                                uint vn_index = lookUp[shapeWeights[w].index].vertIndex;
                                NiNodeRef boneRef = shapeBones[b];
                                float weight = shapeWeights[w].weight;
                                vns[vn_index].weights[boneRef] = weight;
                            }
                        }
                    }

                    //Check to see if the skin is actually a dismember skin instance in which case import the partitions too
                    if (skinInst.GetType().IsSameType(BSDismemberSkinInstance.TYPE))
                    {
                        BSDismemberSkinInstanceRef dismember_skin = (BSDismemberSkinInstance)geom.GetSkinInstance();
                        NiSkinPartitionRef skin_partition = dismember_skin.GetSkinPartition();

                        //These are the partition data of the current shapes
                        List<BodyPartList> current_body_parts;
                        List<int> current_body_parts_faces;

                        for (uint y = 0; y < dismember_skin.GetPartitions().Count; y++)
                            current_body_parts.Add(dismember_skin.GetPartitions().at(y));
                        for (uint y = 0; y < shapeTris.Count; y++)
                            current_body_parts_faces.Add(0);

                        for (int y = 0; y < skin_partition.GetNumPartitions(); y++)
                        {
                            List<Triangle> partition_triangles = skin_partition.GetTriangles(y);
                            List<ushort> partition_vertex_map = skin_partition.GetVertexMap(y);
                            bool has_vertex_map = false;
                            if (partition_vertex_map.Count > 0)
                                has_vertex_map = true;
                            for (uint z = 0; z < partition_triangles.Count; z++)
                            {
                                uint w = faces.Count - shapeTris.Count;
                                int merged_x;
                                int merged_y;
                                int merged_z;
                                if (has_vertex_map)
                                {
                                    merged_x = lookUp[partition_vertex_map[partition_triangles[z].v1]].vertIndex;
                                    merged_y = lookUp[partition_vertex_map[partition_triangles[z].v2]].vertIndex;
                                    merged_z = lookUp[partition_vertex_map[partition_triangles[z].v3]].vertIndex;
                                }
                                else
                                {
                                    merged_x = lookUp[partition_triangles[z].v1].vertIndex;
                                    merged_y = lookUp[partition_triangles[z].v2].vertIndex;
                                    merged_z = lookUp[partition_triangles[z].v3].vertIndex;
                                }
                                for (; w < faces.size(); w++)
                                {
                                    ComplexFace current_face = faces[w];

                                    //keep this commented code is case my theory that all triangles must have vertices arranged in a certain way and that you can't rearrange vertices in a triangle
                                    /*if(current_face.points[0].vertexIndex == merged_x) {
                                        if(current_face.points[1].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[2].vertexIndex == merged_y && current_face.points[1].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } else if(current_face.points[1].vertexIndex == merged_x) {
                                        if(current_face.points[0].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[2].vertexIndex == merged_y && current_face.points[0].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } else if(current_face.points[2].vertexIndex == merged_x) {
                                        if(current_face.points[0].vertexIndex == merged_y && current_face.points[1].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        } else if(current_face.points[1].vertexIndex == merged_y && current_face.points[0].vertexIndex == merged_z) {
                                            is_same_face = true;
                                            break;
                                        }
                                    } */

                                    if (current_face.points[0].vertexIndex == merged_x && current_face.points[1].vertexIndex == merged_y && current_face.points[2].vertexIndex == merged_z)
                                        break;
                                }

                                if (w - (faces.Count - shapeTris.Count) < shapeTris.Count)
                                    current_body_parts_faces[w - (faces.Count - shapeTris.Count)] = y;
                            }
                        }

                        for (uint y = 0; y < current_body_parts.Count; y++)
                        {
                            int match_index = -1;
                            for (uint z = 0; z < dismemberPartitionsBodyParts.Count; z++)
                                if (dismemberPartitionsBodyParts[z].bodyPart == current_body_parts[y].bodyPart
                                    && dismemberPartitionsBodyParts[z].partFlag == current_body_parts[y].partFlag)
                                {
                                    match_index = z;
                                    break;
                                }
                            if (match_index < 0)
                            {
                                dismemberPartitionsBodyParts.Add(current_body_parts[y]);
                                match_index = dismemberPartitionsBodyParts.Count - 1;
                            }
                            for (uint z = 0; z < current_body_parts_faces.Count; z++)
                                if (current_body_parts_faces[z] == y)
                                    current_body_parts_faces[z] = match_index;
                        }
                        for (uint x = 0; x < current_body_parts_faces.Count; x++)
                            dismemberPartitionsFaces.Add(current_body_parts_faces[x]);
                    }
                }
                //Next Shape
                ++prop_group_index;
            }

            //Finished with all shapes.  Build up a list of influences
            Dictionary<NiNodeRef, uint> boneLookUp;
            for (uint v = 0; v < vns.Count; ++v)
                foreach (var w in vns[v].weights)
                    boneLookUp[w.Key] = 0; //will change later

            skinInfluences.resize(boneLookUp.Count);
            uint si_index = 0;
            foreach (var si in boneLookUp)
            {
                si.Value = si_index;
                skinInfluences[si_index] = si.Key;
                ++si_index;
            }

            //Copy vns data to vertices and normals
            if (has_any_verts)
                vertices.resize(vns.Count);
            if (has_any_norms)
                normals.resize(vns.Count);
            for (uint v = 0; v < vns.Count; ++v)
            {
                if (has_any_verts)
                {
                    vertices[v].position = vns[v].position;
                    vertices[v].weights.resize(vns[v].weights.size());
                    uint weight_index = 0;
                    foreach (var w in vns[v].weights)
                    {
                        vertices[v].weights[weight_index].influenceIndex = boneLookUp[w.Key];
                        vertices[v].weights[weight_index].weight = w.Value;
                        ++weight_index;
                    }
                }
                if (has_any_norms)
                    normals[v] = vns[v].normal;
            }
            //Done Merging
        }

        /* 
         * Clears out all the data stored in this ComplexShape
         */
        public void Clear()
        {
            vertices.clear();
            colors.clear();
            normals.clear();
            texCoordSets.clear();
            faces.clear();
            propGroups.clear();
            skinInfluences.clear();
            name.clear();
            dismemberPartitionsBodyParts.clear();
            dismemberPartitionsFaces.clear();
        }

        /*
         * Gets or Sets the name of ComplexShape which will be used when splitting it into NiTriBasedGeom objects.
         */
        public string Name { get; set; }

        /*
         * Gets or Sets the vertex data that will be used by the ComplexShape, which includes position and skin weighting information.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<WeightedVertex> Vertices { get; set; }

        /*
         * Gets or Sets the color data that will be used by the ComplexShape.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<Color4> Colors { get; set; }

        /*
         * Gets or Sets the normal data that will be used by the ComplexShape.  The data is referenced by vector index, so repetition of values is not necessary.
         */
        public List<Vector3> Normals { get; set; }

        /*
         * Gets or Sets the texture coordinate data used by the ComplexShape.  This includes a list of UV sets, each with a texture type and texture coordiantes.  The coordinate data is referenced by index, so repetition of values within a particular set is not necessary.
         */
        public List<TexCoordSet> TexCoordSets { get; set; }

        /*
         * Gets or Sets the faces used by the ComplexShape.  Each face references 3 or more vertex positions (triangles are not required), each of which can reference color, normal, and texture, information.  Each face also references a property group which defines the material and any other per-face attributes.
         * \param[in] n The new face data.  Will replace any existing data.
         */
        public List<ComplexFace> Faces { get; set; }

        /*
         * Gets or Sets the property groups used by the Complex Shape.  Each group of NiProperty values can be assigned by vector index to a face in the ComplexShape by index, allowing for material and other properties to be specified on a per-face basis.  If the ComplexShape is split, each property group that is used by faces in the mesh will result in a separate NiTriBasedGeom with the specified propreties attached.
         */
        public List<List<Ref<NiProperty>>> PropGroups { get; set; }

        /*
         * Gets or Sets the skin influences used by the Complex Shape.  These are the NiNode objects which cause deformations in skin meshes.  They are referenced in the vertex data by vector index.
         * \param[in] n The new skin influences.  Will replace any existing data.
         */
        public List<Ref<NiNode>> SkinInfluences { get; set; }

        /*
         * Gets or Sets the association between the faces in the complex shape and the corresponding body parts
         */
        public List<uint> DismemberPartitionsFaces { get; set; }

        /*
         * Gets or Sets a list of the dismember groups
         */
        public List<BodyPartList> DismemberPartitionsBodyParts { get; set; }
    }




    struct VertNorm
    {
        Vector3 position;
        Vector3 normal;
        Dictionary<NiNodeRef, float> weights;

        //VertNorm() {}
        //VertNorm(VertNorm n ) {
        //	this = n;
        //}
        //public static VertNorm operator=( VertNorm  n ) {
        //	position = n.position;
        //	normal = n.normal;
        //	weights = n.weights;
        //	return *this;
        //}
        //bool operator==( const VertNorm & n ) {
        //	if ( abs(position.x - n.position.x) > 0.001 || abs(position.y - n.position.y) > 0.001 || abs(position.z - n.position.z) > 0.001 ) {
        //		return false;
        //	}
        //	if ( abs(normal.x - n.normal.x) > 0.001 || abs(normal.y - n.normal.y) > 0.001 || abs(normal.z - n.normal.z) > 0.001 ) {
        //		return false;
        //	}
        //	//if ( weights != n.weights ) {
        //	//	return false;
        //	//}

        //	return true;
        //}
    }

    struct CompoundVertex
    {
        Vector3 position;
        Vector3 normal;
        Color4 color;
        Dictionary<TexType, TexCoord> texCoords;
        Dictionary<NiNodeRef, float> weights;

        //CompoundVertex() {}
        //~CompoundVertex() {}
        //CompoundVertex( const CompoundVertex & n ) {
        //	*this = n;
        //}
        //CompoundVertex & operator=( const CompoundVertex & n ) {
        //	position = n.position;
        //	normal = n.normal;
        //	color = n.color;
        //	texCoords = n.texCoords;
        //	weights = n.weights;
        //	return *this;
        //}
        //bool operator==( const CompoundVertex & n ) {
        //	if ( position != n.position ) {
        //		return false;
        //	}
        //	if ( normal != n.normal ) {
        //		return false;
        //	}
        //	if ( color != n.color ) {
        //		return false;
        //	}
        //	if ( texCoords != n.texCoords ) {
        //		return false;
        //	}
        //	if ( weights != n.weights ) {
        //		return false;
        //	}

        //	return true;
        //}
    }

    struct MergeLookUp
    {
        uint vertIndex;
        uint normIndex;
        uint colorIndex;
        Dictionary<uint, uint> uvIndices; //TexCoordSet Index, TexCoord Index
    }


}
