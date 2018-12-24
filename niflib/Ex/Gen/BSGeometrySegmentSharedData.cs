/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSGeometrySegmentSharedData {
	/*!  */
	public uint numSegments;
	/*!  */
	public uint totalSegments;
	/*!  */
	public uint[] segmentStarts;
	/*!  */
	public BSGeometryPerSegmentSharedData[] perSegmentData;
	/*!  */
	public ushort ssfLength;
	/*!  */
	public byte[] ssfFile;
	//Constructor
	public BSGeometrySegmentSharedData() { unchecked {
	numSegments = (uint)0;
	totalSegments = (uint)0;
	ssfLength = (ushort)0;
	
	} }

}

}
