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

/*! Transforms a shape. */
public class bhkTransformShape : bhkShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkTransformShape", bhkShape.TYPE);
	/*! The shape that this object transforms. */
	internal bhkShape shape;
	/*! The material of the shape. */
	internal HavokMaterial material;
	/*!  */
	internal float radius;
	/*! Garbage data from memory. */
	internal Array8<byte> unused;
	/*! A transform matrix. */
	internal Matrix44 transform;

	public bhkTransformShape() {
	shape = null;
	radius = 0.0f;
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
public static NiObject Create() => new bhkTransformShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
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
	Nif.NifStream(out radius, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused[i1], s, info);
	}
	Nif.NifStream(out transform, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	WriteRef((NiObject)shape, s, info, link_map, missing_link_stack);
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
	Nif.NifStream(radius, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused[i1], s, info);
	}
	Nif.NifStream(transform, s, info);

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
	s.AppendLine($"  Unknown Int:  {material.unknownInt}");
	s.AppendLine($"  Material:  {material.material_ob}");
	s.AppendLine($"  Material:  {material.material_fo}");
	s.AppendLine($"  Material:  {material.material_sk}");
	s.AppendLine($"  Radius:  {radius}");
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
	s.AppendLine($"  Transform:  {transform}");
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
         * Gets or sets the shape's material.  This determines the type of noises the object makes as it collides in Oblivion.
         * \param[in] value The new material for this shape to use.
         */
        public HavokMaterial Material
        {
            get => material;
            set => material = value;
        }

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
         * This is a conveniance function that allows you to get or set the rotation, scale, and translation of an AV object with a 4x4 matrix transform.
         * \n A 4x4 transformation matrix to set the AVObject's transform attributes with.
         */
        public virtual Matrix44 Transform
        {
            get => transform;
            set => transform = value;
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
            center = transform.GetTranslation();
            mass = 0.0f; volume = 0.0f;
            inertia = InertiaMatrix.IDENTITY;
            if (shape != null)
            {
                var transform_transposed = transform.Transpose();
                shape.CalcMassProperties(density, solid, out mass, out volume, out center, out inertia);
                center = transform * center;

                var tm = new Matrix44(inertia.Submatrix(0, 0));
                var im = transform_transposed * tm * transform;
                inertia = new InertiaMatrix(im.GetRotation());
            }
        }
//--END:CUSTOM--//

}

}