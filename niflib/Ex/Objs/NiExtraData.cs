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

/*! A generic extra data object. */
public class NiExtraData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiExtraData", NiObject.TYPE);
	/*! Name of this object. */
	internal IndexString name;
	/*! Block number of the next extra data object. */
	internal NiExtraData nextExtraData;

	public NiExtraData() {
	nextExtraData = null;
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
public static NiObject Create() => new NiExtraData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A000100) {
		if ((!IsDerivedType(BSExtraData.TYPE))) {
			Nif.NifStream(out name, s, info);
		}
	}
	if (info.version <= 0x04020200) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if (info.version >= 0x0A000100) {
		if ((!IsDerivedType(BSExtraData.TYPE))) {
			Nif.NifStream(name, s, info);
		}
	}
	if (info.version <= 0x04020200) {
		WriteRef((NiObject)nextExtraData, s, info, link_map, missing_link_stack);
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
	if ((!IsDerivedType(BSExtraData.TYPE))) {
		s.AppendLine($"    Name:  {name}");
	}
	s.AppendLine($"  Next Extra Data:  {nextExtraData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x04020200) {
		nextExtraData = FixLink<NiExtraData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (nextExtraData != null)
		refs.Add((NiObject)nextExtraData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the name of this NiExtraData object.  Will only be written to later
         * version NIF files.
         * \param new_name The new name for this NiExtraData object.
         */
        public string Name
        {
            get => name;
            set => name = value;
        }

        /*!
         * Formats a human readable string that includes the type of the object
         * \return A string in the form:  address(type) {name}
         */
        public virtual string IDString => $"{base.IDString} {{name}}";

        /*!
         * Get or sets the next extra data in early version NIF files which store extra
         * data in a linked list.  This function should only be called by
         * NiObjectNET.
         * \param obj A reference to the object to set as the one after this in the chain.
         */
        internal NiExtraData NextExtraData
        {
            get => nextExtraData;
            set => nextExtraData = value;
        }
//--END:CUSTOM--//

}

}