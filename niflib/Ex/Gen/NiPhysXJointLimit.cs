/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class NiPhysXJointLimit {
	/*!  */
	public Vector3 limitPlaneNormal;
	/*!  */
	public float limitPlaneD;
	/*!  */
	public float limitPlaneR;
	//Constructor
	public NiPhysXJointLimit() { unchecked {
	limitPlaneD = 0.0f;
	limitPlaneR = 0.0f;
	
	} }

}

}