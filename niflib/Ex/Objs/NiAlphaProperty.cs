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

/*! Transparency. Flags 0x00ED. */
public class NiAlphaProperty : NiProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiAlphaProperty", NiProperty.TYPE);
	/*!
	 * Bit 0 : alpha blending enable
	 *             Bits 1-4 : source blend mode
	 *             Bits 5-8 : destination blend mode
	 *             Bit 9 : alpha test enable
	 *             Bit 10-12 : alpha test mode
	 *             Bit 13 : no sorter flag ( disables triangle sorting )
	 * 
	 *             blend modes (glBlendFunc):
	 *             0000 GL_ONE
	 *             0001 GL_ZERO
	 *             0010 GL_SRC_COLOR
	 *             0011 GL_ONE_MINUS_SRC_COLOR
	 *             0100 GL_DST_COLOR
	 *             0101 GL_ONE_MINUS_DST_COLOR
	 *             0110 GL_SRC_ALPHA
	 *             0111 GL_ONE_MINUS_SRC_ALPHA
	 *             1000 GL_DST_ALPHA
	 *             1001 GL_ONE_MINUS_DST_ALPHA
	 *             1010 GL_SRC_ALPHA_SATURATE
	 * 
	 *             test modes (glAlphaFunc):
	 *             000 GL_ALWAYS
	 *             001 GL_LESS
	 *             010 GL_EQUAL
	 *             011 GL_LEQUAL
	 *             100 GL_GREATER
	 *             101 GL_NOTEQUAL
	 *             110 GL_GEQUAL
	 *             111 GL_NEVER
	 */
	internal ushort flags;
	/*! Threshold for alpha testing (see: glAlphaFunc) */
	internal byte threshold;
	/*! Unknown */
	internal ushort unknownShort1;
	/*! Unknown */
	internal uint unknownInt2;

	public NiAlphaProperty() {
	flags = (ushort)4844;
	threshold = (byte)128;
	unknownShort1 = (ushort)0;
	unknownInt2 = (uint)0;
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
public static NiObject Create() => new NiAlphaProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out threshold, s, info);
	if (info.version <= 0x02030000) {
		Nif.NifStream(out unknownShort1, s, info);
		Nif.NifStream(out unknownInt2, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(threshold, s, info);
	if (info.version <= 0x02030000) {
		Nif.NifStream(unknownShort1, s, info);
		Nif.NifStream(unknownInt2, s, info);
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
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Threshold:  {threshold}");
	s.AppendLine($"  Unknown Short 1:  {unknownShort1}");
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
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

//--BEGIN:FILE FOOT--//
        /*! Used to specify the source and destination blending functions.  The function of each value is equivalent to the OpenGL blending function of similar name. */
        public enum BlendFunc
        {
            BF_ONE = 0,
            BF_ZERO = 1,
            BF_SRC_COLOR = 2,
            BF_ONE_MINUS_SRC_COLOR = 3,
            BF_DST_COLOR = 4,
            BF_ONE_MINUS_DST_COLOR = 5,
            BF_SRC_ALPHA = 6,
            BF_ONE_MINUS_SRC_ALPHA = 7,
            BF_DST_ALPHA = 8,
            BF_ONE_MINUS_DST_ALPHA = 9,
            BF_SRC_ALPHA_SATURATE = 10
        }

        /*! Used to set the alpha test function.  The function of each value is equivalent to the OpenGL test function of similar name. */
        public enum TestFunc_
        {
            TF_ALWAYS = 0,
            TF_LESS = 1,
            TF_EQUAL = 2,
            TF_LEQUAL = 3,
            TF_GREATER = 4,
            TF_NOTEQUAL = 5,
            TF_GEQUAL = 6,
            TF_NEVER = 7
        }

        /*!
         * Gets or sets the alpha blending state.  If alpha blending is turned on, the blending functions will be used to mix the values based on the alpha component of each pixel in the texture.
         * \param[in] value True to enable alpha blending, false to disable it.
         */
        public bool BlendState
        {
            get => Nif.UnpackFlag(flags, 0);
            set => Nif.PackFlag(flags, value, 0);
        }

        /*!
         * Gets or sets the source blend function which determines how alpha blending occurs if it is enabled.
         * \param[in] value The new soucre blend function.
         */
        public BlendFunc SourceBlendFunc
        {
            get => (BlendFunc)Nif.UnpackField(flags, 1, 4);
            set => Nif.PackField(flags, (ushort)value, 1, 4);
        }

        /*!
         * Gets or sets the destination blend function which determines how alpha blending occurs if it is enabled.
         * \param[in] value The new destination blend function.
         */
        public BlendFunc DestBlendFunc
        {
            get => (BlendFunc)Nif.UnpackField(flags, 5, 4);
            set => Nif.PackField(flags, (ushort)value, 5, 4);
        }

        /*!
         * Gets or sets the alpha testing state.  If alpha testing is turned on, the alpha test function will be used to compare each pixel's alpha value to the threshold.  If the function is true, the pixel will be drawn, otherwise it will not.
         * \param[in] value True to enable alpha testing, false to disable it.
         */
        public bool TestState
        {
            get => Nif.UnpackFlag(flags, 9);
            set => Nif.PackFlag(flags, value, 9);
        }

        /*!
         * Gets or sets the alpha test function which determines the cut-off alpha value between fully transparent and fully opaque parts of a texture.
         * \param[in] value The new alpha test function.
         */
        public TestFunc_ TestFunc
        {
            get => (TestFunc_)Nif.UnpackField(flags, 10, 3);
            set => Nif.PackField(flags, (ushort)value, 10, 3);
        }

        /*!
         * Gets or sets the threshold value that will be used with the alpha test function to determine whether a particular pixel will be drawn.
         * \param[in] n The new alpha test threshold.
         */
        public byte Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        /*!
         * Gets or sets the triangle sort mode.  If triangle sorting is enabled, the triangles that make up an object will be sorted based on distance, and drawn from farthest away to closest.  This reduces errors when using alpha blending.
         * \param[in] value True to enable triangle sorting, false to disable it.
         */
        public bool TriangleSortMode
        {
            get => Nif.UnpackFlag(flags, 13);
            set => Nif.PackFlag(flags, value, 13);
        }

        /*!
         * Gets or sets the data stored in the flags field for this object.  It is usually better to call more specific flag-toggle functions if they are availiable.
         * \param[in] n The new flag data.  Will overwrite any existing flag data.
         */
        public ushort Flags
        {
            get => flags;
            set => flags = value;
        }
//--END:CUSTOM--//

}

}