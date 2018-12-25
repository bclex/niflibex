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


namespace Niflib
{

    /*!
     * A convex shape built from vertices. Note that if the shape is used in
     *         a non-static object (such as clutter), then they will simply fall
     *         through ground when they are under a bhkListShape.
     */
    public class bhkConvexVerticesShape : bhkConvexShape
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("bhkConvexVerticesShape", bhkConvexShape.TYPE);
        /*!  */
        public hkWorldObjCinfoProperty verticesProperty;
        /*!  */
        public hkWorldObjCinfoProperty normalsProperty;
        /*! Number of vertices. */
        public uint numVertices;
        /*! Vertices. Fourth component is 0. Lexicographically sorted. */
        public Vector4[] vertices;
        /*! The number of half spaces. */
        public uint numNormals;
        /*!
         * Half spaces as determined by the set of vertices above. First three components
         * define the normal pointing to the exterior, fourth component is the signed
         * distance of the separating plane to the origin: it is minus the dot product of v
         * and n, where v is any vertex on the separating plane, and n is the normal.
         * Lexicographically sorted.
         */
        public Vector4[] normals;

        public bhkConvexVerticesShape()
        {
            numVertices = (uint)0;
            numNormals = (uint)0;
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
        public static NiObject Create() => new bhkConvexVerticesShape();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out verticesProperty.data, s, info);
            Nif.NifStream(out verticesProperty.size, s, info);
            Nif.NifStream(out verticesProperty.capacityAndFlags, s, info);
            Nif.NifStream(out normalsProperty.data, s, info);
            Nif.NifStream(out normalsProperty.size, s, info);
            Nif.NifStream(out normalsProperty.capacityAndFlags, s, info);
            Nif.NifStream(out numVertices, s, info);
            vertices = new Vector4[numVertices];
            for (var i1 = 0; i1 < vertices.Length; i1++)
            {
                Nif.NifStream(out vertices[i1], s, info);
            }
            Nif.NifStream(out numNormals, s, info);
            normals = new Vector4[numNormals];
            for (var i1 = 0; i1 < normals.Length; i1++)
            {
                Nif.NifStream(out normals[i1], s, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numNormals = (uint)normals.Length;
            numVertices = (uint)vertices.Length;
            Nif.NifStream(verticesProperty.data, s, info);
            Nif.NifStream(verticesProperty.size, s, info);
            Nif.NifStream(verticesProperty.capacityAndFlags, s, info);
            Nif.NifStream(normalsProperty.data, s, info);
            Nif.NifStream(normalsProperty.size, s, info);
            Nif.NifStream(normalsProperty.capacityAndFlags, s, info);
            Nif.NifStream(numVertices, s, info);
            for (var i1 = 0; i1 < vertices.Length; i1++)
            {
                Nif.NifStream(vertices[i1], s, info);
            }
            Nif.NifStream(numNormals, s, info);
            for (var i1 = 0; i1 < normals.Length; i1++)
            {
                Nif.NifStream(normals[i1], s, info);
            }

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string asString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.asString());
            numNormals = (uint)normals.Length;
            numVertices = (uint)vertices.Length;
            s.AppendLine($"  Data:  {verticesProperty.data}");
            s.AppendLine($"  Size:  {verticesProperty.size}");
            s.AppendLine($"  Capacity and Flags:  {verticesProperty.capacityAndFlags}");
            s.AppendLine($"  Data:  {normalsProperty.data}");
            s.AppendLine($"  Size:  {normalsProperty.size}");
            s.AppendLine($"  Capacity and Flags:  {normalsProperty.capacityAndFlags}");
            s.AppendLine($"  Num Vertices:  {numVertices}");
            array_output_count = 0;
            for (var i1 = 0; i1 < vertices.Length; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Vertices[{i1}]:  {vertices[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Num Normals:  {numNormals}");
            array_output_count = 0;
            for (var i1 = 0; i1 < normals.Length; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Normals[{i1}]:  {normals[i1]}");
                array_output_count++;
            }
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*! 
        * Returns the number of vertices that make up this mesh.  This is also the number of normals, colors, and UV coordinates if these are used.
        * \return The number of vertices that make up this mesh.
        */
        public int VertexCount { get => vertices.Length; }

        /*! 
        * Used to retrieve the vertices used by this mesh.  The size of the vector will be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
        * \return A vector containing the vertices used by this mesh.
        */
        public List<Vector3> GetVertices()
        {
            //Remove any bad triangles
            var good_vertices = new List<Vector3>();
            for (var i = 0; i < vertices.Length; ++i)
            {
                var t = vertices[i];
                var v = new Vector3(t[0], t[1], t[2]);
                good_vertices.Add(v);
            }
            return good_vertices;
        }

        /*! 
        * Used to retrieve the normals used by this mesh.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
        * \return A vector containing the normals used by this mesh, if any.
        */
        public List<Vector3> GetNormals()
        {
            //Remove any bad triangles
            var good_normals = new List<Vector3>();
            for (var i = 0; i < normals.Length; ++i)
            {
                var t = normals[i];
                var v = new Vector3(t[0], t[1], t[2]);
                good_normals.Add(v);
            }
            return good_normals;
        }

        /*! 
        * Used to retrieve the distance to center for vertices.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
        * \return A vector containing the normals used by this mesh, if any.
        */
        public List<float> GetDistToCenter()
        {
            //Remove any bad triangles
            var good_dist = new List<float>();
            for (var i = 0; i < normals.Length; ++i)
                good_dist.Add(normals[i][3]);
            return good_dist;
        }

        /*! 
        * Used to set the vertex data used by this mesh.  Calling this function will clear all other data in this object.
        * \param in A vector containing the vertices to replace those in the mesh with.  Note that there is no way to set vertices one at a time, they must be sent in one batch.
        */
        public void SetVertices(List<Vector3> s)
        {
            var size = s.Count;
            vertices.resize(size);
            for (var i = 0; i < size; ++i)
            {
                var f = vertices[i];
                var v = s[i];
                f[0] = v.x;
                f[1] = v.y;
                f[2] = v.z;
                f[3] = 0.0f;
            }
        }

        /*!
        * Used to set the normal data used by this mesh.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
        * \param in A vector containing the normals to replace those in the mesh with.  Note that there is no way to set normals one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using normals.
        */
        public void SetNormals(List<Vector3> s)
        {
            var size = s.Count;
            normals.resize(size);
            for (var i = 0; i < size; ++i)
            {
                Vector4 f = normals[i];
                Vector3 v = s[i];
                f[0] = v.x;
                f[1] = v.y;
                f[2] = v.z;
                f[3] = 0.0f;
            }
        }

        /*!
        * Used to sets the distance to center for vertices.  The size of the vector must either be zero, or the same as the vertex count retrieved with the IShapeData::GetVertexCount function or the function will throw an exception.
        * \param in A vector containing the normals to replace those in the mesh with.  Note that there is no way to set normals one at a time, they must be sent in one batch.  Use an empty vector to signify that this mesh will not be using normals.
        */
        public void SetDistToCenter(List<float> s)
        {
            if (s.Count != normals.Length)
                throw new Exception("Distance vector size does not match normal size.");
            var size = s.Count;
            normals.resize(size);
            for (var i = 0; i < size; ++i)
            {
                Vector4 f = normals[i];
                f[3] = s[i];
            }
        }

        /*! 
        * Gets or sets the normal and the distance to center for vertices.  The size of the vector will either be zero if no normals are used, or be the same as the vertex count retrieved with the IShapeData::GetVertexCount function.
        */
        public Vector4[] NormalsAndDist
        {
            get => normals;
            set => normals = value;
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
            var verts = GetVertices();
            var tris = new List<Triangle>(); // no tris mean convex
            Inertia.CalcMassPropertiesPolyhedron(verts, tris, density, solid, mass, volume, center, inertia);
        }
        //--END:CUSTOM--//

    }

}