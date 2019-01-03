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

    /*! A texture. */
    public class NiPixelData : NiPixelFormat
    {
        //Definition of TYPE constant
        public static readonly Type_ TYPE = new Type_("NiPixelData", NiPixelFormat.TYPE);
        /*!  */
        internal NiPalette palette;
        /*!  */
        internal uint numMipmaps;
        /*!  */
        internal uint bytesPerPixel;
        /*!  */
        internal IList<MipMap> mipmaps;
        /*!  */
        internal uint numPixels;
        /*!  */
        internal uint numFaces;
        /*!  */
        internal IList<byte> pixelData;

        public NiPixelData()
        {
            palette = null;
            numMipmaps = (uint)0;
            bytesPerPixel = (uint)0;
            numPixels = (uint)0;
            numFaces = (uint)1;
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
        public static NiObject Create() => new NiPixelData();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
        {

            uint block_num;
            base.Read(s, link_stack, info);
            Nif.NifStream(out block_num, s, info);
            link_stack.Add(block_num);
            Nif.NifStream(out numMipmaps, s, info);
            Nif.NifStream(out bytesPerPixel, s, info);
            mipmaps = new MipMap[numMipmaps];
            for (var i1 = 0; i1 < mipmaps.Count; i1++)
            {
                Nif.NifStream(out mipmaps[i1].width, s, info);
                Nif.NifStream(out mipmaps[i1].height, s, info);
                Nif.NifStream(out mipmaps[i1].offset, s, info);
            }
            Nif.NifStream(out numPixels, s, info);
            if (info.version >= 0x0A030006)
            {
                Nif.NifStream(out numFaces, s, info);
            }
            if (info.version <= 0x0A030005)
            {
                pixelData = new byte[numPixels];
                for (var i2 = 0; i2 < pixelData.Count; i2++)
                {
                    Nif.NifStream(out pixelData[i2], s, info);
                }
            }
            if (info.version >= 0x0A030006)
            {
                pixelData = new byte[(numPixels * numFaces)];
                for (var i2 = 0; i2 < pixelData.Count; i2++)
                {
                    Nif.NifStream(out (byte)pixelData[i2], s, info);
                }
            }

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.Write(s, link_map, missing_link_stack, info);
            numPixels = (uint)pixelData.Count;
            numMipmaps = (uint)mipmaps.Count;
            WriteRef((NiObject)palette, s, info, link_map, missing_link_stack);
            Nif.NifStream(numMipmaps, s, info);
            Nif.NifStream(bytesPerPixel, s, info);
            for (var i1 = 0; i1 < mipmaps.Count; i1++)
            {
                Nif.NifStream(mipmaps[i1].width, s, info);
                Nif.NifStream(mipmaps[i1].height, s, info);
                Nif.NifStream(mipmaps[i1].offset, s, info);
            }
            Nif.NifStream(numPixels, s, info);
            if (info.version >= 0x0A030006)
            {
                Nif.NifStream(numFaces, s, info);
            }
            if (info.version <= 0x0A030005)
            {
                for (var i2 = 0; i2 < pixelData.Count; i2++)
                {
                    Nif.NifStream(pixelData[i2], s, info);
                }
            }
            if (info.version >= 0x0A030006)
            {
                for (var i2 = 0; i2 < pixelData.Count; i2++)
                {
                    Nif.NifStream((byte)pixelData[i2], s, info);
                }
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
            numPixels = (uint)pixelData.Count;
            numMipmaps = (uint)mipmaps.Count;
            s.AppendLine($"  Palette:  {palette}");
            s.AppendLine($"  Num Mipmaps:  {numMipmaps}");
            s.AppendLine($"  Bytes Per Pixel:  {bytesPerPixel}");
            array_output_count = 0;
            for (var i1 = 0; i1 < mipmaps.Count; i1++)
            {
                if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
                {
                    s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
                    break;
                }
                s.AppendLine($"    Width:  {mipmaps[i1].width}");
                s.AppendLine($"    Height:  {mipmaps[i1].height}");
                s.AppendLine($"    Offset:  {mipmaps[i1].offset}");
            }
            s.AppendLine($"  Num Pixels:  {numPixels}");
            s.AppendLine($"  Num Faces:  {numFaces}");
            array_output_count = 0;
            for (var i1 = 0; i1 < pixelData.Count; i1++)
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
                s.AppendLine($"    Pixel Data[{i1}]:  {pixelData[i1]}");
                array_output_count++;
            }
            return s.ToString();

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
        {

            base.FixLinks(objects, link_stack, missing_link_stack, info);
            palette = FixLink<NiPalette>(objects, link_stack, missing_link_stack, info);

        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetRefs()
        {
            var refs = base.GetRefs();
            if (palette != null)
                refs.Add((NiObject)palette);
            return refs;
        }

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        internal override List<NiObject> GetPtrs()
        {
            var ptrs = base.GetPtrs();
            return ptrs;
        }
        //--BEGIN:FILE FOOT--//
        /*!
         * Retrieves the height of the texture image stored in this object.
         * \return The height of the texture image stored in this object.
         * \sa NiPixelData::GetWidth, NiPixelData::GetPixelFormat
         */
        public uint Height => mipmaps.Count == 0 ? 0 : mipmaps[0].height;

        /*!
         * Retrieves the width of the texture image stored in this object.
         * \return The width of the texture image stored in this object.
         * \sa NiPixelData::GetHeight, NiPixelData::GetPixelFormat
         */
        public uint Width => mipmaps.Count == 0 ? 0 : mipmaps[0].width;

        /*!
         * Retrieves the pixel format of the texture image stored in this object.
         * \return The pixel format of the texture image stored in this object.
         * \sa NiPixelData::GetWidth, NiPixelData::GetHeight
         */
        public PixelFormat PixelFormat => pixelFormat;

        /*!
         * Deletes all image data and sets a new size and format in preparation for new data to be provided.
         * \param new_width The width of the new texture image.
         * \param new_height The height of the new texture image.
         * \param px_fmt The pixel format of the new texture image.
         * \sa NiPixelData::GetWidth, NiPixelData::GetHeight
         */
        public void Reset(uint new_width, uint new_height, PixelFormat px_fmt)
        {
            //Ensure that texture dimentions are powers of two
            if ((new_height & (new_height - 1)) != 0)
                throw new Exception("Texture height must be a power of two.  1, 2, 4, 8, 16, 32, 64, 256, 512, etc.");
            if ((new_width & (new_width - 1)) != 0)
                throw new Exception("Texture width must be a power of two.  1, 2, 4, 8, 16, 32, 64, 256, 512, etc.");

            //Delete any data that was previously held
            pixelData.Clear();
            mipmaps.Resize(1);

            //Set up first mipmap
            mipmaps[0].width = new_width;
            mipmaps[0].height = new_height;
            mipmaps[0].offset = 0;

            //Set up pixel format fields
            pixelFormat = px_fmt;
            switch (pixelFormat)
            {
                case PixelFormat.PX_FMT_RGB:
                    redMask = 0x000000FF;
                    greenMask = 0x0000FF00;
                    blueMask = 0x00FF0000;
                    alphaMask = 0x00000000;
                    bitsPerPixel = 24;
                    unknown8Bytes[0] = 96;
                    unknown8Bytes[1] = 8;
                    unknown8Bytes[2] = 130;
                    unknown8Bytes[3] = 0;
                    unknown8Bytes[4] = 0;
                    unknown8Bytes[5] = 65;
                    unknown8Bytes[6] = 0;
                    unknown8Bytes[7] = 0;
                    break;
                case PixelFormat.PX_FMT_RGBA:
                    redMask = 0x000000FF;
                    greenMask = 0x0000FF00;
                    blueMask = 0x00FF0000;
                    alphaMask = 0xFF000000;
                    bitsPerPixel = 32;
                    unknown8Bytes[0] = 129;
                    unknown8Bytes[1] = 8;
                    unknown8Bytes[2] = 130;
                    unknown8Bytes[3] = 32;
                    unknown8Bytes[4] = 0;
                    unknown8Bytes[5] = 65;
                    unknown8Bytes[6] = 12;
                    unknown8Bytes[7] = 0;
                    break;
                case PixelFormat.PX_FMT_PAL:
                    redMask = 0x00000000;
                    blueMask = 0x00000000;
                    greenMask = 0x00000000;
                    alphaMask = 0x00000000;
                    bitsPerPixel = 8;
                    unknown8Bytes[0] = 34;
                    unknown8Bytes[1] = 0;
                    unknown8Bytes[2] = 0;
                    unknown8Bytes[3] = 32;
                    unknown8Bytes[4] = 0;
                    unknown8Bytes[5] = 65;
                    unknown8Bytes[6] = 12;
                    unknown8Bytes[7] = 0;
                    break;
                //[4,0,0,0,0,0,0,0] if 0 (?) bits per pixel
                default: throw new Exception("The pixel type you have requested is not currently supported.");
            }
        }
        /*!
         * Retrieves the the pixels of the texture image stored in this object.  This function does not work on palettized textures.
         * \return A vector containing the colors of each pixel in the texture image stored in this object, one row after another starting from the bottom of the image.  The width of the image must be used to interpret them correctly.
         * \sa NiPixelData::SetColors, NiPixelData::GetWidth
         */
        public IList<Color4> GetColors()
        {
            //Return empty vector
            if (mipmaps.Count == 0)
                return new Color4[0];
            //Pack the pixel data from the first mipmap into a vector of
            //Color4 based on the pixel format.
            var pixels = new Color4[mipmaps[0].width * mipmaps[0].height];
            switch (pixelFormat)
            {
                case PixelFormat.PX_FMT_RGB:
                    for (var i = 0; i < pixels.Length; ++i)
                    {
                        pixels[i].r = (float)pixelData[0][i * 3] / 255.0f;
                        pixels[i].g = (float)pixelData[0][i * 3 + 1] / 255.0f;
                        pixels[i].b = (float)pixelData[0][i * 3 + 2] / 255.0f;
                        pixels[i].a = 1.0f;
                    }
                    break;
                case PixelFormat.PX_FMT_RGBA:
                    for (var i = 0; i < pixels.Length; ++i)
                    {
                        pixels[i].r = (float)pixelData[0][i * 4] / 255.0f;
                        pixels[i].g = (float)pixelData[0][i * 4 + 1] / 255.0f;
                        pixels[i].b = (float)pixelData[0][i * 4 + 2] / 255.0f;
                        pixels[i].a = (float)pixelData[0][i * 4 + 3] / 255.0f;
                    }
                    break;
                default: throw new Exception("The GetColors function only supports the PX_FMT_RGB8 and PX_FMT_RGBA8 pixel formats.");
            }

#if IM_DEBUG
            imdebug("rgba b=32f rs=2 w=%d h=%d %p", mipmaps[0].width, mipmaps[0].height, &pixels[0]);
            //delete [] img;
            cout << "Showing image returned by GetColors function." << endl;
            cin.get();
#endif

            return pixels;
        }

        /*!
         * Sets the the pixels of the texture image stored in this object and optionally generates mipmaps.  This function does not work for palettized textures.
         * \param new_pixels A vector containing the colors of each new pixel to be set in the texture image stored in this object, one row after another starting from the botom of the image.
         * \param generate_mipmaps If true, mipmaps will be generated for the new image and stored in the file.
         * \sa NiPixelData::GetColors, NiPixelData::GetWidth
         */
        public void SetColors(IList<Color4> new_pixels, bool generate_mipmaps)
        {
            //Ensure that compatible pixel format is being used
            if (pixelFormat != PixelFormat.PX_FMT_RGB && pixelFormat != PixelFormat.PX_FMT_RGBA)
                throw new Exception("The SetColors function only supports the PX_FMT_RGB8 and PX_FMT_RGBA8 pixel formats.");
            //Ensure that there is size information in the mipmaps
            if (mipmaps.Count == 0)
                throw new Exception("The size informatoin has not been set.  Call the IPixelData::Reset() function first.");
            //Ensure that the right number of pixels for the dimentions set have been passed
            if (new_pixels.Count != mipmaps[0].height * mipmaps[0].width)
                throw new Exception("You must pass one color for every pixel in the image.  There should be height * width colors.");

            uint size = 0;
            mipmaps.Resize(1);
            size = (mipmaps[0].height * mipmaps[0].width * bitsPerPixel) / 8;

            //Deal with multiple mipmaps
            if (generate_mipmaps)
            {
                var m = new MipMap();
                m.height = mipmaps[0].height;
                m.width = mipmaps[0].width;
                size = (mipmaps[0].height * mipmaps[0].width * bitsPerPixel) / 8;
                while (m.width != 1 && m.height != 1)
                {
                    m.width /= 2;
                    m.height /= 2;
                    m.offset = size;
                    size += (m.height * m.width * bitsPerPixel) / 8;
                    mipmaps.Add(m);
                }
            }

            pixelData.Resize(1);
            pixelData[0].Resize(size * bitsPerPixel / 8);

            //Copy pixels to Color4 C array
            var tmp_image = new Color4[new_pixels.Count];
            for (var i = 0; i < new_pixels.Count; ++i)
                tmp_image[i] = new_pixels[i];

            //Pack pixel data
            for (var i = 0; i < mipmaps.Count; ++i)
            {
                if (i > 0)
                {
                    //Allocate space to store re-sized image.
                    var resized = new Color4[mipmaps[i].width * mipmaps[i].height];

                    //Visit every other pixel in each row and column of the previous image
                    for (var w = 0; w < mipmaps[i - 1].width; w += 2)
                        for (var h = 0; h < mipmaps[i - 1].height; h += 2)
                        {
                            var av = resized[(h / 2) * mipmaps[i].width + (w / 2)];
                            //Start with the value of the current pixel
                            av = tmp_image[h * mipmaps[i - 1].width + w];
                            var num_colors = 1.0f;
                            //Only process the pixel above if height is > 1
                            if (h > 1)
                            {
                                var px = tmp_image[(h + 1) * mipmaps[i - 1].width + w];
                                av.r += px.r;
                                av.g += px.g;
                                av.b += px.b;
                                av.a += px.a;
                                num_colors += 1.0f;
                            }
                            //Only process the pixel to the right if width > 1
                            if (w > 1)
                            {
                                var px = tmp_image[h * mipmaps[i - 1].width + (w + 1)];
                                av.r += px.r;
                                av.g += px.g;
                                av.b += px.b;
                                av.a += px.a;
                                num_colors += 1.0f;
                            }
                            //Only process the pixel to the upper right if both width and height are > 1
                            if (w > 1 && (h >> 1) != 0)
                            {
                                var px = tmp_image[(h + 1) * mipmaps[i - 1].width + (w + 1)];
                                av.r += px.r;
                                av.g += px.g;
                                av.b += px.b;
                                av.a += px.a;
                                num_colors += 1.0f;
                            }
                            //Calculate average
                            av.r /= num_colors;
                            av.g /= num_colors;
                            av.b /= num_colors;
                            av.a /= num_colors;
                        }
                    //Resize is complete, set result to tmp_image

                    //delete old tmp_image data
                    tmp_image = null;

                    //Adjust pointer values
                    tmp_image = resized;
                    resized = null;
                }

                //Data is ready to be packed into the byes of this mipmap
#if IM_DEBUG
                cout << "Showing mipmap size " << mipmaps[i].width << " x " << mipmaps[i].height << "." << endl;
                imdebug("rgba b=32f w=%d h=%d %p", mipmaps[i].width, mipmaps[i].height, &tmp_image[0]);
                cin.get();
#endif

                //Start at offset
                var map = pixelData[0][mipmaps[i].offset];
                switch (pixelFormat)
                {
                    case PixelFormat.PX_FMT_RGB:
                        for (var j = 0; j < mipmaps[i].width * mipmaps[i].height; ++j)
                        {
                            map[j * 3] = (int)(tmp_image[j].r * 255.0f);
                            map[j * 3 + 1] = (int)(tmp_image[j].g * 255.0f);
                            map[j * 3 + 2] = (int)(tmp_image[j].b * 255.0f);
                        }

                        //#ifdef IM_DEBUG
                        //	cout << "Showing mipmap after being packed  - size " << mipmaps[i].width << " x " << mipmaps[i].height << "." << endl;
                        //	imdebug("rgb w=%d h=%d %p", mipmaps[i].width, mipmaps[i].height, &map[0] );
                        //	cin.get();
                        //#endif
                        break;
                    case PixelFormat.PX_FMT_RGBA:
                        for (var j = 0; j < mipmaps[i].width * mipmaps[i].height; ++j)
                        {
                            map[j * 4] = (int)(tmp_image[j].r * 255.0f);
                            map[j * 4 + 1] = (int)(tmp_image[j].g * 255.0f);
                            map[j * 4 + 2] = (int)(tmp_image[j].b * 255.0f);
                            map[j * 4 + 3] = (int)(tmp_image[j].a * 255.0f);
                        }

                        //#ifdef IM_DEBUG
                        //	cout << "Showing mipmap after being packed  - size " << mipmaps[i].width << " x " << mipmaps[i].height << "." << endl;
                        //	imdebug("rgba w=%d h=%d %p", mipmaps[i].width, mipmaps[i].height, &map[0] );
                        //	cin.get();
                        //#endif
                        break;
                    case PixelFormat.PX_FMT_PAL:
                        throw new Exception("The SetColors function only supports the PX_FMT_RGB8 and PX_FMT_RGBA8 pixel formats.");
                }
            }
        }
        //--END:CUSTOM--//

    }

}