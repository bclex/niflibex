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

/*! Memory optimized partial polytope bounding volume tree shape (not an entity). */
public class bhkMoppBvTreeShape : bhkBvTreeShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkMoppBvTreeShape", bhkBvTreeShape.TYPE);
	/*! The shape. */
	internal bhkShape shape;
	/*! Garbage data from memory. Referred to as User Data, Shape Collection, and Code. */
	internal Array3<uint> unused;
	/*! Scale. */
	internal float shapeScale;
	/*! Number of bytes for MOPP data. */
	internal uint moppDataSize;
	/*!
	 * Origin of the object in mopp coordinates. This is the minimum of all vertices in
	 * the packed shape along each axis, minus 0.1.
	 */
	internal Vector3 origin;
	/*!
	 * The scaling factor to quantize the MOPP: the quantization factor is equal to
	 * 256*256 divided by this number. In Oblivion files, scale is taken equal to
	 * 256*256*254 / (size + 0.2) where size is the largest dimension of the bounding
	 * box of the packed shape.
	 */
	internal float scale;
	/*! Tells if MOPP Data was organized into smaller chunks (PS3) or not (PC) */
	internal MoppDataBuildType buildType;
	/*! The tree of bounding volume data. */
	internal IList<byte> moppData;

	public bhkMoppBvTreeShape() {
	shape = null;
	shapeScale = 1.0f;
	moppDataSize = (uint)0;
	scale = 0.0f;
	buildType = (MoppDataBuildType)0;
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
public static NiObject Create() => new bhkMoppBvTreeShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	for (var i1 = 0; i1 < 3; i1++) {
		Nif.NifStream(out unused[i1], s, info);
	}
	Nif.NifStream(out shapeScale, s, info);
	Nif.NifStream(out moppDataSize, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out origin, s, info);
		Nif.NifStream(out scale, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out buildType, s, info);
	}
	moppData = new byte[moppDataSize];
	for (var i1 = 0; i1 < moppData.Count; i1++) {
		Nif.NifStream(out moppData[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	moppDataSize = moppDataSizeCalc(info);
	WriteRef((NiObject)shape, s, info, link_map, missing_link_stack);
	for (var i1 = 0; i1 < 3; i1++) {
		Nif.NifStream(unused[i1], s, info);
	}
	Nif.NifStream(shapeScale, s, info);
	Nif.NifStream(moppDataSize, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(origin, s, info);
		Nif.NifStream(scale, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(buildType, s, info);
	}
	for (var i1 = 0; i1 < moppData.Count; i1++) {
		Nif.NifStream(moppData[i1], s, info);
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
	s.AppendLine($"  Shape:  {shape}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 3; i1++) {
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
	s.AppendLine($"  Shape Scale:  {shapeScale}");
	s.AppendLine($"  MOPP Data Size:  {moppDataSize}");
	s.AppendLine($"  Origin:  {origin}");
	s.AppendLine($"  Scale:  {scale}");
	s.AppendLine($"  Build Type:  {buildType}");
	array_output_count = 0;
	for (var i1 = 0; i1 < moppData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    MOPP Data[{i1}]:  {moppData[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	shape = FixLink<bhkShape>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (shape != null)
		refs.Add((NiObject)shape);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the shape object that this body will use.
         * \param[in] value The new shape object for this body to use.
         */
        public bhkShape Shape
        {
            get => shape;
            set => shape = value;
        }

        /*!
         * Gets or Sets the shape's material.  This determines the type of noises the object makes as it collides in Oblivion.
         * \param[in] value The new material for this shape to use.
         */
        //public HavokMaterial Material
        //{
        //    get => material;
        //    set => material = value;
        //}

        /*!
        * Gets or sets the shape's bounding volume code.  The code is specific to the Havok Physics engine.
        * \param[in] value A byte vector containing the code representing the MOPP.
        */
        public IList<byte> MoppCode
        {
            get => moppData;
            set
            {
                moppDataSize = (uint)value.Count;
                moppData = value;
            }
        }

        /*!
        * Gets or sets the origin for the shape's mopp code in mopp coordinates. This is the minimum of all vertices in
        * the packed shape along each axis, minus 0.1.
        * \param[in] value The origin in mopp coordinates.
        */
        public Vector3 MoppOrigin
        {
            get => origin;
            set => origin = value;
        }

        /*!
        * Sets the scale for the shape's mopp code in mopp coordinates. 
        *   The scaling factor to quantize the MOPP: the quantization factor is equal to
        *   256*256 divided by this number. In Oblivion files, scale is taken equal to
        *   256*256*254 / (size + 0.2) where size is the largest dimension of the bounding
        *   box of the packed shape.	
        * \param[in] value The scale in mopp coordinates.
        */
        public float MoppScale
        {
            get => scale;
            set => scale = value;
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
            mass = 0.0f; volume = 0.0f;
            inertia = InertiaMatrix.IDENTITY;
            if (shape != null)
                shape.CalcMassProperties(density, solid, out mass, out volume, out center, out inertia);
        }

        public MoppDataBuildType BuildType
        {
            get => buildType;
            set => buildType = value;
        }

        //uint moppDataSizeCalc(NifInfo info) => info.version <= 0x0A000100 ? (uint)(oldMoppData.Length + 1) : (uint)moppData.Count;
//--END:CUSTOM--//

}

}