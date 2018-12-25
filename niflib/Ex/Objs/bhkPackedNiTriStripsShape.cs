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

/*! A shape constructed from strips data. */
public class bhkPackedNiTriStripsShape : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkPackedNiTriStripsShape", bhkShapeCollection.TYPE);
	/*!  */
	public ushort numSubShapes;
	/*!  */
	public OblivionSubShape[] subShapes;
	/*!  */
	public uint userData;
	/*! Looks like a memory pointer and may be garbage. */
	public uint unused1;
	/*!  */
	public float radius;
	/*! Looks like a memory pointer and may be garbage. */
	public uint unused2;
	/*!  */
	public Vector4 scale;
	/*! Same as radius */
	public float radiusCopy;
	/*! Same as scale. */
	public Vector4 scaleCopy;
	/*!  */
	public hkPackedNiTriStripsData data;

	public bhkPackedNiTriStripsShape() {
	numSubShapes = (ushort)0;
	userData = (uint)0;
	unused1 = (uint)0;
	radius = 0.1f;
	unused2 = (uint)0;
	scale = 1.0, 1.0, 1.0, 0.0;
	radiusCopy = 0.1f;
	scaleCopy = 1.0, 1.0, 1.0, 0.0;
	data = null;
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
public static NiObject Create() => new bhkPackedNiTriStripsShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version <= 0x14000005) {
		Nif.NifStream(out numSubShapes, s, info);
		subShapes = new OblivionSubShape[numSubShapes];
		for (var i2 = 0; i2 < subShapes.Length; i2++) {
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(out subShapes[i2].havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(out subShapes[i2].havokFilter.group, s, info);
			Nif.NifStream(out subShapes[i2].numVertices, s, info);
			if (info.version <= 0x0A000102) {
				Nif.NifStream(out subShapes[i2].material.unknownInt, s, info);
			}
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(out subShapes[i2].material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(out subShapes[i2].material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(out subShapes[i2].material.material_sk, s, info);
			}
		}
	}
	Nif.NifStream(out userData, s, info);
	Nif.NifStream(out unused1, s, info);
	Nif.NifStream(out radius, s, info);
	Nif.NifStream(out unused2, s, info);
	Nif.NifStream(out scale, s, info);
	Nif.NifStream(out radiusCopy, s, info);
	Nif.NifStream(out scaleCopy, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSubShapes = (ushort)subShapes.Length;
	if (info.version <= 0x14000005) {
		Nif.NifStream(numSubShapes, s, info);
		for (var i2 = 0; i2 < subShapes.Length; i2++) {
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(subShapes[i2].havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(subShapes[i2].havokFilter.group, s, info);
			Nif.NifStream(subShapes[i2].numVertices, s, info);
			if (info.version <= 0x0A000102) {
				Nif.NifStream(subShapes[i2].material.unknownInt, s, info);
			}
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(subShapes[i2].material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(subShapes[i2].material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(subShapes[i2].material.material_sk, s, info);
			}
		}
	}
	Nif.NifStream(userData, s, info);
	Nif.NifStream(unused1, s, info);
	Nif.NifStream(radius, s, info);
	Nif.NifStream(unused2, s, info);
	Nif.NifStream(scale, s, info);
	Nif.NifStream(radiusCopy, s, info);
	Nif.NifStream(scaleCopy, s, info);
	WriteRef((NiObject)data, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string asString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.asString());
	numSubShapes = (ushort)subShapes.Length;
	s.AppendLine($"  Num Sub Shapes:  {numSubShapes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < subShapes.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_ob}");
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_fo}");
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_sk}");
		s.AppendLine($"    Flags and Part Number:  {subShapes[i1].havokFilter.flagsAndPartNumber}");
		s.AppendLine($"    Group:  {subShapes[i1].havokFilter.group}");
		s.AppendLine($"    Num Vertices:  {subShapes[i1].numVertices}");
		s.AppendLine($"    Unknown Int:  {subShapes[i1].material.unknownInt}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_ob}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_fo}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_sk}");
	}
	s.AppendLine($"  User Data:  {userData}");
	s.AppendLine($"  Unused 1:  {unused1}");
	s.AppendLine($"  Radius:  {radius}");
	s.AppendLine($"  Unused 2:  {unused2}");
	s.AppendLine($"  Scale:  {scale}");
	s.AppendLine($"  Radius Copy:  {radiusCopy}");
	s.AppendLine($"  Scale Copy:  {scaleCopy}");
	s.AppendLine($"  Data:  {data}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	data = FixLink<hkPackedNiTriStripsData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

//--BEGIN:FILE FOOT--//
    /*!
        * Get or sets the geometry data object used by this geometry node.  This contains the vertices, normals, etc. and can be shared among several geometry nodes.
        * \param[in] n The new geometry data object, or NULL to clear the current one.
        */
    public hkPackedNiTriStripsData Data
    {
        get => data;
        set => data = value;
    }

    /*!
    * Get or sets the subshape data object used by this geometry node. 
    * \param[in] value The subshape data.
    */
    public OblivionSubShape[] SubShapes
    {
        get => subShapes;
        set
        {
            numSubShapes = (ushort)value.Length;
            subShapes = value;
        }
    }

    /*!
        * Gets or sets the scale. Usually (1.0, 1.0, 1.0).
        * \param[in] n The new scale.
        */
    public Vector4 Scale
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
        if (data != null)
        {
            var verts = data.Vertices;
            var tris = data.GetTriangles();
            Inertia.CalcMassPropertiesPolyhedron(verts, tris, density, solid, out mass, out volume, out center, out inertia);
        }
    }
//--END:CUSTOM--//

}

}