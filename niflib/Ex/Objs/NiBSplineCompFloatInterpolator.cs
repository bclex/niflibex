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
 * NiBSplineFloatInterpolator plus the information required for using compact
 * control points.
 */
public class NiBSplineCompFloatInterpolator : NiBSplineFloatInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineCompFloatInterpolator", NiBSplineFloatInterpolator.TYPE);
	/*!  */
	internal float floatOffset;
	/*!  */
	internal float floatHalfRange;

	public NiBSplineCompFloatInterpolator() {
	floatOffset = 3.402823466e+38f;
	floatHalfRange = 3.402823466e+38f;
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
public static NiObject Create() => new NiBSplineCompFloatInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out floatOffset, s, info);
	Nif.NifStream(out floatHalfRange, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(floatOffset, s, info);
	Nif.NifStream(floatHalfRange, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Float Offset:  {floatOffset}");
	s.AppendLine($"  Float Half Range:  {floatHalfRange}");
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
        * Gets or sets the base value when a curve is not defined.
        * \param[in] value The new base value.
        */
        public float Base
        {
            get => base;
            set => base = value;
        }

        /*!
        * Gets or sets value bias.
        * \param[in] value The new value bias.
        */
        public float Bias
        {
            get => bias;
            set => bias = value;
        }

        /*!
        * Gets or sets value multiplier.
        * \param[in] value The new value multiplier.
        */
        public float Multiplier
        {
            get => multiplier;
            set => multiplier = value;
        }

        /*!
        * Retrieves the key data.
        * \return A vector containing control float data which specify a value over time.
        */
        public IList<float> GetControlData()
        {
	vector< float > value;
	if ((offset != USHRT_MAX) && splineData && basisData) { // has translation data
		int nctrl = basisData->GetNumControlPoints();
		int npts = nctrl * SizeofValue;
		vector<short> points = splineData->GetShortControlPointRange(offset, npts);
		value.reserve(nctrl);
		for (int i=0; i<npts; ) {
			float data = float(points[i++]) / float (32767) * multiplier + bias;
			value.push_back(data);
		}
	}
	return value;
        }

        /*!
        * Retrieves the sampled data between start and stop time.
        * \param npoints The number of data points to sample between start and stop time.
        * \param degree N-th order degree of polynomial used to fit the data.
        * \return A vector containing Key<float> data which specify a value over time.
        */
        public IList<Key<float>> SampleKeys(int npoints, int degree)
        {
	vector< Key<float> > value;
	if ((offset != USHRT_MAX) && splineData && basisData) // has rotation data
	{
		int nctrl = basisData->GetNumControlPoints();
		int npts = nctrl * SizeofValue;
		vector<short> points = splineData->GetShortControlPointRange(offset, npts);
		vector<float> control(npts);
		vector<float> output(npoints*SizeofValue);
		for (int i=0, j=0; i<nctrl; ++i) {
			control[i] = float(points[j++]) / float (32767);
		}
		// fit data
		bspline(nctrl-1, degree+1, SizeofValue, &control[0], &output[0], npoints);

		// copy to key
		float time = GetStartTime();
		float incr = (GetStopTime() - GetStartTime()) / float(npoints) ;
		value.reserve(npoints);
		for (int i=0, j=0; i<npoints; i++) {
			Key<float> key;
			key.time = time;
			key.backward_tangent = 0.0f;
			key.forward_tangent = 0.0f; 
			key.data = output[j++] * multiplier + bias;
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
        public int NumControlPoints => basisData != null ? basisData.NumControlPoints : 0;
#endif
//--END:CUSTOM--//

}

}