/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class DataStreamRef {
	/*!
	 * Reference to a data stream object which holds the data used by
	 *             this reference.
	 */
	public NiDataStream stream;
	/*!
	 * Sets whether this stream data is per-instance data for use in
	 *             hardware instancing.
	 */
	public bool isPerInstance;
	/*!
	 * The number of submesh-to-region mappings that this data stream
	 *             has.
	 */
	public ushort numSubmeshes;
	/*!  */
	public ushort[] submeshToRegionMap;
	/*!  */
	public uint numComponents;
	/*! Describes the semantic of each component. */
	public SemanticData[] componentSemantics;
	//Constructor
	public DataStreamRef() { unchecked {
	stream = null;
	isPerInstance = 0;
	numSubmeshes = (ushort)1;
	numComponents = (uint)1;
	
	} }

}

}
