/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class PrismaticDescriptor {
	/*! Pivot. */
	public Vector4 pivotA;
	/*! Rotation axis. */
	public Vector4 rotationA;
	/*! Plane normal. Describes the plane the object is able to move on. */
	public Vector4 planeA;
	/*! Describes the axis the object is able to travel along. Unit vector. */
	public Vector4 slidingA;
	/*!
	 * Describes the axis the object is able to travel along in B coordinates. Unit
	 * vector.
	 */
	public Vector4 slidingB;
	/*! Pivot in B coordinates. */
	public Vector4 pivotB;
	/*! Rotation axis. */
	public Vector4 rotationB;
	/*!
	 * Plane normal. Describes the plane the object is able to move on in B
	 * coordinates.
	 */
	public Vector4 planeB;
	/*! Describe the min distance the object is able to travel. */
	public float minDistance;
	/*! Describe the max distance the object is able to travel. */
	public float maxDistance;
	/*! Friction. */
	public float friction;
	/*!  */
	public MotorDescriptor motor;
	//Constructor
	public PrismaticDescriptor() { unchecked {
	minDistance = 0.0f;
	maxDistance = 0.0f;
	friction = 0.0f;
	
	} }

}

}
