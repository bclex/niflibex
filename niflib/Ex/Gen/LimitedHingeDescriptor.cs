/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!
 * This constraint allows rotation about a specified axis, limited by specified
 * boundaries.
 */
public class LimitedHingeDescriptor {
	/*! Pivot point around which the object will rotate. */
	public Vector4 pivotA;
	/*! Axis of rotation. */
	public Vector4 axisA;
	/*! Vector in the rotation plane which defines the zero angle. */
	public Vector4 perpAxisInA1;
	/*!
	 * Vector in the rotation plane, orthogonal on the previous one, which defines the
	 * positive direction of rotation. This is always the vector product of Axis A and
	 * Perp Axis In A1.
	 */
	public Vector4 perpAxisInA2;
	/*! Pivot A in second entity coordinate system. */
	public Vector4 pivotB;
	/*! Axis A in second entity coordinate system. */
	public Vector4 axisB;
	/*! Perp Axis In A2 in second entity coordinate system. */
	public Vector4 perpAxisInB2;
	/*! Perp Axis In A1 in second entity coordinate system. */
	public Vector4 perpAxisInB1;
	/*! Minimum rotation angle. */
	public float minAngle;
	/*! Maximum rotation angle. */
	public float maxAngle;
	/*! Maximum friction, typically either 0 or 10. In Fallout 3, typically 100. */
	public float maxFriction;
	/*!  */
	public MotorDescriptor motor;
	//Constructor
	public LimitedHingeDescriptor() { unchecked {
	minAngle = 0.0f;
	maxAngle = 0.0f;
	maxFriction = 0.0f;
	
	} }

}

}
