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
 * Describes a visible scene element with vertices like a mesh, a particle system,
 * lines, etc.
 */
public class NiGeometry : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiGeometry", NiAVObject.TYPE);
	/*!  */
	internal NiBound bound;
	/*!  */
	internal NiObject skin;
	/*! Data index (NiTriShapeData/NiTriStripData). */
	internal NiGeometryData data;
	/*!  */
	internal NiSkinInstance skinInstance;
	/*!  */
	internal MaterialData materialData;
	/*!  */
	internal BSShaderProperty shaderProperty;
	/*!  */
	internal NiAlphaProperty alphaProperty;

	public NiGeometry() {
	skin = null;
	data = null;
	skinInstance = null;
	shaderProperty = null;
	alphaProperty = null;
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
public static NiObject Create() => new NiGeometry();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			Nif.NifStream(out bound.center, s, info);
			Nif.NifStream(out bound.radius, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.userVersion2 < 100)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 < 100))) {
		if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
			Nif.NifStream(out materialData.hasShader, s, info);
			if (materialData.hasShader) {
				Nif.NifStream(out materialData.shaderName, s, info);
				Nif.NifStream(out materialData.shaderExtraData, s, info);
			}
		}
		if (info.version >= 0x14020005) {
			Nif.NifStream(out materialData.numMaterials, s, info);
			materialData.materialName = new IndexString[materialData.numMaterials];
			for (var i3 = 0; i3 < materialData.materialName.Count; i3++) {
				Nif.NifStream(out materialData.materialName[i3], s, info);
			}
			materialData.materialExtraData = new int[materialData.numMaterials];
			for (var i3 = 0; i3 < materialData.materialExtraData.Count; i3++) {
				Nif.NifStream(out materialData.materialExtraData[i3], s, info);
			}
			Nif.NifStream(out materialData.activeMaterial, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
			Nif.NifStream(out materialData.unknownByte, s, info);
		}
		if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
			Nif.NifStream(out materialData.unknownInteger2, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(out materialData.materialNeedsUpdate, s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 >= 100))) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
				Nif.NifStream(out materialData.hasShader, s, info);
				if (materialData.hasShader) {
					Nif.NifStream(out materialData.shaderName, s, info);
					Nif.NifStream(out materialData.shaderExtraData, s, info);
				}
			}
			if (info.version >= 0x14020005) {
				Nif.NifStream(out materialData.numMaterials, s, info);
				materialData.materialName = new IndexString[materialData.numMaterials];
				for (var i4 = 0; i4 < materialData.materialName.Count; i4++) {
					Nif.NifStream(out materialData.materialName[i4], s, info);
				}
				materialData.materialExtraData = new int[materialData.numMaterials];
				for (var i4 = 0; i4 < materialData.materialExtraData.Count; i4++) {
					Nif.NifStream(out materialData.materialExtraData[i4], s, info);
				}
				Nif.NifStream(out materialData.activeMaterial, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
				Nif.NifStream(out materialData.unknownByte, s, info);
			}
			if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
				Nif.NifStream(out materialData.unknownInteger2, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(out materialData.materialNeedsUpdate, s, info);
			}
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			Nif.NifStream(bound.center, s, info);
			Nif.NifStream(bound.radius, s, info);
			WriteRef((NiObject)skin, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.userVersion2 < 100)) {
		WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		WriteRef((NiObject)skinInstance, s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			WriteRef((NiObject)skinInstance, s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 < 100))) {
		materialData.numMaterials = (uint)materialData.materialName.Count;
		if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
			Nif.NifStream(materialData.hasShader, s, info);
			if (materialData.hasShader) {
				Nif.NifStream(materialData.shaderName, s, info);
				Nif.NifStream(materialData.shaderExtraData, s, info);
			}
		}
		if (info.version >= 0x14020005) {
			Nif.NifStream(materialData.numMaterials, s, info);
			for (var i3 = 0; i3 < materialData.materialName.Count; i3++) {
				Nif.NifStream(materialData.materialName[i3], s, info);
			}
			for (var i3 = 0; i3 < materialData.materialExtraData.Count; i3++) {
				Nif.NifStream(materialData.materialExtraData[i3], s, info);
			}
			Nif.NifStream(materialData.activeMaterial, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
			Nif.NifStream(materialData.unknownByte, s, info);
		}
		if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
			Nif.NifStream(materialData.unknownInteger2, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(materialData.materialNeedsUpdate, s, info);
		}
	}
	if ((info.version >= 0x0A000100) && ((info.userVersion2 >= 100))) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			materialData.numMaterials = (uint)materialData.materialName.Count;
			if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
				Nif.NifStream(materialData.hasShader, s, info);
				if (materialData.hasShader) {
					Nif.NifStream(materialData.shaderName, s, info);
					Nif.NifStream(materialData.shaderExtraData, s, info);
				}
			}
			if (info.version >= 0x14020005) {
				Nif.NifStream(materialData.numMaterials, s, info);
				for (var i4 = 0; i4 < materialData.materialName.Count; i4++) {
					Nif.NifStream(materialData.materialName[i4], s, info);
				}
				for (var i4 = 0; i4 < materialData.materialExtraData.Count; i4++) {
					Nif.NifStream(materialData.materialExtraData[i4], s, info);
				}
				Nif.NifStream(materialData.activeMaterial, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
				Nif.NifStream(materialData.unknownByte, s, info);
			}
			if ((info.version >= 0x0A040001) && (info.version <= 0x0A040001)) {
				Nif.NifStream(materialData.unknownInteger2, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(materialData.materialNeedsUpdate, s, info);
			}
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		WriteRef((NiObject)shaderProperty, s, info, link_map, missing_link_stack);
		WriteRef((NiObject)alphaProperty, s, info, link_map, missing_link_stack);
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
	if (IsDerivedType(NiParticleSystem.TYPE)) {
		s.AppendLine($"    Center:  {bound.center}");
		s.AppendLine($"    Radius:  {bound.radius}");
		s.AppendLine($"    Skin:  {skin}");
	}
	s.AppendLine($"  Data:  {data}");
	s.AppendLine($"  Skin Instance:  {skinInstance}");
	materialData.numMaterials = (uint)materialData.materialName.Count;
	s.AppendLine($"  Has Shader:  {materialData.hasShader}");
	if (materialData.hasShader) {
		s.AppendLine($"    Shader Name:  {materialData.shaderName}");
		s.AppendLine($"    Shader Extra Data:  {materialData.shaderExtraData}");
	}
	s.AppendLine($"  Num Materials:  {materialData.numMaterials}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materialData.materialName.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Material Name[{i1}]:  {materialData.materialName[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < materialData.materialExtraData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Material Extra Data[{i1}]:  {materialData.materialExtraData[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Active Material:  {materialData.activeMaterial}");
	s.AppendLine($"  Unknown Byte:  {materialData.unknownByte}");
	s.AppendLine($"  Unknown Integer 2:  {materialData.unknownInteger2}");
	s.AppendLine($"  Material Needs Update:  {materialData.materialNeedsUpdate}");
	s.AppendLine($"  Shader Property:  {shaderProperty}");
	s.AppendLine($"  Alpha Property:  {alphaProperty}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if ((info.userVersion2 >= 100)) {
		if (IsDerivedType(NiParticleSystem.TYPE)) {
			skin = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.userVersion2 < 100)) {
		data = FixLink<NiGeometryData>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			data = FixLink<NiGeometryData>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x0303000D) && ((info.userVersion2 < 100))) {
		skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 >= 100)) {
		if ((!IsDerivedType(NiParticleSystem.TYPE))) {
			skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x14020007) && (info.userVersion == 12)) {
		shaderProperty = FixLink<BSShaderProperty>(objects, link_stack, missing_link_stack, info);
		alphaProperty = FixLink<NiAlphaProperty>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (skin != null)
		refs.Add((NiObject)skin);
	if (data != null)
		refs.Add((NiObject)data);
	if (skinInstance != null)
		refs.Add((NiObject)skinInstance);
	if (shaderProperty != null)
		refs.Add((NiObject)shaderProperty);
	if (alphaProperty != null)
		refs.Add((NiObject)alphaProperty);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}

        //--BEGIN:FILE FOOT--//

        /*!
         * Binds this geometry to a list of bones.  Creates and attatches a
         * NiSkinInstance and NiSkinData class. The bones must have a common
         * ancestor in the scenegraph.  This becomes the skeleton root.
         */
        NIFLIB_API void BindSkin(vector<Ref<NiNode>>& bone_nodes );

        /*!
         * Binds this geometry to a list of bones.  Creates and attatches a
         * NiSkinInstance and NiSkinData class. The bones must have a common
         * ancestor in the scenegraph.  This becomes the skeleton root.
         */
        NIFLIB_API void BindSkinWith(vector<Ref<NiNode>>& bone_nodes, NiObject* (* SkinInstConstructor)() );

        /*!
         * Unbinds this geometry from the bones.  This removes the NiSkinInstance and NiSkinData objects and causes this geometry to stop behaving as a skin.
         */
        NIFLIB_API void UnbindSkin();

        /*!
         * Sets the skin weights in the attached NiSkinData object.
         * The version on this class calculates the center and radius of
         * each set of affected vertices automatically.
         */
        NIFLIB_API void SetBoneWeights(unsigned int bone_index, const vector<SkinWeight> & n );

	/*!
	 * Retrieves the NiSkinInstance object used by this geometry node, if any.
	 * \return The NiSkinInstance object used by this geometry node, or NULL if none is used.
	 */
	NIFLIB_API Ref<NiSkinInstance> GetSkinInstance() const;

        /*!
         * Sets the NiSkinInstance object used by this geometry node.
         * \param[in] skin The NiSkinInstance object to be used by this geometry node, or NULL if none is to be used.
         */
        NIFLIB_API void SetSkinInstance(Ref<NiSkinInstance> skin);

        /*!
         * Retrieves the geometry data object used by this geometry node, if any.  This contains the vertices, normals, etc. and can be shared among several geometry nodes.
         * \return The geometry data object, or NULL if there is none.
         */
        NIFLIB_API Ref<NiGeometryData> GetData() const;

        /*!
         * Sets the geometry data object used by this geometry node.  This contains the vertices, normals, etc. and can be shared among several geometry nodes.
         * \param[in] n The new geometry data object, or NULL to clear the current one.
         */
        NIFLIB_API void SetData(NiGeometryData* n);

        /*!
         * Retrieves the name of the shader used by this geometry node.  The allowable values are game-dependent.
         * \return The shader name.
         */
        NIFLIB_API string GetShader() const;

        /*!
         * Sets the name of the shader used by this geometry node.  The allowable values are game-dependent.
         * \param[in] n The new shader name.
         */
        NIFLIB_API void SetShader( const string & n );

	/*
	 * Returns the position of the verticies and values of the normals after they
	 * have been deformed by the positions of their skin influences.
	 * \param[out] vertices A vector that will be filled with the skin deformed position of the verticies.
	 * \param[out] normals A vector thta will be filled with the skin deformed normal values.
	 */
	NIFLIB_API void GetSkinDeformation(vector<Vector3> & vertices, vector<Vector3> & normals ) const;

        /*
         * Applies the local transform values to the vertices of the geometry and
         * zeros them out to the identity.
         */
        NIFLIB_API void ApplyTransforms();

        /*
         * Propogates the transforms between this skin and the skeleton root,
         * and then applies them to the verticies of this skin.  Sets the overall
         * skin data transform to the identity.
         */
        NIFLIB_API void ApplySkinOffset();

        /*
         * This automatically normalizes all the skin weights for this geometry node if it is bound to bones as a skin.  In other words, it will guarantee that the weights for all bones on each vertex will add up to 1.0.  This can be used to correct bad input data.
         */
        NIFLIB_API void NormalizeSkinWeights();

        /*
         * Used to determine whether this mesh is influenced by bones as a skin.
         * \return True if this mesh is a skin, false otherwise.
         */
        NIFLIB_API bool IsSkin();

        // Active Material.
        // \return The current value.
        NIFLIB_API int GetActiveMaterial() const;

        // Active Material.
        // \param[in] value The new value.
        NIFLIB_API void SetActiveMaterial(int value);

        // Shader.
        // \return The current value.
        NIFLIB_API bool HasShader() const;

        // BSProperty
        // \param[in] index Index of property to be retrieved.
        // \return The propterty.
        NIFLIB_API Ref<NiProperty> GetBSProperty(short index);

        // BSProperty
        // \param[in] index Index of property to be set.
        // \param[in] index Property to be set.
        NIFLIB_API void SetBSProperty(short index, Ref<NiProperty> value);

        /*
          * Returns the array of the only 2 properties that are specific to Bethesda
          * \return Returns the array of the 2 properties
          */
        NIFLIB_API array<2, Ref<NiProperty > > GetBSProperties();

        /*
          * Sets the array of the only 2 properties that are specific to Bethesda
          * \param[in] The new array of properties
          */
        NIFLIB_API void SetBSProperties(array<2, Ref<NiProperty>> value);

        //--END:CUSTOM--//
    }

}