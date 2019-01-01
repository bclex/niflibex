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
 * DEPRECATED (10.2), RENAMED (10.2) to NiTransformData.
 *         Wrapper for transformation animation keys.
 */
public class NiKeyframeData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiKeyframeData", NiObject.TYPE);
	/*!
	 * The number of quaternion rotation keys. If the rotation type is XYZ (type 4)
	 * then this *must* be set to 1, and in this case the actual number of keys is
	 * stored in the XYZ Rotations field.
	 */
	internal uint numRotationKeys;
	/*!
	 * The type of interpolation to use for rotation.  Can also be 4 to indicate that
	 * separate X, Y, and Z values are used for the rotation instead of Quaternions.
	 */
	internal KeyType rotationType;
	/*! The rotation keys if Quaternion rotation is used. */
	internal IList<Key<Quaternion>> quaternionKeys;
	/*!  */
	internal float order;
	/*! Individual arrays of keys for rotating X, Y, and Z individually. */
	internal Array3<KeyGroup<float>> xyzRotations;
	/*! Translation keys. */
	internal KeyGroup<Vector3> translations;
	/*! Scale keys. */
	internal KeyGroup<float> scales;

	public NiKeyframeData() {
	numRotationKeys = (uint)0;
	rotationType = (KeyType)0;
	order = 0.0f;
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
public static NiObject Create() => new NiKeyframeData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numRotationKeys, s, info);
	if ((numRotationKeys != 0)) {
		Nif.NifStream(out rotationType, s, info);
	}
	if ((rotationType != 4)) {
		quaternionKeys = new Key[numRotationKeys];
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			Nif.NifStream(out quaternionKeys[i2], s, info, rotationType);
		}
	}
	if (info.version <= 0x0A010000) {
		if ((rotationType == 4)) {
			Nif.NifStream(out order, s, info);
		}
	}
	if ((rotationType == 4)) {
		for (var i2 = 0; i2 < 3; i2++) {
			Nif.NifStream(out xyzRotations[i2].numKeys, s, info);
			if ((xyzRotations[i2].numKeys != 0)) {
				Nif.NifStream(out xyzRotations[i2].interpolation, s, info);
			}
			xyzRotations[i2].keys = new Key[xyzRotations[i2].numKeys];
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				Nif.NifStream(out xyzRotations[i2].keys[i3], s, info, xyzRotations[i2].interpolation);
			}
		}
	}
	Nif.NifStream(out translations.numKeys, s, info);
	if ((translations.numKeys != 0)) {
		Nif.NifStream(out translations.interpolation, s, info);
	}
	translations.keys = new Key[translations.numKeys];
	for (var i1 = 0; i1 < translations.keys.Count; i1++) {
		Nif.NifStream(out translations.keys[i1], s, info, translations.interpolation);
	}
	Nif.NifStream(out scales.numKeys, s, info);
	if ((scales.numKeys != 0)) {
		Nif.NifStream(out scales.interpolation, s, info);
	}
	scales.keys = new Key[scales.numKeys];
	for (var i1 = 0; i1 < scales.keys.Count; i1++) {
		Nif.NifStream(out scales.keys[i1], s, info, scales.interpolation);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(numRotationKeys, s, info);
	if ((numRotationKeys != 0)) {
		Nif.NifStream(rotationType, s, info);
	}
	if ((rotationType != 4)) {
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			Nif.NifStream(quaternionKeys[i2], s, info, rotationType);
		}
	}
	if (info.version <= 0x0A010000) {
		if ((rotationType == 4)) {
			Nif.NifStream(order, s, info);
		}
	}
	if ((rotationType == 4)) {
		for (var i2 = 0; i2 < 3; i2++) {
			xyzRotations[i2].numKeys = (uint)xyzRotations[i2].keys.Count;
			Nif.NifStream(xyzRotations[i2].numKeys, s, info);
			if ((xyzRotations[i2].numKeys != 0)) {
				Nif.NifStream(xyzRotations[i2].interpolation, s, info);
			}
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				Nif.NifStream(xyzRotations[i2].keys[i3], s, info, xyzRotations[i2].interpolation);
			}
		}
	}
	translations.numKeys = (uint)translations.keys.Count;
	Nif.NifStream(translations.numKeys, s, info);
	if ((translations.numKeys != 0)) {
		Nif.NifStream(translations.interpolation, s, info);
	}
	for (var i1 = 0; i1 < translations.keys.Count; i1++) {
		Nif.NifStream(translations.keys[i1], s, info, translations.interpolation);
	}
	scales.numKeys = (uint)scales.keys.Count;
	Nif.NifStream(scales.numKeys, s, info);
	if ((scales.numKeys != 0)) {
		Nif.NifStream(scales.interpolation, s, info);
	}
	for (var i1 = 0; i1 < scales.keys.Count; i1++) {
		Nif.NifStream(scales.keys[i1], s, info, scales.interpolation);
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
	s.AppendLine($"  Num Rotation Keys:  {numRotationKeys}");
	if ((numRotationKeys != 0)) {
		s.AppendLine($"    Rotation Type:  {rotationType}");
	}
	if ((rotationType != 4)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Quaternion Keys[{i2}]:  {quaternionKeys[i2]}");
			array_output_count++;
		}
	}
	if ((rotationType == 4)) {
		s.AppendLine($"    Order:  {order}");
		array_output_count = 0;
		for (var i2 = 0; i2 < 3; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			xyzRotations[i2].numKeys = (uint)xyzRotations[i2].keys.Count;
			s.AppendLine($"      Num Keys:  {xyzRotations[i2].numKeys}");
			if ((xyzRotations[i2].numKeys != 0)) {
				s.AppendLine($"        Interpolation:  {xyzRotations[i2].interpolation}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Keys[{i3}]:  {xyzRotations[i2].keys[i3]}");
				array_output_count++;
			}
		}
	}
	translations.numKeys = (uint)translations.keys.Count;
	s.AppendLine($"  Num Keys:  {translations.numKeys}");
	if ((translations.numKeys != 0)) {
		s.AppendLine($"    Interpolation:  {translations.interpolation}");
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < translations.keys.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Keys[{i1}]:  {translations.keys[i1]}");
		array_output_count++;
	}
	scales.numKeys = (uint)scales.keys.Count;
	s.AppendLine($"  Num Keys:  {scales.numKeys}");
	if ((scales.numKeys != 0)) {
		s.AppendLine($"    Interpolation:  {scales.interpolation}");
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < scales.keys.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Keys[{i1}]:  {scales.keys[i1]}");
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
            Key.NormalizeKeyVector(translations.keys, phase, frequency);
            Key.NormalizeKeyVector(scales.keys, phase, frequency);
            Key.NormalizeKeyVector(quaternionKeys, phase, frequency);
            Key.NormalizeKeyVector(xyzRotations[0].keys, phase, frequency);
            Key.NormalizeKeyVector(xyzRotations[1].keys, phase, frequency);
            Key.NormalizeKeyVector(xyzRotations[2].keys, phase, frequency);
        }

        //--Rotate--//

        /*! Gets or sets the type of rotation interpolation being used.  Does not affect existing key data.
         * \param t The new rotation key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetRotateType
         */
        public KeyType RotateType
        {
            get => rotationType;
            set
            {
                rotationType = value;
                UpdateRotationKeyCount();
            }
        }

        /*! Get or sets the quaternion rotation key data.
         * \param keys A vector containing new Key<Quaternion> data which will replace any existing data.
         * \sa IKeyframeData::GetQuatRotateKeys, Key
         */
        public IList<Key<Quaternion>> QuatRotateKeys
        {
            get => quaternionKeys;
            set
            {
                quaternionKeys = value;
                UpdateRotationKeyCount();
            }
        }

        protected void UpdateRotationKeyCount() => numRotationKeys = rotationType == Niflib.KeyType.XYZ_ROTATION_KEY ? 1 : (uint)quaternionKeys.Count;

        /*! Gets or sets the type of x-rotation interpolation being used.  Does not affect existing key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEY.
         * \param t The new x-rotation key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetXRotateType
         */
        public KeyType XRotateType
        {
            get => xyzRotations[0].interpolation;
            set => xyzRotations[0].interpolation = value;
        }

        /*! Gets or sets the x-rotation key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEY.
         * \param keys A vector containing new Key<float> data which will replace any existing data.
         * \sa IKeyframeData::GetXRotateKeys, Key
         */
        public IList<Key<float>> XRotateKeys
        {
            get => xyzRotations[0].keys;
            set => xyzRotations[0].keys = value;
        }

        /*! Gets or sets the type of y-rotation interpolation being used.  Does not affect existing key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEY.
         * \param t The new y-rotation key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetYRotateType
         */
        public KeyType YRotateType
        {
            get => xyzRotations[1].interpolation;
            set => xyzRotations[1].interpolation = value;
        }

        /*! Gets or sets the y-rotation key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEY.
         * \param keys A vector containing new Key<float> data which will replace any existing data.
         * \sa IKeyframeData::GetXRotateKeys, Key
         */
        public IList<Key<float>> YRotateKeys
        {
            get => xyzRotations[1].keys;
            set => xyzRotations[1].keys = value;
        }

        /*! Gets or sets the type of z-rotation interpolation being used.  Does not affect existing key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEZ.
         * \param t The new z-rotation key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetZRotateType
         */
        public KeyType ZRotateType
        {
            get => xyzRotations[2].interpolation;
            set => xyzRotations[2].interpolation = value;
        }

        /*! Gets or sets the z-rotation key data.  XYZ data is only used if rotation type is set to XYZ_ROTATION_KEZ.
         * \param keys A vector containing new Key<float> data which will replace any existing data.
         * \sa IKeyframeData::GetXRotateKeys, Key
         */
        public IList<Key<float>> ZRotateKeys
        {
            get => xyzRotations[2].keys;
            set => xyzRotations[2].keys = value;
        }

        //--Translate--//

        /*! Gets or sets the type of translation interpolation being used.  Does not affect existing key data.
         * \param t The new translation key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetTranslateType
         */
        public KeyType TranslateType
        {
            get => translations.interpolation;
            set => translations.interpolation = value;
        }

        /*! Gets or sets the translation key data.
         * \param keys A vector containing new Key<Vector3> data which will replace any existing data.
         * \sa IKeyframeData::GetTranslateKeys, Key
         */
        public IList<Key<Vector3>> TranslateKeys
       {
            get => translations.keys;
            set => translations.keys = value;
        }

        //--Scale--//

        /*! Gets or sets the type of scale interpolation being used.  Does not affect existing key data.
         * \param t The new scale key type specifing the type of interpolation to be used.
         * \sa IKeyframeData::GetScaleType
         */
        public KeyType ScaleType
        {
            get => scales.interpolation;
            set => scales.interpolation = value;
        }

        /*! Gets or sets the scale key data.
         * \param keys A vector containing new Key<float> data which will replace any existing data.
         * \sa IKeyframeData::GetScaleKeys, Key
         */
        public IList<Key<float>> ScaleKeys
        {
            get => scales.keys;
            set => scales.keys = value;
        }
//--END:CUSTOM--//

}

}