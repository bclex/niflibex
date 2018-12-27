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


namespace Niflib
{

    /*!
     * Abstract audio-visual base class from which all of Gamebryo's scene graph
     * objects inherit.
     */
    public class NiAVObject : NiObjectNET
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiAVObject", NiObjectNET.TYPE);
        /*! Basic flags for AV objects. For Bethesda streams above 26 only. */
        internal uint flags;
        /*! The translation vector. */
        internal Vector3 translation;
        /*! The rotation part of the transformation matrix. */
        internal Matrix33 rotation;
        /*! Scaling part (only uniform scaling is supported). */
        internal float scale;
        /*! Unknown function. Always seems to be (0, 0, 0) */
        internal Vector3 velocity;
        /*!  */
        internal uint numProperties;
        /*! All rendering properties attached to this object. */
        internal NiProperty[] properties;
        /*! Always 2,0,2,0. */
        internal Array4<uint> unknown1;
        /*! 0 or 1. */
        internal byte unknown2;
        /*!  */
        internal bool hasBoundingVolume;
        /*!  */
        internal BoundingVolume boundingVolume;
        /*!  */
        internal NiCollisionObject collisionObject;

        public NiAVObject()
        {
            flags = (uint)14;
            scale = 1.0f;
            numProperties = (uint)0;
            unknown2 = (byte)0;
            hasBoundingVolume = false;
            collisionObject = null;
            //--BEGIN:CONSTRUCTOR--//
            parent = null;
            //--END:CUSTOM--//
        }

        //--BEGIN:DESTRUCTOR--//
        public void Dispose()
        {
            //Clear Properties
            ClearProperties();
            //Clear Collision Object Link
            if (collisionObject != null)
                collisionObject.SetTarget(null);
        }
        //--END:CUSTOM--//

        /*!
         * Used to determine the type of a particular instance of this object.
         * \return The type constant for the actual type of the object.
         */
        public override Type_ GetType() => TYPE;

        /*!
         * A factory function used during file reading to create an instance of this type of object.
         * \return A pointer to a newly allocated instance of this type of object.
         */
        public static NiObject Create() => new NiAVObject();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            if ((info.userVersion2 > 26))
            {
                Nif.NifStream(out flags, s, info);
            }
            if ((info.version >= 0x03000000) && ((info.userVersion2 <= 26)))
            {
                Nif.NifStream(out (ushort)flags, s, info);
            }
            Nif.NifStream(out translation, s, info);
            Nif.NifStream(out rotation, s, info);
            Nif.NifStream(out scale, s, info);
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(out velocity, s, info);
            }
            if ((info.userVersion2 <= 34))
            {
                Nif.NifStream(out numProperties, s, info);
                properties = new Ref[numProperties];
                for (var i2 = 0; i2 < properties.Length; i2++)
                {
                    Nif.NifStream(out block_num, s, info);
                    link_stack.Add(block_num);
                }
            }
            if (info.version <= 0x02030000)
            {
                for (var i2 = 0; i2 < 4; i2++)
                {
                    Nif.NifStream(out unknown1[i2], s, info);
                }
                Nif.NifStream(out unknown2, s, info);
            }
            if ((info.version >= 0x03000000) && (info.version <= 0x04020200))
            {
                Nif.NifStream(out hasBoundingVolume, s, info);
                if (hasBoundingVolume)
                {
                    Nif.NifStream(out boundingVolume.collisionType, s, info);
                    if ((boundingVolume.collisionType == 0))
                    {
                        Nif.NifStream(out boundingVolume.sphere.center, s, info);
                        Nif.NifStream(out boundingVolume.sphere.radius, s, info);
                    }
                    if ((boundingVolume.collisionType == 1))
                    {
                        Nif.NifStream(out boundingVolume.box.center, s, info);
                        for (var i4 = 0; i4 < 3; i4++)
                        {
                            Nif.NifStream(out boundingVolume.box.axis[i4], s, info);
                        }
                        Nif.NifStream(out boundingVolume.box.extent, s, info);
                    }
                    if ((boundingVolume.collisionType == 2))
                    {
                        Nif.NifStream(out boundingVolume.capsule.center, s, info);
                        Nif.NifStream(out boundingVolume.capsule.origin, s, info);
                        Nif.NifStream(out boundingVolume.capsule.extent, s, info);
                        Nif.NifStream(out boundingVolume.capsule.radius, s, info);
                    }
                    if ((boundingVolume.collisionType == 5))
                    {
                        Nif.NifStream(out boundingVolume.halfSpace.plane.normal, s, info);
                        Nif.NifStream(out boundingVolume.halfSpace.plane.constant, s, info);
                        Nif.NifStream(out boundingVolume.halfSpace.center, s, info);
                    }
                }
            }
            if (info.version >= 0x0A000100)
            {
                Nif.NifStream(out block_num, s, info);
                link_stack.Add(block_num);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numProperties = (uint)properties.Length;
            if ((info.userVersion2 > 26))
            {
                Nif.NifStream(flags, s, info);
            }
            if ((info.version >= 0x03000000) && ((info.userVersion2 <= 26)))
            {
                Nif.NifStream((ushort)flags, s, info);
            }
            Nif.NifStream(translation, s, info);
            Nif.NifStream(rotation, s, info);
            Nif.NifStream(scale, s, info);
            if (info.version <= 0x04020200)
            {
                Nif.NifStream(velocity, s, info);
            }
            if ((info.userVersion2 <= 34))
            {
                Nif.NifStream(numProperties, s, info);
                for (var i2 = 0; i2 < properties.Length; i2++)
                {
                    WriteRef((NiObject)properties[i2], s, info, link_map, missing_link_stack);
                }
            }
            if (info.version <= 0x02030000)
            {
                for (var i2 = 0; i2 < 4; i2++)
                {
                    Nif.NifStream(unknown1[i2], s, info);
                }
                Nif.NifStream(unknown2, s, info);
            }
            if ((info.version >= 0x03000000) && (info.version <= 0x04020200))
            {
                Nif.NifStream(hasBoundingVolume, s, info);
                if (hasBoundingVolume)
                {
                    Nif.NifStream(boundingVolume.collisionType, s, info);
                    if ((boundingVolume.collisionType == 0))
                    {
                        Nif.NifStream(boundingVolume.sphere.center, s, info);
                        Nif.NifStream(boundingVolume.sphere.radius, s, info);
                    }
                    if ((boundingVolume.collisionType == 1))
                    {
                        Nif.NifStream(boundingVolume.box.center, s, info);
                        for (var i4 = 0; i4 < 3; i4++)
                        {
                            Nif.NifStream(boundingVolume.box.axis[i4], s, info);
                        }
                        Nif.NifStream(boundingVolume.box.extent, s, info);
                    }
                    if ((boundingVolume.collisionType == 2))
                    {
                        Nif.NifStream(boundingVolume.capsule.center, s, info);
                        Nif.NifStream(boundingVolume.capsule.origin, s, info);
                        Nif.NifStream(boundingVolume.capsule.extent, s, info);
                        Nif.NifStream(boundingVolume.capsule.radius, s, info);
                    }
                    if ((boundingVolume.collisionType == 5))
                    {
                        Nif.NifStream(boundingVolume.halfSpace.plane.normal, s, info);
                        Nif.NifStream(boundingVolume.halfSpace.plane.constant, s, info);
                        Nif.NifStream(boundingVolume.halfSpace.center, s, info);
                    }
                }
            }
            if (info.version >= 0x0A000100)
            {
                WriteRef((NiObject)collisionObject, s, info, link_map, missing_link_stack);
            }

        }

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public override string AsString(bool verbose = false)
        {

            var s = new System.Text.StringBuilder();
            uint array_output_count = 0;
            s.Append(base.AsString());
            numProperties = (uint)properties.Length;
            s.AppendLine($"  Flags:  {flags}");
            s.AppendLine($"  Translation:  {translation}");
            s.AppendLine($"  Rotation:  {rotation}");
            s.AppendLine($"  Scale:  {scale}");
            s.AppendLine($"  Velocity:  {velocity}");
            s.AppendLine($"  Num Properties:  {numProperties}");
            array_output_count = 0;
            for (var i1 = 0; i1 < properties.Length; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Properties[{i1}]:  {properties[i1]}");
                array_output_count++;
            }
            array_output_count = 0;
            for (var i1 = 0; i1 < 4; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    break;
                }
                s.AppendLine($"    Unknown 1[{i1}]:  {unknown1[i1]}");
                array_output_count++;
            }
            s.AppendLine($"  Unknown 2:  {unknown2}");
            s.AppendLine($"  Has Bounding Volume:  {hasBoundingVolume}");
            if (hasBoundingVolume)
            {
                s.AppendLine($"    Collision Type:  {boundingVolume.collisionType}");
                if ((boundingVolume.collisionType == 0))
                {
                    s.AppendLine($"      Center:  {boundingVolume.sphere.center}");
                    s.AppendLine($"      Radius:  {boundingVolume.sphere.radius}");
                }
                if ((boundingVolume.collisionType == 1))
                {
                    s.AppendLine($"      Center:  {boundingVolume.box.center}");
                    array_output_count = 0;
                    for (var i3 = 0; i3 < 3; i3++)
                    {
                        if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                        {
                            s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                            break;
                        }
                        if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                        {
                            break;
                        }
                        s.AppendLine($"        Axis[{i3}]:  {boundingVolume.box.axis[i3]}");
                        array_output_count++;
                    }
                    s.AppendLine($"      Extent:  {boundingVolume.box.extent}");
                }
                if ((boundingVolume.collisionType == 2))
                {
                    s.AppendLine($"      Center:  {boundingVolume.capsule.center}");
                    s.AppendLine($"      Origin:  {boundingVolume.capsule.origin}");
                    s.AppendLine($"      Extent:  {boundingVolume.capsule.extent}");
                    s.AppendLine($"      Radius:  {boundingVolume.capsule.radius}");
                }
                if ((boundingVolume.collisionType == 5))
                {
                    s.AppendLine($"      Normal:  {boundingVolume.halfSpace.plane.normal}");
                    s.AppendLine($"      Constant:  {boundingVolume.halfSpace.plane.constant}");
                    s.AppendLine($"      Center:  {boundingVolume.halfSpace.center}");
                }
            }
            s.AppendLine($"  Collision Object:  {collisionObject}");
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            if ((info.userVersion2 <= 34))
            {
                for (var i2 = 0; i2 < properties.Length; i2++)
                {
                    properties[i2] = FixLink<NiProperty>(objects, link_stack, missing_link_stack, info);
                }
            }
            if (info.version >= 0x0A000100)
            {
                collisionObject = FixLink<NiCollisionObject>(objects, link_stack, missing_link_stack, info);
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            for (var i1 = 0; i1 < properties.Length; i1++)
            {
                if (properties[i1] != null)
                    refs.Add((NiObject)properties[i1]);
            }
            if (collisionObject != null)
                refs.Add((NiObject)collisionObject);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            for (var i1 = 0; i1 < properties.Length; i1++)
            {
            }
            return ptrs;
        }

        //--BEGIN MISC CUSTOM CODE--//

        /*!
         * Clears all embedded bounding box information.  Older NIF files can have a bounding box specified in them which will be used for collision detection instead of evaluating the triangles.
         */
        NIFLIB_API void ClearBoundingBox();

        /*!
         * Returns any embedded bounding box information.  NiAVObject::HasBoundingBox should be called first, as this function will throw an exception if there is no bounding box information in this object.  Older NIF files can have a bounding box specified in them which will be used for collision detection instead of evaluating the triangles.
         * \return The embedded bounding box dimentions.
         * \sa NiAVObject::HasBoundingBox
         */
        NIFLIB_API BoundingBox GetBoundingBox() const;

        /*!
         * Sets new embedded bounding box information.  Older NIF files can have a bounding box specified in them which will be used for collision detection instead of evaluating the triangles.
         * \param[in] n The new bounding box dimentions.
         */
        NIFLIB_API void SetBoundingBox( const BoundingBox & n );

	/*!
	 * Determines whether this object has embedded bounding box information.  Older NIF files can have a bounding box specified in them which will be used for collision detection instead of evaluating the triangles.
	 * \return True if this object has an embedded bounding box, false otherwise.
	 */
	NIFLIB_API bool HasBoundingBox() const;

        /*! 
         * This is a conveniance function that allows you to retrieve the full 4x4 matrix transform of a node.  It accesses the "Rotation," "Translation," and "Scale" attributes and builds a complete 4x4 transformation matrix from them.
         * \return A 4x4 transformation matrix built from the node's transform attributes.
         * \sa INode::GetWorldTransform
         */
        NIFLIB_API Matrix44 GetLocalTransform() const;

        /*! 
         * This is a conveniance function that allows you to set the rotation, scale, and translation of an AV object with a 4x4 matrix transform.
         * \n A 4x4 transformation matrix to set the AVObject's transform attributes with.
         * \sa INode::GetLocalTransform
         */
        NIFLIB_API void SetLocalTransform( const Matrix44 & n );

	/*! 
	 * This function will return a transform matrix that represents the location of this node in world space.  In other words, it concatenates all parent transforms up to the root of the scene to give the ultimate combined transform from the origin for this node.
	 * \return The 4x4 world transform matrix of this node.
	 * \sa INode::GetLocalTransform
	 */
	NIFLIB_API Matrix44 GetWorldTransform() const;

        /*!
         * Returns the parent of this object in the scene graph.  May be NULL.
         * \return The parent of this object in the scene graph.
         */
        NIFLIB_API Ref<NiNode> GetParent() const;

        /*!
         * Adds a property to this object.  Properties specify various charactaristics of the object that affect rendering.  They may be shared among objects.
         * \param[in] obj The new property that is to affect this object.
         */
        NIFLIB_API void AddProperty(NiProperty* obj);

        /*!
         * Removes a property from this object.  Properties specify various charactaristics of the object that affect rendering.  They may be shared among objects.
         * \param[in] obj The property that is no longer to affect this object.
         */
        NIFLIB_API void RemoveProperty(NiProperty* obj);

        /*!
         * Removes all properties from this object.  Properties specify various charactaristics of the object that affect rendering.  They may be shared among objects.
         */
        NIFLIB_API void ClearProperties();

        /*!
         * Retrieves a list of all properties that affect this object.  Properties specify various charactaristics of the object that affect rendering.  They may be shared among objects.
         * \return All the properties that affect this object.
         */
        NIFLIB_API vector<Ref<NiProperty> > GetProperties() const;

        /*!
         * Retrieves the property that matches the specified type, if there is one.  A valid object should not have more than one property of the same type.  Properties specify various charactaristics of the object that affect rendering.  They may be shared among objects.
         * \param[in] compare_to The type constant of the desired property type.
         * \return The property that matches the specified type, or NULL if there isn't a match.
         * \sa NiObject::TypeConst
         */
        NIFLIB_API Ref<NiProperty> GetPropertyByType( const Type & compare_to );

	/*!
	 * Can be used to get the data stored in the flags field for this object.  It is usually better to call more specific flag-toggle functions if they are availiable.
	 * \return The flag data.
	 */
	NIFLIB_API unsigned short GetFlags() const;

        /*!
         * Can be used to set the data stored in the flags field for this object.  It is usually better to call more specific flag-toggle functions if they are availiable.
         * \param[in] n The new flag data.  Will overwrite any existing flag data.
         */
        NIFLIB_API void SetFlags(unsigned short n);

        /*!
         * Used to get the local rotation matrix for this object.  This is a 3x3 matrix that should not include scale or translation components.
         * \return The local 3x3 rotation matrix for this object.
         */
        NIFLIB_API Matrix33 GetLocalRotation() const;

        /*!
         * Used to set the local rotation matrix for this object.  This is a 3x3 matrix that should not include scale or translation components.
         * \param[in] n The new local 3x3 rotation matrix for this object.
         */
        NIFLIB_API void SetLocalRotation( const Matrix33 & n );

	/*!
	 * Used to get the local translation vector for this object.  This determines the object's offset from its parent.
	 * \return The local translation vector for this object.
	 */
	NIFLIB_API Vector3 GetLocalTranslation() const;

        /*!
         * Used to set the local translation vector for this object.  This determines the object's offset from its parent.
         * \param[in] n The new local translation vector for this object.
         */
        NIFLIB_API void SetLocalTranslation( const Vector3 & n );

	/*!
	 * Used to get the local scale factor for this object.  The NIF format does not support separate scales along different axis, and many games do not react well to scale factors other than 1.0.
	 * \return The local scale factor for this object.
	 */
	NIFLIB_API float GetLocalScale() const;

        /*!
         * Used to set the local scale factor for this object.  The NIF format does not support separate scales along different axis, and many games do not react well to scale factors other than 1.0.
         * \param[in] n The new local scale factor for this object.
         */
        NIFLIB_API void SetLocalScale(float n);

        /*!
         * Gets the velocity vector for this object.  This vector exists in older NIF files and seems to have no function.
         * \return The velocity vector for this object.
         */
        NIFLIB_API Vector3 GetVelocity() const;

        /*!
         * Sets the velocity vector for this object.  This vector exists in older NIF files and seems to have no function.
         * \param[in] n The new velocity vector for this object.
         */
        NIFLIB_API void SetVelocity( const Vector3 & n );

	/*!
	 * Gets the current visibility of this object as specified in its flag data.
	 * \return True if the object is visible, false otherwise.
	 */
	NIFLIB_API bool GetVisibility() const;

        /*!
         * Sets the current visibility of this object by altering its flag data.
         * \param[in] n Whether or not the object will now be visible.  True if visible, false otherwise.
         */
        NIFLIB_API void SetVisibility(bool n);

        /*!
         * Gets the current collision object for this object.  Usually a bounding box.  In Oblivion this links to the Havok objects.
         * \return The current collision object for this object.
         */
        NIFLIB_API Ref<NiCollisionObject> GetCollisionObject() const;

        /*!
         * Sets the collision object for this object.  Usually a bounding box.  In Oblivion this links to the Havok objects.
         * \param[in] value The new collision object to use.
         */
        NIFLIB_API void SetCollisionObject(NiCollisionObject* value);

        /*!
         * Used to get and set the collision type of a NiAVObject.
         */
        enum CollisionType
        {
            /*! No collision */
            CT_NONE = 0,
            /*! Collision detection will use the triangles themselves.  Possibly incompatible with triangle strips. */
            CT_TRIANGLES = 1,
            /*! Collision detection will use the embedded bounding box information. */
            CT_BOUNDINGBOX = 2,
            /*! Collision detection will continue on to the lower objects in the scene graph. */
            CT_CONTINUE = 3
        };

        /*!
         * Gets the current collision detection setting from the object's flag data.
         * \return The collision detection setting for this object.
         */
        NIFLIB_API CollisionType GetCollisionMode() const;

        /*!
         * Sets the current collision detection setting in the object's flag data.
         * \param[in] value The new collision detection setting for this object.
         */
        NIFLIB_API void SetCollisionMode(CollisionType value);

        /*!
         * NIFLIB_HIDDEN function.  For internal use only.
         * Called by NiNode during the addition of new children.
         */
        NIFLIB_HIDDEN void SetParent(NiNode* new_parent);

        protected:
	NiNode* parent;

        //--END CUSTOM CODE--//


    }

}