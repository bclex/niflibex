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

/*! A sphere. */
public class bhkSphereShape : bhkConvexShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkSphereShape", bhkConvexShape.TYPE);

	public bhkSphereShape() {
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
	public static NiObject Create() => new bhkSphereShape();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);

	}

	/*!
	 * Summarizes the information contained in this object in English.
	 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
	 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
	 */
	public override string AsString(bool verbose = false) {

		var s = new System.Text.StringBuilder();
		s.Append(base.AsString());
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
         * Gets or sets a new value for the radius of the sphere.
         * \param[in] value The new radius of the sphere.
         */
        public float Radius
        {
            get => radius;
            set => radius = value;
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
            center = new Vector3(0, 0, 0);
            mass = 0.0f;  volume = 0.0f;
            inertia = InertiaMatrix.IDENTITY;
            Inertia.CalcMassPropertiesSphere(radius, density, solid, out mass, out volume, out center, out inertia);
        }

	//--END:CUSTOM--//

}

}