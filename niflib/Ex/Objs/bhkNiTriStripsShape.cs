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

/*! A shape constructed from a bunch of strips. */
public class bhkNiTriStripsShape : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkNiTriStripsShape", bhkShapeCollection.TYPE);
	/*! The material of the shape. */
	internal HavokMaterial material;
	/*!  */
	internal float radius;
	/*!
	 * Garbage data from memory though the last 3 are referred to as maxSize, size, and
	 * eSize.
	 */
	internal Array5<uint> unused;
	/*!  */
	internal uint growBy;
	/*! Scale. Usually (1.0, 1.0, 1.0, 0.0). */
	internal Vector4 scale;
	/*! The number of strips data objects referenced. */
	internal uint numStripsData;
	/*! Refers to a bunch of NiTriStripsData objects that make up this shape. */
	internal NiTriStripsData[] stripsData;
	/*! Number of Havok Layers, equal to Number of strips data objects. */
	internal uint numDataLayers;
	/*! Havok Layers for each strip data. */
	internal HavokFilter[] dataLayers;

	public bhkNiTriStripsShape() {
	radius = 0.1f;
	growBy = (uint)1;
	scale = 1.0, 1.0, 1.0, 0.0;
	numStripsData = (uint)0;
	numDataLayers = (uint)0;
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
public static NiObject Create() => new bhkNiTriStripsShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
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
	for (var i1 = 0; i1 < 5; i1++) {
		Nif.NifStream(out unused[i1], s, info);
	}
	Nif.NifStream(out growBy, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out scale, s, info);
	}
	Nif.NifStream(out numStripsData, s, info);
	stripsData = new Ref[numStripsData];
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numDataLayers, s, info);
	dataLayers = new HavokFilter[numDataLayers];
	for (var i1 = 0; i1 < dataLayers.Length; i1++) {
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(out dataLayers[i1].layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(out dataLayers[i1].layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(out dataLayers[i1].layer_sk, s, info);
		}
		Nif.NifStream(out dataLayers[i1].flagsAndPartNumber, s, info);
		Nif.NifStream(out dataLayers[i1].group, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numDataLayers = (uint)dataLayers.Length;
	numStripsData = (uint)stripsData.Length;
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
	for (var i1 = 0; i1 < 5; i1++) {
		Nif.NifStream(unused[i1], s, info);
	}
	Nif.NifStream(growBy, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(scale, s, info);
	}
	Nif.NifStream(numStripsData, s, info);
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
		WriteRef((NiObject)stripsData[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numDataLayers, s, info);
	for (var i1 = 0; i1 < dataLayers.Length; i1++) {
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(dataLayers[i1].layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(dataLayers[i1].layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(dataLayers[i1].layer_sk, s, info);
		}
		Nif.NifStream(dataLayers[i1].flagsAndPartNumber, s, info);
		Nif.NifStream(dataLayers[i1].group, s, info);
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
	numDataLayers = (uint)dataLayers.Length;
	numStripsData = (uint)stripsData.Length;
	s.AppendLine($"  Unknown Int:  {material.unknownInt}");
	s.AppendLine($"  Material:  {material.material_ob}");
	s.AppendLine($"  Material:  {material.material_fo}");
	s.AppendLine($"  Material:  {material.material_sk}");
	s.AppendLine($"  Radius:  {radius}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 5; i1++) {
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
	s.AppendLine($"  Grow By:  {growBy}");
	s.AppendLine($"  Scale:  {scale}");
	s.AppendLine($"  Num Strips Data:  {numStripsData}");
	array_output_count = 0;
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Strips Data[{i1}]:  {stripsData[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Data Layers:  {numDataLayers}");
	array_output_count = 0;
	for (var i1 = 0; i1 < dataLayers.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Layer:  {dataLayers[i1].layer_ob}");
		s.AppendLine($"    Layer:  {dataLayers[i1].layer_fo}");
		s.AppendLine($"    Layer:  {dataLayers[i1].layer_sk}");
		s.AppendLine($"    Flags and Part Number:  {dataLayers[i1].flagsAndPartNumber}");
		s.AppendLine($"    Group:  {dataLayers[i1].group}");
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
		stripsData[i1] = FixLink<NiTriStripsData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
		if (stripsData[i1] != null)
			refs.Add((NiObject)stripsData[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < stripsData.Length; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the scale. Usually (1.0, 1.0, 1.0).
         * \param[in] n The new scale.
         */
        public Vector4 Scale
        {
            get => scale;
            set => scale = value;
        }

        /*!
         * Gets or sets the number of NiTriStripsData objects referenced by this shape.
         * \param[in] i The new number of NiTriStripsData objects.
         */
        public uint NumStripsData
        {
            get => numStripsData;
            set
            {
                numStripsData = value;
                Array.Resize(ref stripsData, (int)value);
            }
        }

        /*!
         * Gets the NiTriStripsData object referenced by this shape at the specified index.
         * \param[in] index The index at which the given NiTriStripsData object will be referenced.  Should be lower than the value set with bhkNiTriStripsShape::SetNumStripsData.
         */
        public NiTriStripsData GetStripsData(int index) => stripsData[index];

        /*!
         * Sets the NiTriStripsData object referenced by this shape at the specified index.
         * \param[in] index The index at which the given NiTriStripsData object will be referenced.  Should be lower than the value set with bhkNiTriStripsShape::SetNumStripsData.
         * \param[in] strips The new NiTriStripsData object to be referenced by this shape at the specified index.
         */
        public void SetStripsData(int index, NiTriStripsData strips) => stripsData[index] = strips;

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
        * Gets or sets the number of OblivionColFilter objects referenced by this shape.
        * \param[in] i The new number of OblivionColFilter objects.
        */
        public uint NumDataLayers
        {
            get => numDataLayers;
            set
            {
                numDataLayers = value;
                Array.Resize(ref dataLayers, (int)value);
            }
        }

        /*!
         * Gets the OblivionLayer referenced for the filter at the specified index.
         * \param[in] index The index at which the given OblivionLayer will be referenced.  Should be lower than the value set with bhkNiTriStripsShape::SetNumDataLayers.
         */
        public Fallout3Layer GetFallout3Layer(uint index) => dataLayers[index].layer_fo;
        public OblivionLayer GetOblivionLayer(uint index) => dataLayers[index].layer_ob;
        public SkyrimLayer GetSkyrimLayer(uint index) => dataLayers[index].layer_sk;

        /*!
         * Sets the OblivionLayer referenced for the filter at the specified index.
         * \param[in] index The index at which the given OblivionLayer will be referenced.  Should be lower than the value set with bhkNiTriStripsShape::SetNumDataLayers.
         */
        public void SetFallout3Layer(uint index, Fallout3Layer layer) => dataLayers[index].layer_fo = layer;
        public void SetOblivionLayer(uint index, OblivionLayer layer) => dataLayers[index].layer_ob = layer;
        public void SetSkyrimLayer(uint index, SkyrimLayer layer) => dataLayers[index].layer_sk = layer;

        //public byte GetOblivionFilter(uint index) => dataLayers[index].colFilter;
        //public void SetOblivionFilter(uint index, byte filter) => dataLayers[index].colFilter = filter;

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

            var verts = new List<Vector3>();
            var tris = new List<Triangle>();
            foreach (var itr in stripsData)
            {
                int nv = verts.Count, nt = tris.Count;
                var v = itr.Vertices;
                var t = itr.Triangles;
                for (var i = 0; i < nv; ++i)
                    verts.Add(v[i]);
                for (var i = 0; i < nt; ++i)
                    tris.Add(new Triangle(t[i][0] + nt, t[i][1] + nt, t[i][2] + nt));
            }
            Inertia.CalcMassPropertiesPolyhedron(verts, tris, density, solid, out mass, out volume, out center, out inertia);
        }
//--END:CUSTOM--//

}

}