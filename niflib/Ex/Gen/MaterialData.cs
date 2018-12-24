/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class MaterialData {
	/*! Shader. */
	public bool hasShader;
	/*! The shader name. */
	public IndexString shaderName;
	/*!
	 * Extra data associated with the shader. A value of -1 means the shader is the
	 * default implementation.
	 */
	public int shaderExtraData;
	/*!  */
	public uint numMaterials;
	/*! The name of the material. */
	public IndexString[] materialName;
	/*!
	 * Extra data associated with the material. A value of -1 means the material is the
	 * default implementation.
	 */
	public int[] materialExtraData;
	/*! The index of the currently active material. */
	public int activeMaterial;
	/*! Cyanide extension (only in version 10.2.0.0?). */
	public byte unknownByte;
	/*! Unknown. */
	public int unknownInteger2;
	/*!
	 * Whether the materials for this object always needs to be updated before
	 * rendering with them.
	 */
	public bool materialNeedsUpdate;
	//Constructor
	public MaterialData() { unchecked {
	hasShader = false;
	shaderExtraData = (int)0;
	numMaterials = (uint)0;
	activeMaterial = (int)-1;
	unknownByte = (byte)255;
	unknownInteger2 = (int)0;
	materialNeedsUpdate = false;
	
	} }

}

}
