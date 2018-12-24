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

/*! An object that can be rendered. */
public class NiRenderObject : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRenderObject", NiAVObject.TYPE);
	/*! Per-material data. */
	public MaterialData materialData;

	public NiRenderObject() {
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
	public static NiObject Create() => new NiRenderObject();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
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
			for (var i3 = 0; i3 < materialData.materialName.Length; i3++) {
				Nif.NifStream(out materialData.materialName[i3], s, info);
			}
			materialData.materialExtraData = new int[materialData.numMaterials];
			for (var i3 = 0; i3 < materialData.materialExtraData.Length; i3++) {
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

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		materialData.numMaterials = (uint)materialData.materialName.Length;
		if ((info.version >= 0x0A000100) && (info.version <= 0x14010003)) {
			Nif.NifStream(materialData.hasShader, s, info);
			if (materialData.hasShader) {
				Nif.NifStream(materialData.shaderName, s, info);
				Nif.NifStream(materialData.shaderExtraData, s, info);
			}
		}
		if (info.version >= 0x14020005) {
			Nif.NifStream(materialData.numMaterials, s, info);
			for (var i3 = 0; i3 < materialData.materialName.Length; i3++) {
				Nif.NifStream(materialData.materialName[i3], s, info);
			}
			for (var i3 = 0; i3 < materialData.materialExtraData.Length; i3++) {
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

	/*!
	 * Summarizes the information contained in this object in English.
	 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
	 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
	 */
	public override string asString(bool verbose = false) {

		var s = new System.Text.StringBuilder();
		uint array_output_count = 0;
		s.Append(base.asString());
		materialData.numMaterials = (uint)materialData.materialName.Length;
		s.AppendLine($"    Has Shader:  {materialData.hasShader}");
		if (materialData.hasShader) {
			s.AppendLine($"      Shader Name:  {materialData.shaderName}");
			s.AppendLine($"      Shader Extra Data:  {materialData.shaderExtraData}");
		}
		s.AppendLine($"    Num Materials:  {materialData.numMaterials}");
		array_output_count = 0;
		for (var i2 = 0; i2 < materialData.materialName.Length; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Material Name[{i2}]:  {materialData.materialName[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < materialData.materialExtraData.Length; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Material Extra Data[{i2}]:  {materialData.materialExtraData[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Active Material:  {materialData.activeMaterial}");
		s.AppendLine($"    Unknown Byte:  {materialData.unknownByte}");
		s.AppendLine($"    Unknown Integer 2:  {materialData.unknownInteger2}");
		s.AppendLine($"    Material Needs Update:  {materialData.materialNeedsUpdate}");
		return s.ToString();

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

		base.FixLinks(objects, link_stack, missing_link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetRefs() {
		var refs = base.GetRefs();
		return refs;
	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetPtrs() {
		var ptrs = base.GetPtrs();
		return ptrs;
	}


}

}