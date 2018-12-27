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

/*! Unknown. */
public class bhkMultiSphereShape : bhkSphereRepShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkMultiSphereShape", bhkSphereRepShape.TYPE);
	/*! Unknown. */
	internal float unknownFloat1;
	/*! Unknown. */
	internal float unknownFloat2;
	/*! The number of spheres in this multi sphere shape. */
	internal uint numSpheres;
	/*! This array holds the spheres which make up the multi sphere shape. */
	internal NiBound[] spheres;

	public bhkMultiSphereShape() {
	unknownFloat1 = 0.0f;
	unknownFloat2 = 0.0f;
	numSpheres = (uint)0;
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
public static NiObject Create() => new bhkMultiSphereShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out unknownFloat1, s, info);
	Nif.NifStream(out unknownFloat2, s, info);
	Nif.NifStream(out numSpheres, s, info);
	spheres = new NiBound[numSpheres];
	for (var i1 = 0; i1 < spheres.Length; i1++) {
		Nif.NifStream(out spheres[i1].center, s, info);
		Nif.NifStream(out spheres[i1].radius, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSpheres = (uint)spheres.Length;
	Nif.NifStream(unknownFloat1, s, info);
	Nif.NifStream(unknownFloat2, s, info);
	Nif.NifStream(numSpheres, s, info);
	for (var i1 = 0; i1 < spheres.Length; i1++) {
		Nif.NifStream(spheres[i1].center, s, info);
		Nif.NifStream(spheres[i1].radius, s, info);
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
	numSpheres = (uint)spheres.Length;
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	s.AppendLine($"  Unknown Float 2:  {unknownFloat2}");
	s.AppendLine($"  Num Spheres:  {numSpheres}");
	array_output_count = 0;
	for (var i1 = 0; i1 < spheres.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Center:  {spheres[i1].center}");
		s.AppendLine($"    Radius:  {spheres[i1].radius}");
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
         * Gets or sets the spheres which make up the multi sphere shape.
         * \param[in] value The new spheres which will make up the multi sphere shape.
         */
        public NiBound[] Spheres
        {
            get => spheres;
            set
            {
                numSpheres = (uint)value.Length;
                spheres = value;
            }
        }

        /*! Helper routine for calculating mass properties.
         *  \param[in]  density Uniform density of object
         *  \param[in]  solid Determines whether the object is assumed to be solid or not
         *  \param[out] mass Calculated mass of the object
         *  \param[out] center Center of mass
         *  \param[out] inertia Mass Inertia Tensor
         *  \return Return mass, center, and inertia tensor.
         */
        public virtual void CalcMassProperties(float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia)
        {
            // TODO: Calculate this properly
            center = new Vector3(0, 0, 0);
            mass = 0.0f; volume = 0.0f;
            inertia = InertiaMatrix.IDENTITY;
        }
//--END:CUSTOM--//

}

}