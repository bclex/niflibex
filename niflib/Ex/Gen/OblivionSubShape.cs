/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Bethesda Havok. Havok Information for packed TriStrip shapes. */
public class OblivionSubShape {
	/*!  */
	HavokFilter havokFilter;
	/*! The number of vertices that form this sub shape. */
	uint numVertices;
	/*! The material of the subshape. */
	HavokMaterial material;
	//Constructor
	public OblivionSubShape() { unchecked {
	numVertices = (uint)0;
	
	} }

}

}
