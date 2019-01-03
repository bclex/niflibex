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
        public void BindSkin(IList<NiNode> bone_nodes) => BindSkinWith(bone_nodes, null);

        /*!
         * Binds this geometry to a list of bones.  Creates and attatches a
         * NiSkinInstance and NiSkinData class. The bones must have a common
         * ancestor in the scenegraph.  This becomes the skeleton root.
         */
        public void BindSkinWith(IList<NiNode> bone_nodes, Action<NiObject> skinInstConstructor)
        {
            if (skinInstConstructor == null)
                skinInstConstructor = NiSkinInstance.Create;
            //Ensure skin is not aleady bound
            if (skinInstance != null)
                throw new Exception("You have attempted to re-bind a skin that is already bound.  Unbind it first.");
            //Ensure that some bones are given
            if (bone_nodes.Count == 0)
                throw new Exception("You must specify at least one bone node.");

            //--Find a suitable skeleton root--//
            //The skeleton root will be the common ancestor of all bones which influence this skin,
            //and the skin object itself.
            var objects = new List<NiAVObject>();
            objects.Add(this);
            for (var i = 0; i < bone_nodes.Count; ++i)
                objects.Add(bone_nodes[i]);
            var skeleton_root = Nif.FindCommonAncestor(objects);
            if (skeleton_root == null)
                throw new Exception("Failed to find suitable skeleton root.");
            //Create a skin instance using the bone and root data
            skinInstance = skinInstConstructor() as NiSkinInstance;
            if (skinInstance == null)
                throw new Exception("Failed to construct NiSkinInstance");
            skinInstance.BindSkin(skeleton_root, bone_nodes);
            //Create a NiSkinData object based on this mesh
            skinInstance.SkinData = new NiSkinData(this);
        }

        /*!
         * Unbinds this geometry from the bones.  This removes the NiSkinInstance and NiSkinData objects and causes this geometry to stop behaving as a skin.
         */
        public void UnbindSkin()
        {
            //Clear skin instance
            skinInstance = null;
        }

        // Calculate bounding sphere using minimum-volume axis-align bounding box.  Its fast but not a very good fit.
        static void CalcAxisAlignedBox(IList<BoneVertData> n, IList<Vector3> vertices, out Vector3 center, out float radius)
        {
            //--Calculate center & radius--//

            //Set lows and highs to first vertex
            var lows = vertices[n[0].index];
            var highs = vertices[n[0].index];

            //Iterate through the vertices, adjusting the stored values
            //if a vertex with lower or higher values is found
            for (var i = 0; i < n.Count; ++i)
            {
                var v = vertices[n[i].index];
                if (v.x > highs.x) highs.x = v.x;
                else if (v.x < lows.x) lows.x = v.x;
                if (v.y > highs.y) highs.y = v.y;
                else if (v.y < lows.y) lows.y = v.y;
                if (v.z > highs.z) highs.z = v.z;
                else if (v.z < lows.z) lows.z = v.z;
            }

            //Now we know the extent of the shape, so the center will be the average
            //of the lows and highs
            center = (highs + lows) / 2.0f;

            //The radius will be the largest distance from the center
            Vector3 diff;
            float dist2 = 0.0f, maxdist2 = 0.0f;
            for (var i = 0; i < n.Count; ++i)
            {
                var v = vertices[n[i].index];
                diff = center - v;
                dist2 = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (dist2 > maxdist2) maxdist2 = dist2;
            }
            radius = (float)Math.Sqrt(maxdist2);
        }

        // Calculate bounding sphere using average position of the points.  Better fit but slower.
        static void CalcCenteredSphere(IList<BoneVertData> n, IList<Vector3> vertices, out Vector3 center, out float radius)
        {
            var nv = n.Count;
            var sum = new Vector3();
            for (var i = 0; i < nv; ++i)
                sum += vertices[n[i].index];
            center = sum / (float)nv;
            radius = 0.0f;
            for (var i = 0; i < nv; ++i)
            {
                var diff = vertices[n[i].index] - center;
                var mag = diff.Magnitude();
                radius = Math.Max(radius, mag);
            }
        }

        /*!
         * Sets the skin weights in the attached NiSkinData object.
         * The version on this class calculates the center and radius of
         * each set of affected vertices automatically.
         */
        public void SetBoneWeights(uint bone_index, IList<BoneVertData> value)
        {
            if (value.Count == 0)
                throw new Exception("You must specify at least one weight value.");
            var skinInst = SkinInstance;
            if (skinInst == null)
                throw new Exception("You must bind a skin before setting vertex weights.  No NiSkinInstance found.");
            var skinData = skinInst.SkinData;
            if (skinData == null)
                throw new Exception("You must bind a skin before setting vertex weights.  No NiSkinData found.");
            var geomData = Data;
            if (geomData == null)
                throw new Exception("Attempted to set weights on a mesh with no geometry data.");
            //Get vertex array
            var vertices = geomData.GetVertices();
            Vector3 center; float radius;
            //CalcCenteredSphere(value, vertices, center, radius);
            CalcAxisAlignedBox(value, vertices, out center, out radius);
            //Translate center by bone matrix
            center = skinData.GetBoneTransform(bone_index) * center;
            skinData.SetBoneWeights(bone_index, value, center, radius);
        }

        /*!
         * Gets or sets the NiSkinInstance object used by this geometry node.
         * \param[in] skin The NiSkinInstance object to be used by this geometry node, or NULL if none is to be used.
         */
        public NiSkinInstance SkinInstance
        {
            get => skinInstance;
            set => skinInstance = value;
        }

        /*!
         * Gets or sets the geometry data object used by this geometry node.  This contains the vertices, normals, etc. and can be shared among several geometry nodes.
         * \param[in] n The new geometry data object, or NULL to clear the current one.
         */
        public NiGeometryData Data
        {
            get => data;
            set => data = value;
        }

        /*!
         * Sets the name of the shader used by this geometry node.  The allowable values are game-dependent.
         * \param[in] n The new shader name.
         */
        public string Shader
        {
            get => shaderName;
            set
            {
                //Check if name is blank, if so clear shader
                if (value.Length == 0)
                {
                    hasShader = false;
                    shaderName = string.Empty;
                }
                else shaderName = value;
            }
        }

        /*
         * Returns the position of the verticies and values of the normals after they
         * have been deformed by the positions of their skin influences.
         * \param[out] vertices A vector that will be filled with the skin deformed position of the verticies.
         * \param[out] normals A vector thta will be filled with the skin deformed normal values.
         */
        public void GetSkinDeformation(IList<Vector3> vertices, IList<Vector3> normals)
        {
            //--Get required data & insure validity--//
            var geom_data = Data;
            if (geom_data == null)
                throw new Exception("This NiGeometry has no NiGeometryData so there are no vertices to get.");
            var skin_inst = SkinInstance;
            if (skin_inst == null)
                throw new Exception("This NiGeometry is not influenced by a skin.");
            var skin_data = skin_inst.SkinData;
            if (skin_data == null)
                throw new Exception("Skin Data is missing, cannot calculate skin influenced vertex position.");
            //Ensure that skin instance bone count & skin data bone count match
            if (skin_inst.BoneCount != skin_data.BoneCount)
                throw new Exception("Skin Instance and Skin Data bone count do not match.");
            //Get skeleton root
            var skel_root = skin_inst.GetSkeletonRoot();
            if (skel_root == null)
                throw new Exception("Skin Instance is not bound to a skeleton root.");

            //Get the vertices & bone nodes
            var in_verts = geom_data.GetVertices();
            var in_norms = geom_data.GetNormals();
            var bone_nodes = skin_inst.GetBones();

            //Set up output arrays to hold the transformed vertices and normals
            vertices.Resize(in_verts.size());
            normals.Resize(in_norms.size());

            //Transform vertices into position based on skin data
            var root_world = skel_root->GetWorldTransform();
            var geom_world = GetWorldTransform();
            for (var i = 0; i < skin_data.BoneCount; ++i)
            {
                var bone_world = bone_nodes[i].GetWorldTransform();
                var bone_offset = skin_data.GetBoneTransform(i);
                var weights = skin_data.GetBoneWeights(i);
                var vert_trans = bone_offset * bone_world;
                var norm_trans = new Matrix44(vert_trans.GetRotation());
                for (var j = 0; j < weights.Count; ++j)
                {
                    var index = weights[j].index;
                    var weight = weights[j].weight;
                    if (index < vertices.Count)
                        vertices[index] += (vert_trans * in_verts[index]) * weight;
                    if (index < normals.Count)
                        normals[index] += (norm_trans * in_norms[index]) * weight;
                }
            }

            //Transform all vertices to final position
            var geom_world_inv = geom_world.Inverse();
            var geom_world_inv_rot = new Matrix44(geom_world_inv.GetRotation());
            for (var i = 0; i < vertices.Count; ++i)
                vertices[i] = geom_world_inv * vertices[i];
            for (var i = 0; i < normals.Count; ++i)
                normals[i] = geom_world_inv_rot * normals[i];
            //normals[i] = normals[i].Normalized();
        }

        /*
         * Applies the local transform values to the vertices of the geometry and
         * zeros them out to the identity.
         */
        public void ApplyTransforms()
        {
            //Get Data
            var geom_data = Data;
            if (geom_data == null)
                throw new Exception("Called ApplyTransform on a NiGeometry object that has no NiGeometryData attached.");
            //Transform the vertices by the local transform of this mesh
            geom_data.Transform(GetLocalTransform());
            //Now that the transforms have been applied, clear them to the identity
            SetLocalTransform(Matrix44.IDENTITY);
        }

        /*
         * Propogates the transforms between this skin and the skeleton root,
         * and then applies them to the verticies of this skin.  Sets the overall
         * skin data transform to the identity.
         */
        public void ApplySkinOffset()
        {
            if (Parent == null)
                throw new Exception("Attempted to apply skin transforms on a shape with no parent.");
            if (skinInstance == null)
                throw new Exception("Attempted to apply skin transforms on a shape with no skin instance.");
            if (skinInstance.SkinData == null)
                throw new Exception("Attempted to apply skin transforms on a shape with no skin data.");
            //Get ancestors
            var ancestors = Nif.ListAncestors(this);
            //Propagate transforms on ancestors below skeleton root
            var below_root = false;
            foreach (var it in ancestors)
            {
                if (it == skinInstance.SkeletonRoot)
                {
                    below_root = true;
                    continue;
                }
                if (below_root)
                    it.PropagateTransform();
            }
            //Now apply the transforms to the vertices and normals of this mesh
            ApplyTransforms();
            //Set the skin overall transform to the identity
            skinInstance.SkinData.SetOverallTransform(Matrix44.IDENTITY);
            //Reset skin offsets
            skinInstance.SkinData.ResetOffsets(this);
        }

        /*
         * This automatically normalizes all the skin weights for this geometry node if it is bound to bones as a skin.  In other words, it will guarantee that the weights for all bones on each vertex will add up to 1.0.  This can be used to correct bad input data.
         */
        public void NormalizeSkinWeights()
        {
            if (!IsSkin)
                throw new Exception("NormalizeSkinWeights called on a mesh that is not a skin.");
            var niSkinData = SkinInstance.SkinData;
            var niGeomData = Data;
            if (niGeomData == null)
                throw new Exception("NormalizeSkinWeights called on a mesh with no geometry data.");
            niSkinData.NormalizeWeights(niGeomData.VertexCount);
        }

        /*
         * Used to determine whether this mesh is influenced by bones as a skin.
         * \return True if this mesh is a skin, false otherwise.
         */
        //Determine whether this is a skin by looking for a skin instance and skin data
        public bool IsSkin => skinInstance != null && skinInstance.SkinData != null;

        // Active Material.
        // \param[in] value The new value.
        public int ActiveMaterial
        {
            get => activeMaterial;
            set => activeMaterial = value;
        }

        // Shader.
        // \return The current value.
        public bool HasShader => hasShader;

        // BSProperty
        // \param[in] index Index of property to be retrieved.
        // \return The propterty.
        public NiProperty GetBSProperty(short index) => index < 0 || index > 1 ? null : bsProperties[index];

        // BSProperty
        // \param[in] index Index of property to be set.
        // \param[in] index Property to be set.
        public void SetBSProperty(short index, NiProperty value)
        {
            if (index >= 0 && index <= 1)
                bsProperties[index] = value;
        }

        /*
          * Gets or sets the array of the only 2 properties that are specific to Bethesda
          * \param[in] The new array of properties
          */
        public Array2<NiProperty> BSProperties
        {
            get => bsProperties;
            set => bsProperties = value;
        }
//--END:CUSTOM--//

}

}