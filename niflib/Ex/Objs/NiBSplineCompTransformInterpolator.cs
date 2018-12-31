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
 * NiBSplineTransformInterpolator plus the information required for using compact
 * control points.
 */
public class NiBSplineCompTransformInterpolator : NiBSplineTransformInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineCompTransformInterpolator", NiBSplineTransformInterpolator.TYPE);
	/*!  */
	internal float translationOffset;
	/*!  */
	internal float translationHalfRange;
	/*!  */
	internal float rotationOffset;
	/*!  */
	internal float rotationHalfRange;
	/*!  */
	internal float scaleOffset;
	/*!  */
	internal float scaleHalfRange;

	public NiBSplineCompTransformInterpolator() {
	translationOffset = 3.402823466e+38f;
	translationHalfRange = 3.402823466e+38f;
	rotationOffset = 3.402823466e+38f;
	rotationHalfRange = 3.402823466e+38f;
	scaleOffset = 3.402823466e+38f;
	scaleHalfRange = 3.402823466e+38f;
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
public static NiObject Create() => new NiBSplineCompTransformInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out translationOffset, s, info);
	Nif.NifStream(out translationHalfRange, s, info);
	Nif.NifStream(out rotationOffset, s, info);
	Nif.NifStream(out rotationHalfRange, s, info);
	Nif.NifStream(out scaleOffset, s, info);
	Nif.NifStream(out scaleHalfRange, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(translationOffset, s, info);
	Nif.NifStream(translationHalfRange, s, info);
	Nif.NifStream(rotationOffset, s, info);
	Nif.NifStream(rotationHalfRange, s, info);
	Nif.NifStream(scaleOffset, s, info);
	Nif.NifStream(scaleHalfRange, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Translation Offset:  {translationOffset}");
	s.AppendLine($"  Translation Half Range:  {translationHalfRange}");
	s.AppendLine($"  Rotation Offset:  {rotationOffset}");
	s.AppendLine($"  Rotation Half Range:  {rotationHalfRange}");
	s.AppendLine($"  Scale Offset:  {scaleOffset}");
	s.AppendLine($"  Scale Half Range:  {scaleHalfRange}");
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
         * Gets or sets translate bias.
         * \param[in] value The new translate bias.
         */
        public float TranslateBias
        {
            get => ;
            set => ;
        }

        /*!
         * Gets or sets translate multiplier.
         * \param[in] value The new translate bias.
         */
        public float TranslateMultiplier
        {
            get => ;
            set => ;
        }

        /*!
         * Gets or sets rotation bias.
         * \param[in] value The new rotation bias.
         */
        public float RotationBias
        {
            get => ;
            set => ;
        }

        /*!
         * Gets or sets rotation multiplier.
         * \param[in] value The new translate bias.
         */
        public float RotationMultiplier
        {
            get => ;
            set => ;
        }

        /*!
         * Gets or sets scale bias.
         * \param[in] value The new scale bias.
         */
        public float ScaleBias
        {
            get => ;
            set => ;
        }
        
        /*!
         * Gets or sets scale multiplier.
         * \param[in] value The new scale multiplier.
         */
        public float ScaleMultiplier
        {
            get => ;
            set => ;
        }

        /*!
         * Retrieves the control quaternion rotation data.
         * \return A vector containing control Quaternion data which specify rotation over time.
         */
        NIFLIB_API vector<Quaternion> GetQuatRotateControlData() const;

        /*!
         * Retrieves the control translation data.
         * \return A vector containing control Vector3 data which specify translation over time.
         */
        NIFLIB_API vector<Vector3> GetTranslateControlData() const;

        /*!
         * Retrieves the scale key data.
         * \return A vector containing control float data which specify scale over time.
         */
        NIFLIB_API vector< float > GetScaleControlData() const;

        /*!
         * Retrieves the sampled quaternion rotation key data between start and stop time.
         * \param npoints The number of data points to sample between start and stop time.
         * \param degree N-th order degree of polynomial used to fit the data.
         * \return A vector containing Key<Quaternion> data which specify rotation over time.
         */
        NIFLIB_API vector<Key<Quaternion> > SampleQuatRotateKeys(int npoints, int degree) const;

        /*!
         * Retrieves the sampled scale key data between start and stop time.
         * \param npoints The number of data points to sample between start and stop time.
         * \param degree N-th order degree of polynomial used to fit the data.
         * \return A vector containing Key<Vector3> data which specify translation over time.
         */
        NIFLIB_API vector<Key<Vector3> > SampleTranslateKeys(int npoints, int degree) const;

        /*!
         * Retrieves the sampled scale key data between start and stop time.
         * \param npoints The number of data points to sample between start and stop time.
         * \param degree N-th order degree of polynomial used to fit the data.
         * \return A vector containing Key<float> data which specify scale over time.
         */
        NIFLIB_API vector<Key<float> > SampleScaleKeys(int npoints, int degree) const;

        /*!
         * Retrieves the number of control points used in the spline curve.
         * \return The number of control points used in the spline curve.
         */
        public int NumControlPoints => ;
#endif
//--END:CUSTOM--//

}

}