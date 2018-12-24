/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Fallout 4 Bone Transform */
public class BSSkinBoneTrans {
	/*!  */
	public NiBound boundingSphere;
	/*!  */
	public Matrix33 rotation;
	/*!  */
	public Vector3 translation;
	/*!  */
	public float scale;
	//Constructor
	public BSSkinBoneTrans() { unchecked {
	scale = 0.0f;
	
	} }

}

}
