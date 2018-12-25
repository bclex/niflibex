/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class NxMaterialDesc {
	/*!  */
	float dynamicFriction;
	/*!  */
	float staticFriction;
	/*!  */
	float restitution;
	/*!  */
	float dynamicFrictionV;
	/*!  */
	float staticFrictionV;
	/*!  */
	Vector3 directionOfAnisotropy;
	/*!  */
	NxMaterialFlag flags;
	/*!  */
	NxCombineMode frictionCombineMode;
	/*!  */
	NxCombineMode restitutionCombineMode;
	/*!  */
	bool hasSpring;
	/*!  */
	NxSpringDesc spring;
	//Constructor
	public NxMaterialDesc() { unchecked {
	dynamicFriction = 0.0f;
	staticFriction = 0.0f;
	restitution = 0.0f;
	dynamicFrictionV = 0.0f;
	staticFrictionV = 0.0f;
	flags = (NxMaterialFlag)0;
	frictionCombineMode = (NxCombineMode)0;
	restitutionCombineMode = (NxCombineMode)0;
	hasSpring = false;
	
	} }

}

}
