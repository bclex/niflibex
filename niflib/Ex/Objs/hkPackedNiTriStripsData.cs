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

/*! NiTriStripsData for havok data? */
public class hkPackedNiTriStripsData : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("hkPackedNiTriStripsData", bhkShapeCollection.TYPE);
	/*!  */
	internal uint numTriangles;
	/*!  */
	internal IList<TriangleData> triangles;
	/*!  */
	internal uint numVertices;
	/*! Unknown. */
	internal byte unknownByte1;
	/*!  */
	internal IList<Vector3> vertices;
	/*! Number of subparts. */
	internal ushort numSubShapes;
	/*! The subparts. */
	internal IList<OblivionSubShape> subShapes;

	public hkPackedNiTriStripsData() {
	numTriangles = (uint)0;
	numVertices = (uint)0;
	unknownByte1 = (byte)0;
	numSubShapes = (ushort)0;
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
public static NiObject Create() => new hkPackedNiTriStripsData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numTriangles, s, info);
	triangles = new TriangleData[numTriangles];
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		Nif.NifStream(out triangles[i1].triangle, s, info);
		Nif.NifStream(out triangles[i1].weldingInfo, s, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(out triangles[i1].normal, s, info);
		}
	}
	Nif.NifStream(out numVertices, s, info);
	if (info.version >= 0x14020007) {
		Nif.NifStream(out unknownByte1, s, info);
	}
	vertices = new Vector3[numVertices];
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(out vertices[i1], s, info);
	}
	if (info.version >= 0x14020007) {
		Nif.NifStream(out numSubShapes, s, info);
		subShapes = new OblivionSubShape[numSubShapes];
		for (var i2 = 0; i2 < subShapes.Count; i2++) {
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

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSubShapes = (ushort)subShapes.Count;
	numVertices = (uint)vertices.Count;
	numTriangles = (uint)triangles.Count;
	Nif.NifStream(numTriangles, s, info);
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		Nif.NifStream(triangles[i1].triangle, s, info);
		Nif.NifStream(triangles[i1].weldingInfo, s, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(triangles[i1].normal, s, info);
		}
	}
	Nif.NifStream(numVertices, s, info);
	if (info.version >= 0x14020007) {
		Nif.NifStream(unknownByte1, s, info);
	}
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(vertices[i1], s, info);
	}
	if (info.version >= 0x14020007) {
		Nif.NifStream(numSubShapes, s, info);
		for (var i2 = 0; i2 < subShapes.Count; i2++) {
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
	numSubShapes = (ushort)subShapes.Count;
	numVertices = (uint)vertices.Count;
	numTriangles = (uint)triangles.Count;
	s.AppendLine($"  Num Triangles:  {numTriangles}");
	array_output_count = 0;
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Triangle:  {triangles[i1].triangle}");
		s.AppendLine($"    Welding Info:  {triangles[i1].weldingInfo}");
		s.AppendLine($"    Normal:  {triangles[i1].normal}");
	}
	s.AppendLine($"  Num Vertices:  {numVertices}");
	s.AppendLine($"  Unknown Byte 1:  {unknownByte1}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Vertices[{i1}]:  {vertices[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Sub Shapes:  {numSubShapes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
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
         * Returns the triangle faces that make up this mesh.
         * \return A vector containing the triangle faces that make up this mesh.
         * \sa hkPackedNiTriStripsData::SetTriangles
         */
        public virtual IList<Triangle> GetTriangles()
        {
            //Remove any bad triangles
            var good_triangles = new List<Triangle>();
            for (var i = 0; i < triangles.Count; ++i)
            {
                var t = triangles[i].triangle;
                if (t.v1 != t.v2 && t.v2 != t.v3 && t.v1 != t.v3)
                    good_triangles.Add(t);
            }
            return good_triangles;
        }

        /*!
         * Returns the triangle data that make up this mesh.
         * \return A vector containing the triangle data that make up this mesh.
         * \sa hkPackedNiTriStripsData::SetTriangles
         */
        public virtual IList<TriangleData> GetHavokTriangles()
        {
            //Remove any bad triangles
            var good_triangles = new List<TriangleData>();
            for (var i = 0; i < triangles.Count; ++i)
            {
                var t = triangles[i];
                if (t.triangle.v1 != t.triangle.v2 && t.triangle.v2 != t.triangle.v3 && t.triangle.v1 != t.triangle.v3)
                    good_triangles.Add(t);
            }
            return good_triangles;
        }

        /*! 
         * Returns the number of vertices that make up this mesh.  This is also the number of normals, colors, and UV coordinates if these are used.
         * \return The number of vertices that make up this mesh.
         */
        public int VertexCount => vertices.Count;

        /*! 
         * Used to retrieve the vertices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector containing the vertices used by this mesh.
         */
        public IList<Vector3> GetVertices() => vertices;

        /*! 
         * Used to retrieve the normals used by this mesh.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
         * \return A vector containing the normals used by this mesh, if any.
         */
        public IList<Vector3> GetNormals()
        {
            //Remove any bad triangles
            var good_normals = new List<Vector3>();
            for (var i = 0; i < triangles.Count; ++i)
            {
                var t = triangles[i].normal;
                good_normals.Add(t);
            }
            return good_normals;
        }

        /*! Gets or sets the number of vertices that make up this mesh.
        * \param value The number of faces that make up this mesh.
        */
        public virtual int NumFaces
        {
            get => triangles.Count;
            set
            {
                if (value > 65535 || value < 0)
                    throw new Exception("Invalid Face Count: must be between 0 and 65535.");
                triangles.Resize(value);
            }
        }

        /*! Replaces the triangle face data in this mesh with new data.
        * \param in A vector containing the new face data.  Maximum size is 65,535.
        * \sa ITriShapeData::GetTriangles
        */
        public virtual void SetTriangles(IList<Triangle> value)
        {
            if (triangles.Count != value.Count)
                throw new Exception("Invalid Face Count: triangle count must be same as face count.");
            for (var i = 0; i < triangles.Count; ++i)
                triangles[i].triangle = value[i];
        }

        /*! Replaces the triangle face data in this mesh with new data.
        * \param in A vector containing the new face data.  Maximum size is 65,535.
        * \sa ITriShapeData::GetHavokTriangles
        */
        public virtual void SetHavokTriangles(List<TriangleData> value)
        {
            if (value.Count > 65535 || value.Count < 0)
                throw new Exception("Invalid Face Count: must be between 0 and 65535.");
            triangles = value;
        }

        /*! Replaces the face normal data in this mesh with new data.
        * \param in A vector containing the new face normal data.
        */
        public virtual void SetNormals(IList<Vector3> value)
        {
            if (triangles.Count != value.Count)
                throw new Exception("Invalid Face Count: normal count must be same as face count.");
            for (var i = 0; i < triangles.Count; ++i)
                triangles[i].normal = value[i];
        }


        /*! Replaces the vertex data in this mesh with new data.
        * \param in A vector containing the new vertex data.
        */
        public virtual void SetVertices(IList<Vector3> value)
        {
            if (value.Count > 65535 || value.Count < 0)
                throw new Exception("Invalid Vertex Count: must be between 0 and 65535.");
            vertices = value;
        }

        /*!
        * Gets or Sets the subshape data object used by this geometry node. 
        * \param[in] value The subshape data.
        */
        public IList<OblivionSubShape> SubShapes
        {
            get => subShapes;
            set
            {
                numSubShapes = (ushort)value.Count;
                subShapes = value;
            }
        }
//--END:CUSTOM--//

}

}