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

/*!
 * DEPRECATED (20.5), replaced by NiMorphMeshModifier.
 *         Geometry morphing data.
 */
public class NiMorphData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiMorphData", NiObject.TYPE);
	/*! Number of morphing object. */
	internal uint numMorphs;
	/*! Number of vertices. */
	internal uint numVertices;
	/*! This byte is always 1 in all official files. */
	internal byte relativeTargets;
	/*! The geometry morphing objects. */
	internal IList<Morph> morphs;

	public NiMorphData() {
	numMorphs = (uint)0;
	numVertices = (uint)0;
	relativeTargets = (byte)1;
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
public static NiObject Create() => new NiMorphData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numMorphs, s, info);
	Nif.NifStream(out numVertices, s, info);
	Nif.NifStream(out relativeTargets, s, info);
	morphs = new Morph[numMorphs];
	for (var i1 = 0; i1 < morphs.Count; i1++) {
		if (info.version >= 0x0A01006A) {
			Nif.NifStream(out morphs[i1].frameName, s, info);
		}
		if (info.version <= 0x0A010000) {
			Nif.NifStream(out morphs[i1].numKeys, s, info);
			Nif.NifStream(out morphs[i1].interpolation, s, info);
			morphs[i1].keys = new Key[morphs[i1].numKeys];
			for (var i3 = 0; i3 < morphs[i1].keys.Count; i3++) {
				Nif.NifStream(out morphs[i1].keys[i3], s, info, morphs[i1].interpolation);
			}
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x14010002) && ((info.userVersion2 < 10))) {
			Nif.NifStream(out morphs[i1].legacyWeight, s, info);
		}
		morphs[i1].vectors = new Vector3[numVertices];
		for (var i2 = 0; i2 < morphs[i1].vectors.Count; i2++) {
			Nif.NifStream(out morphs[i1].vectors[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numMorphs = (uint)morphs.Count;
	Nif.NifStream(numMorphs, s, info);
	Nif.NifStream(numVertices, s, info);
	Nif.NifStream(relativeTargets, s, info);
	for (var i1 = 0; i1 < morphs.Count; i1++) {
		morphs[i1].numKeys = (uint)morphs[i1].keys.Count;
		if (info.version >= 0x0A01006A) {
			Nif.NifStream(morphs[i1].frameName, s, info);
		}
		if (info.version <= 0x0A010000) {
			Nif.NifStream(morphs[i1].numKeys, s, info);
			Nif.NifStream(morphs[i1].interpolation, s, info);
			for (var i3 = 0; i3 < morphs[i1].keys.Count; i3++) {
				Nif.NifStream(morphs[i1].keys[i3], s, info, morphs[i1].interpolation);
			}
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x14010002) && ((info.userVersion2 < 10))) {
			Nif.NifStream(morphs[i1].legacyWeight, s, info);
		}
		for (var i2 = 0; i2 < morphs[i1].vectors.Count; i2++) {
			Nif.NifStream(morphs[i1].vectors[i2], s, info);
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
	numMorphs = (uint)morphs.Count;
	s.AppendLine($"  Num Morphs:  {numMorphs}");
	s.AppendLine($"  Num Vertices:  {numVertices}");
	s.AppendLine($"  Relative Targets:  {relativeTargets}");
	array_output_count = 0;
	for (var i1 = 0; i1 < morphs.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		morphs[i1].numKeys = (uint)morphs[i1].keys.Count;
		s.AppendLine($"    Frame Name:  {morphs[i1].frameName}");
		s.AppendLine($"    Num Keys:  {morphs[i1].numKeys}");
		s.AppendLine($"    Interpolation:  {morphs[i1].interpolation}");
		array_output_count = 0;
		for (var i2 = 0; i2 < morphs[i1].keys.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Keys[{i2}]:  {morphs[i1].keys[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Legacy Weight:  {morphs[i1].legacyWeight}");
		array_output_count = 0;
		for (var i2 = 0; i2 < morphs[i1].vectors.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vectors[{i2}]:  {morphs[i1].vectors[i2]}");
			array_output_count++;
		}
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
        public virtual void NormalizeKeys(float phase, float frequency)
        {
            for (var i = 0; i < morphs.Count; ++i)
                Key.NormalizeKeyVector(morphs[i].keys, phase, frequency);
        }



        /*!
         * Gets or sets the number of verticies used in the morph targets.  This must be the same as the number of verticies in the base mesh that the morph controller for which this object stores data is attatched.  This is not done automatically by Niflib.  If the new size is smaller, vertices at the ends of the morph targets will be lost.
         * \param n The new size of the morph target's vertex arrays.
         * \sa NiMorphData::GetVertexCount
         */
        public int VertexCount
        {
            get => (int)numVertices;
            set
            {
                numVertices = (uint)value;
                for (var i = 0; i < morphs.Count; ++i)
                    morphs[i].vectors.Resize(value);
            }
        }

        /*!
         * Resizes the morph target array used by this morph controller data.  If the new size is smaller, morph targets at the end of the array and all associated data will be lost.
         * \param n The new size of the morph target array.
         * \sa NiMorphData::GetMorphCount
         */
        public int MorphCount
        {
            get => morphs.Count;
            set
            {
                var old_size = morphs.Count;
                morphs.Resize(value);
                //Make sure any new vertex groups are the right size
                for (var i = old_size; i < morphs.Count; ++i)
                    morphs[i].vectors.Resize((int)numVertices);
            }
        }

        /*!
         * Retrieves the type of morph interpolation being used by a specific morph target.
         * \param n The index of the morph to get the interpolation key type from.  A zero-based positive value which must be less than that returned by IMoprhData::GetMorphCount.
         * \return The morph key type specifing the type of interpolation being used by the specified morph target.
         * \sa NiMorphData::SetMorphKeyType
         */
        public KeyType GetMorphKeyType(int n) => morphs[n].interpolation;

        /*!
         * Sets the type of morph interpolation being used by a specific morph target.  Does not affect existing key data.
         * \param n The index of the morph to get the interpolation key type from.  A zero-based positive value which must be less than that returned by IMoprhData::GetMorphCount.
         * \param t The new morph key type specifing the type of interpolation to be used by the specified morph target.
         * \sa NiMorphData::GetMorphKeyType
         */
        public void SetMorphKeyType(int n, KeyType t) => morphs[n].interpolation = t;

        /*!
         * Retrieves the morph key data for a specified morph target.
         * \return A vector containing Key<float> data which specify the influence of this morph target over time.
         * \sa NiMorphData::SetMorphKeys, Key
         */
        public IList<Key<float>> GetMorphKeys(int n) => morphs[n].keys;

        /*!
         * Sets the morph key data.
         * \param n The index of the morph target to set the keys for.
         * \param keys A vector containing new Key<float> data which will replace any existing data for this morph target.
         * \sa NiMorphData::GetMorphKeys, Key
         */
        public void SetMorphKeys(int n, IList<Key<float>> keys) => morphs[n].keys = keys;

        /*!
         * Retrieves the vertex data from the specified morph target
         * \param n The index of the morph target to retrieve vertex data for.  This is a zero-based index whoes value that must be less than that returned by NiMorphData::GetMorphCount.
         * \return A vector containing the vertices used by this morph target.  The size will be equal to the value returned by NiMorphData::GetVertexCount.
         * \sa NiMorphData::SetMorphVerts
         */
        public IList<Vector3> GetMorphVerts(int n) => morphs[n].vectors;

        /*!
         * Sets the vertex data for a specified morph target
         * \param n The index of the morph target to set vertex data for.  This is a zero-based index whoes value that must be less than that returned by NiMorphData::GetMorphCount.
         * \param in A vector containing the new vertices to be used by this morph target.  The size will be equal to the value returned by NiMorphData::GetVertexCount.
         * \sa NiMorphData::SetMorphVerts
         */
        public void SetMorphVerts(int n, IList<Vector3> value) => morphs[n].vectors = value;

        /*!
        * Retrieves the morph frame name for a specified morph target.
        * \return A string which specifies the name of the morph frame.
        */
        public string GetFrameName(int n) => morphs[n].frameName;

        /*!
        * Sets the morph frame name for a specified morph target.
        * \param n The index of the morph target to set the name for.
        * \param keys A frame name which will replace any existing data for this morph target.
        */
        public void SetFrameName(int n, string key) => morphs[n].frameName = key;
//--END:CUSTOM--//

}

}