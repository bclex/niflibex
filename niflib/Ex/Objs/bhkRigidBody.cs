/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*!
 * This is the default body type for all "normal" usable and static world objects.
 * The "T" suffix
 *         marks this body as active for translation and rotation, a normal
 * bhkRigidBody ignores those
 *         properties. Because the properties are equal, a bhkRigidBody may be
 * renamed into a bhkRigidBodyT and vice-versa.
 */
public class bhkRigidBody : bhkEntity {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkRigidBody", bhkEntity.TYPE);
	/*!
	 * How the body reacts to collisions. See hkResponseType for hkpWorld default
	 * implementations.
	 */
	hkResponseType collisionResponse;
	/*! Skipped over when writing Collision Response and Callback Delay. */
	byte unusedByte1;
	/*!
	 * Lowers the frequency for processContactCallbacks. A value of 5 means that a
	 * callback is raised every 5th frame. The default is once every 65535 frames.
	 */
	ushort processContactCallbackDelay;
	/*! Unknown. */
	uint unknownInt1;
	/*! Copy of Havok Filter */
	HavokFilter havokFilterCopy;
	/*! Garbage data from memory. Matches previous Unused value. */
	Array4<byte> unused2;
	/*! Unknown. */
	uint unknownInt2;
	/*!  */
	hkResponseType collisionResponse2;
	/*! Skipped over when writing Collision Response and Callback Delay. */
	byte unusedByte2;
	/*!  */
	ushort processContactCallbackDelay2;
	/*!
	 * A vector that moves the body by the specified amount. Only enabled in
	 * bhkRigidBodyT objects.
	 */
	Vector4 translation;
	/*!
	 * The rotation Yaw/Pitch/Roll to apply to the body. Only enabled in bhkRigidBodyT
	 * objects.
	 */
	hkQuaternion rotation;
	/*! Linear velocity. */
	Vector4 linearVelocity;
	/*! Angular velocity. */
	Vector4 angularVelocity;
	/*!
	 * Defines how the mass is distributed among the body, i.e. how difficult it is to
	 * rotate around any given axis.
	 */
	InertiaMatrix inertiaTensor;
	/*! The body's center of mass. */
	Vector4 center;
	/*! The body's mass in kg. A mass of zero represents an immovable object. */
	float mass;
	/*!
	 * Reduces the movement of the body over time. A value of 0.1 will remove 10% of
	 * the linear velocity every second.
	 */
	float linearDamping;
	/*!
	 * Reduces the movement of the body over time. A value of 0.05 will remove 5% of
	 * the angular velocity every second.
	 */
	float angularDamping;
	/*!  */
	float timeFactor;
	/*!  */
	float gravityFactor;
	/*! How smooth its surfaces is and how easily it will slide along other bodies. */
	float friction;
	/*!  */
	float rollingFrictionMultiplier;
	/*!
	 * How "bouncy" the body is, i.e. how much energy it has after colliding. Less than
	 * 1.0 loses energy, greater than 1.0 gains energy.
	 *             If the restitution is not 0.0 the object will need extra CPU for all
	 * new collisions.
	 */
	float restitution;
	/*! Maximal linear velocity. */
	float maxLinearVelocity;
	/*! Maximal angular velocity. */
	float maxAngularVelocity;
	/*!
	 * The maximum allowed penetration for this object.
	 *             This is a hint to the engine to see how much CPU the engine should
	 * invest to keep this object from penetrating.
	 *             A good choice is 5% - 20% of the smallest diameter of the object.
	 */
	float penetrationDepth;
	/*! Motion system? Overrides Quality when on Keyframed? */
	hkMotionType motionSystem;
	/*! The initial deactivator type of the body. */
	hkDeactivatorType deactivatorType;
	/*!  */
	bool enableDeactivation;
	/*!
	 * How aggressively the engine will try to zero the velocity for slow objects. This
	 * does not save CPU.
	 */
	hkSolverDeactivation solverDeactivation;
	/*! The type of interaction with other objects. */
	hkQualityType qualityType;
	/*! Unknown. */
	float unknownFloat1;
	/*! Unknown. */
	Array12<byte> unknownBytes1;
	/*! Unknown. Skyrim only. */
	Array4<byte> unknownBytes2;
	/*!  */
	uint numConstraints;
	/*!  */
	bhkSerializable[] constraints;
	/*! 1 = respond to wind */
	uint bodyFlags;

	public bhkRigidBody() {
	collisionResponse = hkResponseType.RESPONSE_SIMPLE_CONTACT;
	unusedByte1 = (byte)0;
	processContactCallbackDelay = (ushort)0xffff;
	unknownInt1 = (uint)0;
	unknownInt2 = (uint)0;
	collisionResponse2 = hkResponseType.RESPONSE_SIMPLE_CONTACT;
	unusedByte2 = (byte)0;
	processContactCallbackDelay2 = (ushort)0xffff;
	mass = 1.0f;
	linearDamping = 0.1f;
	angularDamping = 0.05f;
	timeFactor = 1.0f;
	gravityFactor = 1.0f;
	friction = 0.5f;
	rollingFrictionMultiplier = 0.0f;
	restitution = 0.4f;
	maxLinearVelocity = 104.4f;
	maxAngularVelocity = 31.57f;
	penetrationDepth = 0.15f;
	motionSystem = hkMotionType.MO_SYS_DYNAMIC;
	deactivatorType = hkDeactivatorType.DEACTIVATOR_NEVER;
	enableDeactivation = 1;
	solverDeactivation = hkSolverDeactivation.SOLVER_DEACTIVATION_OFF;
	qualityType = hkQualityType.MO_QUAL_FIXED;
	unknownFloat1 = 0.0f;
	numConstraints = (uint)0;
	bodyFlags = (uint)0;
}

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new bhkRigidBody();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out collisionResponse, s, info);
	Nif.NifStream(out unusedByte1, s, info);
	Nif.NifStream(out processContactCallbackDelay, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out unknownInt1, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(out havokFilterCopy.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(out havokFilterCopy.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(out havokFilterCopy.layer_sk, s, info);
		}
		Nif.NifStream(out havokFilterCopy.flagsAndPartNumber, s, info);
		Nif.NifStream(out havokFilterCopy.group, s, info);
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out unused2[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 > 34))) {
		Nif.NifStream(out unknownInt2, s, info);
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out collisionResponse2, s, info);
		Nif.NifStream(out unusedByte2, s, info);
		Nif.NifStream(out processContactCallbackDelay2, s, info);
	}
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(out (uint)unknownInt2, s, info);
	}
	Nif.NifStream(out translation, s, info);
	Nif.NifStream(out rotation.x, s, info);
	Nif.NifStream(out rotation.y, s, info);
	Nif.NifStream(out rotation.z, s, info);
	Nif.NifStream(out rotation.w, s, info);
	Nif.NifStream(out linearVelocity, s, info);
	Nif.NifStream(out angularVelocity, s, info);
	Nif.NifStream(out inertiaTensor, s, info);
	Nif.NifStream(out center, s, info);
	Nif.NifStream(out mass, s, info);
	Nif.NifStream(out linearDamping, s, info);
	Nif.NifStream(out angularDamping, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out timeFactor, s, info);
	}
	if (((info.userVersion2 > 34) && (info.userVersion2 != 130))) {
		Nif.NifStream(out gravityFactor, s, info);
	}
	Nif.NifStream(out friction, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out rollingFrictionMultiplier, s, info);
	}
	Nif.NifStream(out restitution, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out maxLinearVelocity, s, info);
		Nif.NifStream(out maxAngularVelocity, s, info);
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 != 130))) {
		Nif.NifStream(out penetrationDepth, s, info);
	}
	Nif.NifStream(out motionSystem, s, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(out deactivatorType, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(out enableDeactivation, s, info);
	}
	Nif.NifStream(out solverDeactivation, s, info);
	Nif.NifStream(out qualityType, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(out (float)penetrationDepth, s, info);
		Nif.NifStream(out unknownFloat1, s, info);
	}
	for (var i1 = 0; i1 < 12; i1++) {
		Nif.NifStream(out unknownBytes1[i1], s, info);
	}
	if ((info.userVersion2 > 34)) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out unknownBytes2[i2], s, info);
		}
	}
	Nif.NifStream(out numConstraints, s, info);
	constraints = new Ref[numConstraints];
	for (var i1 = 0; i1 < constraints.Length; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 < 76)) {
		Nif.NifStream(out bodyFlags, s, info);
	}
	if ((info.userVersion2 >= 76)) {
		Nif.NifStream(out (ushort)bodyFlags, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numConstraints = (uint)constraints.Length;
	Nif.NifStream(collisionResponse, s, info);
	Nif.NifStream(unusedByte1, s, info);
	Nif.NifStream(processContactCallbackDelay, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(unknownInt1, s, info);
		if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
			Nif.NifStream(havokFilterCopy.layer_ob, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
			Nif.NifStream(havokFilterCopy.layer_fo, s, info);
		}
		if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
			Nif.NifStream(havokFilterCopy.layer_sk, s, info);
		}
		Nif.NifStream(havokFilterCopy.flagsAndPartNumber, s, info);
		Nif.NifStream(havokFilterCopy.group, s, info);
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(unused2[i2], s, info);
		}
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 > 34))) {
		Nif.NifStream(unknownInt2, s, info);
	}
	if (info.version >= 0x0A010000) {
		Nif.NifStream(collisionResponse2, s, info);
		Nif.NifStream(unusedByte2, s, info);
		Nif.NifStream(processContactCallbackDelay2, s, info);
	}
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream((uint)unknownInt2, s, info);
	}
	Nif.NifStream(translation, s, info);
	Nif.NifStream(rotation.x, s, info);
	Nif.NifStream(rotation.y, s, info);
	Nif.NifStream(rotation.z, s, info);
	Nif.NifStream(rotation.w, s, info);
	Nif.NifStream(linearVelocity, s, info);
	Nif.NifStream(angularVelocity, s, info);
	Nif.NifStream(inertiaTensor, s, info);
	Nif.NifStream(center, s, info);
	Nif.NifStream(mass, s, info);
	Nif.NifStream(linearDamping, s, info);
	Nif.NifStream(angularDamping, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(timeFactor, s, info);
	}
	if (((info.userVersion2 > 34) && (info.userVersion2 != 130))) {
		Nif.NifStream(gravityFactor, s, info);
	}
	Nif.NifStream(friction, s, info);
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(rollingFrictionMultiplier, s, info);
	}
	Nif.NifStream(restitution, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(maxLinearVelocity, s, info);
		Nif.NifStream(maxAngularVelocity, s, info);
	}
	if ((info.version >= 0x0A010000) && ((info.userVersion2 != 130))) {
		Nif.NifStream(penetrationDepth, s, info);
	}
	Nif.NifStream(motionSystem, s, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(deactivatorType, s, info);
	}
	if ((info.userVersion2 > 34)) {
		Nif.NifStream(enableDeactivation, s, info);
	}
	Nif.NifStream(solverDeactivation, s, info);
	Nif.NifStream(qualityType, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream((float)penetrationDepth, s, info);
		Nif.NifStream(unknownFloat1, s, info);
	}
	for (var i1 = 0; i1 < 12; i1++) {
		Nif.NifStream(unknownBytes1[i1], s, info);
	}
	if ((info.userVersion2 > 34)) {
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(unknownBytes2[i2], s, info);
		}
	}
	Nif.NifStream(numConstraints, s, info);
	for (var i1 = 0; i1 < constraints.Length; i1++) {
		WriteRef((NiObject)constraints[i1], s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 < 76)) {
		Nif.NifStream(bodyFlags, s, info);
	}
	if ((info.userVersion2 >= 76)) {
		Nif.NifStream((ushort)bodyFlags, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numConstraints = (uint)constraints.Length;
	s.AppendLine($"  Collision Response:  {collisionResponse}");
	s.AppendLine($"  Unused Byte 1:  {unusedByte1}");
	s.AppendLine($"  Process Contact Callback Delay:  {processContactCallbackDelay}");
	s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_ob}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_fo}");
	s.AppendLine($"  Layer:  {havokFilterCopy.layer_sk}");
	s.AppendLine($"  Flags and Part Number:  {havokFilterCopy.flagsAndPartNumber}");
	s.AppendLine($"  Group:  {havokFilterCopy.group}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused 2[{i1}]:  {unused2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	s.AppendLine($"  Collision Response 2:  {collisionResponse2}");
	s.AppendLine($"  Unused Byte 2:  {unusedByte2}");
	s.AppendLine($"  Process Contact Callback Delay 2:  {processContactCallbackDelay2}");
	s.AppendLine($"  Translation:  {translation}");
	s.AppendLine($"  x:  {rotation.x}");
	s.AppendLine($"  y:  {rotation.y}");
	s.AppendLine($"  z:  {rotation.z}");
	s.AppendLine($"  w:  {rotation.w}");
	s.AppendLine($"  Linear Velocity:  {linearVelocity}");
	s.AppendLine($"  Angular Velocity:  {angularVelocity}");
	s.AppendLine($"  Inertia Tensor:  {inertiaTensor}");
	s.AppendLine($"  Center:  {center}");
	s.AppendLine($"  Mass:  {mass}");
	s.AppendLine($"  Linear Damping:  {linearDamping}");
	s.AppendLine($"  Angular Damping:  {angularDamping}");
	s.AppendLine($"  Time Factor:  {timeFactor}");
	s.AppendLine($"  Gravity Factor:  {gravityFactor}");
	s.AppendLine($"  Friction:  {friction}");
	s.AppendLine($"  Rolling Friction Multiplier:  {rollingFrictionMultiplier}");
	s.AppendLine($"  Restitution:  {restitution}");
	s.AppendLine($"  Max Linear Velocity:  {maxLinearVelocity}");
	s.AppendLine($"  Max Angular Velocity:  {maxAngularVelocity}");
	s.AppendLine($"  Penetration Depth:  {penetrationDepth}");
	s.AppendLine($"  Motion System:  {motionSystem}");
	s.AppendLine($"  Deactivator Type:  {deactivatorType}");
	s.AppendLine($"  Enable Deactivation:  {enableDeactivation}");
	s.AppendLine($"  Solver Deactivation:  {solverDeactivation}");
	s.AppendLine($"  Quality Type:  {qualityType}");
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 12; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Bytes 1[{i1}]:  {unknownBytes1[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Bytes 2[{i1}]:  {unknownBytes2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Constraints:  {numConstraints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < constraints.Length; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Constraints[{i1}]:  {constraints[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Body Flags:  {bodyFlags}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < constraints.Length; i1++) {
		constraints[i1] = FixLink<bhkSerializable>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < constraints.Length; i1++) {
		if (constraints[i1] != null)
			refs.Add((NiObject)constraints[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < constraints.Length; i1++) {
	}
	return ptrs;
}

//--BEGIN:FILE FOOT--//

        /*!
         * Retrieves what appears to be a copy of the layer value.
         * \return The duplicate layer value.
         */
        NIFLIB_API OblivionLayer GetLayerCopy() const;

        /*!
         * Sets what appears to be a copy of the layer value.
         * \param[in] value The new duplicate layer value.
         */
        NIFLIB_API void SetLayerCopy(OblivionLayer value);

        NIFLIB_API SkyrimLayer GetSkyrimLayerCopy() const;
        NIFLIB_API void SetSkyrimLayerCopy(SkyrimLayer value);

        /*!
         * Gets the current translation of this rigid body.
         * \return The translation of this rigid body.
         */
        NIFLIB_API Vector4 GetTranslation() const;

        /*!
         * Sets a new translation for this rigid body.
         * \param[in] value  The new translation for this rigid body.
         */
        NIFLIB_API void SetTranslation( const Vector4 & value );

	/*!
	 * Gets the current rotation of this rigid body.
	 * \return The rotation of this rigid body.
	 */
	NIFLIB_API QuaternionXYZW GetRotation() const;

        /*!
         * Sets a new rotation for this rigid body.
         * \param[in] value The new rotation for this rigid body.
         */
        NIFLIB_API void SetRotation( const QuaternionXYZW & value );

	/*!
	 * Gets the current linear velocity of this rigid body.
	 * \return The linear velocity of this rigid body.
	 */
	NIFLIB_API Vector4 GetLinearVelocity() const;

        /*!
         * Sets a new linear velocity for this rigid body.
         * \param[in] value The new linear velocity for this rigid body.
         */
        NIFLIB_API void SetLinearVelocity( const Vector4 & value );

	/*!
	 * Gets the current angular velocity of this rigid body.
	 * \return The angular velocity of this rigid body.
	 */
	NIFLIB_API Vector4 GetAngularVelocity() const;

        /*!
         * Sets a new angular velocity for this rigid body.
         * \param[in] value The new angular velocity for this rigid body.
         */
        NIFLIB_API void SetAngularVelocity( const Vector4 & value );

	/*!
	 * Gets the current inertia of this rigid body.
	 * \return The inertia of this rigid body.
	 */
	NIFLIB_API InertiaMatrix GetInertia() const;

        /*!
         * Sets a new inertia for this rigid body.
         * \param[in] value The new inertia for this rigid body.
         */
        NIFLIB_API void SetInertia( const InertiaMatrix & value );

	/*!
	 * Gets the current center point of this rigid body.
	 * \return The center point of this rigid body.
	 */
	NIFLIB_API Vector4 GetCenter() const;

        /*!
         * Sets a new center point for this rigid body.
         * \param[in] value The new center point for this rigid body.
         */
        NIFLIB_API void SetCenter( const Vector4 & value );

	/*!
	 * Gets the current mass of this rigid body.
	 * \return The mass of this rigid body.
	 */
	NIFLIB_API float GetMass() const;

        /*!
         * Sets a new mass for this rigid body.
         * \param[in] value The new mass for this rigid body.
         */
        NIFLIB_API void SetMass(float value);

        /*!
         * Gets the current linear damping level of this rigid body.
         * \return The linear damping level of this rigid body.
         */
        NIFLIB_API float GetLinearDamping() const;

        /*!
         * Sets a new linear damping level for this rigid body.
         * \param[in] value The new linear damping level for this rigid body.
         */
        NIFLIB_API void SetLinearDamping(float value);

        /*!
         * Gets the current angular damping level of this rigid body.
         * \return The angular damping level of this rigid body.
         */
        NIFLIB_API float GetAngularDamping() const;

        /*!
         * Sets a new angular damping level for this rigid body.
         * \param[in] value The new angular damping level for this rigid body.
         */
        NIFLIB_API void SetAngularDamping(float value);

        /*!
         * Gets the current friction of this rigid body.
         * \return The friction of this rigid body.
         */
        NIFLIB_API float GetFriction() const;

        /*!
         * Sets a new friction for this rigid body.
         * \param[in] value The new friction for this rigid body.
         */
        NIFLIB_API void SetFriction(float value);

        /*!
         * Gets the current restitution of this rigid body.
         * \return The restitution of this rigid body.
         */
        NIFLIB_API float GetRestitution() const;

        /*!
         * Sets a new restitution for this rigid body.
         * \param[in] value The new restitution for this rigid body.
         */
        NIFLIB_API void SetRestitution(float value);

        /*!
         * Gets the current maximum linear velocity of this rigid body.
         * \return The maximum linear velocity of this rigid body.
         */
        NIFLIB_API float GetMaxLinearVelocity() const;

        /*!
         * Sets a new maximum linear velocit for this rigid body.
         * \param[in] value The new maximum linear velocity for this rigid body.
         */
        NIFLIB_API void SetMaxLinearVelocity(float value);

        /*!
         * Gets the current maximum angular velocity of this rigid body.
         * \return The maximum angular velocity of this rigid body.
         */
        NIFLIB_API float GetMaxAngularVelocity() const;

        /*!
         * Sets a new maximum angular velocity for this rigid body.
         * \param[in] value The new maximum angular velocit for this rigid body.
         */
        NIFLIB_API void SetMaxAngularVelocity(float value);

        /*!
         * Gets the current allowable penetration depth of this rigid body.
         * \return The allowable penetration depth of this rigid body.
         */
        NIFLIB_API float GetPenetrationDepth() const;

        /*!
         * Sets a new allowable penetration depth for this rigid body.
         * \param[in] value The new allowable penetration depth for this rigid body.
         */
        NIFLIB_API void SetPenetrationDepth(float value);

        /*!
         * Sets the current motion system for this rigid body.  Seems to override motion quality when set to keyframed.
         * \return The current motion system setting of this rigid body.
         */
        NIFLIB_API MotionSystem GetMotionSystem() const;

        /*!
         * Gets the current motion system of this rigid body.  Seems to override motion quality when set to keyframed.
         * \param[in] value The new motion system setting for this rigid body.
         */
        NIFLIB_API void SetMotionSystem(MotionSystem value);

        /*!
         * Sets the quality of the calculations used to detect collisions for this object.  Essentially, the faster the object goes, the higher quality of motion it will require.
         * \return The current motion quality setting of this rigid body.
         */
        NIFLIB_API MotionQuality GetQualityType() const;

        /*!
         * Gets the quality of the calculations used to detect collisions for this object.  Essentially, the faster the object goes, the higher quality of motion it will require.
         * \param[in] value The new motion quality setting for this rigid body.
         */
        NIFLIB_API void SetQualityType(MotionQuality value);

        // The initial deactivator type of the body.
        // \return The current value.
        NIFLIB_API DeactivatorType GetDeactivatorType() const;

        // The initial deactivator type of the body.
        // \param[in] value The new value.
        NIFLIB_API void SetDeactivatorType( const DeactivatorType & value );

	// Usually set to 1 for fixed objects, or set to 2 for moving ones.  Seems to
	// always be same as Unknown Byte 1.
	// \return The current value.
	NIFLIB_API SolverDeactivation GetSolverDeactivation() const;

        // Usually set to 1 for fixed objects, or set to 2 for moving ones.  Seems to
        // always be same as Unknown Byte 1.
        // \param[in] value The new value.
        NIFLIB_API void SetSolverDeactivation( const SolverDeactivation & value );

	/*!
	 * Adds a constraint to this bhkRigidBody.
	 */
	NIFLIB_API void AddConstraint(bhkSerializable* obj);

        /*!
         * Removes a constraint from this bhkRigidBody.
         */
        NIFLIB_API void RemoveConstraint(bhkSerializable* obj);

        /*!
         * Removes all constraints from this bhkRigidBody.
         */
        NIFLIB_API void ClearConstraints();

        /*!
         * Retrieves all the constraints attached to this bhkRigidBody.
         */
        NIFLIB_API vector<Ref<bhkSerializable> > GetConstraints() const;

        // Apply scale factor <scale> on data.
        // \param[in] scale Factor to scale by
        NIFLIB_API void ApplyScale(float scale);

        // Look at all the objects under this rigid body and update the mass
        //  center of gravity, and inertia tensor accordingly. If the mass parameter
        //  is given then the density argument is ignored.
        NIFLIB_API void UpdateMassProperties(float density = 1.0f, bool solid = true, float mass = 0.0f);

        /*!
         * Returns the unknown 7 shorts
         * \return An array containing the 7 unknown shorts within this object.
         */
        NIFLIB_API virtual array<7, unsigned short> GetUnknown7Shorts() const;

        /*! Replaces the unknown 7 shorts with new data
         * \param in An array containing the new data.  Size is 7.
         */
        NIFLIB_API virtual void SetUnknown7Shorts( const array<7, unsigned short> & in );


//--END:CUSTOM--//

}

}