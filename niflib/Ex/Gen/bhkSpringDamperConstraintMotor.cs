/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class bhkSpringDamperConstraintMotor {
	/*! Minimum motor force */
	public float minForce;
	/*! Maximum motor force */
	public float maxForce;
	/*! The spring constant in N/m */
	public float springConstant;
	/*! The spring damping in Nsec/m */
	public float springDamping;
	/*! Is Motor enabled */
	public bool motorEnabled;
	//Constructor
	public bhkSpringDamperConstraintMotor() { unchecked {
	minForce = -1000000.0f;
	maxForce = 1000000.0f;
	springConstant = 0f;
	springDamping = 0f;
	motorEnabled = 0;
	
	} }

}

}
