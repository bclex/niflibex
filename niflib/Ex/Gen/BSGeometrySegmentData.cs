/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!
 * Bethesda-specific. Describes groups of triangles either segmented in a grid (for
 * LOD) or by body part for skinned FO4 meshes.
 */
public class BSGeometrySegmentData {
	/*!  */
	public byte flags;
	/*! Index = previous Index + previous Num Tris in Segment * 3 */
	public uint index;
	/*! The number of triangles belonging to this segment */
	public uint numTrisInSegment;
	/*!  */
	public uint startIndex;
	/*!  */
	public uint numPrimitives;
	/*!  */
	public uint parentArrayIndex;
	/*!  */
	public uint numSubSegments;
	/*!  */
	public BSGeometrySubSegment[] subSegment;
	//Constructor
	public BSGeometrySegmentData() { unchecked {
	flags = (byte)0;
	index = (uint)0;
	numTrisInSegment = (uint)0;
	startIndex = (uint)0;
	numPrimitives = (uint)0;
	parentArrayIndex = (uint)0;
	numSubSegments = (uint)0;
	
	} }

}

}
