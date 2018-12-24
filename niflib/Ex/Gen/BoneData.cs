/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! NiSkinData::BoneData. Skinning data component. */
public class BoneData {
	/*! Offset of the skin from this bone in bind position. */
	public NiTransform skinTransform;
	/*!
	 * Translation offset of a bounding sphere holding all vertices. (Note that its a
	 * Sphere Containing Axis Aligned Box not a minimum volume Sphere)
	 */
	public Vector3 boundingSphereOffset;
	/*! Radius for bounding sphere holding all vertices. */
	public float boundingSphereRadius;
	/*! Unknown, always 0? */
	public Array13<short> unknown13Shorts;
	/*! Number of weighted vertices. */
	public ushort numVertices;
	/*! The vertex weights. */
	public BoneVertData[] vertexWeights;
	//Constructor
	public BoneData() { unchecked {
	boundingSphereRadius = 0.0f;
	numVertices = (ushort)0;
	
	} }

}

}