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

/*! Root node in Gamebryo .kf files (version 10.0.1.0 and up). */
public class NiControllerSequence : NiSequence {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiControllerSequence", NiSequence.TYPE);
	/*!
	 * The weight of a sequence describes how it blends with other sequences at the
	 * same priority.
	 */
	internal float weight;
	/*!  */
	internal NiTextKeyExtraData textKeys;
	/*!  */
	internal CycleType cycleType;
	/*!  */
	internal float frequency;
	/*!  */
	internal float phase;
	/*!  */
	internal float startTime;
	/*!  */
	internal float stopTime;
	/*!  */
	internal bool playBackwards;
	/*! The owner of this sequence. */
	internal NiControllerManager manager;
	/*!
	 * The name of the NiAVObject serving as the accumulation root. This is where all
	 * accumulated translations, scales, and rotations are applied.
	 */
	internal IndexString accumRootName;
	/*!  */
	internal AccumFlags accumFlags;
	/*!  */
	internal NiStringPalette stringPalette;
	/*!  */
	internal BSAnimNotes animNotes;
	/*!  */
	internal ushort numAnimNoteArrays;
	/*!  */
	internal IList<BSAnimNotes> animNoteArrays;

	public NiControllerSequence() {
	weight = 1.0f;
	textKeys = null;
	cycleType = (CycleType)0;
	frequency = 1.0f;
	phase = 0.0f;
	startTime = 3.402823466e+38f;
	stopTime = -3.402823466e+38f;
	playBackwards = false;
	manager = null;
	accumFlags = (AccumFlags)ACCUM_X_FRONT;
	stringPalette = null;
	animNotes = null;
	numAnimNoteArrays = (ushort)0;
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
public static NiObject Create() => new NiControllerSequence();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out weight, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out cycleType, s, info);
		Nif.NifStream(out frequency, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A040001)) {
		Nif.NifStream(out phase, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out startTime, s, info);
		Nif.NifStream(out stopTime, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A01006A)) {
		Nif.NifStream(out playBackwards, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out accumRootName, s, info);
	}
	if (info.version >= 0x14030008) {
		Nif.NifStream(out accumFlags, s, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		Nif.NifStream(out numAnimNoteArrays, s, info);
		animNoteArrays = new Ref[numAnimNoteArrays];
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numAnimNoteArrays = (ushort)animNoteArrays.Count;
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(weight, s, info);
		WriteRef((NiObject)textKeys, s, info, link_map, missing_link_stack);
		Nif.NifStream(cycleType, s, info);
		Nif.NifStream(frequency, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A040001)) {
		Nif.NifStream(phase, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(startTime, s, info);
		Nif.NifStream(stopTime, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A01006A)) {
		Nif.NifStream(playBackwards, s, info);
	}
	if (info.version >= 0x0A01006A) {
		WriteRef((NiObject)manager, s, info, link_map, missing_link_stack);
		Nif.NifStream(accumRootName, s, info);
	}
	if (info.version >= 0x14030008) {
		Nif.NifStream(accumFlags, s, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		WriteRef((NiObject)stringPalette, s, info, link_map, missing_link_stack);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		WriteRef((NiObject)animNotes, s, info, link_map, missing_link_stack);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		Nif.NifStream(numAnimNoteArrays, s, info);
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			WriteRef((NiObject)animNoteArrays[i2], s, info, link_map, missing_link_stack);
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
	numAnimNoteArrays = (ushort)animNoteArrays.Count;
	s.AppendLine($"  Weight:  {weight}");
	s.AppendLine($"  Text Keys:  {textKeys}");
	s.AppendLine($"  Cycle Type:  {cycleType}");
	s.AppendLine($"  Frequency:  {frequency}");
	s.AppendLine($"  Phase:  {phase}");
	s.AppendLine($"  Start Time:  {startTime}");
	s.AppendLine($"  Stop Time:  {stopTime}");
	s.AppendLine($"  Play Backwards:  {playBackwards}");
	s.AppendLine($"  Manager:  {manager}");
	s.AppendLine($"  Accum Root Name:  {accumRootName}");
	s.AppendLine($"  Accum Flags:  {accumFlags}");
	s.AppendLine($"  String Palette:  {stringPalette}");
	s.AppendLine($"  Anim Notes:  {animNotes}");
	s.AppendLine($"  Num Anim Note Arrays:  {numAnimNoteArrays}");
	array_output_count = 0;
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Anim Note Arrays[{i1}]:  {animNoteArrays[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A01006A) {
		textKeys = FixLink<NiTextKeyExtraData>(objects, link_stack, missing_link_stack, info);
		manager = FixLink<NiControllerManager>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		stringPalette = FixLink<NiStringPalette>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		animNotes = FixLink<BSAnimNotes>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			animNoteArrays[i2] = FixLink<BSAnimNotes>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (textKeys != null)
		refs.Add((NiObject)textKeys);
	if (stringPalette != null)
		refs.Add((NiObject)stringPalette);
	if (animNotes != null)
		refs.Add((NiObject)animNotes);
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
		if (animNoteArrays[i1] != null)
			refs.Add((NiObject)animNoteArrays[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (manager != null)
		ptrs.Add((NiObject)manager);
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//
        protected NiControllerManager Parent
        {
            get => manager;
            set => manager = value;
        }

        /*!
         * Gets or sets the name of this NiControllerSequence object. This is also the name of the action
         * associated with this file. For instance, if the original NIF file is called
         * "demon.nif" and this animation file contains an attack sequence, then
         * the file would be called "demon_attack1.kf" and this field would
         * contain the string "attack1".
         * \param[in] value The new name for this NiControllerSequence object.
         */
        public string Name
        {
            get => name;
            set => name = value;
        }

        /*!
         * Sets the name and reference to the NiTextKeyExtraData object which will be used by this controller sequence to specify the keyframe labels or "notes."
         * \param[in] txt_key A reference to the NiTextKeyExtraData object to use.
         * \sa NiTextKeyExtraData
         */
        public void SetTextKey(NiTextKeyExtraData txt_key)
        {
            //Set new name
            textKeysName = txt_key.Name;
            textKeys = txt_key;
        }

        /*! 
         * Attatches a controler to this KF file for a KF file of version 10.2.0.0 or below.  Versions above this use interpolators.
         * \param[in] obj A reference to the new NiTimeController to attach.
         * \sa NiControllerSequence::ClearChildren, NiControllerSequence::AddInterpolator
         */
        public void AddController(NiTimeController obj)
        {
            //Make sure the link isn't null
            if (obj == null)
                throw new Exception("Attempted to add a null controller to NiControllerSequence.");
            var target = obj.Target;
            if (target == null)
                throw new Exception("Controller must have a target to be added to a NiControllerSequence.");
            //Make a new ControlledBlock and fill out necessary data
            var cl = new ControlledBlock();
            cl.controller = obj;
            cl.targetName = target->GetName();
            cl.nodeName = target->GetName();

            var prop = target as NiProperty;
            if (prop != null)
                cl.propertyType = prop.GetType().GetTypeName();
            cl.controllerType = obj.GetType().GetTypeName();
            //Add finished ControllerLink to list
            controlledBlocks.Add(cl);
        }

        /*! 
        * Attaches a controler to this KF file for a KF file of version 10.2.0.0 or below.  Versions above this use interpolators.
        * \param[in] obj A reference to the new NiTimeController to attach.
        * \sa NiControllerSequence::ClearChildren, NiControllerSequence::AddInterpolator
        */
        public void AddController(string targetName, NiTimeController obj)
        {
            //Make sure the link isn't null
            if (obj == null)
                throw new Exception("Attempted to add a null controller to NiControllerSequence.");
            //Make a new ControllerLink and fill out necessary data
            var cl = new ControlledBlock();
            cl.controller = obj;
            cl.targetName = targetName;
            cl.nodeName = targetName;
            cl.controllerType = obj.GetType().GetTypeName();
            //Add finished ControllerLink to list
            controlledBlocks.Add(cl);
        }

        /*!
         * Attaches an interpolator to this KF file for a KF file of version greater than 10.2.0.0.  Versions below this use controllers.
         * \param[in] obj A reference to the new controller which has an interpolator to attach.
         * \param[in] priority Used only in Oblivion to set the priority of one controller over another when the two are merged.
         * \sa NiControllerSequence::ClearChildren, NiControllerSequence::AddController
         */
        public void AddInterpolator(NiSingleInterpController obj, byte priority = 0)
        {
            //Make sure the link isn't null
            if (obj == null)
                throw new Exception("Attempted to add a null controller to NiControllerSequence block.");
            var interp = obj.GetInterpolator();
            if (interp == null)
                throw new Exception("Controller must have an interpolator attached to be added to a NiControllerSequence with the AddInterpolator function.");
            var target = obj.Target;
            if (target == null)
                throw new Exception("Controller must have a target to be added to a NiControllerSequence.");
            //If there are existing ControllerLinks, use the same StringPalette they're using
            if (stringPalette == null)
                stringPalette = new NiStringPalette();
            //Make a new ControllerLink and fill out necessary data
            var cl = new ControlledBlock();
            cl.interpolator = interp;
            cl.priority = priority;
            cl.stringPalette = stringPalette;
            cl.nodeName = target->GetName();
            cl.nodeNameOffset = stringPalette.AddSubStr(target.GetName());
            var prop = target as NiProperty;
            if (prop != null)
                cl.propertyTypeOffset = stringPalette.AddSubStr(prop.GetType().GetTypeName());
            cl.controllerTypeOffset = stringPalette.AddSubStr(obj.GetType().GetTypeName());
            //Add finished ControllerLink to list
            controlledBlocks.Add(cl);
        }

        /*!
         * Attaches an interpolator to this KF file for a KF file of version greater than 10.2.0.0.  Versions below this use controllers.
         * \param[in] obj A reference to the new controller which has an interpolator to attach.
         * \param[in] priority Used only in Oblivion to set the priority of one controller over another when the two are merged.
         * \param[in] include_string_pallete Indicates if the resulting ControllerLinks will hold reference to the NiStringPallete in the NiControllerSequence
         * \sa NiControllerSequence::ClearChildren, NiControllerSequence::AddController
         */
        public void AddInterpolator(NiSingleInterpController obj, byte priority, bool include_string_pallete)
        {
            //Make sure the link isn't null
            if (obj == null)
                throw new Exception("Attempted to add a null controller to NiControllerSequence block.");
            var interp = obj.GetInterpolator();
            if (interp == null)
                throw new Exception("Controller must have an interpolator attached to be added to a NiControllerSequence with the AddInterpolator function.");
            var target = obj.Target;
            if (target == null)
                throw new Exception("Controller must have a target to be added to a NiControllerSequence.");
            //Make a new ControlledBlock and fill out necessary data
            var cl = new ControlledBlock();
            var prop = target as NiProperty;
            cl.interpolator = interp;
            cl.priority = priority;
            if (include_string_pallete)
            {
                //If there are existing ControllerLinks, use the same StringPalette they're using
                if (stringPalette == null)
                    stringPalette = new NiStringPalette();
                cl.stringPalette = stringPalette;
                cl.nodeNameOffset = stringPalette.AddSubStr(target.GetName());
                if (prop != null)
                    cl.propertyTypeOffset = stringPalette.AddSubStr(prop.GetType().GetTypeName());
                cl.controllerTypeOffset = stringPalette.AddSubStr(obj.GetType().GetTypeName());
            }
            else
            {
                cl.stringPalette = null;
                cl.nodeName = target.GetName();
                if (prop != null)
                    cl.propertyType = prop.GetType().GetTypeName();
                cl.controllerType = obj.GetType().GetTypeName();
            }
            //Add finished ControllerLink to list
            controlledBlocks.Add(cl);
        }

        /*!
         * Attaches a generic interpolator to this KF file for a KF file of version greater than 10.2.0.0.  Versions below this use controllers.
         * \param[in] interpolator A reference to the new interpolator to insert into the controllersequence
         * \param[in] target The target object that the controller which held the interpolator would act on
         * \param[in] controller_type_name The name of the type of the controller that held the interpolator
         * \param[in] priority Used only in Oblivion to set the priority of one controller over another when the two are merged.
         * \param[in] include_string_pallete Indicates if the resulting ControllerLinks will hold reference to the NiStringPallete in the NiControllerSequence
         * \sa NiControllerSequence::ClearChildren, NiControllerSequence::AddController
         */
        public void AddGenericInterpolator(NiInterpolator interpolator, NiObjectNET target, string controller_type_name, byte priority = 0, bool include_string_pallete = true)
        {
            //Make sure the parameters aren't null
            if (interpolator == null)
                throw new Exception("Attempted to add a null interpolator to the controller sequence");
            if (target == null)
                throw new Exception("Attempted to add a null target to the controller sequence");
            //Make a new ControllerLink and fill out necessary data
            var cl = new ControlledBlock();
            var prop = target as NiProperty;
            cl.interpolator = interpolator;
            cl.priority = priority;
            if (include_string_pallete)
            {
                //If there are existing ControllerLinks, use the same StringPalette they're using
                if (stringPalette == null)
                    stringPalette = new NiStringPalette();
                cl.stringPalette = stringPalette;
                cl.nodeNameOffset = stringPalette.AddSubStr(target.Name);
                if (prop != null)
                    cl.propertyTypeOffset = stringPalette.AddSubStr(prop.GetType().GetTypeName());
                cl.controllerTypeOffset = stringPalette.AddSubStr(controller_type_name);
            }
            else
            {
                cl.stringPalette = null;
                cl.nodeName = target.Name;
                if (prop != null)
                    cl.propertyType = prop.GetType().GetTypeName();
                cl.controllerType = controller_type_name;
            }
            //Add finished ControllerLink to list
            controlledBlocks.Add(cl);
        }

        /*! 
         * Removes all controllers and interpolators from this Kf file root object.
         * \sa NiControllerSequence::AddController, NiControllerSequence::AddInterpolator
         */
        public void ClearControllerData()
        {
            throw new Exception("The AddInterpolator function cannot be implemented until problems in the XML are solved.");
            //Clear list
            controlledBlocks.Clear();
        }

        /*!
        * Gets or sets the data for the controllers or interpolators which are attached to this controller sequence.
        * \return A vector containing the data for all controllers.
        * \sa NiControllerSequence::AddController, NiControllerSequence::AddInterpolator, NiControllerSequence::GetContollerData
        */
        public IList<ControlledBlock> ControllerData
        {
            get => controlledBlocks;
            set
            {
                if (value.Count != controlledBlocks.Count)
                    throw new Exception("The SetControllerData requires the ControllerLink array size to match the existing array.");
                controlledBlocks = value;
            }
        }

        /*!
         * Retrieves the text keys, which are tags associated with keyframe times that mark the start and stop of each sequence, among other things such as the triggering of sound effects.
         * \return The text key data.
         */
        public NiTextKeyExtraData TextKeyExtraData => textKeys != null ? textKeys : NiSequence.TextKeys;

        /*!
         * Gets or sets the animation frequency.
         * \param[in] value The animation frequency.
         */
        public float Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        /*!
         * Gets or ets the controller sequence start time.
         * \param[in] value The controller sequence start time.
         */
        public float StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        /*!
         * Gets or sets the controller sequence stop time.
         * \param[in] value The conroller sequence stop time.
         */
        public float StopTime
        {
            get => stopTime;
            set => stopTime = value;
        }

        /*!
         * Get or sets the controller cyle behavior. Can be loop, reverse, or clamp.
         * \param[in] n The new animation cycle behavior.
         */
        public CycleType CycleType
        {
            get => cycleType;
            set => cycleType = value;
        }

        /*! 
         * Gets or sets the number of controllers.
         * \return Number of total controllers in this sequence
         * \sa GetControllerData
         */
        public int NumControllers => controlledBlocks.Count;

        /*! 
         * Gets controller priority.  Oblivion Specific.
         * \return Priority of a specific controller.
         * \param[in] controller The index of the controller to get the priority for.
         * \sa GetControllerData, GetNumControllers, SetControllerPriority
         */
        public int GetControllerPriority(int controller)
        {
            if (controller < 0 && controller < controlledBlocks.Count)
                throw new Exception("Invalid controller index.");
            return controlledBlocks[controller].priority;
        }

        /*! 
         * Sets controller priority.  Oblivion Specific.
         * \param[in] controller The index of the controller to set the priority for.
         * \param[in] priority The amount of priority the controller should have.
         * \sa GetControllerData, GetNumControllers, GetControllerPriority
         */
        public void SetControllerPriority(int controller, int priority)
        {
            if (controller < 0 && controller < controlledBlocks.Count)
                throw new Exception("Invalid controller index.");
            if (priority < 0 || priority > 0xFF)
            controlledBlocks[controller].priority = (byte)priority;
        }

        /*!
         * Sets weight/priority of animation?
         * \param[in] value The weight/priority of the animation?
         */
        public float Weight
        {
            get => weight;
            set => weight = value;
        }

        /*!
         * Gets or sets the name of target node this controller acts on.
         * \param[in] value The target node name.
         */
        public string TargetName
        {
            get => targetName;
            set => targetName = value;
        }

        /*!
        * Gets or sets the string palette for this controller.
        * \param[in] value The string palette.
        */
        public NiStringPalette StringPalette
        {
            get => stringPalette;
            set => stringPalette = value;
        }
//--END:CUSTOM--//

}

}