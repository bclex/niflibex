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

/*! A capsule. */
public class bhkCapsuleShape : bhkConvexShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkCapsuleShape", bhkConvexShape.TYPE);
	/*! Not used. The following wants to be aligned at 16 bytes. */
	Array8<byte> unused;
	/*! First point on the capsule's axis. */
	Vector3 firstPoint;
	/*! Matches first capsule radius. */
	float radius1;
	/*! Second point on the capsule's axis. */
	Vector3 secondPoint;
	/*! Matches second capsule radius. */
	float radius2;

	public bhkCapsuleShape() {
	radius1 = 0.0f;
	radius2 = 0.0f;
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
public static NiObject Create() => new bhkCapsuleShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused[i1], s, info);
	}
	Nif.NifStream(out firstPoint, s, info);
	Nif.NifStream(out radius1, s, info);
	Nif.NifStream(out secondPoint, s, info);
	Nif.NifStream(out radius2, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused[i1], s, info);
	}
	Nif.NifStream(firstPoint, s, info);
	Nif.NifStream(radius1, s, info);
	Nif.NifStream(secondPoint, s, info);
	Nif.NifStream(radius2, s, info);

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
	array_output_count = 0;
	for (var i1 = 0; i1 < 8; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused[{i1}]:  {unused[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  First Point:  {firstPoint}");
	s.AppendLine($"  Radius 1:  {radius1}");
	s.AppendLine($"  Second Point:  {secondPoint}");
	s.AppendLine($"  Radius 2:  {radius2}");
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
         * Gets or sets the capsule's radius.
         * \param[in] value The new radius for the capsule.
         */
        public float Radius
        {
            get => radius;
            set => radius = value;
        }

        /*!
         * Gets or sets the first point on the capsule's axis.
         * \return The new first point on the capsule's axis.
         */
        public Vector3 FirstPoint
        {
            get => firstPoint;
            set => firstPoint = value;
        }

        /*!
         * Gets or sets the second capsule radius.  Seems to match the first capsule radius.
         * \param[in] value The new second capsule radius.
         */
        public float Radius1
        {
            get => radius1;
            set => radius1 = value;
        }

        /*!
         * Gets or sets the second point on the capsule's axis.
         * \return The new second point on the capsule's axis.
         */
        public Vector3 SecondPoint
        {
            get => secondPoint;
            set => secondPoint = value;
        }

        /*!
         * Gets or sets the third capsule radius.  Seems to match the second capsule radius.
         * \param[in] value The new third capsule radius.
         */
        public float Radius2
        {
            get => radius2;
            set => radius2 = value;
        }

        /*! Helper routine for calculating mass properties.
        *  \param[in]  density Uniform density of object
        *  \param[in]  solid Determines whether the object is assumed to be solid or not
        *  \param[out] mass Calculated mass of the object
        *  \param[out] center Center of mass
        *  \param[out] inertia Mass Inertia Tensor
        *  \return Return mass, center, and inertia tensor.
        */
        public virtual void CalcMassProperties(float density, bool solid, out float mass, out float volume, out Vector3 center, out InertiaMatrix inertia) => Inertia.CalcMassPropertiesCapsule(firstPoint, secondPoint, radius, density, solid, mass, volume, center, inertia);
//--END:CUSTOM--//

}

}