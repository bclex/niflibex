/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib
{

    /*! Mesh data: vertices, vertex normals, etc. */
    public class NiGeometryData : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiGeometryData", NiObject.TYPE);
        /*! Always zero. */
        internal int groupId;
        /*! Number of vertices. */
        internal ushort numVertices;
        /*! Bethesda uses this for max number of particles in NiPSysData. */
        internal ushort bsMaxVertices;
        /*! Used with NiCollision objects when OBB or TRI is set. */
        internal byte keepFlags;
        /*! Unknown. */
        internal byte compressFlags;
        /*! Is the vertex array present? (Always non-zero.) */
        internal bool hasVertices;
        /*! The mesh vertices. */
        internal IList<Vector3> vertices;
        /*!  */
        internal VectorFlags vectorFlags;
        /*!  */
        internal BSVectorFlags bsVectorFlags;
        /*!  */
        internal uint materialCrc;
        /*!
         * Do we have lighting normals? These are essential for proper lighting: if not
         * present, the model will only be influenced by ambient light.
         */
        internal bool hasNormals;
        /*! The lighting normals. */
        internal IList<Vector3> normals;
        /*! Tangent vectors. */
        internal IList<Vector3> tangents;
        /*! Bitangent vectors. */
        internal IList<Vector3> bitangents;
        /*!
         * Center of the bounding box (smallest box that contains all vertices) of the
         * mesh.
         */
        internal Vector3 center;
        /*!
         * Radius of the mesh: maximal Euclidean distance between the center and all
         * vertices.
         */
        internal float radius;
        /*! Unknown, always 0? */
        internal Array13<short> unknown13Shorts;
        /*!
         * Do we have vertex colors? These are usually used to fine-tune the lighting of
         * the model.
         * 
         *             Note: how vertex colors influence the model can be controlled by
         * having a NiVertexColorProperty object as a property child of the root node. If
         * this property object is not present, the vertex colors fine-tune lighting.
         * 
         *             Note 2: set to either 0 or 0xFFFFFFFF for NifTexture compatibility.
         */
        internal bool hasVertexColors;
        /*! The vertex colors. */
        internal IList<Color4> vertexColors;
        /*!
         * The lower 6 (or less?) bits of this field represent the number of UV texture
         * sets. The other bits are probably flag bits. For versions 10.1.0.0 and up, if
         * bit 12 is set then extra vectors are present after the normals.
         */
        internal ushort numUvSets;
        /*!
         * Do we have UV coordinates?
         * 
         *             Note: for compatibility with NifTexture, set this value to either
         * 0x00000000 or 0xFFFFFFFF.
         */
        internal bool hasUv;
        /*!
         * The UV texture coordinates. They follow the OpenGL standard: some programs may
         * require you to flip the second coordinate.
         */
        internal IList<TexCoord[]> uvSets;
        /*! Consistency Flags */
        internal ConsistencyType consistencyFlags;
        /*! Unknown. */
        internal AbstractAdditionalGeometryData additionalData;

        public NiGeometryData()
        {
            groupId = (int)0;
            numVertices = (ushort)0;
            bsMaxVertices = (ushort)0;
            keepFlags = (byte)0;
            compressFlags = (byte)0;
            hasVertices = 1;
            vectorFlags = (VectorFlags)0;
            bsVectorFlags = (BSVectorFlags)0;
            materialCrc = (uint)0;
            hasNormals = false;
            radius = 0.0f;
            hasVertexColors = false;
            numUvSets = (ushort)0;
            hasUv = false;
            consistencyFlags = ConsistencyType.CT_MUTABLE;
            additionalData = null;
        }

        /*!
         * Used to determine the type of a particular instance of this object.
         * \return The type constant for the actual type of the object.
         */
        public override Type_ GetType() => TYPE;

        /*!
         * A factory function used during file reading to create an instance of this type of object.
         * \return A pointer to a newly allocated instance of this type of object.
         */
        public static NiObject Create() => new NiGeometryData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            if (info.version >= 0x0A010072)
            {
                Nif.NifStream(out groupId, s, info);
            }
            if ((!IsDerivedType(NiPSysData.TYPE)))
            {
                Nif.NifStream(out numVertices, s, info);
            }
            if ((info.userVersion2 < 34))
            {
                if (IsDerivedType(NiPSysData.TYPE))
                {
                    Nif.NifStream(out (ushort)numVertices, s, info);
                }
            }
            if ((info.userVersion2 >= 34))
            {
                if (IsDerivedType(NiPSysData.TYPE))
                {
                    Nif.NifStream(out bsMaxVertices, s, info);
                }
            }
            if (info.version >= 0x0A010000)
            {
                Nif.NifStream(out keepFlags, s, info);
                Nif.NifStream(out compressFlags, s, info);
            }
            Nif.NifStream(out hasVertices, s, info);
            if (hasVertices)
            {
                vertices = new Vector3[numVertices];
                for (var i2 = 0; i2 < vertices.Count; i2++)
                {
                    Nif.NifStream(out vertices[i2], s, info);
                }
            }
            if ((info.version >= 0x0A000100) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
            {
                Nif.NifStream(out vectorFlags, s, info);
            }
            if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
            {
                Nif.NifStream(out bsVectorFlags, s, info);
            }
            if ((info.version >= 0x14020007) && (info.version <= 0x14020007) && (info.userVersion == 12))
            {
                Nif.NifStream(out materialCrc, s, info);
            }
            Nif.NifStream(out hasNormals, s, info);
            if (hasNormals)
            {
                normals = new Vector3[numVertices];
                for (var i2 = 0; i2 < normals.Count; i2++)
                {
                    Nif.NifStream(out normals[i2], s, info);
                }
            }
            if (info.version >= 0x0A010000)
            {
                if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
                {
                    tangents = new Vector3[numVertices];
                    for (var i3 = 0; i3 < tangents.Count; i3++)
                    {
                        Nif.NifStream(out tangents[i3], s, info);
                    }
                    bitangents = new Vector3[numVertices];
                    for (var i3 = 0; i3 < bitangents.Count; i3++)
                    {
                        Nif.NifStream(out bitangents[i3], s, info);
                    }
                }
            }
            Nif.NifStream(out center, s, info);
            Nif.NifStream(out radius, s, info);
            if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072))
            {
                for (var i2 = 0; i2 < 13; i2++)
                {
                    Nif.NifStream(out unknown13Shorts[i2], s, info);
                }
            }
            Nif.NifStream(out hasVertexColors, s, info);
            if (hasVertexColors)
            {
                vertexColors = new Color4[numVertices];
                for (var i2 = 0; i2 < vertexColors.Count; i2++)
                {
                    Nif.NifStream(out vertexColors[i2], s, info);
                }
            }
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(out numUvSets, s, info);
            }
            if (info.version <= 0x04000002)
            {
                Nif.NifStream(out hasUv, s, info);
            }
            uvSets = new TexCoord[((numUvSets & 63) | ((vectorFlags & 63) | (bsVectorFlags & 1)))];
            for (var i1 = 0; i1 < uvSets.Count; i1++)
            {
                uvSets[i1].Resize(numVertices);
                for (var i2 = 0; i2 < uvSets[i1].Count; i2++)
                {
                    Nif.NifStream(out uvSets[i1][i2], s, info);
                }
            }
            if (info.version >= 0x0A000100)
            {
                Nif.NifStream(out consistencyFlags, s, info);
            }
            if (info.version >= 0x14000004)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numVertices = (ushort)vertices.Count;
            if (info.version >= 0x0A010072)
            {
                Nif.NifStream(groupId, s, info);
            }
            if ((!IsDerivedType(NiPSysData.TYPE)))
            {
                Nif.NifStream(numVertices, s, info);
            }
            if ((info.userVersion2 < 34))
            {
                if (IsDerivedType(NiPSysData.TYPE))
                {
                    Nif.NifStream((ushort)numVertices, s, info);
                }
            }
            if ((info.userVersion2 >= 34))
            {
                if (IsDerivedType(NiPSysData.TYPE))
                {
                    Nif.NifStream(bsMaxVertices, s, info);
                }
            }
            if (info.version >= 0x0A010000)
            {
                Nif.NifStream(keepFlags, s, info);
                Nif.NifStream(compressFlags, s, info);
            }
            Nif.NifStream(hasVertices, s, info);
            if (hasVertices)
            {
                for (var i2 = 0; i2 < vertices.Count; i2++)
                {
                    Nif.NifStream(vertices[i2], s, info);
                }
            }
            if ((info.version >= 0x0A000100) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))))
            {
                Nif.NifStream(vectorFlags, s, info);
            }
            if (((info.version == 0x14020007) && (info.userVersion2 > 0)))
            {
                Nif.NifStream(bsVectorFlags, s, info);
            }
            if ((info.version >= 0x14020007) && (info.version <= 0x14020007) && (info.userVersion == 12))
            {
                Nif.NifStream(materialCrc, s, info);
            }
            Nif.NifStream(hasNormals, s, info);
            if (hasNormals)
            {
                for (var i2 = 0; i2 < normals.Count; i2++)
                {
                    Nif.NifStream(normals[i2], s, info);
                }
            }
            if (info.version >= 0x0A010000)
            {
                if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
                {
                    for (var i3 = 0; i3 < tangents.Count; i3++)
                    {
                        Nif.NifStream(tangents[i3], s, info);
                    }
                    for (var i3 = 0; i3 < bitangents.Count; i3++)
                    {
                        Nif.NifStream(bitangents[i3], s, info);
                    }
                }
            }
            Nif.NifStream(center, s, info);
            Nif.NifStream(radius, s, info);
            if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072))
            {
                for (var i2 = 0; i2 < 13; i2++)
                {
                    Nif.NifStream(unknown13Shorts[i2], s, info);
                }
            }
            Nif.NifStream(hasVertexColors, s, info);
            if (hasVertexColors)
            {
                for (var i2 = 0; i2 < vertexColors.Count; i2++)
                {
                    Nif.NifStream(vertexColors[i2], s, info);
                }
            }
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(numUvSets, s, info);
            }
            if (info.version <= 0x04000002)
            {
                Nif.NifStream(hasUv, s, info);
            }
            for (var i1 = 0; i1 < uvSets.Count; i1++)
            {
                for (var i2 = 0; i2 < uvSets[i1].Count; i2++)
                {
                    Nif.NifStream(uvSets[i1][i2], s, info);
                }
            }
            if (info.version >= 0x0A000100)
            {
                Nif.NifStream(consistencyFlags, s, info);
            }
            if (info.version >= 0x14000004)
            {
                WriteRef((NiObject)additionalData, s, info, link_map, missing_link_stack);
            }

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.AsString());
            numVertices = (ushort)vertices.Count;
            s.AppendLine($"  Group ID:  {groupId}");
            if ((!IsDerivedType(NiPSysData.TYPE)))
            {
                s.AppendLine($"    Num Vertices:  {numVertices}");
            }
            if (IsDerivedType(NiPSysData.TYPE))
            {
                s.AppendLine($"    BS Max Vertices:  {bsMaxVertices}");
            }
            s.AppendLine($"  Keep Flags:  {keepFlags}");
            s.AppendLine($"  Compress Flags:  {compressFlags}");
            s.AppendLine($"  Has Vertices:  {hasVertices}");
            if (hasVertices)
            {
                array_output_count = 0;
                for (var i2 = 0; i2 < vertices.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Vertices[{i2}]:  {vertices[i2]}");
                    array_output_count++;
                }
            }
            s.AppendLine($"  Vector Flags:  {vectorFlags}");
            s.AppendLine($"  BS Vector Flags:  {bsVectorFlags}");
            s.AppendLine($"  Material CRC:  {materialCrc}");
            s.AppendLine($"  Has Normals:  {hasNormals}");
            if (hasNormals)
            {
                array_output_count = 0;
                for (var i2 = 0; i2 < normals.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Normals[{i2}]:  {normals[i2]}");
                    array_output_count++;
                }
            }
            if ((hasNormals && ((vectorFlags | bsVectorFlags) & 4096)))
            {
                array_output_count = 0;
                for (var i2 = 0; i2 < tangents.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Tangents[{i2}]:  {tangents[i2]}");
                    array_output_count++;
                }
                array_output_count = 0;
                for (var i2 = 0; i2 < bitangents.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Bitangents[{i2}]:  {bitangents[i2]}");
                    array_output_count++;
                }
            }
            s.AppendLine($"  Center:  {center}");
            s.AppendLine($"  Radius:  {radius}");
            array_output_count = 0;
            for (var i1 = 0; i1 < 13; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Unknown 13 shorts[{i1}]:  {unknown13Shorts[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Has Vertex Colors:  {hasVertexColors}");
            if (hasVertexColors)
            {
                array_output_count = 0;
                for (var i2 = 0; i2 < vertexColors.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Vertex Colors[{i2}]:  {vertexColors[i2]}");
                    array_output_count++;
                }
            }
            s.AppendLine($"  Num UV Sets:  {numUvSets}");
            s.AppendLine($"  Has UV:  {hasUv}");
            array_output_count = 0;
            for (var i1 = 0; i1 < uvSets.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                for (var i2 = 0; i2 < uvSets[i1].Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      UV Sets[{i2}]:  {uvSets[i1][i2]}");
                    array_output_count++;
                }
            }
            s.AppendLine($"  Consistency Flags:  {consistencyFlags}");
            s.AppendLine($"  Additional Data:  {additionalData}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            if (info.version >= 0x14000004)
            {
                additionalData = FixLink<AbstractAdditionalGeometryData>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (additionalData != null)
                refs.Add((NiObject)additionalData);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*! The mesh vertex indices. */
        protected IList<int> vertexIndices;

        /*! The mapping between Nif & Max UV sets. */
        protected Dictionary<int, int> uvSetMap; // first = Max index, second = Nif index

        //--Counts--//

        /*! 
         * Returns the number of verticies that make up this mesh.  This is also the number of normals, colors, and UV coordinates if these are used.
         * \return The number of vertices that make up this mesh.
         * \sa IShapeData::SetVertexCount
         */
        public int GetVertexCount();

        /*! 
         * Returns the number of texture coordinate sets used by this mesh.  For each UV set, there is a pair of texture coordinates for every vertex in the mesh.  Each set corresponds to a texture entry in the NiTexturingPropery object.
         * \return The number of texture cooridnate sets used by this mesh.  Can be zero.
         * \sa IShapeData::SetUVSetCount, ITexturingProperty
         */
        NIFLIB_API short GetUVSetCount() const;

        /*! 
         * Changes the number of UV sets used by this mesh.  If the new size is smaller, data at the end of the array will be lost.  Otherwise it will be retained.  The number of UV sets must correspond with the number of textures defined in the corresponding NiTexturingProperty object.
         * \param n The new size of the uv set array.
         * \sa IShapeData::GetUVSetCount, ITexturingProperty
         */
        NIFLIB_API void SetUVSetCount(int n);

        /*! 
         * Returns the number of vertec indices that make up this mesh.
         * \return The number of vertex indices that make up this mesh.
         * \sa IShapeData::SetVertexIndexCount
         */
        NIFLIB_API int GetVertexIndexCount() const;

        //--Getters--//

        /*! 
         * Returns the 3D center of the mesh.
         * \return The center of this mesh.
         */
        NIFLIB_API Vector3 GetCenter() const;

        /*! 
         * Returns the radius of the mesh.  That is the distance from the center to
         * the farthest point from the center.
         * \return The radius of this mesh.
         */
        NIFLIB_API float GetRadius() const;

        /*! 
         * Assigns the center and radius of the spherical bound of this data.
         * \remark GeoMorpher controllers will alter the model bound.
         */
        NIFLIB_API void SetBound(Vector3 const & center, float radius);

        /*! 
         * Used to retrive the vertices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the vertices used by this mesh.
         * \sa IShapeData::SetVertices, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API vector<Vector3> GetVertices() const;

        /*! 
         * Used to retrive the normals used by this mesh.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the normals used by this mesh, if any.
         * \sa IShapeData::SetNormals, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API vector<Vector3> GetNormals() const;

        /*! 
         * Used to retrive the vertex colors used by this mesh.  The size of the vector will either be zero if no vertex colors are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector cntaining the vertex colors used by this mesh, if any.
         * \sa IShapeData::SetVertexColors, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API vector<Color4> GetColors() const;

        /*! 
         * Used to retrive the texture coordinates from one of the texture sets used by this mesh.  The function will throw an exception if a texture set index that does not exist is specified.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \param index The index of the texture coordinate set to retrieve the texture coordinates from.  This index is zero based and must be a positive number smaller than that returned by the IShapeData::GetUVSetCount function.  If there are no texture coordinate sets, this function will throw an exception.
         * \return A vector cntaining the the texture coordinates used by the requested texture coordinate set.
         * \sa IShapeData::SetUVSet, IShapeData::GetUVSetCount, IShapeData::SetUVSetCount, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API vector<TexCoord> GetUVSet(int index ) const;

        /*! 
         * Used to retrive the vertex indices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexIndexCount function.
         * \return A vector containing the vertex indices used by this mesh.
         * \sa IShapeData::SetVertexIndices, IShapeData::GetVertexIndexCount, IShapeData::SetVertexIndexCount.
         */
        NIFLIB_API vector<int> GetVertexIndices() const;

        /*! 
         * Used to retrive the the NIF index corresponding to the Max map channel. If there isn't one, -1 is returned.
         * \param maxMapChannel The max map channel of the desired UV set.
         * \return A int representing the NIF index of the UV se used.
         */
        NIFLIB_API int GetUVSetIndex(int maxMapChannel) const;

        //--Setters--//

        /*! 
         * Used to set the vertex data used by this mesh.  Calling this function will clear all other data in this object.
         * \param in A vector containing the vertices to replace those in the mesh with.  Note that there is no way to set vertices one at a time, they must be sent in one batch.
         * \sa IShapeData::GetVertices, IShapeData::GetVertexCount
         */
        NIFLIB_API virtual void SetVertices( const vector<Vector3> & in );

        /*!
         * Used to set the normal data used by this mesh.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
         * \param in A vector containing the normals to replace those in the mesh with.  Note that there is no way to set normals one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using normals.
         * \sa IShapeData::GetNormals, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API void SetNormals( const vector<Vector3> & in );

        /*!
         * Used to set the vertex color data used by this mesh.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
         * \param in A vector containing the vertex colors to replace those in the mesh with.  Note that there is no way to set vertex colors one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using vertex colors.
         * \sa IShapeData::GetColors, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API void SetVertexColors( const vector<Color4> & in );

        /*!
         * Used to set the texture coordinate data from one of the texture sets used by this mesh.  The function will throw an exception if a texture set index that does not exist is specified.  The size of the vector must be the same as the vertex count retrieved with the IShapeData::GetVertexCount function, or the function will throw an exception.
         * \param index The index of the texture coordinate set to retrieve the texture coordinates from.  This index is zero based and must be a positive number smaller than that returned by the IShapeData::GetUVSetCount function.  If there are no texture coordinate sets, this function will throw an exception.
         * \param in A vector containing the the new texture coordinates to replace those in the requested texture coordinate set.
         * \sa IShapeData::GetUVSet, IShapeData::GetUVSetCount, IShapeData::SetUVSetCount, IShapeData::GetVertexCount, IShapeData::SetVertexCount.
         */
        NIFLIB_API void SetUVSet(int index, const vector<TexCoord> & in );

        /*! 
         * Used to set the vertex index data used by this mesh.  Calling this function will clear all other data in this object.
         * \param in A vector containing the vertex indices to replace those in the mesh with.  Note that there is no way to set vertices one at a time, they must be sent in one batch.
         * \sa IShapeData::GetVertexIndices, IShapeData::GetVertexIndexCount
         */
        NIFLIB_API virtual void SetVertexIndices( const vector<int> & in );

        /*! 
         * Used to set the UV set mapping data used by this mesh.  This info maps the Max map channel to the index used in the NIF.
         * \param in A map of UV set indices; first is the Max map channel and the second is the index used in the Nif mesh.
         */
        NIFLIB_API virtual void SetUVSetMap( const std::map<int, int> & in );

        /*!
         * Used to apply a transformation directly to all the vertices and normals in
         * this mesh.
         * \param[in] transform The 4x4 transformation matrix to apply to the vertices and normals in this mesh.  Normals are only affected by the rotation portion of this matrix.
         */
        NIFLIB_API void Transform( const Matrix44 & transform );

	// Consistency Flags
	// \return The current value.
	NIFLIB_API ConsistencyType GetConsistencyFlags() const;

        // Consistency Flags
        // \param[in] value The new value.
        NIFLIB_API void SetConsistencyFlags( const ConsistencyType & value );

   // Methods for saving bitangents and tangents saved in upper byte.
   // \return The current value.
   NIFLIB_API byte GetTspaceFlag() const;

        // Methods for saving bitangents and tangents saved in upper byte.
        // \param[in] value The new value.
        NIFLIB_API void SetTspaceFlag(byte value);

        // Do we have lighting normals? These are essential for proper lighting: if not
        // present, the model will only be influenced by ambient light.
        // \return The current value.
        NIFLIB_API bool GetHasNormals() const;

        // Do we have lighting normals? These are essential for proper lighting: if not
        // present, the model will only be influenced by ambient light.
        // \param[in] value The new value.
        NIFLIB_API void SetHasNormals(bool value);

        // Unknown. Binormal & tangents? has_normals must be set as well for this field to
        // be present.
        // \return The current value.
        NIFLIB_API vector<Vector3> GetBitangents() const;

        // Unknown. Binormal & tangents? has_normals must be set as well for this field to
        // be present.
        // \param[in] value The new value.
        NIFLIB_API void SetBitangents( const vector<Vector3>& value );

   // Unknown. Binormal & tangents?
   // \return The current value.
   NIFLIB_API vector<Vector3> GetTangents() const;

        // Unknown. Binormal & tangents?
        // \param[in] value The new value.
        NIFLIB_API void SetTangents( const vector<Vector3>& value );

   NIFLIB_API SkyrimHavokMaterial GetSkyrimMaterial() const;

        private ushort numUvSetsCalc(const NifInfo &) const;
        private ushort bsNumUvSetsCalc(const NifInfo &) const;

        //--END:CUSTOM--//

    }

}