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

    /*! Allows control of stencil testing. */
    public class NiStencilProperty : NiProperty
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiStencilProperty", NiProperty.TYPE);
        /*! Property flags. */
        internal ushort flags;
        /*! Enables or disables the stencil test. */
        internal byte stencilEnabled;
        /*! Selects the compare mode function (see: glStencilFunc). */
        internal StencilCompareMode stencilFunction;
        /*!  */
        internal uint stencilRef;
        /*! A bit mask. The default is 0xffffffff. */
        internal uint stencilMask;
        /*!  */
        internal StencilAction failAction;
        /*!  */
        internal StencilAction zFailAction;
        /*!  */
        internal StencilAction passAction;
        /*! Used to enabled double sided faces. Default is 3 (DRAW_BOTH). */
        internal StencilDrawMode drawMode;

        public NiStencilProperty()
        {
            flags = (ushort)0;
            stencilEnabled = (byte)0;
            stencilFunction = (StencilCompareMode)0;
            stencilRef = (uint)0;
            stencilMask = (uint)4294967295;
            failAction = (StencilAction)0;
            zFailAction = (StencilAction)0;
            passAction = (StencilAction)0;
            drawMode = StencilDrawMode.DRAW_BOTH;
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
        public static NiObject Create() => new NiStencilProperty();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            base.Read(s, link_stack, info);
            if (info.version <= 0x0A000102)
            {
                Nif.NifStream(out flags, s, info);
            }
            if (info.version <= 0x14000005)
            {
                Nif.NifStream(out stencilEnabled, s, info);
                Nif.NifStream(out stencilFunction, s, info);
                Nif.NifStream(out stencilRef, s, info);
                Nif.NifStream(out stencilMask, s, info);
                Nif.NifStream(out failAction, s, info);
                Nif.NifStream(out zFailAction, s, info);
                Nif.NifStream(out passAction, s, info);
                Nif.NifStream(out drawMode, s, info);
            }
            if (info.version >= 0x14010003)
            {
                Nif.NifStream(out (ushort)flags, s, info);
                Nif.NifStream(out (uint)stencilRef, s, info);
                Nif.NifStream(out (uint)stencilMask, s, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            if (info.version <= 0x0A000102)
            {
                Nif.NifStream(flags, s, info);
            }
            if (info.version <= 0x14000005)
            {
                Nif.NifStream(stencilEnabled, s, info);
                Nif.NifStream(stencilFunction, s, info);
                Nif.NifStream(stencilRef, s, info);
                Nif.NifStream(stencilMask, s, info);
                Nif.NifStream(failAction, s, info);
                Nif.NifStream(zFailAction, s, info);
                Nif.NifStream(passAction, s, info);
                Nif.NifStream(drawMode, s, info);
            }
            if (info.version >= 0x14010003)
            {
                Nif.NifStream((ushort)flags, s, info);
                Nif.NifStream((uint)stencilRef, s, info);
                Nif.NifStream((uint)stencilMask, s, info);
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
            s.Append(base.AsString());
            s.AppendLine($"  Flags:  {flags}");
            s.AppendLine($"  Stencil Enabled:  {stencilEnabled}");
            s.AppendLine($"  Stencil Function:  {stencilFunction}");
            s.AppendLine($"  Stencil Ref:  {stencilRef}");
            s.AppendLine($"  Stencil Mask:  {stencilMask}");
            s.AppendLine($"  Fail Action:  {failAction}");
            s.AppendLine($"  Z Fail Action:  {zFailAction}");
            s.AppendLine($"  Pass Action:  {passAction}");
            s.AppendLine($"  Draw Mode:  {drawMode}");
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
         * Gets or sets the data stored in the flags field for this object.  It is usually better to call more specific flag-toggle functions if they are availiable.
         * \param[in] value The new flag data.  Will overwrite any existing flag data.
         */
        public ushort Flags
        {
            get => flags;
            set => flags = value;
        }

        /*!
         * Gets or sets the stencil state buffer state.  This determines whether or not the stencil buffer is being used.
         * \param[in] value True to enable the stencil, false to disable it.
         */
        public bool StencilState
        {
            get => stencilEnabled != 0;
            set => stencilEnabled = (byte)(value ? 1 : 0);
        }

        /*!
         * Gets or sets the current stencil buffer comparison function.  This function determines whether a particular pixel will be drawn based on the contents of the stencil buffer at that location.
         * \param[in] value The new stencil buffer comparison function.
         */
        public StencilCompareMode StencilFunction
        {
            get => stencilFunction;
            set => stencilFunction = value;
        }

        /*!
         * Gets or sets the current reference value used in the stencil test.  This is the value that the stencil function compares against to determine whether a pixel is drawn.
         * \param[in] value The new stencil reference value.
         */
        public uint StencilRef
        {
            get => stencilRef;
            set => stencilRef = value;
        }

        /*!
         * Gets or sets the bit mask used in the stencil test.  The reference value and the stored stencil value are both bitwise ANDed with this mask before being compared.  The default is 0xFFFFFFFF.
         * \param[in] value The new stencil test bit mask.
         */
        public uint StencilMask
        {
            get => stencilMask;
            set => stencilMask = value;
        }

        /*!
         * Gets or sets the action that will occur if the stencil test fails (evaluates to false).  This involves whether the stencil buffer will be altered and how.
         * \param[in] value The new action that occur if the stencil test fails.
         */
        public StencilAction FailAction
        {
            get => failAction;
            set => failAction = value;
        }

        /*!
         * Gets or sets the action that will occur if the depth buffer (Z-buffer) fails (evaluates to false).  This involves whether the stencil buffer will be altered and how.
         * \param[in] value The new action that occur if the depth buffer fails.
         */
        public StencilAction ZFailAction
        {
            get => zFailAction;
            set => zFailAction = value;
        }

        /*!
         * Gets or sets the action that will occur if the depth buffer (Z-buffer) test passes (evaluate to true).  This involves whether the stencil buffer will be altered and how.
         * \param[in] value The new action that occur if the depth buffer test passes.
         */
        public StencilAction PassAction
        {
            get => passAction;
            set => passAction = value;
        }

        /*!
         * Gets or sets whether the front, back, or both sides of triangles will be drawn.   This isn't related to the stencil buffer, but happens to be included in this propery, probably for conveniance.
         * \param[in] value The new face drawing mode.
         */
        public StencilDrawMode FaceDrawMode
        {
            get => drawMode;
            set => drawMode = value;
        }
    //--END:CUSTOM--//
}

}