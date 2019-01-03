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
     * Abstract base class that provides the base timing and update functionality for
     * all the Gamebryo animation controllers.
     */
    public class NiTimeController : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiTimeController", NiObject.TYPE);
        /*! Index of the next controller. */
        internal NiTimeController nextController;
        /*!
         * Controller flags.
         *             Bit 0 : Anim type, 0=APP_TIME 1=APP_INIT
         *             Bit 1-2 : Cycle type, 00=Loop 01=Reverse 10=Clamp
         *             Bit 3 : Active
         *             Bit 4 : Play backwards
         *             Bit 5 : Is manager controlled
         *             Bit 6 : Compute scaled time (take frequency and phase into account)
         *             Bit 7 : Force update
         */
        internal ushort flags;
        /*! Frequency (is usually 1.0). */
        internal float frequency;
        /*! Phase (usually 0.0). */
        internal float phase;
        /*! Controller start time. */
        internal float startTime;
        /*! Controller stop time. */
        internal float stopTime;
        /*!
         * Controller target (object index of the first controllable ancestor of this
         * object).
         */
        internal NiObjectNET target;
        /*! Unknown integer. */
        internal uint unknownInteger;

        public NiTimeController()
        {
            nextController = null;
            flags = (ushort)0;
            frequency = 1.0f;
            phase = 0.0f;
            startTime = 3.402823466e+38f;
            stopTime = -3.402823466e+38f;
            target = null;
            unknownInteger = (uint)0;
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
        public static NiObject Create() => new NiTimeController();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out block_num, s, info);
            link_stack.Add(block_num);
            Nif.NifStream(out flags, s, info);
            Nif.NifStream(out frequency, s, info);
            Nif.NifStream(out phase, s, info);
            Nif.NifStream(out startTime, s, info);
            Nif.NifStream(out stopTime, s, info);
            if (info.version >= 0x0303000D)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }
            if (info.version <= 0x03010000)
            {
                Nif.NifStream(out unknownInteger, s, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            WriteRef((NiObject)nextController, s, info, link_map, missing_link_stack);
            Nif.NifStream(flags, s, info);
            Nif.NifStream(frequency, s, info);
            Nif.NifStream(phase, s, info);
            Nif.NifStream(startTime, s, info);
            Nif.NifStream(stopTime, s, info);
            if (info.version >= 0x0303000D)
            {
                WriteRef((NiObject)target, s, info, link_map, missing_link_stack);
            }
            if (info.version <= 0x03010000)
            {
                Nif.NifStream(unknownInteger, s, info);
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
            s.AppendLine($"  Next Controller:  {nextController}");
            s.AppendLine($"  Flags:  {flags}");
            s.AppendLine($"  Frequency:  {frequency}");
            s.AppendLine($"  Phase:  {phase}");
            s.AppendLine($"  Start Time:  {startTime}");
            s.AppendLine($"  Stop Time:  {stopTime}");
            s.AppendLine($"  Target:  {target}");
            s.AppendLine($"  Unknown Integer:  {unknownInteger}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            nextController = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
            if (info.version >= 0x0303000D)
            {
                target = FixLink<NiObjectNET>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (nextController != null)
                refs.Add((NiObject)nextController);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            if (target != null)
                ptrs.Add((NiObject)target);
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*! 
         * Gets or sets the next controller in a linked list.
         * This function should only be called by NiObjectNET.
         * \param obj A reference to the object to set as the one after this in the chain.
         */
        internal NiTimeController NextController
        {
            get => nextController;
            set => nextController = value;
        }

        /*! 
         * This function should only be called by NiObjectNET.  It sets the target of
         * this controller when it is attatched to the NiObjectNET class. */
        internal NiObjectNET Target
        {
            get => target;
            set => target = value;
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

        /*!
         * Sets the frequency value of this controller.  This is used to map the time indices stored in the controller to seconds.  The value is multiplied by the application time to arrive at the controller time, so the default value of 1.0 means that the times in the controller are already in seconds.  Calling this function with a new value will not cause any changes to the data stored in the controller.
         * \param[in] n The new frequency.
         */
        public float Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        /*!
         * Gets or sets the phase value of this controller.  This is used to map the time indices stored in the controller to seconds.  The value is is added to the result of the multiplication of application time by frequency to arrive at the controller time, so the default value of 0.0 means that there is no phase shift in the time indices.  Calling this function with a new value will not cause any changes to the data stored in the controller.
         * \param[in] n The new phase.
         */
        public float Phase
        {
            get => phase;
            set => phase = value;
        }

        /*!
         * This function will adjust the times in all the keys in the data objects
         * referenced by this controller and any of its interpolators such that the
         * phase will equal 0 and frequency will equal one.  In other words, it
         * will cause the key times to be in seconds starting from zero.
         */
        public virtual void NormalizeKeys()
        {
            //Normalize the start and stop times
            startTime = frequency * startTime + phase;
            stopTime = frequency * stopTime + phase;

            //Set phase to 0 and frequency to 1
            phase = 0.0f;
            frequency = 0.0f;
        }
        
        /*!
         * Gets or sets the time index where this controller begins to take affect.  If the animation type is set to wrap or cycle, the animation will not occur only between these time intervals but will be mapped to the right spot between them.  This value is in controller time, i.e. phase and frequency are applied to transform it to application time.
         * \param[in] n The new start time for the controller animation.
         */
        public float StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        /*!
         * Gets or Sets the time index where this controller stops taking affect.  If the animation type is set to wrap or cycle, the animation will not occur only between these time intervals but will be mapped to the right spot between them.  This value is in controller time, i.e. phase and frequency are applied to transform it to application time.
         * \param[in] n The new end time for the controller animation.
         */
        public float StopTime
        {
            get => stopTime;
            set => stopTime = value;
        }
        //--END:CUSTOM--//

    }

}