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
 * Supports the animation of position, rotation, and scale using an
 * NiQuatTransform.
 *         The NiQuatTransform can be an unchanging pose or interpolated from
 * B-Spline control point channels.
 */
public class NiBSplineTransformInterpolator : NiBSplineInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineTransformInterpolator", NiBSplineInterpolator.TYPE);
	/*!  */
	internal NiQuatTransform transform;
	/*! Handle into the translation data. (USHRT_MAX for invalid handle.) */
	internal uint translationHandle;
	/*! Handle into the rotation data. (USHRT_MAX for invalid handle.) */
	internal uint rotationHandle;
	/*! Handle into the scale data. (USHRT_MAX for invalid handle.) */
	internal uint scaleHandle;

	public NiBSplineTransformInterpolator() {
	translationHandle = (uint)0xFFFF;
	rotationHandle = (uint)0xFFFF;
	scaleHandle = (uint)0xFFFF;
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
public static NiObject Create() => new NiBSplineTransformInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out transform.translation, s, info);
	Nif.NifStream(out transform.rotation, s, info);
	Nif.NifStream(out transform.scale, s, info);
	if (info.version <= 0x0A01006D) {
		for (var i2 = 0; i2 < 3; i2++) {
			{
				bool tmp;
				Nif.NifStream(out tmp, s, info);
				transform.trsValid[i2] = tmp;
			}
		}
	}
	Nif.NifStream(out translationHandle, s, info);
	Nif.NifStream(out rotationHandle, s, info);
	Nif.NifStream(out scaleHandle, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(transform.translation, s, info);
	Nif.NifStream(transform.rotation, s, info);
	Nif.NifStream(transform.scale, s, info);
	if (info.version <= 0x0A01006D) {
		for (var i2 = 0; i2 < 3; i2++) {
			{
				bool tmp = transform.trsValid[i2];
				Nif.NifStream(tmp, s, info);
			}
		}
	}
	Nif.NifStream(translationHandle, s, info);
	Nif.NifStream(rotationHandle, s, info);
	Nif.NifStream(scaleHandle, s, info);

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
	s.AppendLine($"  Translation:  {transform.translation}");
	s.AppendLine($"  Rotation:  {transform.rotation}");
	s.AppendLine($"  Scale:  {transform.scale}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 3; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    TRS Valid[{i1}]:  {transform.trsValid[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Translation Handle:  {translationHandle}");
	s.AppendLine($"  Rotation Handle:  {rotationHandle}");
	s.AppendLine($"  Scale Handle:  {scaleHandle}");
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
#if false
        /*!
        * Gets or sets the base translation when a translate curve is not defined.
        * \param[in] value The new base translation.
        */
        public Vector3 Translation
        {
            get => transform.translation;
            set => transform.translation = value;
        }

        /*!
        * Gets or sets the translation offset for the control points in the NiSplineData
        * \param[in] The new translation offset
        */
        public int TranslationOffset
        {
            get => translationOffset;
            set => translationOffset = value;
        }

        /*!
        * Gets or sets the base rotation when a translate curve is not defined.
        * \param[in] value The new base rotation.
        */
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        /*!
        * Gets or sets the rotation offset for the control points in the NiSplineData
        * \param[in] The new rotation offset
        */
        public int RotationOffset
        {
            get => rotationOffset;
            set => rotationOffset = value;
        }

        /*!
        * Gets or sets the base scale when a translate curve is not defined.
        * \param[in] value The new base scale.
        */
        public float Scale
        {
            get => transform.scale;
            set => transform.scale = value;
        }

        /*!
        * Gets or sets the scale offset for the control points in the NiSplineData
        * \param[in] The new scale offset
        */
        public int ScaleOffset
        {
            get => scaleOffset;
            set => scaleOffset = value;
        }


        /*!
        * Retrieves the control quaternion rotation data.
        * \return A vector containing control Quaternion data which specify rotation over time.
        */
        public virtual IList<Quaternion> GetQuatRotateControlData()
        {
            vector<Quaternion> value;
            if ((rotationOffset != USHRT_MAX) && splineData && basisData)
            { // has rotation data
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofQuat;
                vector<float> points = splineData->GetFloatControlPointRange(rotationOffset, npts);
                value.reserve(nctrl);
                for (int i = 0; i < npts;)
                {
                    Quaternion key;
                    key.w = float(points[i++]);
                    key.x = float(points[i++]);
                    key.y = float(points[i++]);
                    key.z = float(points[i++]);
                    value.push_back(key);
                }
            }
            return value;
        }

        /*!
        * Retrieves the control translation data.
        * \return A vector containing control Vector3 data which specify translation over time.
        */
        public virtual IList<Vector3> GetTranslateControlData()
        {
            vector<Vector3> value;
            if ((translationOffset != USHRT_MAX) && splineData && basisData)
            { // has translation data
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofTrans;
                vector<float> points = splineData->GetFloatControlPointRange(translationOffset, npts);
                value.reserve(nctrl);
                for (int i = 0; i < npts;)
                {
                    Vector3 key;
                    key.x = float(points[i++]);
                    key.y = float(points[i++]);
                    key.z = float(points[i++]);
                    value.push_back(key);
                }
            }
            return value;
        }

        /*!
        * Retrieves the scale key data.
        * \return A vector containing control float data which specify scale over time.
        */
        public virtual IList<float> GetScaleControlData()
        {
            vector<float> value;
            if ((scaleOffset != USHRT_MAX) && splineData && basisData)
            { // has translation data
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofScale;
                vector<float> points = splineData->GetFloatControlPointRange(scaleOffset, npts);
                value.reserve(nctrl);
                for (int i = 0; i < npts;)
                {
                    float data = float(points[i++]);
                    value.push_back(data);
                }
            }
            return value;
        }

        /*!
        * Retrieves the sampled quaternion rotation key data between start and stop time.
        * \param npoints The number of data points to sample between start and stop time.
        * \param degree N-th order degree of polynomial used to fit the data.
        * \return A vector containing Key<Quaternion> data which specify rotation over time.
        */
        public virtual IList<Key<Quaternion>> SampleQuatRotateKeys(int npoints, int degree)
        {
            vector<Key<Quaternion>> value;
            if ((rotationOffset != USHRT_MAX) && splineData && basisData)
            { // has rotation data
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofQuat;
                vector<float> points = splineData->GetFloatControlPointRange(rotationOffset, npts);
                vector<float> control(npts);
                vector<float> output(npoints* SizeofQuat);
                for (int i = 0, j = 0; i < nctrl; ++i)
                {
                    for (int k = 0; k < SizeofQuat; ++k)
                        control[i * SizeofQuat + k] = float(points[j++]);
                }
                if (degree >= nctrl)
                    degree = nctrl - 1;
                // fit data
                bspline(nctrl - 1, degree + 1, SizeofQuat, &control[0], &output[0], npoints);

                // copy to key
                float time = GetStartTime();
                float incr = (GetStopTime() - GetStartTime()) / float(npoints);
                value.reserve(npoints);
                for (int i = 0, j = 0; i < npoints; i++)
                {
                    Key<Quaternion> key;
                    key.time = time;
                    key.backward_tangent.Set(1.0f, 0.0f, 0.0f, 0.0f);
                    key.forward_tangent.Set(1.0f, 0.0f, 0.0f, 0.0f);
                    key.data.w = output[j++];
                    key.data.x = output[j++];
                    key.data.y = output[j++];
                    key.data.z = output[j++];
                    value.push_back(key);
                    time += incr;
                }
            }
            return value;
        }

        /*!
        * Retrieves the sampled scale key data between start and stop time.
        * \param npoints The number of data points to sample between start and stop time.
        * \param degree N-th order degree of polynomial used to fit the data.
        * \return A vector containing Key<Vector3> data which specify translation over time.
        */
        public virtual IList<Key<Vector3>> SampleTranslateKeys(int npoints, int degree)
        {
            vector<Key<Vector3>> value;
            if ((translationOffset != USHRT_MAX) && splineData && basisData)
            { // has rotation data
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofTrans;
                vector<float> points = splineData->GetFloatControlPointRange(translationOffset, npts);
                vector<float> control(npts);
                vector<float> output(npoints* SizeofTrans);
                for (int i = 0, j = 0; i < nctrl; ++i)
                {
                    for (int k = 0; k < SizeofTrans; ++k)
                        control[i * SizeofTrans + k] = float(points[j++]);
                }
                // fit data
                bspline(nctrl - 1, degree + 1, SizeofTrans, &control[0], &output[0], npoints);

                // copy to key
                float time = GetStartTime();
                float incr = (GetStopTime() - GetStartTime()) / float(npoints);
                value.reserve(npoints);
                for (int i = 0, j = 0; i < npoints; i++)
                {
                    Key<Vector3> key;
                    key.time = time;
                    key.backward_tangent.Set(0.0f, 0.0f, 0.0f);
                    key.forward_tangent.Set(0.0f, 0.0f, 0.0f);
                    key.data.x = output[j++];
                    key.data.y = output[j++];
                    key.data.z = output[j++];
                    value.push_back(key);
                    time += incr;
                }
            }
            return value;
        }

        /*!
        * Retrieves the sampled scale key data between start and stop time.
        * \param npoints The number of data points to sample between start and stop time.
        * \param degree N-th order degree of polynomial used to fit the data.
        * \return A vector containing Key<float> data which specify scale over time.
        */
        public virtual IList<Key<float>> SampleScaleKeys(int npoints, int degree)
        {
            vector<Key<float>> value;
            if ((scaleOffset != USHRT_MAX) && splineData && basisData) // has rotation data
            {
                int nctrl = basisData->GetNumControlPoints();
                int npts = nctrl * SizeofScale;
                vector<float> points = splineData->GetFloatControlPointRange(scaleOffset, npts);
                vector<float> control(npts);
                vector<float> output(npoints* SizeofScale);
                for (int i = 0, j = 0; i < nctrl; ++i)
                {
                    control[i] = float(points[j++]) / float(32767);
                }
                // fit data
                bspline(nctrl - 1, degree + 1, SizeofScale, &control[0], &output[0], npoints);

                // copy to key
                float time = GetStartTime();
                float incr = (GetStopTime() - GetStartTime()) / float(npoints);
                value.reserve(npoints);
                for (int i = 0, j = 0; i < npoints; i++)
                {
                    Key<float> key;
                    key.time = time;
                    key.backward_tangent = 0.0f;
                    key.forward_tangent = 0.0f;
                    key.data = output[j++];
                    value.push_back(key);
                    time += incr;
                }
            }
            return value;
        }

        /*!
        * Retrieves the number of control points used in the spline curve.
        * \return The number of control points used in the spline curve.
        */
        public virtual uint NumControlPoints => basisData != null ? basisData.NumControlPoints : 0;
#endif
//--END:CUSTOM--//

}

}