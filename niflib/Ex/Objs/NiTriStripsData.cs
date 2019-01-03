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

    /*! Holds mesh data using strips of triangles. */
    public class NiTriStripsData : NiTriBasedGeomData
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiTriStripsData", NiTriBasedGeomData.TYPE);
        /*! Number of OpenGL triangle strips that are present. */
        internal ushort numStrips;
        /*! The number of points in each triangle strip. */
        internal IList<ushort> stripLengths;
        /*! Do we have strip point data? */
        internal bool hasPoints;
        /*!
         * The points in the Triangle strips.  Size is the sum of all entries in Strip
         * Lengths.
         */
        internal IList<ushort[]> points;

        public NiTriStripsData()
        {
            numStrips = (ushort)0;
            hasPoints = false;
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
        public static NiObject Create() => new NiTriStripsData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out numStrips, s, info);
            stripLengths = new ushort[numStrips];
            for (var i1 = 0; i1 < stripLengths.Count; i1++)
            {
                Nif.NifStream(out stripLengths[i1], s, info);
            }
            if (info.version >= 0x0A000103)
            {
                Nif.NifStream(out hasPoints, s, info);
            }
            if (info.version <= 0x0A000102)
            {
                points = new ushort[numStrips];
                for (var i2 = 0; i2 < points.Count; i2++)
                {
                    points[i2].Resize(stripLengths[i2]);
                    for (var i3 = 0; i3 < stripLengths[i2]; i3++)
                    {
                        Nif.NifStream(out points[i2][i3], s, info);
                    }
                }
            }
            if (info.version >= 0x0A000103)
            {
                if (hasPoints)
                {
                    points = new ushort[numStrips];
                    for (var i3 = 0; i3 < points.Count; i3++)
                    {
                        points[i3].Resize(stripLengths[i3]);
                        for (var i4 = 0; i4 < stripLengths[i3]; i4++)
                        {
                            Nif.NifStream(out (ushort)points[i3][i4], s, info);
                        }
                    }
                }
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            for (var i1 = 0; i1 < points.Count; i1++)
                stripLengths[i1] = (ushort)points[i1].Count;
            numStrips = (ushort)stripLengths.Count;
            Nif.NifStream(numStrips, s, info);
            for (var i1 = 0; i1 < stripLengths.Count; i1++)
            {
                Nif.NifStream(stripLengths[i1], s, info);
            }
            if (info.version >= 0x0A000103)
            {
                Nif.NifStream(hasPoints, s, info);
            }
            if (info.version <= 0x0A000102)
            {
                for (var i2 = 0; i2 < points.Count; i2++)
                {
                    for (var i3 = 0; i3 < stripLengths[i2]; i3++)
                    {
                        Nif.NifStream(points[i2][i3], s, info);
                    }
                }
            }
            if (info.version >= 0x0A000103)
            {
                if (hasPoints)
                {
                    for (var i3 = 0; i3 < points.Count; i3++)
                    {
                        for (var i4 = 0; i4 < stripLengths[i3]; i4++)
                        {
                            Nif.NifStream((ushort)points[i3][i4], s, info);
                        }
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
            for (var i1 = 0; i1 < points.Count; i1++)
                stripLengths[i1] = (ushort)points[i1].Count;
            numStrips = (ushort)stripLengths.Count;
            s.AppendLine($"  Num Strips:  {numStrips}");
            array_output_count = 0;
            for (var i1 = 0; i1 < stripLengths.Count; i1++)
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
                s.AppendLine($"    Strip Lengths[{i1}]:  {stripLengths[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Has Points:  {hasPoints}");
            array_output_count = 0;
            for (var i1 = 0; i1 < points.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                for (var i2 = 0; i2 < stripLengths[i1]; i2++)
                {
                    if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                    {
                        break;
                    }
                    s.AppendLine($"      Points[{i2}]:  {points[i1][i2]}");
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
         * A constructor which can be used to create a NiTriStripsData and initialize it with triangles.
         * \param[in] tris The triangles to use to initialize the NiTriShapeData with.
         * \param[in] nvtristrips Whether or not to use the NvTriStrips library from nVidia to generate triangle strips from the given data.
         */
        public NiTriStripsData(IList<Triangle> tris, bool nvtristrips = true)
        {
            if (nvtristrips) SetNvTriangles(tris);
            else SetTSTriangles(tris);
        }

        //--Counts--//

        /*!
         * Gets or sets the triangle strips array.  If the new size is smaller, strips at the end of the array will be deleted.
         * \param n The new size of the triangle strips array.
         * \sa NiTriStripData::GetStripCount
         */
        public short StripCount
        {
            get => (short)points.Count;
            set
            {
                points.Resize(value);
                stripLengths.Resize(value);
                hasPoints = value != 0;
                //Recalculate Triangle Count
                numTriangles = CalcTriangleCount();
            }
        }

        //--Getters--//

        /*!
         * Used to retrieve all the triangles from a specific triangle strip.
         * \param index The index of the triangle strip to retrieve the triangles from.  This is a zero-based index which must be a positive number less than that returned by NiTriStripsData::GetStripCount.
         * \return A vector containing all the triangle faces from the triangle strip specified by index.
         * \sa NiTriStripData::SetStrip, NiTriStripData::GetTriangles
         */
        public IList<ushort> GetStrip(int index) => points[index];

        /*!
         * This is a conveniance function which returns all triangle faces in all triangle strips that make up this mesh.  It is similar to the ITriShapeData::GetTriangles function.
         * \return A vector containing all the triangle faces from all the triangle strips that make up this mesh.
         * \sa NiTriStripData::GetTriangles, NiTriStripData::GetStrip, NiTriStripData::SetStrip
         */
        IList<Triangle> GetTriangles()
        {
            //Create a vector to hold the triangles
            vector<Triangle> triangles;
            int n = 0; // Current triangle

            //Cycle through all strips
            vector < vector < unsigned short> >::const_iterator it;
            Triangle t;
            for (it = points.begin(); it != points.end(); ++it)
            {
                //The first three values in the strip are the first triangle
                t.Set((*it)[0], (*it)[1], (*it)[2]);

                //Only add triangles to the list if none of the vertices match
                if (t[0] != t[1] && t[0] != t[2] && t[1] != t[2])
                {
                    triangles.push_back(t);
                }

                //Move to the next triangle
                ++n;

                //The remaining triangles use the previous two indices as their first two indices.
                for (unsigned int i = 3; i < it->size(); ++i)
                {
                    //Odd numbered triangles need to be reversed to keep the vertices in counter-clockwise order
                    if (i % 2 == 0)
                    {
                        t.Set((*it)[i - 2], (*it)[i - 1], (*it)[i]);
                    }
                    else
                    {
                        t.Set((*it)[i], (*it)[i - 1], (*it)[i - 2]);
                    }

                    //Only add triangles to the list if none of the vertices match
                    if (t[0] != t[1] && t[0] != t[2] && t[1] != t[2])
                    {
                        triangles.push_back(t);
                    }

                    //Move to the next triangle
                    ++n;
                }
            }

            return triangles;
        }
        //--Setter--/

        /*!
         * Used to set the triangle face data in a specific triangle strip.
         * \param index The index of the triangle strip to set the face data for.  This is a zero-based index which must be a positive number less than that returned by NiTriStripsData::GetStripCount.
         * \param in The vertex indices that make up this strip, in standard OpenGL triangle strip order.
         * \sa NiTriStripData::GetStrip, NiTriStripData::GetTriangles
         */
        public void SetStrip(int index, IList<ushort> value)
        {
            points[index] = value;
            //Recalculate Triangle Count
            numTriangles = CalcTriangleCount();
        }

        /*!
         * Replaces the triangle face data in this mesh with new data.
         * \param in A vector containing the new face data.  Maximum size is 65,535.
         * \sa GetTriangles
         */
        public virtual void SetTriangles(IList<Triangle> value) => SetNvTriangles(value);

        void SetNvTriangles(IList<Triangle> value)
        {
            if (value.Count > 65535 || value.Count < 0)
                throw new Exception("Invalid Triangle Count: must be between 0 and 65535.");
            points.Clear();
            numTriangles = 0;

            var data = new ushort[value.Count * 3 * 2];
            for (var i = 0; i < value.Count; i++)
            {
                data[i * 3 + 0] = value[i][0];
                data[i * 3 + 1] = value[i][1];
                data[i * 3 + 2] = value[i][2];
            }

            PrimitiveGroup groups = 0;
            ushort numGroups = 0;

            // GF 3+
            CacheSize = CACHESIZE_GEFORCE3;
            // don't generate hundreds of strips
            SetStitchStrips = true;
            GenerateStrips(data, (int)(value.Count * 3), out groups, out numGroups);
            data = null;

            if (!groups)
                return;

            SetStripCount(numGroups);
            for (var g = 0; g < numGroups; g++)
                if (groups[g].type == PT_STRIP)
                {
                    strip = ushort[groups[g].numIndices];
                    for (var s = 0; s < groups[g].numIndices; s++)
                        strip[s] = groups[g].indices[s];
                    SetStrip(g, strip);
                }
            groups = null;

            //Recalculate Triangle Count
            numTriangles = CalcTriangleCount();
        }

        void SetTSTriangles(IList<Triangle> value)
        {
            if (value.Count > 65535 || value.Count < 0)
                throw new Exception("Invalid Triangle Count: must be between 0 and 65535.");
            points.Clear();
            numTriangles = 0;

            //var strips = new TriStrips();
            //triangle_stripper::indices idcs(value.Count *3);
            int i, j;
            for (i = 0; i < value.Count; i++)
            {
                idcs[i * 3 + 0] = value[i][0];
                idcs[i * 3 + 1] = value[i][1];
                idcs[i * 3 + 2] = value[i][2];
            }

            tri_stripper stripper(idcs);

            primitive_vector groups;
            stripper.Strip(groups);

            // triangles left over
            var stris = new List<Triangle>();

            for (i = 0; i < groups.size(); i++)
            {
                if (groups[i].Type == TRIANGLE_STRIP)
                {
                    var strip = new TriStrip((ushort)(groups[i].Indices.size()))
                    strips.Add(strip);
                    for (j = 0; j < groups[i].Indices.size(); j++)
                        strip[j] = groups[i].Indices[j];
                }
                else
                {
                    var size = stris.Count;
                    stris.Resize(size + groups[i].Indices.size() / 3);
                    for (j = (size > 0) ? (size - 1) : 0; j < stris.Count; j++)
                    {
                        stris[j][0] = groups[i].Indices[j * 3 + 0];
                        stris[j][1] = groups[i].Indices[j * 3 + 1];
                        stris[j][2] = groups[i].Indices[j * 3 + 2];
                    }
                }
            }

            if (stris.Count != 0)
            {
                // stitch em
                var strip = new TriStrip();
                if (strips.size() > 0)
                {
                    strip.push_back(strips.back()[strips.back().size() - 1]);
                    strip.push_back(stris[0][0]);
                }
                for (i = 0; i < stris.Count; i++)
                {
                    if (i > 0)
                    {
                        strip.push_back(stris[i][0]);
                        strip.push_back(stris[i][0]);
                    }

                    strip.push_back(stris[i][0]);
                    strip.push_back(stris[i][1]);
                    strip.push_back(stris[i][2]);
                    if (i < stris.Count - 1)
                        strip.push_back(stris[i][2]);
                }
                strips.push_back(strip);
            }

            if (strips.Count > 0)
            {
                StripCount = strips.Count;
                var i = 0;
                foreach (var it in strips)
                    SetStrip(i++, it);
            }

            //Recalculate Triangle Count
            numTriangles = CalcTriangleCount();
        }

        ushort CalcTriangleCount()
        {
            //Calculate number of triangles
            //Sum of length of each strip - 2
            ushort numTriangles = 0;
            for (var i = 0; i < points.Count; ++i)
                numTriangles += (ushort)(points[i].Length - 2);
            return numTriangles;
        }
        //--END:CUSTOM--//

    }

}