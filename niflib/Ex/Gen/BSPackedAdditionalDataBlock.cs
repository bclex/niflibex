/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSPackedAdditionalDataBlock {
	/*! Has data */
	public bool hasData;
	/*!
	 * Total number of bytes (over all channels and all elements, equals num total
	 * bytes per element times num vertices).
	 */
	public int numTotalBytes;
	/*! Number of blocks? Usually equal to one. */
	public int numBlocks;
	/*! Block offsets in the data? Usually equal to zero. */
	public int[] blockOffsets;
	/*! Number of atoms? */
	public int numAtoms;
	/*!
	 * The sum of all of these equal num total bytes per element, so this probably
	 * describes how each data element breaks down into smaller chunks (i.e. atoms).
	 */
	public int[] atomSizes;
	/*!  */
	public byte[] data;
	/*! Unknown. */
	public int unknownInt1;
	/*!
	 * Unsure, but this seems to correspond again to the number of total bytes per
	 * element.
	 */
	public int numTotalBytesPerElement;
	//Constructor
	public BSPackedAdditionalDataBlock() { unchecked {
	hasData = false;
	numTotalBytes = (int)0;
	numBlocks = (int)0;
	numAtoms = (int)0;
	unknownInt1 = (int)0;
	numTotalBytesPerElement = (int)0;
	
	} }

}

}