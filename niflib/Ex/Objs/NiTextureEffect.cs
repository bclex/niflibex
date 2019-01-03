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
 * Represents an effect that uses projected textures such as projected lights
 * (gobos), environment maps, and fog maps.
 */
public class NiTextureEffect : NiDynamicEffect {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiTextureEffect", NiDynamicEffect.TYPE);
	/*! Model projection matrix.  Always identity? */
	internal Matrix33 modelProjectionMatrix;
	/*! Model projection transform.  Always (0,0,0)? */
	internal Vector3 modelProjectionTransform;
	/*! Texture Filtering mode. */
	internal TexFilterMode textureFiltering;
	/*!  */
	internal ushort maxAnisotropy;
	/*! Texture Clamp mode. */
	internal TexClampMode textureClamping;
	/*! The type of effect that the texture is used for. */
	internal TextureType textureType;
	/*! The method that will be used to generate UV coordinates for the texture effect. */
	internal CoordGenType coordinateGenerationType;
	/*! Image index. */
	internal NiImage image;
	/*! Source texture index. */
	internal NiSourceTexture sourceTexture;
	/*! Determines whether a clipping plane is used. */
	internal byte enablePlane;
	/*!  */
	internal NiPlane plane;
	/*!  */
	internal short ps2L;
	/*!  */
	internal short ps2K;
	/*! Unknown: 0. */
	internal ushort unknownShort;

	public NiTextureEffect() {
	textureFiltering = TexFilterMode.FILTER_TRILERP;
	maxAnisotropy = (ushort)0;
	textureClamping = TexClampMode.WRAP_S_WRAP_T;
	textureType = TextureType.TEX_ENVIRONMENT_MAP;
	coordinateGenerationType = CoordGenType.CG_SPHERE_MAP;
	image = null;
	sourceTexture = null;
	enablePlane = (byte)0;
	ps2L = (short)0;
	ps2K = (short)-75;
	unknownShort = (ushort)0;
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
public static NiObject Create() => new NiTextureEffect();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out modelProjectionMatrix, s, info);
	Nif.NifStream(out modelProjectionTransform, s, info);
	Nif.NifStream(out textureFiltering, s, info);
	if (info.version >= 0x14050004) {
		Nif.NifStream(out maxAnisotropy, s, info);
	}
	Nif.NifStream(out textureClamping, s, info);
	Nif.NifStream(out textureType, s, info);
	Nif.NifStream(out coordinateGenerationType, s, info);
	if (info.version <= 0x03010000) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if (info.version >= 0x04000000) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out enablePlane, s, info);
	Nif.NifStream(out plane.normal, s, info);
	Nif.NifStream(out plane.constant, s, info);
	if (info.version <= 0x0A020000) {
		Nif.NifStream(out ps2L, s, info);
		Nif.NifStream(out ps2K, s, info);
	}
	if (info.version <= 0x0401000C) {
		Nif.NifStream(out unknownShort, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(modelProjectionMatrix, s, info);
	Nif.NifStream(modelProjectionTransform, s, info);
	Nif.NifStream(textureFiltering, s, info);
	if (info.version >= 0x14050004) {
		Nif.NifStream(maxAnisotropy, s, info);
	}
	Nif.NifStream(textureClamping, s, info);
	Nif.NifStream(textureType, s, info);
	Nif.NifStream(coordinateGenerationType, s, info);
	if (info.version <= 0x03010000) {
		WriteRef((NiObject)image, s, info, link_map, missing_link_stack);
	}
	if (info.version >= 0x04000000) {
		WriteRef((NiObject)sourceTexture, s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(enablePlane, s, info);
	Nif.NifStream(plane.normal, s, info);
	Nif.NifStream(plane.constant, s, info);
	if (info.version <= 0x0A020000) {
		Nif.NifStream(ps2L, s, info);
		Nif.NifStream(ps2K, s, info);
	}
	if (info.version <= 0x0401000C) {
		Nif.NifStream(unknownShort, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Model Projection Matrix:  {modelProjectionMatrix}");
	s.AppendLine($"  Model Projection Transform:  {modelProjectionTransform}");
	s.AppendLine($"  Texture Filtering:  {textureFiltering}");
	s.AppendLine($"  Max Anisotropy:  {maxAnisotropy}");
	s.AppendLine($"  Texture Clamping:  {textureClamping}");
	s.AppendLine($"  Texture Type:  {textureType}");
	s.AppendLine($"  Coordinate Generation Type:  {coordinateGenerationType}");
	s.AppendLine($"  Image:  {image}");
	s.AppendLine($"  Source Texture:  {sourceTexture}");
	s.AppendLine($"  Enable Plane:  {enablePlane}");
	s.AppendLine($"  Normal:  {plane.normal}");
	s.AppendLine($"  Constant:  {plane.constant}");
	s.AppendLine($"  PS2 L:  {ps2L}");
	s.AppendLine($"  PS2 K:  {ps2K}");
	s.AppendLine($"  Unknown Short:  {unknownShort}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x03010000) {
		image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
	}
	if (info.version >= 0x04000000) {
		sourceTexture = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (image != null)
		refs.Add((NiObject)image);
	if (sourceTexture != null)
		refs.Add((NiObject)sourceTexture);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

        //--BEGIN:FILE FOOT--//
        /*!
         * Gets or sets the model projection matrix of this effect.  This always seems to be set to the identity.
         * \param[in] value The new model projection matrix.
         */
        public Matrix33 ModelProjectionMatrix
        {
            get => modelProjectionMatrix;
            set => modelProjectionMatrix = value;
        }

        /*!
         * Gets or sets the model projection transform of this effect.  This always seems to be set to (0,0,0).
         * \param[in] value The new model projection transform.
         */
        public Vector3 ModelProjectionTransform
        {
            get => modelProjectionTransform;
            set => modelProjectionTransform = value;
        }

        /*!
         * Gets or sets the texture filtering mode used by this effect.
         * \param[in] value The new texture filtering mode.
         */
        public TexFilterMode TextureFiltering
        {
            get => textureFiltering;
            set => textureFiltering = value;
        }
        
        /*!
         * Gets or sets the texture clamping mode used by this effect.
         * \param[in] value The new texture clamping mode.
         */
        public TexClampMode TextureClamping
        {
            get => textureClamping;
            set => textureClamping = value;
        }

        /*!
         * Gets or sets the texture type used by this effect.  Valid values are:
         * 0: PROJECTED_LIGHT
         * 1: PROJECTED_SHADOW
         * 2: ENVIRONMENT_MAP (Usual value)
         * 3: FOG_MAP
         * \param[in] value The new texture type.
         */
        public TextureType TextureType
        {
            get => textureType;
            set => textureType = value;
        }

        /*!
         * Gets or sets the texture coordinate generation mode.  Valid values are:
         * 0: WORLD_PARALLEL
         * 1: WORLD_PERSPECTIVE
         * 2: SPHERE_MAP (Usual value)
         * 3: SPECULAR_CUBE_MAP
         * 4: DIFFUSE_CUBE_MAP
         * \return The new texture coordinate generation mode.
         */
        public CoordGenType CoordinateGenerationType
        {
            get => coordinateGenerationType;
            set => coordinateGenerationType = value;
        }

        /*!
         * Gets or sets the source texture index.
         * \param[in] value The new source texture index.
         */
        public NiSourceTexture SourceTexture
        {
            get => sourceTexture;
            set => sourceTexture = value;
        }

        /*!
         * Gets or sets the clipping plane behavior.  Valid values are:
         * 0: Disabled (Usual value)
         * 1: Enabled
         * \param[in] value The new clipping plane behavior.
         */
        public byte EnablePlane
        {
            get => enablePlane;
            set => enablePlane = value;
        }
        /*!
         * Gets or sets a Playstation 2 - specific value.  Can just be left at the default of 0.
         * \param[in] value The new PS2 L value.
         */
        public short Ps2L
        {
            get => ps2L;
            set => ps2L = value;
        }

        /*!
         * Gets or sets a Playstation 2 - specific value.  Can just be left at the default of 0xFFB5.
         * \param[in] value The new PS2 K value.
         */
        public short Ps2K
        {
            get => ps2K;
            set => ps2K = value;
        }
        //--END:CUSTOM--//
    }

}