/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class AdditionalDataBlock {
	/*! Has data */
	public bool hasData;
	/*! Size of Block */
	public int blockSize;
	/*!  */
	public int numBlocks;
	/*!  */
	public int[] blockOffsets;
	/*!  */
	public int numData;
	/*!  */
	public int[] dataSizes;
	/*!  */
	public byte[][] data;
	//Constructor
	public AdditionalDataBlock() { unchecked {
	hasData = false;
	blockSize = (int)0;
	numBlocks = (int)0;
	numData = (int)0;
	
	} }

}

}
