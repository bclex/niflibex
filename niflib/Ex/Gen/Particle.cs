/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! particle array entry */
public class Particle {
	/*! Particle velocity */
	Vector3 velocity;
	/*! Unknown */
	Vector3 unknownVector;
	/*! The particle age. */
	float lifetime;
	/*! Maximum age of the particle. */
	float lifespan;
	/*! Timestamp of the last update. */
	float timestamp;
	/*! Unknown short */
	ushort unknownShort;
	/*! Particle/vertex index matches array index */
	ushort vertexId;
	//Constructor
	public Particle() { unchecked {
	lifetime = 0.0f;
	lifespan = 0.0f;
	timestamp = 0.0f;
	unknownShort = (ushort)0;
	vertexId = (ushort)0;
	
	} }

}

}
