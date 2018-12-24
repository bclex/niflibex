/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Bethesda Havok. A triangle with extra data used for physics. */
public class TriangleData {
	/*! The triangle. */
	public Triangle triangle;
	/*! Additional havok information on how triangles are welded. */
	public ushort weldingInfo;
	/*! This is the triangle's normal. */
	public Vector3 normal;
	//Constructor
	public TriangleData() { unchecked {
	weldingInfo = (ushort)0;
	
	} }

}

}
