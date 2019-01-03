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

    /*! Gives specularity to a shape. Flags 0x0001. */
    public class NiSpecularProperty : NiProperty
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiSpecularProperty", NiProperty.TYPE);
        /*! Bit 0 = Enable specular lighting on this shape. */
        internal ushort flags;

        public NiSpecularProperty()
        {
            flags = (ushort)0;
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
        public static NiObject Create() => new NiSpecularProperty();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            Nif.NifStream(out flags, s, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            Nif.NifStream(flags, s, info);

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            s.Append(base.AsString());
            s.AppendLine($"  Flags:  {flags}");
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
         * Gets or sets whether this specular property enables or disables specular lighting.
         * \param[in] n True to enable specular lighting with this property, false to disable it.
         */
        public bool SpecularState
        {
            get => Nif.UnpackFlag(flags, 0);
            set => Nif.PackFlag(flags, value, 0);
        }

        /*!
         * Gets or sets the data stored in the flags field for this object.  It is usually better to call more specific flag-toggle functions if they are availiable.
         * \param[in] n The new flag data.  Will overwrite any existing flag data.
         */
        public ushort Flags
        {
            get => flags;
            set => flags = value;
        }
        //--END:CUSTOM--//
    }

}