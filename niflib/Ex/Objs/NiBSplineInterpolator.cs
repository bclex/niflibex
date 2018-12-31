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

/*! Abstract base class for interpolators storing data via a B-spline. */
public class NiBSplineInterpolator : NiInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineInterpolator", NiInterpolator.TYPE);
	/*! Animation start time. */
	internal float startTime;
	/*! Animation stop time. */
	internal float stopTime;
	/*!  */
	internal NiBSplineData splineData;
	/*!  */
	internal NiBSplineBasisData basisData;

	public NiBSplineInterpolator() {
	startTime = 3.402823466e+38f;
	stopTime = -3.402823466e+38f;
	splineData = null;
	basisData = null;
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
public static NiObject Create() => new NiBSplineInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out startTime, s, info);
	Nif.NifStream(out stopTime, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(startTime, s, info);
	Nif.NifStream(stopTime, s, info);
	WriteRef((NiObject)splineData, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)basisData, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Start Time:  {startTime}");
	s.AppendLine($"  Stop Time:  {stopTime}");
	s.AppendLine($"  Spline Data:  {splineData}");
	s.AppendLine($"  Basis Data:  {basisData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	splineData = FixLink<NiBSplineData>(objects, link_stack, missing_link_stack, info);
	basisData = FixLink<NiBSplineBasisData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (splineData != null)
		refs.Add((NiObject)splineData);
	if (basisData != null)
		refs.Add((NiObject)basisData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the animation start time.
         * \param[in] value The new animation start time
         */
        public float StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        /*!
         * Gets or sets the animation stop time.
         * \param[in] value The new animation stop time
         */
        public float StopTime
        {
            get => stopTime;
            set => stopTime = value;
        }

        /*!
         * Gets or sets the NiBSplineData used by this interpolator.
         * \param[in] value The NiBSplineData used by this interpolator.
         */
        public NiBSplineData SplineData
        {
            get => splineData;
            set => splineData = value;
        }

        /*!
         * Getsor sets the SetBasisData used by this interpolator.
         * \param[in] value The SetBasisData used by this interpolator.
         */
        public NiBSplineBasisData BasisData
        {
            get => basisData;
            set => basisData = value;
        }

        // internal method for bspline calculation in child classes
#if false
        //protected static void bspline(int n, int t, int l, out float control, out float output, int num_output);
        /*********************************************************************
        Simple b-spline curve algorithm

        Copyright 1994 by Keith Vertanen (vertankd@cda.mrs.umn.edu)

        Released to the public domain (your mileage may vary)

        Found at: Programmers Heaven (www.programmersheaven.com/zone3/cat415/6660.htm)
        Modified by: Theo 
        - reformat and convert doubles to floats
        - removed point structure in favor of arbitrary sized float array
        **********************************************************************/
        static void copy_floats(float* dest, float* src, int l)
        {
            for (int i = 0; i < l; ++i)
                dest[i] = src[i];
        }

        // calculate the blending value
        static float blend(int k, int t, int* u, float v)
        {
            float value;
            if (t == 1)
            {           // base case for the recursion
                value = ((u[k] <= v) && (v < u[k + 1])) ? 1.0f : 0.0f;
            }
            else
            {
                if ((u[k + t - 1] == u[k]) && (u[k + t] == u[k + 1]))  // check for divide by zero
                    value = 0;
                else if (u[k + t - 1] == u[k]) // if a term's denominator is zero,use just the other
                    value = (u[k + t] - v) / (u[k + t] - u[k + 1]) * blend(k + 1, t - 1, u, v);
                else if (u[k + t] == u[k + 1])
                    value = (v - u[k]) / (u[k + t - 1] - u[k]) * blend(k, t - 1, u, v);
                else
                    value = (v - u[k]) / (u[k + t - 1] - u[k]) * blend(k, t - 1, u, v) +
                    (u[k + t] - v) / (u[k + t] - u[k + 1]) * blend(k + 1, t - 1, u, v);
            }
            return value;
        }

        // figure out the knots
        static void compute_intervals(int* u, int n, int t)
        {
            for (int j = 0; j <= n + t; j++)
            {
                if (j < t)
                    u[j] = 0;
                else if ((t <= j) && (j <= n))
                    u[j] = j - t + 1;
                else if (j > n)
                    u[j] = n - t + 2;  // if n-t=-2 then we're screwed, everything goes to 0
            }
        }

        static void compute_point(int* u, int n, int t, float v, int l, float* control, float* output)
        {
            // initialize the variables that will hold our output
            for (int j = 0; j < l; j++)
                output[j] = 0;
            for (int k = 0; k <= n; k++)
            {
                float temp = blend(k, t, u, v);  // same blend is used for each dimension coordinate
                for (int j = 0; j < l; j++)
                    output[j] = output[j] + control[k * l + j] * temp;
            }
        }

        /*********************************************************************
        bspline(int n, int t, int l, float *control, float *output, int num_output)

        Parameters:
        n          - the number of control points minus 1
        t          - the degree of the polynomial plus 1
        l          - size of control and output float vector block
        control    - control point array made up of float structure
        output     - array in which the calculate spline points are to be put
        num_output - how many points on the spline are to be calculated

        Pre-conditions:
        n+2>t  (no curve results if n+2<=t)
        control array contains the number of points specified by n
        output array is the proper size to hold num_output point structures

        control and output vectors must be contiguous float arrays

        **********************************************************************/
        internal void bspline(int n, int t, int l, out float control, out float output, int num_output)
        {
            float* calc = new float[l];
            int* u = new int[n + t + 1];
            compute_intervals(u, n, t);

            float increment = (float)(n - t + 2) / (num_output - 1);  // how much parameter goes up each time
            float interval = 0;
            for (int output_index = 0; output_index < num_output - 1; output_index++)
            {
                compute_point(u, n, t, interval, l, control, calc);
                copy_floats(&output[output_index * l], calc, l);
                interval = interval + increment;  // increment our parameter
            }
            copy_floats(&output[(num_output - 1) * l], &control[n * l], l); // put in the last points
            delete[] u;
            delete[] calc;
        }
#endif
//--END:CUSTOM--//

}

}