/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!
 * In a .kf file, this links to a controllable object, via its name (or for version
 * 10.2.0.0 and up, a link and offset to a NiStringPalette that contains the name),
 * and a sequence of interpolators that apply to this controllable object, via
 * links.
 *         For Controller ID, NiInterpController::GetCtlrID() virtual function
 * returns a string formatted specifically for the derived type.
 *         For Interpolator ID, NiInterpController::GetInterpolatorID() virtual
 * function returns a string formatted specifically for the derived type.
 *         The string formats are documented on the relevant niobject blocks.
 */
public class ControlledBlock {
	/*! Name of a controllable object in another NIF file. */
	IndexString targetName;
	/*!  */
	NiInterpolator interpolator;
	/*!  */
	NiTimeController controller;
	/*!  */
	NiBlendInterpolator blendInterpolator;
	/*!  */
	ushort blendIndex;
	/*!
	 * Idle animations tend to have low values for this, and high values tend to
	 * correspond with the important parts of the animations.
	 */
	byte priority;
	/*! The name of the animated NiAVObject. */
	IndexString nodeName;
	/*! The RTTI type of the NiProperty the controller is attached to, if applicable. */
	IndexString propertyType;
	/*! The RTTI type of the NiTimeController. */
	IndexString controllerType;
	/*!
	 * An ID that can uniquely identify the controller among others of the same type on
	 * the same NiObjectNET.
	 */
	IndexString controllerId;
	/*!
	 * An ID that can uniquely identify the interpolator among others of the same type
	 * on the same NiObjectNET.
	 */
	IndexString interpolatorId;
	/*!
	 * Refers to the NiStringPalette which contains the name of the controlled NIF
	 * object.
	 */
	NiStringPalette stringPalette;
	/*! Offset in NiStringPalette to the name of the animated NiAVObject. */
	uint nodeNameOffset;
	/*!
	 * Offset in NiStringPalette to the RTTI type of the NiProperty the controller is
	 * attached to, if applicable.
	 */
	uint propertyTypeOffset;
	/*! Offset in NiStringPalette to the RTTI type of the NiTimeController. */
	uint controllerTypeOffset;
	/*!
	 * Offset in NiStringPalette to an ID that can uniquely identify the controller
	 * among others of the same type on the same NiObjectNET.
	 */
	uint controllerIdOffset;
	/*!
	 * Offset in NiStringPalette to an ID that can uniquely identify the interpolator
	 * among others of the same type on the same NiObjectNET.
	 */
	uint interpolatorIdOffset;
	//Constructor
	public ControlledBlock() { unchecked {
	interpolator = null;
	controller = null;
	blendInterpolator = null;
	blendIndex = (ushort)0;
	priority = (byte)0;
	stringPalette = null;
	nodeNameOffset = (uint)-1;
	propertyTypeOffset = (uint)-1;
	controllerTypeOffset = (uint)-1;
	controllerIdOffset = (uint)-1;
	interpolatorIdOffset = (uint)-1;
	
	} }

}

}
