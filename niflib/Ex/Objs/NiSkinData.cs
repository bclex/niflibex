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

    /*! Skinning data. */
    public class NiSkinData : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiSkinData", NiObject.TYPE);
        /*! Offset of the skin from this bone in bind position. */
        internal NiTransform skinTransform;
        /*! Number of bones. */
        internal uint numBones;
        /*! This optionally links a NiSkinPartition for hardware-acceleration information. */
        internal NiSkinPartition skinPartition;
        /*! Enables Vertex Weights for this NiSkinData. */
        internal byte hasVertexWeights;
        /*! Contains offset data for each node that this skin is influenced by. */
        internal IList<BoneData> boneList;

        public NiSkinData()
        {
            numBones = (uint)0;
            skinPartition = null;
            hasVertexWeights = (byte)1;
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
        public static NiObject Create() => new NiSkinData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out skinTransform.rotation, s, info);
            Nif.NifStream(out skinTransform.translation, s, info);
            Nif.NifStream(out skinTransform.scale, s, info);
            Nif.NifStream(out numBones, s, info);
            if ((info.version >= 0x04000002) && (info.version <= 0x0A010000))
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }
            if (info.version >= 0x04020100)
            {
                Nif.NifStream(out hasVertexWeights, s, info);
            }
            boneList = new BoneData[numBones];
            for (var i1 = 0; i1 < boneList.Count; i1++)
            {
                Nif.NifStream(out boneList[i1].skinTransform.rotation, s, info);
                Nif.NifStream(out boneList[i1].skinTransform.translation, s, info);
                Nif.NifStream(out boneList[i1].skinTransform.scale, s, info);
                Nif.NifStream(out boneList[i1].boundingSphereOffset, s, info);
                Nif.NifStream(out boneList[i1].boundingSphereRadius, s, info);
                if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072))
                {
                    for (var i3 = 0; i3 < 13; i3++)
                    {
                        Nif.NifStream(out boneList[i1].unknown13Shorts[i3], s, info);
                    }
                }
                Nif.NifStream(out boneList[i1].numVertices, s, info);
                if (info.version <= 0x04020100)
                {
                    boneList[i1].vertexWeights = new BoneVertData[boneList[i1].numVertices];
                    for (var i3 = 0; i3 < boneList[i1].vertexWeights.Count; i3++)
                    {
                        Nif.NifStream(out boneList[i1].vertexWeights[i3].index, s, info);
                        Nif.NifStream(out boneList[i1].vertexWeights[i3].weight, s, info);
                    }
                }
                if (info.version >= 0x04020200)
                {
                    if ((hasVertexWeights != 0))
                    {
                        boneList[i1].vertexWeights = new BoneVertData[boneList[i1].numVertices];
                        for (var i4 = 0; i4 < boneList[i1].vertexWeights.Count; i4++)
                        {
                            Nif.NifStream(out boneList[i1].vertexWeights[i4].index, s, info);
                            Nif.NifStream(out boneList[i1].vertexWeights[i4].weight, s, info);
                        }
                    }
                }
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numBones = (uint)boneList.Count;
            Nif.NifStream(skinTransform.rotation, s, info);
            Nif.NifStream(skinTransform.translation, s, info);
            Nif.NifStream(skinTransform.scale, s, info);
            Nif.NifStream(numBones, s, info);
            if ((info.version >= 0x04000002) && (info.version <= 0x0A010000))
            {
                WriteRef((NiObject)skinPartition, s, info, link_map, missing_link_stack);
            }
            if (info.version >= 0x04020100)
            {
                Nif.NifStream(hasVertexWeights, s, info);
            }
            for (var i1 = 0; i1 < boneList.Count; i1++)
            {
                boneList[i1].numVertices = (ushort)boneList[i1].vertexWeights.Count;
                Nif.NifStream(boneList[i1].skinTransform.rotation, s, info);
                Nif.NifStream(boneList[i1].skinTransform.translation, s, info);
                Nif.NifStream(boneList[i1].skinTransform.scale, s, info);
                Nif.NifStream(boneList[i1].boundingSphereOffset, s, info);
                Nif.NifStream(boneList[i1].boundingSphereRadius, s, info);
                if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072))
                {
                    for (var i3 = 0; i3 < 13; i3++)
                    {
                        Nif.NifStream(boneList[i1].unknown13Shorts[i3], s, info);
                    }
                }
                Nif.NifStream(boneList[i1].numVertices, s, info);
                if (info.version <= 0x04020100)
                {
                    for (var i3 = 0; i3 < boneList[i1].vertexWeights.Count; i3++)
                    {
                        Nif.NifStream(boneList[i1].vertexWeights[i3].index, s, info);
                        Nif.NifStream(boneList[i1].vertexWeights[i3].weight, s, info);
                    }
                }
                if (info.version >= 0x04020200)
                {
                    if ((hasVertexWeights != 0))
                    {
                        for (var i4 = 0; i4 < boneList[i1].vertexWeights.Count; i4++)
                        {
                            Nif.NifStream(boneList[i1].vertexWeights[i4].index, s, info);
                            Nif.NifStream(boneList[i1].vertexWeights[i4].weight, s, info);
                        }
                    }
                }
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
            numBones = (uint)boneList.Count;
            s.AppendLine($"  Rotation:  {skinTransform.rotation}");
            s.AppendLine($"  Translation:  {skinTransform.translation}");
            s.AppendLine($"  Scale:  {skinTransform.scale}");
            s.AppendLine($"  Num Bones:  {numBones}");
            s.AppendLine($"  Skin Partition:  {skinPartition}");
            s.AppendLine($"  Has Vertex Weights:  {hasVertexWeights}");
            array_output_count = 0;
            for (var i1 = 0; i1 < boneList.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                boneList[i1].numVertices = (ushort)boneList[i1].vertexWeights.Count;
                s.AppendLine($"    Rotation:  {boneList[i1].skinTransform.rotation}");
                s.AppendLine($"    Translation:  {boneList[i1].skinTransform.translation}");
                s.AppendLine($"    Scale:  {boneList[i1].skinTransform.scale}");
                s.AppendLine($"    Bounding Sphere Offset:  {boneList[i1].boundingSphereOffset}");
                s.AppendLine($"    Bounding Sphere Radius:  {boneList[i1].boundingSphereRadius}");
                array_output_count = 0;
                for (var i2 = 0; i2 < 13; i2++)
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
                    s.AppendLine($"      Unknown 13 Shorts[{i2}]:  {boneList[i1].unknown13Shorts[i2]}");
                    array_output_count++;
                }
                s.AppendLine($"    Num Vertices:  {boneList[i1].numVertices}");
                array_output_count = 0;
                for (var i2 = 0; i2 < boneList[i1].vertexWeights.Count; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                        break;
                    }
                    s.AppendLine($"      Index:  {boneList[i1].vertexWeights[i2].index}");
                    s.AppendLine($"      Weight:  {boneList[i1].vertexWeights[i2].weight}");
                }
            }
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            if ((info.version >= 0x04000002) && (info.version <= 0x0A010000))
            {
                skinPartition = FixLink<NiSkinPartition>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (skinPartition != null)
                refs.Add((NiObject)skinPartition);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }

        //--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the overall transform for this skin.  This is the transform from the geometry node back to the skeleton root.
         * \param[in] transform The new overall transform for this skin.
         */
        public Matrix44 OverallTransform
        {
            get => new Matrix44(skinTransform.translation, skinTransform.rotation, skinTransform.scale);
            set => value.Decompose(skinTransform.translation, skinTransform.rotation, skinTransform.scale);
        }

        /*!
         * Retrieves the number of bones influences affecting this skin.  These are NiNodes which cause the skin to deform when they move.
         * \return The number of bonees influencing this skin.
         */
        public uint BoneCount => (uint)boneList.Count;

        /*!
         * Retrieves the transform for a particular bone.  This is the transform from geometry node back to this bone in skeleton root coordinates.
         * \param[in] bone_index The numeric index of the bone that the transform matrix should be returned for.  Must be >= zero and < the number returned by GetBoneCount.
         * \return The transform matrix for the specified bone.
         */
        public Matrix44 GetBoneTransform(uint bone_index)
        {
            if (bone_index > boneList.Count)
                throw new Exception("The specified bone index was larger than the number of bones in this NiSkinData.");
            return new Matrix44(boneList[(int)bone_index].skinTransform.translation, boneList[(int)bone_index].skinTransform.rotation, boneList[(int)bone_index].skinTransform.scale);
        }

        /*!
         * Retrieves the skin weights for a particular bone.  This information includes the vertex index into the geometry data's vertex array, and the percentage weight that defines how much the movement of this bone influences its position.
         * \param[in] bone_index The numeric index of the bone that the skin weight data should be returned for.  Must be >= zero and < the number returned by GetBoneCount.
         * \return The skin weight data for the specified bone.
         */
        public IList<BoneVertData> GetBoneWeights(uint bone_index)
        {
            if (bone_index > boneList.Count)
                throw new Exception("The specified bone index was larger than the number of bones in this NiSkinData.");
            return boneList[(int)bone_index].vertexWeights;
        }

        /*!
         * Sets the skin weights for a particular bone.  This information includes the vertex index into the geometry data's vertex array, and the percentage weight that defines how much the movement of this bone influences its position.
         * \param[in] bone_index The numeric index of the bone that the skin weight data should be set for.  Must be >= zero and < the number returned by GetBoneCount.
         * \param[in] weights The new skin weight data for the specified bone.
         * \param[in] center The center point of all the vertices affected by this bone.  This is the mid point between the minimums and maximums in each of the 3 directions.
         * \param[in] radius The radius of a bounding circle centered at the center point which contains all the vertices affected by this bone.  This is the distance from the center to vertex that is the greatest distance away.
         * \return The skin weight data for the specified bone.
         */
        public void SetBoneWeights(uint bone_index, IList<BoneVertData> weights, Vector3 center, float radius)
        {
            if (bone_index > boneList.Count)
                throw new Exception("The specified bone index was larger than the number of bones in this NiSkinData.");
            hasVertexWeights = 1;
            boneList[(int)bone_index].vertexWeights = weights;
            boneList[(int)bone_index].boundingSphereOffset = center;
            boneList[(int)bone_index].boundingSphereRadius = radius;
        }

        /*!
         * Sets the skin weights for a particular bone, without changing center and radius
         * \sa NiSkinData::SetBoneWeights
         */
        public void SetBoneWeights(uint bone_index, IList<BoneVertData> weights)
        {
            if (bone_index > boneList.Count)
                throw new Exception("The specified bone index was larger than the number of bones in this NiSkinData.");
            hasVertexWeights = 1;
            boneList[(int)bone_index].vertexWeights = weights;
        }

        /*!
         * Returns a reference to the hardware skin partition data object, if any.
         * \return The hardware skin partition data, or NULL if none is used.
         */
        public NiSkinPartition SkinPartition
        {
            get => skinPartition;
            /*
            * NIFLIB_HIDDEN function.  For internal use only.
            * This can be used to set or clear the hardware skin partition data.  To create partition data, the NiTriBasedGeom::GenHardwareSkinInfo function should be used.
            * \param[in] skinPartition The new hardware skin partition data object to use, or NULL to clear the existing one.
            */
            internal set => skinPartition = value;
        }

        /*!
         * NIFLIB_HIDDEN function.  For internal use only.
         * This constructor is called by NiGeometry when it creates a new skin instance using the BindSkin function.
         */
        internal NiSkinData(NiGeometry owner)
        {   //Call normal constructor
            ResetOffsets(owner);
        }

        /*
         * NIFLIB_HIDDEN function.  For internal use only.
         * This is called by NiGeometry when the NormalizeSkinWeights function of that object is called, which is a public function.
         */
        internal void NormalizeWeights(uint numVertices)
        {
            var totals = new double[numVertices];
            var counts = new int[numVertices];

            //Set all totals to 1.0 and all counts to 0
            for (var v = 0; v < numVertices; ++v)
            {
                totals[v] = 1.0;
                counts[v] = 0;
            }

            //Calculate the total error for each vertex by subtracting the weight from
            //each bone from the starting total of 1.0
            //Also count the number of bones affecting each vertex
            for (var b = 0; b < boneList.Count; ++b)
                for (var w = 0; w < boneList[b].vertexWeights.Count; ++w)
                {
                    var sw = boneList[b].vertexWeights[w];
                    totals[sw.index] -= sw.weight;
                    counts[sw.index]++;
                }

            //Divide all error amounts by the number of bones affecting that vertex to
            //get the amount of error that should be distributed to each bone.
            for (var v = 0; v < numVertices; ++v)
                totals[v] = totals[v] / (double)counts[v];

            //Distribute the calculated error to each weight
            for (var b = 0; b < boneList.Count; ++b)
                for (var w = 0; w < boneList[b].vertexWeights.Count; ++w)
                {
                    var sw = boneList[b].vertexWeights[w];
                    var temp = (double)sw.weight + totals[sw.index];
                    sw.weight = (float)temp;
                }
        }

        /*!
         * NIFLIB_HIDDEN function.  For internal use only.
         * This function resets the bone offsets to their current positions, effetivley changing the bind pose.  This does not cause any tranformations to the vertex positions, however, so is mostly usful for instances where the world positions of the old and new bind pose are equivalent, but result from different local transformations along the way.  It is called by NiGeometry when the interum transforms are flattened.
         */
        internal void ResetOffsets(NiGeometry owner)
        {
            //Get skin instance
            var skinInst = owner.SkinInstance;
            if (skinInst == null)
                throw new Exception("Skin instance should have already been created.");
            boneList.Resize(skinInst.BoneCount);
            var bone_nodes = skinInst.Bones;

            //--Calculate Overall Offset--//

            //Get TriBasedGeom world transform
            var owner_mat = owner.GetWorldTransform();

            //Get Skeleton root world transform
            var sr_world = skinInst.SkeletonRoot.GetWorldTransform();

            //Inverse owner NiGeometry matrix & multiply with skeleton root matrix
            var overall_mat = (owner_mat * sr_world.Inverse()).Inverse();

            //Store result
            overall_mat.Decompose(skinTransform.translation, skinTransform.rotation, skinTransform.scale);

            //--Calculate Bone Offsets--//
            Matrix44 res_mat;
            Matrix44 bone_mat;
            for (var i = 0; i < boneList.Count; ++i)
            {
                //--Get Bone Bind Pose--//
                //Get bone world position
                bone_mat = bone_nodes[i].GetWorldTransform();
                //Multiply NiGeometry matrix with inversed bone matrix
                res_mat = owner_mat * bone_mat.Inverse();
                //Store result
                res_mat.Decompose(boneList[i].skinTransform.translation, boneList[i].skinTransform.rotation, boneList[i].skinTransform.scale);
            }
        }
        //--END:CUSTOM--//
    }

}