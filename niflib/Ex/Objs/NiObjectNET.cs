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
     * Abstract base class for NiObjects that support names, extra data, and time
     * controllers.
     */
    public class NiObjectNET : NiObject
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiObjectNET", NiObject.TYPE);
        /*! Configures the main shader path */
        internal BSLightingShaderPropertyShaderType skyrimShaderType;
        /*! Name of this controllable object, used to refer to the object in .kf files. */
        internal IndexString name;
        /*! Extra data for pre-3.0 versions. */
        internal bool hasOldExtraData;
        /*! (=NiStringExtraData) */
        internal IndexString oldExtraPropName;
        /*! ref */
        internal uint oldExtraInternalId;
        /*! Extra string data. */
        internal IndexString oldExtraString;
        /*! Always 0. */
        internal byte unknownByte;
        /*! Extra data object index. (The first in a chain) */
        internal NiExtraData extraData;
        /*! The number of Extra Data objects referenced through the list. */
        internal uint numExtraDataList;
        /*! List of extra data indices. */
        internal IList<NiExtraData> extraDataList;
        /*! Controller object index. (The first in a chain) */
        internal NiTimeController controller;

        public NiObjectNET()
        {
            skyrimShaderType = (BSLightingShaderPropertyShaderType)0;
            hasOldExtraData = false;
            oldExtraInternalId = (uint)0;
            unknownByte = (byte)0;
            extraData = null;
            numExtraDataList = (uint)0;
            controller = null;
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
        public static NiObject Create() => new NiObjectNET();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            if ((info.userVersion2 >= 83))
            {
                if (IsDerivedType(BSLightingShaderProperty.TYPE))
                {
                    Nif.NifStream(out skyrimShaderType, s, info);
                }
            }
            Nif.NifStream(out name, s, info);
            if (info.version <= 0x02030000)
            {
                Nif.NifStream(out hasOldExtraData, s, info);
                if (hasOldExtraData)
                {
                    Nif.NifStream(out oldExtraPropName, s, info);
                    Nif.NifStream(out oldExtraInternalId, s, info);
                    Nif.NifStream(out oldExtraString, s, info);
                }
                Nif.NifStream(out unknownByte, s, info);
            }
            if ((info.version >= 0x03000000) && (info.version <= 0x04020200))
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }
            if (info.version >= 0x0A000100)
            {
                Nif.NifStream(out numExtraDataList, s, info);
                extraDataList = new Ref[numExtraDataList];
                for (var i2 = 0; i2 < extraDataList.Count; i2++)
                {
                    Nif.NifStream(out block_num, s, info);
                    link_stack.Add(block_num);
                }
            }
            if (info.version >= 0x03000000)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numExtraDataList = (uint)extraDataList.Count;
            if ((info.userVersion2 >= 83))
            {
                if (IsDerivedType(BSLightingShaderProperty.TYPE))
                {
                    Nif.NifStream(skyrimShaderType, s, info);
                }
            }
            Nif.NifStream(name, s, info);
            if (info.version <= 0x02030000)
            {
                Nif.NifStream(hasOldExtraData, s, info);
                if (hasOldExtraData)
                {
                    Nif.NifStream(oldExtraPropName, s, info);
                    Nif.NifStream(oldExtraInternalId, s, info);
                    Nif.NifStream(oldExtraString, s, info);
                }
                Nif.NifStream(unknownByte, s, info);
            }
            if ((info.version >= 0x03000000) && (info.version <= 0x04020200))
            {
                WriteRef((NiObject)extraData, s, info, link_map, missing_link_stack);
            }
            if (info.version >= 0x0A000100)
            {
                Nif.NifStream(numExtraDataList, s, info);
                for (var i2 = 0; i2 < extraDataList.Count; i2++)
                {
                    WriteRef((NiObject)extraDataList[i2], s, info, link_map, missing_link_stack);
                }
            }
            if (info.version >= 0x03000000)
            {
                WriteRef((NiObject)controller, s, info, link_map, missing_link_stack);
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
            numExtraDataList = (uint)extraDataList.Count;
            if (IsDerivedType(BSLightingShaderProperty.TYPE))
            {
                s.AppendLine($"    Skyrim Shader Type:  {skyrimShaderType}");
            }
            s.AppendLine($"  Name:  {name}");
            s.AppendLine($"  Has Old Extra Data:  {hasOldExtraData}");
            if (hasOldExtraData)
            {
                s.AppendLine($"    Old Extra Prop Name:  {oldExtraPropName}");
                s.AppendLine($"    Old Extra Internal Id:  {oldExtraInternalId}");
                s.AppendLine($"    Old Extra String:  {oldExtraString}");
            }
            s.AppendLine($"  Unknown Byte:  {unknownByte}");
            s.AppendLine($"  Extra Data:  {extraData}");
            s.AppendLine($"  Num Extra Data List:  {numExtraDataList}");
            array_output_count = 0;
            for (var i1 = 0; i1 < extraDataList.Count; i1++)
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
                s.AppendLine($"    Extra Data List[{i1}]:  {extraDataList[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Controller:  {controller}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            if ((info.version >= 0x03000000) && (info.version <= 0x04020200))
            {
                extraData = FixLink<NiExtraData>(objects, link_stack, missing_link_stack, info);
            }
            if (info.version >= 0x0A000100)
            {
                for (var i2 = 0; i2 < extraDataList.Count; i2++)
                {
                    extraDataList[i2] = FixLink<NiExtraData>(objects, link_stack, missing_link_stack, info);
                }
            }
            if (info.version >= 0x03000000)
            {
                controller = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (extraData != null)
                refs.Add((NiObject)extraData);
            for (var i1 = 0; i1 < extraDataList.Count; i1++)
            {
                if (extraDataList[i1] != null)
                    refs.Add((NiObject)extraDataList[i1]);
            }
            if (controller != null)
                refs.Add((NiObject)controller);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            for (var i1 = 0; i1 < extraDataList.Count; i1++)
            {
            }
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        public string Name
        {
            get => name;
            set => name = value;
        }

        /*!
         * Formats a human readable string that includes the type of the object
         * \return A string in the form:  address(type) {name}
         */
        public virtual string IDString => $"{NiObject.IDString} {name}";

        /*! 
         * Adds an extra data object to this one.  The way this data is stored changed after version 10.0.1.0, so the version
         * can optionally be included to specify the old storage method.
         * \param[in] obj The NiExtraData object to attach to this object.
         * \param[in] version The way the extra data is arranged internally varies with the NIF version, so if a file is to be written, it is best to pass the intended version.  The default is 10.0.1.0, which specifies the newer behavior.
         */
        public void AddExtraData(NiExtraData obj, uint version = Nif.VER_10_0_1_0)
        {
            if (version >= Nif.VER_10_0_1_0)
                //In later versions, extra data is just stored in a vector
                extraDataList.Add(obj);
            else
            {
                //In earlier versions, extra data is a singly linked list
                //Insert at begining of list
                obj.NextExtraData = extraData;
                extraData = obj;
            }
        }

        /*! 
         * Removes an extra data object to this one.
         * \param[in] obj The NiExtraData object to remove from this object.
         */
        public void RemoveExtraData(NiExtraData obj)
        {
            //Search both types of extra data list for the one to remove
            extraDataList.Remove(obj);
            //
            NiExtraData lastExtra = null;
            var extra = extraData;
            while (extra != null)
            {
                if (extra == obj)
                {
                    //Cut this reference out of the list
                    if (lastExtra == null) extra = extraData = extra.NextExtraData;
                    else extra = lastExtra.NextExtraData = extra.NextExtraData;
                    return;
                }
                lastExtra = extra;
                extra = extra.NextExtraData;
            }
        }

        /*! 
         * Changes the internal storage method of the extra data in preparation for writing to a file.  This is only necessary if the
         * extra data was added in one way and needs to be output in another.  This would happen if extra data was loaded from an old file and needed to be written to a file with a newer version.
         * \param[in] version Specifies the NIF version that the internal data should be arranged for.
         */
        public void ShiftExtraData(uint version = Nif.VER_10_0_1_0)
        {
            //Shift any extra data references that are stored in a way that doesn't match
            //the requested version to the way that does
            if (version >= Nif.VER_10_0_1_0)
            {
                //In later versions, extra data is just stored in a vector
                //Empty the linked list into the vector
                var extra = extraData;
                while (extra != null)
                {
                    extra.NextExtraData = null;
                    extraDataList.Add(extra);
                    extra = extra.NextExtraData;
                }
                extraData = null;
            }
            else
            {
                //In earlier versions, extra data is a singly linked list
                //Insert at begining of list
                //Empty the list into the linked list
                foreach (var it in extraDataList)
                {
                    it.NextExtraData = extraData;
                    extraData = it;
                }
                extraDataList.Clear();
            }
        }

        /*!
         * Removes all extra data from this object.
         */
        public void ClearExtraData()
        {
            extraDataList.Clear();
            extraData = null;
        }

        /*!
         * Returns a list of references to all the extra data referenced by this object.
         * \return All the extra data objects referenced by this one.
         */
        public IList<NiExtraData> GetExtraData()
        {
            var extras = new List<NiExtraData>();
            foreach (var it in extraDataList)
                extras.Add(it);
            var extra = extraData;
            while (extra != null)
            {
                extras.Add(extra);
                extra = extra.NextExtraData;
            }
            return extras;
        }

        /*!
         * Used to determine whether this object is animated.  In other words, whether it has any controllers.
         * \return True if the object has controllers, false otherwise.
         */
        public bool IsAnimated => controller != null;

        /*!
         * Adds a controller to this object.  Controllers allow various properties to be animated over time.
         * \param[in] obj The controller to add.
         */
        public void AddController(NiTimeController obj)
        {
            //Insert at begining of list
            obj.Target = this;
            obj.NextController = controller;
            controller = obj;
        }

        /*!
         * Removes a controller from this object.  Controllers allow various properties to be animated over time.
         * \param[in] obj The controller to remove.
         */
        public void RemoveController(NiTimeController obj)
        {
            for (NiTimeController last = controller, cont = last, next; cont != null; cont = next)
            {
                next = cont.NextController;
                if (cont == obj)
                {
                    //Cut this reference out of the list
                    cont.Target = null;
                    cont.NextController = new NiTimeController();
                    if (cont == controller) controller = next;
                    else last.NextController = next;
                }
                //Advance last to current controller
                else last = cont;
            }
        }

        /*!
         * Removes all controllers from this object.  This will remove any animation from it.
         */
        public void ClearControllers()
        {
            var cont = controller;
            while (cont != null)
            {
                cont.Target = null;
                cont = cont.NextController;
            }
        }

        /*!
         * Gets a list of all controllers affecting this object.
         * \return All the controllers affecting this object.
         */
        public IList<NiTimeController> GetControllers()
        {
            var conts = new List<NiTimeController>();
            var cont = controller;
            while (cont != null)
            {
                conts.Add(cont);
                cont = cont.NextController;
            }
            return conts;
        }

        /*!
        * Gets or sets the skyrim shader type
        * \param[in] The new skyrim shader value
        */
        public BSLightingShaderPropertyShaderType SkyrimShaderType
        {
            get => skyrimShaderType;
            set => skyrimShaderType = value;
        }
        //--END:CUSTOM--//

    }

}