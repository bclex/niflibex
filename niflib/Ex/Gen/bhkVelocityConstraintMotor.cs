/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class bhkVelocityConstraintMotor {
	/*! Minimum motor force */
	float minForce;
	/*! Maximum motor force */
	float maxForce;
	/*! Relative stiffness */
	float tau;
	/*!  */
	float targetVelocity;
	/*!  */
	bool useVelocityTarget;
	/*! Is Motor enabled */
	bool motorEnabled;
	//Constructor
	public bhkVelocityConstraintMotor() { unchecked {
	minForce = -1000000.0f;
	maxForce = 1000000.0f;
	tau = 0f;
	targetVelocity = 0f;
	useVelocityTarget = 0;
	motorEnabled = 0;
	
	} }

}

}
