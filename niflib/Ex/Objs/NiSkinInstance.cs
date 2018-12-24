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

/*! Skinning instance. */
public class NiSkinInstance : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSkinInstance", NiObject.TYPE);
	/*! Skinning data reference. */
	public NiSkinData data;
	/*!
	 * Refers to a NiSkinPartition objects, which partitions the mesh such that every
	 * vertex is only influenced by a limited number of bones.
	 */
	public NiSkinPartition skinPartition;
	/*! Armature root node. */
	public NiNode skeletonRoot;
	/*! The number of node bones referenced as influences. */
	public uint numBones;
	/*! List of all armature bones. */
	public NiNode[] bones;

	public NiSkinInstance() {
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
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	if (info.version >= 0x0A010065) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out numBones, s, info);
	bones = new *[numBones];
	for (var i1 = 0; i1 < bones.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numBones = (uint)bones.Length;
	WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
	if (info.version >= 0x0A010065) {
		WriteRef((NiObject)skinPartition, s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)skeletonRoot, s, info, link_map, missing_link_stack);
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string asString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.asString());
	numBones = (uint)bones.Length;
	s.AppendLine($"  Data:  {data}");
	s.AppendLine($"  Skin Partition:  {skinPartition}");
	s.AppendLine($"  Skeleton Root:  {skeletonRoot}");
	s.AppendLine($"  Num Bones:  {numBones}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bones.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Bones[{i1}]:  {bones[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	data = FixLink<NiSkinData>(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A010065) {
		skinPartition = FixLink<NiSkinPartition>(objects, link_stack, missing_link_stack, info);
	}
	skeletonRoot = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		bones[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	if (skinPartition != null)
		refs.Add((NiObject)skinPartition);
	for (var i1 = 0; i1 < bones.Length; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (skeletonRoot != null)
		ptrs.Add((NiObject)skeletonRoot);
	for (var i1 = 0; i1 < bones.Length; i1++) {
		if (bones[i1] != null)
			ptrs.Add((NiObject)bones[i1]);
	}
	return ptrs;
}


}

}