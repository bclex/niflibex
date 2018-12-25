/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class PixelFormatComponent {
	/*! Component Type */
	PixelComponent type;
	/*! Data Storage Convention */
	PixelRepresentation convention;
	/*! Bits per component */
	byte bitsPerChannel;
	/*!  */
	bool isSigned;
	//Constructor
	public PixelFormatComponent() { unchecked {
	type = (PixelComponent)0;
	convention = (PixelRepresentation)0;
	bitsPerChannel = (byte)0;
	isSigned = false;
	
	} }

}

}
