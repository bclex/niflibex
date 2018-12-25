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
 * A list of shapes.
 * 
 *         Do not put a bhkPackedNiTriStripsShape in the Sub Shapes. Use a
 *         separate collision nodes without a list shape for those.
 * 
 *         Also, shapes collected in a bhkListShape may not have the correct
 *         walking noise, so only use it for non-walkable objects.
 */
public class bhkListShape : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkListShape", bhkShapeCollection.TYPE);
	/*! The number of sub shapes referenced. */
	uint numSubShapes;
	/*! List of shapes. */
	bhkShape[] subShapes;
	/*! The material of the shape. */
	HavokMaterial material;
	/*!  */
	hkWorldObjCinfoProperty childShapeProperty;
	/*!  */
	hkWorldObjCinfoProperty childFilterProperty;
	/*! Count. */
	uint numUnknownInts;
	/*! Unknown. */
	uint[] unknownInts;

	public bhkListShape() {
	numSubShapes = (uint)0;
	numUnknownInts = (uint)0;
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
public static NiObject Create() => new bhkListShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numSubShapes, s, info);
	subShapes = new Ref[numSubShapes];
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if (info.version <= 0x0A000102) {
		Nif.NifStream(out material.unknownInt, s, info);
	}
	if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
		Nif.NifStream(out material.material_ob, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
		Nif.NifStream(out material.material_fo, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
		Nif.NifStream(out material.material_sk, s, info);
	}
	Nif.NifStream(out childShapeProperty.data, s, info);
	Nif.NifStream(out childShapeProperty.size, s, info);
	Nif.NifStream(out childShapeProperty.capacityAndFlags, s, info);
	Nif.NifStream(out childFilterProperty.data, s, info);
	Nif.NifStream(out childFilterProperty.size, s, info);
	Nif.NifStream(out childFilterProperty.capacityAndFlags, s, info);
	Nif.NifStream(out numUnknownInts, s, info);
	unknownInts = new uint[numUnknownInts];
	for (var i1 = 0; i1 < unknownInts.Length; i1++) {
		Nif.NifStream(out unknownInts[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numUnknownInts = (uint)unknownInts.Length;
	numSubShapes = (uint)subShapes.Length;
	Nif.NifStream(numSubShapes, s, info);
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		WriteRef((NiObject)subShapes[i1], s, info, link_map, missing_link_stack);
	}
	if (info.version <= 0x0A000102) {
		Nif.NifStream(material.unknownInt, s, info);
	}
	if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
		Nif.NifStream(material.material_ob, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
		Nif.NifStream(material.material_fo, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
		Nif.NifStream(material.material_sk, s, info);
	}
	Nif.NifStream(childShapeProperty.data, s, info);
	Nif.NifStream(childShapeProperty.size, s, info);
	Nif.NifStream(childShapeProperty.capacityAndFlags, s, info);
	Nif.NifStream(childFilterProperty.data, s, info);
	Nif.NifStream(childFilterProperty.size, s, info);
	Nif.NifStream(childFilterProperty.capacityAndFlags, s, info);
	Nif.NifStream(numUnknownInts, s, info);
	for (var i1 = 0; i1 < unknownInts.Length; i1++) {
		Nif.NifStream(unknownInts[i1], s, info);
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
	numUnknownInts = (uint)unknownInts.Length;
	numSubShapes = (uint)subShapes.Length;
	s.AppendLine($"  Num Sub Shapes:  {numSubShapes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Sub Shapes[{i1}]:  {subShapes[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown Int:  {material.unknownInt}");
	s.AppendLine($"  Material:  {material.material_ob}");
	s.AppendLine($"  Material:  {material.material_fo}");
	s.AppendLine($"  Material:  {material.material_sk}");
	s.AppendLine($"  Data:  {childShapeProperty.data}");
	s.AppendLine($"  Size:  {childShapeProperty.size}");
	s.AppendLine($"  Capacity and Flags:  {childShapeProperty.capacityAndFlags}");
	s.AppendLine($"  Data:  {childFilterProperty.data}");
	s.AppendLine($"  Size:  {childFilterProperty.size}");
	s.AppendLine($"  Capacity and Flags:  {childFilterProperty.capacityAndFlags}");
	s.AppendLine($"  Num Unknown Ints:  {numUnknownInts}");
	array_output_count = 0;
	for (var i1 = 0; i1 < unknownInts.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Ints[{i1}]:  {unknownInts[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		subShapes[i1] = FixLink<bhkShape>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		if (subShapes[i1] != null)
			refs.Add((NiObject)subShapes[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
        * Gets or sets the child shape objects that this body is using.
        * \param[in] shapes The shape objects being used by this body.
        */
        public bhkShape[] SubShapes
        {
            get => subShapes;
            set
            {
                subShapes = value;
                // Becuase this vector matches the subshape vector
                Array.Resize<ref unknownInts, subShapes.Length);
            }
        }

        /*!
         * Gets or sets the shape's material.  This determines the type of noises the object makes as it collides in Oblivion.
         * \param[in] value The new material for this shape to use.
         */
        public HavokMaterial Material
        {
            get => material;
            set => material = value;
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
            mass = 0.0f;
            volume = 0.0f;
            inertia = InertiaMatrix.IDENTITY;

            var masses = new List<float>();
            var volumes = new List<float>();
            var centers = new List<Vector3>();
            var inertias = new List<InertiaMatrix>();
            var transforms = new List<Matrix44>();
            foreach (var itr in subShapes)
            {
                float m; float v; Vector3 c; InertiaMatrix i;
                itr.CalcMassProperties(density, solid, out m, out v, out c, out i);
                masses.Add(m);
                volumes.Add(v);
                centers.Add(c);
                inertias.Add(i);
                transforms.Add(Matrix44.IDENTITY);
            }
            Inertia.CombineMassProperties(
                masses, volumes, centers, inertias, transforms,
                mass, volume, center, inertia);
        }
//--END:CUSTOM--//

}

}