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

/*! Wrapper for boolean animation keys. */
public class NiBoolData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBoolData", NiObject.TYPE);
	/*! The boolean keys. */
	internal KeyGroup<byte> data;

	public NiBoolData() {
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
	public static NiObject Create() => new NiBoolData();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
		Nif.NifStream(out data.numKeys, s, info);
		if ((data.numKeys != 0)) {
			Nif.NifStream(out data.interpolation, s, info);
		}
		data.keys = new Key[data.numKeys];
		for (var i2 = 0; i2 < data.keys.Count; i2++) {
			Nif.NifStream(out data.keys[i2], s, info, data.interpolation);
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		data.numKeys = (uint)data.keys.Count;
		Nif.NifStream(data.numKeys, s, info);
		if ((data.numKeys != 0)) {
			Nif.NifStream(data.interpolation, s, info);
		}
		for (var i2 = 0; i2 < data.keys.Count; i2++) {
			Nif.NifStream(data.keys[i2], s, info, data.interpolation);
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
		data.numKeys = (uint)data.keys.Count;
		s.AppendLine($"    Num Keys:  {data.numKeys}");
		if ((data.numKeys != 0)) {
			s.AppendLine($"      Interpolation:  {data.interpolation}");
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < data.keys.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Keys[{i2}]:  {data.keys[i2]}");
			array_output_count++;
		}
		return s.ToString();

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

		base.FixLinks(objects, link_stack, missing_link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetRefs() {
		var refs = base.GetRefs();
		return refs;
	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetPtrs() {
		var ptrs = base.GetPtrs();
		return ptrs;
	}

	//--BEGIN:FILE FOOT--//
        /*! Gets or sets the type of boolean interpolation being used.  Does not affect existing key data.
         * \param t The new boolean key type specifing the type of interpolation to be used.
         * \sa NiBoolData::GetKeyType
         */
        public KeyType KeyType
        {
            get => data.interpolation;
            set => data.interpolation = value;
        }

        /*! Gets or sets the boolean key data.
         * \param keys A vector containing new Key<unsigned char> data which will replace any existing data.
         * \sa NiBoolData::GetKeys, Key
         */
        public IList<Key<byte>> Keys
        {
            get => data.keys;
            set => data.keys = value;
        }

        /*!
         * This function will adjust the times in all the keys stored in this data
         * object such that phase will equal 0 and frequency will equal one.  In
         * other words, it will cause the key times to be in seconds starting from
         * zero.
         * \param[in] frequency The frequency to normalize to 1.0 for any keys
         * stored in this object
         * \param[in] phase The phase shift to remove from any keys stored in this
         * object.
         */
        public virtual void NormalizeKeys(float phase, float frequency) => Key.NormalizeKeyVector(data.keys, phase, frequency);
	//--END:CUSTOM--//

}

}