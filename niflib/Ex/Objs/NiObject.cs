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

    /*! Abstract object type. */
    public class NiObject : RefObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiObject", RefObject.TYPE);

        public NiObject()
        {
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
        public static NiObject Create() => new NiObject();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {


        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {


        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {


        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            return ptrs;
        }

        //--BEGIN:FILE FOOT--//
        /*! Returns A new object that contains all the same data that this object does,
         * but occupies a different part of memory.  The data stored in a NIF file varies
         * from version to version.  Usually you are safe with the default option
         * (the highest availiable version) but you may need to use an earlier version
         * if you need to clone an obsolete piece of information.
         * \param[in] version The version number to use in the memory streaming operation.  Default is the highest version availiable.
         * \param[in] user_version The game-specific version number extention.
         * \return A cloned copy of this object as a new object.
         */
        public NiObject Clone(uint version = 0xFFFFFFFF, uint user_version = 0)
        {
            //Create a string stream to temporarily hold the state-save of this object
            var tmp = new OStream();

            //Create a new object of the same type
            var clone = ObjectRegistry.CreateObject(GetType().GetTypeName());

            //Dummy map
            var link_map = new Dictionary<NiObject, uint>();

            //Write this object's data to the stream
            var info = new NifInfo(version, user_version);
            var missing_link_stack = new List<NiObject>();
            Write(tmp, link_map, missing_link_stack, info);

            //Dummy stack
            var link_stack = new List<uint>();

            //Read the data back from the stream into the clone
            clone.Read(tmp, link_stack, info);

            //We don't fix the links, causing the clone to be a copy of all
            //data but have none of the linkage of the original.

            //return new object
            return clone;
        }

        /*! Block number in the nif file. Only set when you read blocks from the file. */
        public int internal_block_number;
        //--END:CUSTOM--//
    }

}