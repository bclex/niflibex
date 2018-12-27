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

/*! NiRoomGroup represents a set of connected rooms i.e. a game level. */
public class NiRoomGroup : NiNode {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRoomGroup", NiNode.TYPE);
	/*! Object that represents the room group as seen from the outside. */
	internal NiNode shell;
	/*!  */
	internal int numRooms;
	/*!  */
	internal NiRoom[] rooms;

	public NiRoomGroup() {
	shell = null;
	numRooms = (int)0;
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
public static NiObject Create() => new NiRoomGroup();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out numRooms, s, info);
	rooms = new *[numRooms];
	for (var i1 = 0; i1 < rooms.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numRooms = (int)rooms.Length;
	WriteRef((NiObject)shell, s, info, link_map, missing_link_stack);
	Nif.NifStream(numRooms, s, info);
	for (var i1 = 0; i1 < rooms.Length; i1++) {
		WriteRef((NiObject)rooms[i1], s, info, link_map, missing_link_stack);
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
	numRooms = (int)rooms.Length;
	s.AppendLine($"  Shell:  {shell}");
	s.AppendLine($"  Num Rooms:  {numRooms}");
	array_output_count = 0;
	for (var i1 = 0; i1 < rooms.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Rooms[{i1}]:  {rooms[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	shell = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < rooms.Length; i1++) {
		rooms[i1] = FixLink<NiRoom>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < rooms.Length; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (shell != null)
		ptrs.Add((NiObject)shell);
	for (var i1 = 0; i1 < rooms.Length; i1++) {
		if (rooms[i1] != null)
			ptrs.Add((NiObject)rooms[i1]);
	}
	return ptrs;
}


}

}