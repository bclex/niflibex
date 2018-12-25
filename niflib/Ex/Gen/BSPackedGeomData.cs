/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSPackedGeomData {
	/*!  */
	uint numVerts;
	/*!  */
	uint lodLevels;
	/*!  */
	uint triCountLod0;
	/*!  */
	uint triOffsetLod0;
	/*!  */
	uint triCountLod1;
	/*!  */
	uint triOffsetLod1;
	/*!  */
	uint triCountLod2;
	/*!  */
	uint triOffsetLod2;
	/*!  */
	uint numCombined;
	/*!  */
	BSPackedGeomDataCombined[] combined;
	/*!  */
	BSVertexDesc vertexDesc;
	/*!  */
	BSVertexData[] vertexData;
	/*!  */
	Triangle[] triangles;
	//Constructor
	public BSPackedGeomData() { unchecked {
	numVerts = (uint)0;
	lodLevels = (uint)0;
	triCountLod0 = (uint)0;
	triOffsetLod0 = (uint)0;
	triCountLod1 = (uint)0;
	triOffsetLod1 = (uint)0;
	triCountLod2 = (uint)0;
	triOffsetLod2 = (uint)0;
	numCombined = (uint)0;
	
	} }

}

}
