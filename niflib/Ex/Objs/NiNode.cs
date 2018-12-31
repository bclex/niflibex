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


namespace Niflib {

/*! Generic node object for grouping. */
public class NiNode : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiNode", NiAVObject.TYPE);
	//--BEGIN:MISC--//
        List<NiSkinInstance> skins = new List<NiSkinInstance>();

        /*!
         * Adds a child AV Object to this node.  This is a sub-leaf in the scene graph contained in a NIF file.  Each AV Object can only be the child of one node.
         * \param[in] obj The AV Object to add as a child of this node.
         */
        public void AddChild(NiAVObject obj)
        {
            if (obj.GetParent() != null)
                throw new Exception("You have attempted to add a child to a NiNode which already is the child of another NiNode.");
            obj.SetParent(this);
            //Sometimes NiTriBasedGeom with skins can be siblings of NiNodes that
            //represent joints for that same skin.  When this is the case, NiTriBasedGeom
            //must com first, so we enforce that by always adding NiTriBasedGeom to the
            //begining of the child list.
            var niGeom = obj as NiTriBasedGeom;
            if (niGeom != null)
            {
                //This is a NiTriBasedGeom, so shift all children to the right
                var old_size = children.Count;
                children.resize(children.Count + 1);
                for (var i = children.Count - 1; i >= 1; --i)
                    children[i] = children[i - 1];

                //Now add the new child to the begining of the list
                children[0] = obj;
            }
            //This is some other type of object.  Just add it to the end of the list.
            else children.Add(obj);
        }

        /*!
         * Removes an AV Object child from this node.  This is a sub-leaf in the scene graph contained in a NIF file.  Each AV Object can only be the child of one node.
         * The caller is responsible that the child is no longer weakly linked elsewhere, for instance, as a skin influence.
         * \param[in] obj The AV Object to remove as a child from this node.
         */
        void RemoveChild(NiAVObject obj)
        {
            //Search child list for the one to remove
            foreach (var it in children.ToList())
                if (it == obj)
                    it.SetParent(null);
            children.Remove(it);
        }

        /*!
         * Removes all AV Object children from this node.  These are a sub-leafs in the scene graph contained in a NIF file.  Each AV Object can only be the child of one node.
         * The caller is responsible that no child is still weakly linked elsewhere, for instance, as a skin influence.
         */
        public void ClearChildren()
        {
            foreach (var it in children)
                if (it != null) it.SetParent(null);
            children.clear();
        }

        /*!
         * Retrieves all AV Object children from this node.  These are a sub-leafs in the scene graph contained in a NIF file.  Each AV Object can only be the child of one node.
         * \return A list of all the AV Objects that are children of this node in the scene graph.
         */
        public List<NiAVObject> Children => children;

        /*!
         * Adds a dynamic effect to this node.  This is usually a light, but can also be a texture effect or something else.  Can affect nodes further down the scene graph from this one as well.
         * \param[in] effect The new dynamic effect to add to this node.
         */
        public void AddEffect(NiDynamicEffect obj)
        {
            obj.SetParent(this);
            effects.Add(obj);
        }

        /*!
         * Removes a dynamic effect to this node.  This is usually a light, but can also be a texture effect or something else.  Can affect nodes further down the scene graph from this one as well.
         * \param[in] effect The dynamic effect to remove from this node.
         */
        public void RemoveEffect(NiDynamicEffect obj)
        {
            //Search Effect list for the one to remove
            foreach (var it in effects)
                if (it == obj)
                {
                    it.SetParent(null);
                    effects.Remove(it);
                }
        }

        /*!
         * Removes all dynamic effects from this node.  These is usually lights, but can also be a texture effects or something else.  Can affect nodes further down the scene graph from this one as well.
         */
        public void ClearEffects()
        {
            foreach (var it in effects)
                if (it != null) it.SetParent(null);
            effects.Clear();
        }

        /*!
         * Retrieves all the dynamic effects attached to this node.  This is usually a light, but can also be a texture effect or something else.  Can affect nodes further down the scene graph from this one as well.
         * \return The dynamic effects attached to this node.
         */
        public List<NiDynamicEffect> Effects => effects;

        /*! Checks if this node has any skins attached. */
        public bool IsSkeletonRoot => skins.Count > 0;

        /*! Checks if this node influences the vertices in any skins. */
        public bool IsSkinInfluence => (flags & 8) == 0;

        /*! 
        * Applies a huristic to guess whether this node was created as a proxy
        * when a mesh which had more than one material in the original model
        * was split in an exporter.
        * /return Whether or not this node is probably a split mesh proxy
        */
        public bool IsSplitMeshProxy
        {
            //Let us guess that a node is a split mesh proxy if:
            // 1)  It is not a skin influence
            // 2)  All its children are NiTriBasedGeom derived objects.
            // 3)  All its children have identity transforms.
            // 4)  It has more than one child
            // 5)  All meshes are visible
            // 6)  ????  May need more criteria as time goes on.
            get
            {
                if (IsSkinInfluence) return false;
                if (children.Count < 2) return false;
                for (var i = 0; i < children.Count; ++i)
                {
                    if (!children[i].IsDerivedType(NiTriBasedGeom.TYPE)) return false;
                    if (children[i].GetLocalTransform() != Matrix44.IDENTITY) return false;
                    if (!children[i].GetVisibility()) return false;
                }
                //Made it all the way through the loop without returning false
                return true;
            }
        }

        /*! 
        * Causes all children's transforms to be changed so that all the skin
        * pieces line up without any vertex transformations.
        */
        public void GoToSkeletonBindPosition()
        {
            //Dictionary<NiNodeRef, Matrix44> world_positions;
            //Loop through all attached skins, straightening the skeleton on each
            foreach (var it in skins)
            {
                //Get Bone list and Skin Data
                var bone_nodes = it.GetBones();
                var skin_data = it.GetSkinData();
                if (skin_data == null)
                    //There's no skin data for this skin instance; skip it.
                    continue;

                //Make sure the counts match
                if (bone_nodes.Count != skin_data.GetBoneCount())
                    throw new Exception("Bone counts in NiSkinInstance and attached NiSkinData must match");

                //Loop through all bones influencing this skin
                for (var i = 0; i < bone_nodes.Count; ++i)
                {
                    //Get current offset Matrix for this bone
                    var parent_offset = skin_data.GetBoneTransform(i);
                    //Loop through all bones again, checking for any that have this bone as a parent
                    for (var j = 0; j < bone_nodes.Count; ++j)
                        if (bone_nodes[j]->GetParent() == bone_nodes[i])
                        {
                            //Node 2 has node 1 as a parent
                            //Get child offset Matrix33
                            var child_offset = skin_data.GetBoneTransform(j);
                            //Do calculation to get correct bone postion in relation to parent
                            var child_pos = child_offset.Inverse() * parent_offset;
                            //bones[j].SetWorldBindPos( child_pos );
                            bone_nodes[j].SetLocalRotation(child_pos.GetRotation());
                            bone_nodes[j].SetLocalScale(1.0f);
                            bone_nodes[j].SetLocalTranslation(child_pos.GetTranslation());
                        }
                }
            }
        }

        /*!
         * Applies the local transforms of this node to its children,
         * causing itself to be cleared to identity transforms.
         */
        public void PropagateTransform()
        {
            var par_trans = GetLocalTransform();
            //Loop through each child and apply this node's transform to it
            for (var i = 0; i < children.Count; ++i)
                children[i].SetLocalTransform(children[i].GetLocalTransform() * par_trans);
            //Nowthat the transforms have been propogated, clear them out
            SetLocalTransform(Matrix44.IDENTITY);
        }

        /*! 
         * NIFLIB_HIDDEN function.  For internal use only.
         * Should only be called by NiTriBasedGeom.  Adds a new SkinInstance to the specified mesh.  The bones must be below this node in the scene graph tree
         */
        public void AddSkin(NiSkinInstance skin_inst) => skins.Add(skin_inst);

        /*! 
         * NIFLIB_HIDDEN function.  For internal use only.
         * Should only be called by NiTriBasedGeom.  Detaches the skin associated with a child mesh.
         */
        public void RemoveSkin(NiSkinInstance skin_inst)
        {
            //Remove the reference
            skins.remove(skin_inst);

            //Ensure that any multiply referenced bone nodes still
            //have their skin flag set
            List<NiNode> bones;
            foreach (var it in skins.begin())
            {
                bones = it.GetBones();
                for (var i = 0; i < bones.Count; ++i)
                    bones[i].SetSkinFlag(true);
            }
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        public void SetSkinFlag(bool n)
        {
            //Already set to the requested value
            if (IsSkinInfluence() == n) return;
            //Requested value is different, flip bit
            else flags ^= 8;
        }
	//--END:CUSTOM--//
	/*! The number of child objects. */
	internal uint numChildren;
	/*! List of child node object indices. */
	internal IList<NiAVObject> children;
	/*! The number of references to effect objects that follow. */
	internal uint numEffects;
	/*! List of node effects. ADynamicEffect? */
	internal IList<NiDynamicEffect> effects;

	public NiNode() {
	numChildren = (uint)0;
	numEffects = (uint)0;
	//--BEGIN:CONSTRUCTOR--//
            //Set flag to default of 8: not a skin influence
            flags = 8;
	//--END:CUSTOM--//
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
public static NiObject Create() => new NiNode();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numChildren, s, info);
	children = new Ref[numChildren];
	for (var i1 = 0; i1 < children.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream(out numEffects, s, info);
		effects = new Ref[numEffects];
		for (var i2 = 0; i2 < effects.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numEffects = (uint)effects.Count;
	numChildren = (uint)children.Count;
	Nif.NifStream(numChildren, s, info);
	for (var i1 = 0; i1 < children.Count; i1++) {
		WriteRef((NiObject)children[i1], s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream(numEffects, s, info);
		for (var i2 = 0; i2 < effects.Count; i2++) {
			WriteRef((NiObject)effects[i2], s, info, link_map, missing_link_stack);
		}
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numEffects = (uint)effects.Count;
	numChildren = (uint)children.Count;
	s.AppendLine($"  Num Children:  {numChildren}");
	array_output_count = 0;
	for (var i1 = 0; i1 < children.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Children[{i1}]:  {children[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Effects:  {numEffects}");
	array_output_count = 0;
	for (var i1 = 0; i1 < effects.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Effects[{i1}]:  {effects[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < children.Count; i1++) {
		children[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 < 130)) {
		for (var i2 = 0; i2 < effects.Count; i2++) {
			effects[i2] = FixLink<NiDynamicEffect>(objects, link_stack, missing_link_stack, info);
		}
	}

	//--BEGIN:POST-FIXLINKS--//
            //Connect children to their parents and remove any NULL ones
            foreach (var it in children)
            {
                if (it == null) children.erase(it);
                else it.SetParent(this);
            }
	//--END:CUSTOM--//
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < children.Count; i1++) {
		if (children[i1] != null)
			refs.Add((NiObject)children[i1]);
	}
	for (var i1 = 0; i1 < effects.Count; i1++) {
		if (effects[i1] != null)
			refs.Add((NiObject)effects[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < children.Count; i1++) {
	}
	for (var i1 = 0; i1 < effects.Count; i1++) {
	}
	return ptrs;
}


}

}