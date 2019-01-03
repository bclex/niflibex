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

    /*! Skinning instance. */
    public class NiSkinInstance : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiSkinInstance", NiObject.TYPE);
        /*! Skinning data reference. */
        internal NiSkinData data;
        /*!
         * Refers to a NiSkinPartition objects, which partitions the mesh such that every
         * vertex is only influenced by a limited number of bones.
         */
        internal NiSkinPartition skinPartition;
        /*! Armature root node. */
        internal NiNode skeletonRoot;
        /*! The number of node bones referenced as influences. */
        internal uint numBones;
        /*! List of all armature bones. */
        internal IList<NiNode> bones;

        public NiSkinInstance()
        {
            data = null;
            skinPartition = null;
            skeletonRoot = null;
            numBones = (uint)0;
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
        public static NiObject Create() => new NiSkinInstance();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out block_num, s, info);
            link_stack.Add(block_num);
            if (info.version >= 0x0A010065)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }
            Nif.NifStream(out block_num, s, info);
            link_stack.Add(block_num);
            Nif.NifStream(out numBones, s, info);
            bones = new *[numBones];
            for (var i1 = 0; i1 < bones.Count; i1++)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numBones = (uint)bones.Count;
            WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
            if (info.version >= 0x0A010065)
            {
                WriteRef((NiObject)skinPartition, s, info, link_map, missing_link_stack);
            }
            WriteRef((NiObject)skeletonRoot, s, info, link_map, missing_link_stack);
            Nif.NifStream(numBones, s, info);
            for (var i1 = 0; i1 < bones.Count; i1++)
            {
                WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
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
            numBones = (uint)bones.Count;
            s.AppendLine($"  Data:  {data}");
            s.AppendLine($"  Skin Partition:  {skinPartition}");
            s.AppendLine($"  Skeleton Root:  {skeletonRoot}");
            s.AppendLine($"  Num Bones:  {numBones}");
            array_output_count = 0;
            for (var i1 = 0; i1 < bones.Count; i1++)
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
                s.AppendLine($"    Bones[{i1}]:  {bones[i1]}");
                array_output_count++;
            }
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            data = FixLink<NiSkinData>(objects, link_stack, missing_link_stack, info);
            if (info.version >= 0x0A010065)
            {
                skinPartition = FixLink<NiSkinPartition>(objects, link_stack, missing_link_stack, info);
            }
            skeletonRoot = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
            for (var i1 = 0; i1 < bones.Count; i1++)
            {
                bones[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (data != null)
                refs.Add((NiObject)data);
            if (skinPartition != null)
                refs.Add((NiObject)skinPartition);
            for (var i1 = 0; i1 < bones.Count; i1++)
            {
            }
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            if (skeletonRoot != null)
                ptrs.Add((NiObject)skeletonRoot);
            for (var i1 = 0; i1 < bones.Count; i1++)
            {
                if (bones[i1] != null)
                    ptrs.Add((NiObject)bones[i1]);
            }
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//

        /*!
         * Retrieves the number of NiNode bones that influence this skin.
         * \return The number of bones that influence this skin.
         */
        public uint BoneCount => (uint)bones.Count;

        /*!
         * Retrieves a list of references to all the NiNode bones that influence this skin.
         * \return All the bones that influence this skin.
         */
        public IList<NiNode> GetBones()
        {
            var ref_bones = new NiNode[bones.Count];
            for (var i = 0; i < bones.Count; ++i)
                ref_bones[i] = bones[i];
            return ref_bones;
        }

        /*!
         * Retrieves the root node of the skeleton that influences this skin.  This is the common ancestor of all bones and the skin itself.
         * \return The skeleton root.
         */
        public NiNode SkeletonRoot => skeletonRoot;

        /*!
         * Retrieves the root node of the skeleton that influences this skin.  This is the common ancestor of all bones and the skin itself.
         * \return The skeleton root.
         */
        public NiSkinData SkinData
        {
            get => data;
            internal set => data = value;
        }

        /*!
         * Retrieves the hardare skin partition, if any.
         * \return The skeleton root.
         */
        public NiSkinPartition SkinPartition
        {
            get => skinPartition;
            internal set => skinPartition = value;
        }

        /*!
         * This constructor is called by NiGeometry when it creates a new skin
         * instance using the BindSkin function.
         */
        internal void BindSkin(NiNode skeleton_root, IList<NiNode> bone_nodes)
        {
            //Call normal constructor
            //Ensure that all bones are below the skeleton root node on the scene graph
            for (var i = 0; i < bone_nodes.Count; ++i)
            {
                var is_decended = false;
                var node = bone_nodes[i];
                while (node != null)
                {
                    if (node == skeleton_root)
                    {
                        is_decended = true;
                        break;
                    }
                    node = node.Parent;
                }
                if (!is_decended)
                    throw new Exception("All bones must be lower than the skeleton root in the scene graph.");
            }

            //Add the bones to the internal list
            bones.Resize(bone_nodes.Count);
            for (var i = 0; i < bone_nodes.Count; ++i)
                bones[i] = bone_nodes[i];
            //Flag any bones that are part of this skin instance
            for (var i = 0; i < bones.Count; ++i)
                if (bones[i] != null)
                    bones[i].SetSkinFlag(true);

            //Store skeleton root and inform it of this attachment
            skeletonRoot = skeleton_root;
            skeletonRoot.AddSkin(this);
        }


        /*! 
         * NIFLIB_HIDDEN function.  For internal use only.
         * Called by skeleton root NiNode when it self destructs to inform this skin
         * instance that the skeleton has been lost.  Should not be called directly.
         */
        internal void SkeletonLost()
        {
            skeletonRoot = null;
            //Clear bone list
            bones.Clear();
            //Destroy skin data
            data = null;
            skinPartition = null;
        }
        //--END:CUSTOM--//

    }

}