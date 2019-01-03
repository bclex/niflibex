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

    /*! Uses NiPosKeys to animate an NiPoint3 value over time. */
    public class NiPoint3Interpolator : NiKeyBasedInterpolator
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiPoint3Interpolator", NiKeyBasedInterpolator.TYPE);
        /*! Pose value if lacking NiPosData. */
        internal Vector3 value;
        /*!  */
        internal NiPosData data;

        public NiPoint3Interpolator()
        {
            value = -3.402823466e+38, -3.402823466e+38, -3.402823466e+38;
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
        public static NiObject Create() => new NiPoint3Interpolator();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out value, s, info);
            Nif.NifStream(out block_num, s, info);
            link_stack.Add(block_num);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            Nif.NifStream(value, s, info);
            WriteRef((NiObject)data, s, info, link_map, missing_link_stack);

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
            s.AppendLine($"  Value:  {value}");
            s.AppendLine($"  Data:  {data}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            data = FixLink<NiPosData>(objects, link_stack, missing_link_stack, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (data != null)
                refs.Add((NiObject)data);
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
         * Sets the 3D point value stored in this object.  Perhaps this is the current interpolated value, the value when posed, or at time index 0.
         * \param[in] value The new 3D point value to store in this object.
         */
        public Vector3 Point3Value
        {
            get => value;
            set => this.value = value;
        }

        /*!
         * Gets or sets the NiPosData object that this interpolator links to, if any.
         * \return The NiPosData object that this interpolator should now link to, or NULL to clear the current one.
         */
        public NiPosData Data
        {
            get => data;
            set => data = value;
        }

        /*!
         * This function will adjust the times in all the keys stored in the data
         * objects referenced by this interpolator such that phase will equal 0 and
         * frequency will equal one.  In other words, it will cause the key times
         * to be in seconds starting from zero.
         * \param[in] phase The phase shift to remove from any keys stored in this
         * object.
         * \param[in] frequency The frequency to normalize to 1.0 for any keys
         * stored in this object
         */
        public virtual void NormalizeKeys(float phase, float frequency)
        {
            if (data != null)
                data.NormalizeKeys(phase, frequency);
        }
        //--END:CUSTOM--//

    }

}