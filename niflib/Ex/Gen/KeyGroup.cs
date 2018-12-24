/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Array of vector keys (anything that can be interpolated, except rotations). */
public class KeyGroup<T> {
	/*! Number of keys in the array. */
	public uint numKeys;
	/*! The key type. */
	public KeyType interpolation;
	/*! The keys. */
	public Key<T>[] keys;
}

}
