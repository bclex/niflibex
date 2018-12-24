/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class ConstraintData {
	/*! Type of constraint. */
	public hkConstraintType type;
	/*! Always 2 (Hardcoded). Number of bodies affected by this constraint. */
	public uint numEntities2;
	/*! Usually NONE. The entity affected by this constraint. */
	public bhkEntity entityA;
	/*! Usually NONE. The entity affected by this constraint. */
	public bhkEntity entityB;
	/*! Usually 1. Higher values indicate higher priority of this constraint? */
	public uint priority;
	/*!  */
	public BallAndSocketDescriptor ballAndSocket;
	/*!  */
	public HingeDescriptor hinge;
	/*!  */
	public LimitedHingeDescriptor limitedHinge;
	/*!  */
	public PrismaticDescriptor prismatic;
	/*!  */
	public RagdollDescriptor ragdoll;
	/*!  */
	public StiffSpringDescriptor stiffSpring;
	/*!  */
	public MalleableDescriptor malleable;
	//Constructor
	public ConstraintData() { unchecked {
	type = (hkConstraintType)0;
	numEntities2 = (uint)2;
	entityA = null;
	entityB = null;
	priority = (uint)1;
	
	} }

}

}
