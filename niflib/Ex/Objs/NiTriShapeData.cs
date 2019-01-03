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

    /*! Holds mesh data using a list of singular triangles. */
    public class NiTriShapeData : NiTriBasedGeomData
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiTriShapeData", NiTriBasedGeomData.TYPE);
        /*! Num Triangles times 3. */
        internal uint numTrianglePoints;
        /*! Do we have triangle data? */
        internal bool hasTriangles;
        /*! Triangle data. */
        internal IList<Triangle> triangles;
        /*! Number of shared normals groups. */
        internal ushort numMatchGroups;
        /*! The shared normals. */
        internal IList<MatchGroup> matchGroups;

        public NiTriShapeData()
        {
            numTrianglePoints = (uint)0;
            hasTriangles = false;
            numMatchGroups = (ushort)0;
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
        public static NiObject Create() => new NiTriShapeData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out numTrianglePoints, s, info);
            if (info.version >= 0x0A010000)
            {
                Nif.NifStream(out hasTriangles, s, info);
            }
            if (info.version <= 0x0A000102)
            {
                triangles = new Triangle[numTriangles];
                for (var i2 = 0; i2 < triangles.Count; i2++)
                {
                    Nif.NifStream(out triangles[i2], s, info);
                }
            }
            if (info.version >= 0x0A000103)
            {
                if (hasTriangles)
                {
                    triangles = new Triangle[numTriangles];
                    for (var i3 = 0; i3 < triangles.Count; i3++)
                    {
                        Nif.NifStream(out (Triangle)triangles[i3], s, info);
                    }
                }
            }
            if (info.version >= 0x03010000)
            {
                Nif.NifStream(out numMatchGroups, s, info);
                matchGroups = new MatchGroup[numMatchGroups];
                for (var i2 = 0; i2 < matchGroups.Count; i2++)
                {
                    Nif.NifStream(out matchGroups[i2].numVertices, s, info);
                    matchGroups[i2].vertexIndices = new ushort[matchGroups[i2].numVertices];
                    for (var i3 = 0; i3 < matchGroups[i2].vertexIndices.Count; i3++)
                    {
                        Nif.NifStream(out matchGroups[i2].vertexIndices[i3], s, info);
                    }
                }
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numMatchGroups = (ushort)matchGroups.Count;
            hasTriangles = hasTrianglesCalc(info);
            Nif.NifStream(numTrianglePoints, s, info);
            if (info.version >= 0x0A010000)
            {
                Nif.NifStream(hasTriangles, s, info);
            }
            if (info.version <= 0x0A000102)
            {
                for (var i2 = 0; i2 < triangles.Count; i2++)
                {
                    Nif.NifStream(triangles[i2], s, info);
                }
            }
            if (info.version >= 0x0A000103)
            {
                if (hasTriangles)
                {
                    for (var i3 = 0; i3 < triangles.Count; i3++)
                    {
                        Nif.NifStream((Triangle)triangles[i3], s, info);
                    }
                }
            }
            if (info.version >= 0x03010000)
            {
                Nif.NifStream(numMatchGroups, s, info);
                for (var i2 = 0; i2 < matchGroups.Count; i2++)
                {
                    matchGroups[i2].numVertices = (ushort)matchGroups[i2].vertexIndices.Count;
                    Nif.NifStream(matchGroups[i2].numVertices, s, info);
                    for (var i3 = 0; i3 < matchGroups[i2].vertexIndices.Count; i3++)
                    {
                        Nif.NifStream(matchGroups[i2].vertexIndices[i3], s, info);
                    }
                }
            }

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.AsString());
            numMatchGroups = (ushort)matchGroups.Count;
            s.AppendLine($"  Num Triangle Points:  {numTrianglePoints}");
            s.AppendLine($"  Has Triangles:  {hasTriangles}");
            array_output_count = 0;
            for (var i1 = 0; i1 < triangles.Count; i1++)
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
                s.AppendLine($"    Triangles[{i1}]:  {triangles[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Num Match Groups:  {numMatchGroups}");
            array_output_count = 0;
            for (var i1 = 0; i1 < matchGroups.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                matchGroups[i1].numVertices = (ushort)matchGroups[i1].vertexIndices.Count;
                s.AppendLine($"    Num Vertices:  {matchGroups[i1].numVertices}");
                array_output_count = 0;
                for (var i2 = 0; i2 < matchGroups[i1].vertexIndices.Count; i2++)
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
                    s.AppendLine($"      Vertex Indices[{i2}]:  {matchGroups[i1].vertexIndices[i2]}");
                    array_output_count++;
                }
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
         * A constructor which can be used to create a NiTriShapeData and initialize it with vertices.
         * \param[in] verts The verticies to initialize the NiTriShapeData with.
         */
        public NiTriShapeData(IList<Triangle> verts)
        {
            Triangles = verts;
        }

        //--Match Detection--//

        //Reimplemented only to casue match detection data to be cleared
        //when vertices are updated.
        public virtual void SetVertices(IList<Vector3> value)
        {
            //Take normal action
            base.SetVertices(value);

            //Also, clear match detection data
            RemoveMatchData();
        }

        /*!
         * This function generates match detection data based on the current
         * vertex list.  The function of this data is unknown and appears to be
         * optional.  The data contains a list of all the vertices that have
         * identical positions are stored in the file.  If the vertex data is
         * updated, match detection data will be cleared.
         * \sa NiTriShapeData::HasMatchData
         */
        public void DoMatchDetection()
        {
            /* minimum number of groups of shared normals */
            matchGroups.Resize(0);
            /* counting sharing */
            var is_shared = new bool[vertices.Count];
            for (var i = 0; i < vertices.Count - 1; ++i)
            {
                /* this index belongs to a group already */
                if (is_shared[i])
                    continue;

                /* we may find a valid group for this vertex */
                var group = new MatchGroup();
                /* this vertex belongs to the group as well */
                group.vertexIndices.Add((ushort)i);

                // Find all vertices that match this one.
                for (var j = i + 1; j < vertices.Count; ++j)
                {
                    /* this index belongs to another group already */
                    /* so its vert/norm cannot match this group! */
                    if (is_shared[j])
                        continue;
                    /* for automatic regeneration we just consider
                     * identical positions, though the format would
                     * allow distinct positions to share a normal
                     */
                    if (vertices[j] != vertices[i])
                        continue;
                    if (normals[j] != normals[i])
                        continue;
                    /* remember this vertex' index */
                    group.vertexIndices.Add(j);
                }

                /* the currently observed vertex shares a normal with others */
                if ((group.numVertices = (ushort)group.vertexIndices.Count) > 1)
                {
                    /* mark all of the participating vertices to belong to a group */
                    for (var n = 0; n < group.numVertices; n++)
                        is_shared[group.vertexIndices[n]] = true;

                    /* register the group */
                    matchGroups.Add(group);
                }
            }
        }

        /*!
         * Remove match detection data.
         */
        public void RemoveMatchData() => matchGroups.Clear();

        /*!
         * Used to determine whether current match detection data has been previously
         * generated.
         * \return true if there is current match data, false otherwise.
         * \sa NiTriShapeData::DoMatchDetection
         */
        public bool HasMatchData => matchGroups.Count > 0;

        /*! Replaces the triangle face data in this mesh with new data.
         * \param in A vector containing the new face data.  Maximum size is 65,535.
         * \sa ITriShapeData::GetTriangles
         */
        public virtual IList<Triangle> Triangles
        {
            get
            {
                //Remove any bad triangles
                var good_triangles = new List<Triangle>();
                for (var i = 0; i < triangles.Count; ++i)
                {
                    var t = triangles[i];
                    if (t.v1 != t.v2 && t.v2 != t.v3 && t.v1 != t.v3)
                        good_triangles.Add(t);
                }
                return good_triangles;
            }
            set
            {
                if (value.Count > 65535 || value.Count < 0)
                    throw new Exception("Invalid Triangle Count: must be between 0 and 65535.");
                triangles = value;
                hasTriangles = triangles.Count != 0;
                //Set nuber of triangles
                numTriangles = (ushort)triangles.Count;
                //Set number of trianble points to the number of triangles times 3
                numTrianglePoints = numTriangles * 3U;
            }
        }

        bool HasTrianglesCalc(NifInfo info) => triangles.Count > 0;
        //--END:CUSTOM--//
    }

}