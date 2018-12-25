/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

#define DEBUG_HEADER_FOOTER
#define PRINT_OBJECT_NAMES

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Niflib
{
    //--Constants--//

    /*! Keyframe trees are game dependent, so here we define a few games. */
    public enum NifGame
    {
        KF_MW = 0, /*!< keyframe files: NiSequenceStreamHelper header, .kf extension */
        KF_DAOC = 1, /*!< keyframe files: NiNode header, .kfa extension */
        KF_CIV4 = 2, /*!< keyframe files: NiControllerSequence header, .kf extension */
        KF_FFVT3R = 3, /*!< keyframe files: NiControllerSequence header, .kf extension */
    }

    /*! Export options. */
    public enum ExportOptions
    {
        EXPORT_NIF = 0, /*!< NIF */
        EXPORT_NIF_KF = 1, /*!< NIF + single KF + KFM */
        EXPORT_NIF_KF_MULTI = 2, /*!< NIF + multiple KF + KFM */
        EXPORT_KF = 3, /*!< single KF */
        EXPORT_KF_MULTI = 4 /*!< multiple KF */
    }

    //--Main Functions--//
    public static partial class Nif
    {
        internal const uint VER_2_3 = 0x02030000; /*!< NIF Version 2.3 */
        internal const uint VER_3_0 = 0x03000000; /*!< NIF Version 3.0 */
        internal const uint VER_3_03 = 0x03000300; /*!< NIF Version 3.03 */
        internal const uint VER_3_1 = 0x03010000; /*!< NIF Version 3.1 */
        internal const uint VER_3_3_0_13 = 0x0303000D; /*!< NIF Version 3.3.0.13 */
        internal const uint VER_4_0_0_0 = 0x04000000; /*!< NIF Version 4.0.0.0 */
        internal const uint VER_4_0_0_2 = 0x04000002; /*!< NIF Version 4.0.0.2 */
        internal const uint VER_4_1_0_12 = 0x0401000C; /*!< NIF Version 4.1.0.12 */
        internal const uint VER_4_2_0_2 = 0x04020002; /*!< NIF Version 4.2.0.2 */
        internal const uint VER_4_2_1_0 = 0x04020100; /*!< NIF Version 4.2.1.0 */
        internal const uint VER_4_2_2_0 = 0x04020200; /*!< NIF Version 4.2.2.0 */
        internal const uint VER_10_0_1_0 = 0x0A000100; /*!< NIF Version 10.0.1.0 */
        internal const uint VER_10_0_1_2 = 0x0A000102; /*!< NIF Version 10.0.1.2 */
        internal const uint VER_10_0_1_3 = 0x0A000103; /*!< NIF Version 10.0.1.3 */
        internal const uint VER_10_1_0_0 = 0x0A010000; /*!< NIF Version 10.1.0.0 */
        internal const uint VER_10_1_0_101 = 0x0A010065; /*!< NIF Version 10.1.0.101 */
        internal const uint VER_10_1_0_106 = 0x0A01006A; /*!< NIF Version 10.1.0.106 */
        internal const uint VER_10_2_0_0 = 0x0A020000; /*!< NIF Version 10.2.0.0 */
        internal const uint VER_10_4_0_1 = 0x0A040001; /*!< NIF Version 10.4.0.1 */
        internal const uint VER_20_0_0_4 = 0x14000004; /*!< NIF Version 20.0.0.4 */
        internal const uint VER_20_0_0_5 = 0x14000005; /*!< NIF Version 20.0.0.4 */
        internal const uint VER_20_1_0_3 = 0x14010003; /*!< NIF Version 20.1.0.3 */
        internal const uint VER_20_2_0_7 = 0x14020007; /*!< NIF Version 20.2.0.7 */
        internal const uint VER_20_2_0_8 = 0x14020008; /*!< NIF Version 20.2.0.7 */
        internal const uint VER_20_3_0_1 = 0x14030001; /*!< NIF Version 20.3.0.1 */
        internal const uint VER_20_3_0_2 = 0x14030002; /*!< NIF Version 20.3.0.2 */
        internal const uint VER_20_3_0_3 = 0x14030003; /*!< NIF Version 20.3.0.3 */
        internal const uint VER_20_3_0_6 = 0x14030006; /*!< NIF Version 20.3.0.6 */
        internal const uint VER_20_3_0_9 = 0x14030009; /*!< NIF Version 20.3.0.9 */
        internal const uint VER_UNSUPPORTED = 0xFFFFFFFF; /*!< Unsupported NIF Version */
        internal const uint VER_INVALID = 0xFFFFFFFE; /*!< Not a NIF file */

        /*!
         * Reads the header of the given file by file name and returns the NIF version
         * if it is a valid NIF file. Call this function prior to calling ReadNifList
         * or ReadNifTree, if you want to make sure that its NIF version is supported
         * before trying to read it.
         * \param file_name The name of the file to load, or the complete path if it is not in the working directory.
         * \return The NIF version of the file, in hexadecimal format. If the file is not a NIF file, it returns VER_INVALID.
         * 
         * <b>Example:</b> 
         * \code
         * unsigned ver = GetNifVersion("test_in.nif");
         * if ( IsSupportedVersion(ver) == false ) {
         *    cout << "Unsupported.\n" << endl;
         * } else if ( ver == VER_INVALID ) {
         *    cout << "Not a NIF file.\n" << endl;
         * }
         * \endcode
         */
        public static uint GetNifVersion(string file_name)
        {
            //--Open File--//
            var s = new IStream(File.OpenRead(file_name));
            //--Read Header String--//
            var info = new NifInfo();
            NifStream(out HeaderString header, s, info);
            return info.version;
        }

        /*!
         * Returns the nif info without reading the entire file which includes the nif version,
         * the nif user version1 and the nif user version2
         * \param The full path to the nif file which includes the file name and the location of the nif file
         * \return The nif info structure which contains the nif header info
         */
        public static NifInfo ReadHeaderInfo(string file_name)
        {
            //--Open File--//
            var s = new IStream(File.OpenRead(file_name));
            //--Read Header Info--//
            var nif_header = new Header();
            var info = nif_header.Read(s);
            return info;
        }

        /*!
         * Returns the nif header without reading the entire file 
         * \param The full path to the nif file which includes the file name and the location of the nif file
         * \return The nif info structure which contains the nif header
         */
        public static Header ReadHeader(string file_name)
        {
            var s = new IStream(File.OpenRead(file_name));
            //--Read Header Info--//
            var nif_header = new Header();
            nif_header.Read(s);
            return nif_header;
        }

        /*!
         * Return the missing link stack with objects replaced from nif trees at specified root.
         */
        public static List<NiObject> ResolveMissingLinkStack(NiObject root, List<NiObject> missing_link_stack)
        {
            var r = new List<NiObject>();
            foreach (var obj in missing_link_stack)
                r.Add(_ResolveMissingLinkStackHelper(root, obj));
            return r;
        }

        /*!
         * Reads the given input stream and returns a vector of object references
         * \param in The input stream to read NIF data from.
         * \param missing_link_stack A stack where to copy null refs from (in case of reading a nif from an incomplete nif tree)
         * \param info Optionally, a NifInfo structure pointer can be passed in, and it will be filled with information from the header of the NIF file.
         * \return All the NIF objects read from the stream.
         */
        public static List<NiObject> ReadNifList(IStream s, List<NiObject> missing_link_stack, NifInfo info)
        {
            //Ensure that objects are registered
            if (!g_objects_registered)
            {
                g_objects_registered = true;
                ObjectRegistry.RegisterObjects();
            }

            //--Read Header--//
            var header = new Header();
            var hinfo = new hdrInfo(header);

            // set the header pointer in the stream
            hdrInfo.Set(s, header);

            //Create a new NifInfo if one isn't given.
            var delete_info = false;
            if (info == null)
            {
                info = new NifInfo();
                delete_info = true;
            }

            //Read header.
            info = header.Read(s);

            //If NifInfo structure is provided, fill it with info from header
            info.version = header.version;
            info.userVersion = header.userVersion;
            info.userVersion2 = header.userVersion2;
            info.endian = header.endianType;
            info.author = header.exportInfo.author.str;
            info.processScript = header.exportInfo.processScript.str;
            info.exportScript = header.exportInfo.exportScript.str;

#if DEBUG_HEADER_FOOTER
            //Print debug output for header
            Console.Write(header.asString());
#endif

#if PRINT_OBJECT_NAMES
            Console.Write("\nReading Objects:");
#endif

            //--Read Objects--//
            var numObjects = header.numBlocks;
            var objects = new Dictionary<uint, NiObject>(); //Map to hold objects by number
            var obj_list = new List<NiObject>(); //Vector to hold links in the order they were created.
            var link_stack = new List<uint>(); //List to add link values to as they're read in from the file
            string objectType;
            var err = new StringBuilder();

            var headerpos = s.Position;
            var nextobjpos = headerpos;

            //Loop through all objects in the file
            uint i = 0;
            NiObject new_obj = null;
            while (true)
            {
                // Check if the size information matches in version 20.3 and greater
                if (header.version >= VER_20_3_0_3)
                {
                    if (nextobjpos != s.Position)
                        s.Position = nextobjpos; // incorrect positioning seek to expected location
                    // update next location
                    nextobjpos += header.blockSize[i];
                }

                //Check for EOF
                if (s.IsEof)
                {
                    err.AppendLine("End of file reached prematurely.  This NIF may be corrupt or improperly supported.");
                    if (new_obj != null)
                    {
                        err.AppendLine("Last successfuly read object was:  ");
                        err.AppendLine($"====[ Object {i - 1} | {new_obj.GetType().GetTypeName()} ]====");
                        err.AppendLine(new_obj.asString());
                    }
                    else err.AppendLine("No objects were read successfully.");
                    throw new Exception(err.ToString());
                }

                // Starting position of block in stream
                var startobjpos = s.Position;

                //There are two main ways to read objects
                //One before version 5.0.0.1 and one after
                if (header.version >= 0x05000001)
                {
                    //From version 5.0.0.1 to version 10.0.1.106  there is a zero byte at the begining of each object
                    if (header.version <= VER_10_1_0_106)
                    {
                        var checkValue = ReadUInt(s);
                        if (checkValue != 0)
                        {
                            //Throw an exception if it's not zero
                            err.AppendLine($"Read failue - Bad object position.  Invalid check value:  {checkValue}");
                            if (new_obj != null)
                            {
                                err.AppendLine("Last successfuly read object was:  ");
                                err.AppendLine($"====[ Object {i - 1} | {new_obj.GetType().GetTypeName()} ]====");
                                err.AppendLine(new_obj.asString());
                            }
                            else err.AppendLine("No objects were read successfully.");
                            throw new Exception(err.ToString());
                        }
                    }

                    // Find which NIF object type this is by using the header arrays
                    objectType = header.blockTypes[header.blockTypeIndex[i]];

#if PRINT_OBJECT_NAMES
                    Console.WriteLine($"\n{i}:  {objectType}");
#endif
                }
                else
                {
                    // Find which object type this is by reading the string at this location
                    var objectTypeLength = ReadUInt(s);
                    if (objectTypeLength > 30 || objectTypeLength < 6)
                    {
                        err.AppendLine($"Read failue - Bad object position.  Invalid Type Name Length:  {objectTypeLength}");
                        if (new_obj != null)
                        {
                            err.AppendLine("Last successfuly read object was:  ");
                            err.AppendLine("====[ Object {i - 1} | {new_obj.GetType().GetTypeName()} ]====");
                            err.Append(new_obj.asString());
                        }
                        else err.AppendLine("No objects were read successfully.");
                        throw new Exception(err.ToString());
                    }
                    var charobjectType = new byte[objectTypeLength + 1];
                    s.Read(charobjectType, 0, (int)objectTypeLength);
                    charobjectType[objectTypeLength] = 0;
                    objectType = Encoding.ASCII.GetString(charobjectType);

#if PRINT_OBJECT_NAMES
                    Console.WriteLine($"\n{i}:  {objectType}");
#endif

                    if (header.version < VER_3_3_0_13)
                    {
                        //There can be special commands instead of object names
                        //in these versions
                        if (objectType == "Top Level Object") continue; //Just continue on to the next object
                        if (objectType == "End Of File") break; //File is finished
                    }
                }

                //Create object of the type that was found
                new_obj = ObjectRegistry.CreateObject(objectType);

                //Check for an unknown object type
                if (new_obj == null)
                {
                    err.AppendLine("Unknown object type encountered during file read:  ", objectType);
                    if (new_obj != null)
                    {
                        err.AppendLine("Last successfully read object was:  ");
                        err.AppendLine($"====[ Object {i - 1} | {new_obj.GetType().GetTypeName()} ]====");
                        err.AppendLine(new_obj.asString());
                    }
                    else err.AppendLine("No objects were read successfully.");
                    throw new Exception(err.ToString());
                }

                uint index;
                if (header.version < VER_3_3_0_13)
                {
                    //These old versions have a pointer value after the name
                    //which is used as the index
                    index = ReadUInt(s);
                }
                else
                {
                    //These newer verisons use their position in the file as their index
                    index = i;
                }

                //Read new object
                new_obj.Read(s, link_stack, info);

                //Add object to map
                objects[index] = new_obj;

                //Add object to list
                obj_list.push_back(new_obj);

                //Store block number
                new_obj.internal_block_number = index;

                // Ending position of block in stream
                std::streampos endobjpos = s.tellg();

                // Check if the size information matches
                if (header.version >= Nif.VER_20_3_0_3)
                {
                    std::streamsize calcobjsize = endobjpos - startobjpos;
                    uint objsize = header.blockSize[i];
                    if (calcobjsize != objsize)
                    {
                        err << "Object size mismatch occurred during file read:" << endl;
                        err << "====[ " << "Object " << i << " | " << objectType << " ]====" << endl;
                        err << "  Start: " << startobjpos << "  Expected Size: " << objsize << "  Read Size: " << calcobjsize << endl;
                        err << endl;
                    }
                }

#if PRINT_OBJECT_CONTENTS
                cout << endl << new_obj.asString() << endl;
#endif

                if (header.version >= VER_3_3_0_13)
                {
                    //We know the number of objects, so increment the count
                    //and break if we've finished
                    ++i;
                    if (i >= numObjects)
                    {
                        break;
                    }
                }
            }

            //--Read Footer--//
            Footer footer;
            footer.Read(s, link_stack, info);

#if DEBUG_HEADER_FOOTER
            //Print footer debug output
            footer.asString();
#endif

            // Check for accumulated warnings
            if (err.tellp() > 0)
            {
                throw runtime_error(err.str());
            }

#if DEBUG_LINK_PHASE
            cout << "Link Stack:" << endl;
            list < unsigned int>::iterator it;
            for (it = link_stack.begin(); it != link_stack.end(); ++it)
            {
                cout << *it << endl;
            }

            cout << "Fixing Links:" << endl;
#endif
            //--Now that all objects are read, go back and fix the links--//


            for (uint i = 0; i < obj_list.size(); ++i)
            {
#if DEBUG_LINK_PHASE
                cout << "   " << i << ":  " << obj_list[i] << endl;
#endif
                //Fix links & other pre-processing
                obj_list[i].FixLinks(objects, link_stack, missing_link_stack, *info);
            }

            //delete info if it was dynamically allocated
            if (delete_info)
            {
                delete info;
            }

            // clear the header pointer in the stream.  Should be in try/catch block
            hdrInfo hinfo2(null);
            s >> hinfo2;

            //Return completed object list
            return obj_list;
        }

        /*!
         * Reads the given file by file name and returns a vector of object references
         * \param file_name The name of the file to load, or the complete path if it is not in the working directory.
         * \param info Optionally, a NifInfo structure pointer can be passed in, and it will be filled with information from the header of the NIF file.
         * \return All the NIF objects read from the Nif file. 
         * \sa ReadNifTree, WriteNifTree
         */
        public static List<NiObject> ReadNifList(string file_name, NifInfo info = null)
        {
            //--Open File--//
            var s = new IStream(File.OpenRead(file_name));
            var ret = ReadNifList(s, info);
            s.Close();
            return ret;
        }

        /*!
         * Reads the given input stream and returns a vector of object references
         * \param in The input stream to read NIF data from.
         * \param info Optionally, a NifInfo structure pointer can be passed in, and it will be filled with information from the header of the NIF file.
         * \return All the NIF objects read from the stream.
         */
        public static List<NiObject> ReadNifList(IStream s, NifInfo info = null)
        {
            List<NiObject> missing_link_stack;
            return ReadNifList(s, missing_link_stack, info);
        }

        /*!
         * Like ReadNifList but returns root.
         */
        public static NiObject ReadNifTree(IStream s, List<NiObject> missing_link_stack, NifInfo info = null)
        {
            var objects = ReadNifList(s, missing_link_stack, info);
            return FindRoot(objects);
        }

        /*!
         * Reads the given file by file name and returns a reference to the root object.
         * \param file_name The name of the file to load, or the complete path if it is not in the working directory.
         * \param info Optionally, a NifInfo structure pointer can be passed in, and it will be filled with information from the header of the NIF file.
         * \return The root of tree of NIF objects contained in the NIF file.
         * \sa ReadNifList, WriteNifTree
         */
        public static NiObject ReadNifTree(string file_name, NifInfo info = null)
        {
            //Read object list
            var objects = ReadNifList(file_name, info);
            return FindRoot(objects);
        }

        /*!
         * Reads the given input stream and returns a reference to the root object.
         * \param[in] in The input stream to read NIF data from.
         * \param[out] info Optionally, a NifInfo structure pointer can be passed in, and it will be filled with information from the header of the NIF file.
         * \return The root of the tree of NIF Objects contained in the NIF file.
         */
        public static NiObject ReadNifTree(IStream s, NifInfo info = null)
        {
            //Read object list
            var objects = ReadNifList(s, info);
            return FindRoot(objects);
        }

        // Writes a valid Nif File given an OStream, a list to the root objects of a file tree
        // (missing_link_stack stores a stack of links which are referred to but which
        // are not inside the tree rooted by roots)
        static void WriteNifTree(OStream s, List<NiObject> roots, List<NiObject> missing_link_stack, NifInfo info)
        {
            //Enumerate all objects in tree
            Dictionary<Type_, uint> type_map;
            Dictionary<NiObject, uint> link_map;

            foreach (var it in roots)
                EnumerateObjects(it, type_map, link_map);

            //Build vectors for reverse look-up
            var objects = new NiObject[link_map.Count];
            foreach (var it in link_map)
                objects[it.Value] = it.Key;

            var types = new Type_[type_map.Count];
            foreach (var it in type_map)
                types[it.Value] = it.Key;

            uint version = info.version;

            //--Write Header--//
            var header = new Header();
            header.version = info.version;
            header.userVersion = info.userVersion;
            header.userVersion2 = info.userVersion2;
            header.endianType = info.endian;
            header.exportInfo.author = info.author;
            header.exportInfo.processScript = info.processScript;
            header.exportInfo.exportScript = info.exportScript;
            header.copyright[0] = "Numerical Design Limited, Chapel Hill, NC 27514";
            header.copyright[1] = "Copyright (c) 1996-2000";
            header.copyright[2] = "All Rights Reserved";

            // set the header pointer in the stream
            hdrInfo.Set(s, header);

            //Set Type Names
            header.blockTypes = new string[types.Length];
            for (var i = 0; i < types.Length; ++i)
                header.blockTypes[i] = types[i].GetTypeName();

            //Set type number of each object
            header.blockTypeIndex = new ushort[objects.Length];
            for (var i = 0; i < objects.Length; ++i)
                header.blockTypeIndex[i] = type_map[(Type_) & (objects[i].GetType())];

            // Set object sizes and accumulate string types
            if (version >= VER_20_1_0_3)
            {
                // Zero string information
                header.maxStringLength = 0;
                header.numStrings = 0;
                header.strings.clear();

                NifSizeStream ostr;
                ostr << hdrInfo(&header);

                header.blockSize = new uint[objects.Length];
                for (var i = 0; i < objects.Length; ++i)
                {
                    ostr.reset();
                    objects[i].Write(ostr, link_map, missing_link_stack, info);
                    header.blockSize[i] = (uint)ostr.tellp();
                }
                header.numStrings = header.strings.size();
            }

            //Write header to file
            header.Write(s, info);

#if PRINT_OBJECT_NAMES
            cout << endl << "Writing Objects:";
#endif

            //--Write Objects--//
            for (uint i = 0; i < objects.size(); ++i)
            {

#if PRINT_OBJECT_NAMES
                cout << endl << i << ":  " << objects[i].GetType().GetTypeName();
#endif

                if (version < VER_3_3_0_13)
                {
                    //Check if this object is one of the roots.
                    for (list<NiObjectRef>::const_iterator it = roots.begin(); it != roots.end(); ++it)
                    {
                        if (objects[i] == *it)
                        {
                            //Write "Top Level Object"
                            Nif.WriteString("Top Level Object", s);
                            break;
                        }
                    }

                    //Write Object Type
                    WriteString(objects[i].GetType().GetTypeName(), s);
                    //Write pointer number of object
                    WritePtr32(&(*objects[i]), s);

                }
                else if (version < 0x05000001)
                {
                    //Write Object Type
                    Nif.WriteString(objects[i].GetType().GetTypeName(), out );
                }
                else if (version >= 0x05000001 && version <= VER_10_1_0_106)
                {
                    Nif.WriteUInt(0, s);
                }

                objects[i].Write(s, link_map, missing_link_stack, info);
            }

            //--Write Footer--//

            if (version < VER_3_3_0_13)
            {
                //Write "End Of File"
                WriteString("End Of File", out );
            }
            else
            {
                Footer footer;
                footer.numRoots = 0;
                if (roots.size() == 1)
                {
                    const NiObjectRef&root = roots.front();
                    if (root.IsDerivedType(NiControllerSequence.TYPE))
                    {
                        // KF animation files allow for multiple roots of type NiControllerSequence
                        for (var i = 0; i < objects.size(); ++i)
                        {
                            if (objects[i].IsDerivedType(NiControllerSequence::TYPE))
                            {
                                footer.roots.push_back(objects[i]);
                            }
                        }
                    }
                    else
                    { // just assume its correctly passed in 
                        footer.numRoots = 1;
                        footer.roots.resize(1);
                        footer.roots[0] = root;
                    }
                }
                else
                {
                    footer.numRoots = roots.size();
                    footer.roots.insert(footer.roots.end(), roots.begin(), roots.end());
                }
                footer.Write(s, link_map, missing_link_stack, info);
            }

            // clear the header pointer in the stream.  Should be in try/catch block
            s << hdrInfo(null);
        }

        public static void WriteNifTree(OStream s, List<NiObjectRef> roots, NifInfo info)
        {
            List<NiObject> missing_link_stack;
            WriteNifTree(s, roots, missing_link_stack, info);
        }

        /*!
         * Creates a new NIF file of the given file name by crawling through the data tree starting with the root objects given, and keeps track of links that cannot been written.
         * \param[in] out The output stream to write the NIF data to.
         * \param[in] root The root object to start from when writing out the NIF file.  All decedents of this block will be written to the file in tree-descending order.
         * \param[in] missing_link_stack stack of links which are referred to but which are not inside the tree rooted by roots.
         * \param[in] info A NifInfo structure that contains information such as the version of the NIF file to create.
         * \sa ReadNifList, WriteNifTree
         */
        public static void WriteNifTree(OStream s, NiObject root, List<NiObject> missing_link_stack, NifInfo info = null)
        {
            var roots = new List<NiObject>();
            roots.Add(root);
            WriteNifTree(s, roots, missing_link_stack, info ?? NifInfo.Empty);
        }

        static void WriteNifTree(string file_name, List<NiObject> roots, NifInfo info)
        {
            //Open output file
            var s = new OStream(File.OpenWrite(file_name));
            WriteNifTree(s, roots, info);
            //Close file
            s.Close();
        }

        /*!
         * Creates a new NIF file of the given file name by crawling through the data tree starting with the root object given.
         * \param[in] file_name The desired file name for the new NIF file.  The path is relative to the working directory unless a full path is specified.
         * \param[in] root The root object to start from when writing out the NIF file.  All decedents of this block will be written to the file in tree-descending order.
         * \param[in] info A NifInfo structure that contains information such as the version of the NIF file to create.
         * \sa ReadNifList, WriteNifTree
         */
        public static void WriteNifTree(string file_name, NiObject root, NifInfo info = null)
        {
            //Open output file
            var s = new OStream(File.OpenWrite(file_name));
            var roots = new List<NiObject>();
            roots.Add(root);
            WriteNifTree(s, roots, info ?? NifInfo.Empty);
            //Close file
            s.Close();
        }

        /*!
         * Writes a nif tree to an OStream starting at the given root object.
         * \param[in] out The output stream to write the NIF data to.
         * \param[in] root The root object to start from when writing out the NIF data.  All decedents of this object will be written to the stream in tree-descending order.
         * \param[in] info A NifInfo structure that contains information such as the version of the NIF file to create.
         */
        public static void WriteNifTree(OStream s, NiObject root, NifInfo info = null)
        {
            var roots = new List<NiObject>();
            roots.Add(root);
            WriteNifTree(s, roots, info ?? NifInfo.Empty);
        }

        /*!
         * Writes a bunch of files given a base file name, and a pointer to the root object of the Nif file tree.
         * \param[in] file_name The desired file name for the base NIF file. This name serves as the basis for the names of any Kf files and Kfm files as well.  The path is relative to the working directory unless a full path is specified.
         * \param[in] root_object The root object to start from when writing out the NIF file.
         * \param[in] info A NifInfo structure that contains information such as the version of the NIF file to create.
         * \param[in] export_files What files to write: NIF, NIF + KF + KFM, NIF + KF's + KFM, KF only, KF's only
         * \param[in] kf_type The KF type (Morrowind style, DAoC style, CivIV style, ...)
         */
        public static void WriteFileGroup(string file_name, NiObject root_object, NifInfo info = null, ExportOptions export_files = ExportOptions.EXPORT_NIF, NifGame kf_type = NifGame.KF_MW)
        {
            //info ?? NifInfo.Empty;
            throw new NotImplementedException();
        }

        /*!
         * Creates a clone of an entire tree of objects.
         * \param[in] root The root object to start from when cloning the NIF data.  All referenced objects will be included in the new tree.
         * \param[in] version The version of the NIF format to use when writing a file.  Default is version 4.0.0.2.
         * \param[in] user_version The user version of the NIF format to use when writing a file.  Default is user version 0.
         * \param[in] target_root The root of the nif tree in which the cloned tree will be embedded. If specified, missing links will be resolved to that tree.
         * \return The root of the new cloned tree.
         */
        public static NiObject CloneNifTree(NiObject root, uint version = 0xFFFFFFFF, uint user_version = 0, NiObject target_root = null)
        {
            throw new NotImplementedException();
        }

        //TODO:  Figure out how to fix this to work with the new system
        /*!
         * Merges two Nif trees into one.  For standard Nif files, any NIF Objects with the same name are merged.  For Kf files, objects are attatched to those that match the name specified in the KF root object.  The data stored in a NIF file varies from version to version.  Usually you are safe with the default option (the highest availiable version) but you may need to use an earlier version if you need to clone an obsolete piece of information.
         * \param[in,out] target The root object of the first Nif tree to merge.
         * \param[in] right The root object of the second Nif tree to merge.
         * \param[in] version The version of the nif format to use during the clone operation on the right-hand tree.  The default is the highest version availiable.
         * \param[in] user_version The user version to use during the clone operation.
         */
        public static void MergeNifTrees(NiNode target, NiControllerSequence right, uint version = 0xFFFFFFFF, uint user_version = 0)
        {
            throw new NotImplementedException();
        }

        /*! 
         * Traverses a tree of NIF objects, attempting to move each skeleton root
         * to the natural bind position where no meshes are distorted by skin
         * influences.  This is not always successful and only affects nodes that
         * are skin influences.
         * \param[in] root The root NiNode of the tree.
         */
        public static void SendNifTreeToBindPos(NiNode root)
        {
            throw new NotImplementedException();
        }

        /*!
         * Returns the common ancestor of several NiAVObjects, or null if there is no common
         * ancestor.  None of the objects given can be the common ansestor, the search starts
         * with their parents.
         * \param[in] objects The list of NiAVObjects to try to find the commen ancestor of.
         * \return The common anscestor if one is found, otherwise a null reference.
         */
        public static NiNode FindCommonAncestor(List<NiAVObject> objects)
        {
            throw new NotImplementedException();
        }

        /*!
         * Returns a list of all the ancestors of a given NiAVObject
         * \param[in] leaf The NiAVObject to list the ancestors of.
         * \return A list containing all the ancestors of the given NiAVObject
         */
        public static List<NiNode> ListAncestors(NiAVObject leaf)
        {
            throw new NotImplementedException();
        }

        /*!
         * Returns whether the requested version is explicitly supported.  This does
         * not mean that the file will not open, rather it means that we have not
         * encountered files with this version in our tests yet.
         * \param[in] version The version of the NIF format to test for the support level of.
         * \return Whether the requested version is supported.
         */
        public static bool IsSupportedVersion(uint version)
        {
            throw new NotImplementedException();
        }

        /*!
         * Parses a version string and returns the equivalent version as a byte-packed integer.
         * \param[in] version The version number of the NIF format to parse in string format.
         * \return The version in integer format or VER_INVALID if the version string is not in the correct format.
         */
        public static uint ParseVersionString(string version)
        {
            throw new NotImplementedException();
        }

        /*!
         * Takes a NIF version in byte-packed integer format and returns a formatted human-
         * readable string.  For example, 0x04000002 returns the string "4.0.0.2"
         * \param[in] version The NIF version in integer form.
         * \return The equivalent string representation of the version number.
         */
        public static string FormatVersionString(uint version)
        {
            throw new NotImplementedException();
        }

        //Object Registration
        static bool g_objects_registered = false;



        //static void SplitNifTree(NiObject* root_object, NiObjectRef& xnif_root, list<NiObjectRef> & xkf_roots, Kfm & kfm, int kf_type, const NifInfo & info );

        //--Function Bodies--//

        static NiObject FindRoot(List<NiObject> objects)
        {
            //--Look for a NiNode that has no parents--//

            //Find the first NiObjectNET derived object
            NiAVObject root = null;
            for (var i = 0; i < objects.Count; ++i)
            {
                root = (NiAVObject)objects[i];
                if (root != null)
                    break;
            }
            //Make sure a node was found, if not return first node
            if (root == null)
                return objects[0];
            //Move up the chain to the root node
            while (root.GetParent() != null)
                root = (NiAVObject)root.GetParent();
            return (NiObject)root;
        }

        static NiObjectf _ResolveMissingLinkStackHelper(NiObject root, NiObject obj)
        {
            // search by name
            NiNodeRef rootnode = DynamicCast<NiNode>(root);
            NiNodeRef objnode = DynamicCast<NiNode>(obj);
            if (rootnode != null && objnode != null)
            {
                if (!(rootnode.GetName().empty()) && rootnode.GetName() == objnode.GetName())
                    return StaticCast<NiObject>(rootnode);
                list<NiObjectRef> children = root.GetRefs();
                for (list<NiObjectRef>::iterator child = children.begin(); child != children.end(); ++child)
                {
                    NiObjectRef result = _ResolveMissingLinkStackHelper(*child, obj);
                    if (result != null)
                        return result;
                }
            }
            // nothing found
            return NiObjectRef();
        }

        // Determine whether block comes before its parent or not, depending on the block type.
        // return: 'True' if child should come first, 'False' otherwise.
        static bool BlockChildBeforeParent(NiObject root)
        {
            Type* t = (Type*)&(root.GetType());
            return (t.IsDerivedType(bhkRefObject::TYPE) && !t.IsDerivedType(bhkConstraint::TYPE));
        }

        // This is a helper function for write to set up the list of all blocks,
        // the block index map, and the block type map.
        static void EnumerateObjects(NiObject root, Dictionary<Type_, uint> type_map, Dictionary<NiObject, uint> link_map)
        {
            // Ensure that this object has not already been visited
            if (link_map.find(root) != link_map.end())
            {
                //This object has already been visited.  Return.
                return;
            }

            list<NiObjectRef> links = root.GetRefs();
            Type* t = (Type*)&(root.GetType());

            // special case: add bhkConstraint entities before bhkConstraint
            // (these are actually links, not refs)
            if (t.IsDerivedType(bhkConstraint::TYPE))
            {
                vector<bhkEntity*> entities = ((bhkConstraint*)root).GetEntities();
                for (vector<bhkEntity*>::iterator it = entities.begin(); it != entities.end(); ++it)
                {
                    if (*it != null)
                    {
                        EnumerateObjects((NiObject*)(*it), type_map, link_map);
                    }
                }
            }

            // Call this function on all links of this object
            // add children that come before the block
            for (list<NiObjectRef>::iterator it = links.begin(); it != links.end(); ++it)
            {
                if (*it != null && BlockChildBeforeParent(*it))
                {
                    EnumerateObjects(*it, type_map, link_map);
                }
            }

            // Add this object type to the map if it isn't there already
            // TODO: add support for NiDataStreams
            if (type_map.find(t) == type_map.end())
            {
                //The type has not yet been registered, so register it
                unsigned int n = type_map.size();
                type_map[t] = n;
            }

            // add the block
            unsigned int n = link_map.size();
            link_map[root] = n;

            // add children that come after the block
            for (list<NiObjectRef>::iterator it = links.begin(); it != links.end(); ++it)
            {
                if (*it != null && !BlockChildBeforeParent(*it))
                {
                    EnumerateObjects(*it, type_map, link_map);
                }
            }
        }

        //TODO: Should this be returning an object of a derived type too?
        // Searches for the first object in the hierarchy of type.
        static NiObject GetObjectByType(NiObject root, Type_ type)
        {
            if (root.IsSameType(type))
                return root;
            var links = root.GetRefs();
            foreach (var it in links)
            {
                // Can no longer guarantee that some objects won't be visited twice.  Oh well.
                var r = GetObjectByType(it, type);
                if (r != null)
                    return r;
            }
            return null; // return null reference
        }

        //TODO: Should this be returning all objects of a derived type too?
        // Returns all in the in the tree of type.
        static List<NiObject> GetAllObjectsByType(NiObject root, Type_ type)
        {
            var r = new List<NiObject>();
            if (root.IsSameType(type))
                r.Add(root);
            var links = root.GetRefs();
            foreach (var it in links)
            {
                // Can no longer guarantee that some objects won't be visited twice.  Oh well.
                var childresult = GetAllObjectsByType(it, type);
                r.merge(childresult);
            }
            return r;
        }

        // Create a valid file name
        static string CreateFileName(string name)
        {
            string retname = name;
            //    std::string::size_type off = 0;
            //    std::string::size_type pos = 0;
            //    for (; ; )
            //    {
            //        pos = retname.find_first_not_of("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_^$~!#%&-{}()@'` ", off);
            //        if (pos == std::string::npos)
            // break;
            //    retname[pos] = '_';
            //    off = pos;
            //}
            return retname;
        }

        /*!
         * Helper function to split off animation from a nif tree. If no animation groups are defined, then both xnif_root and xkf_root will be null.
         * \param root_object The root object of the full tree.
         * \param xnif_root The root object of the tree without animation.
         * \param xkf_roots The root objects of the animation trees.
         * \param kfm The KFM structure (if required by style).
         * \param kf_type What type of keyframe tree to write (Morrowind style, DAoC style, ...).
         * \param info A NifInfo structure that contains information such as the version of the NIF file to create.
         */
        static void SplitNifTree(NiObject root_object, NiObject xnif_root, List<NiObject> xkf_roots, Kfm kfm, int kf_type, NifInfo info)
        {
            // Do we have animation groups (a NiTextKeyExtraData object)?
            // If so, create XNif and XKf trees.
            var txtkey = GetObjectByType(root_object, NiTextKeyExtraData.TYPE);
            NiTextKeyExtraData txtkey_obj = null;
            if (txtkey != null)
                txtkey_obj = (NiTextKeyExtraData)txtkey;
            if (txtkey_obj != null)
            {
                if (kf_type == KF_MW)
                {
                    // Construct the XNif file...
                    xnif_root = CloneNifTree(root_object, info.version, info.userVersion);

                    // Now search and locate newer timeframe controllers and convert to keyframecontrollers
                    list<NiObjectRef> mgrs = GetAllObjectsByType(xnif_root, NiControllerManager.TYPE);
                    for (list<NiObjectRef>::iterator it = mgrs.begin(); it != mgrs.end(); ++it)
                    {
                        NiControllerManagerRef mgr = DynamicCast<NiControllerManager>(*it);
                        if (mgr == null)
                        {
                            continue;
                        }
                        NiObjectNETRef target = mgr.GetTarget();
                        target.RemoveController(StaticCast<NiTimeController>(mgr));
                        vector<NiControllerSequenceRef> seqs = mgr.GetControllerSequences();
                        for (vector<NiControllerSequenceRef>::iterator itr = seqs.begin(); itr != seqs.end(); ++itr)
                        {
                            NiControllerSequenceRef seq = (*itr);
                            MergeNifTrees(DynamicCast<NiNode>(target), seq, info.version, info.userVersion);
                        }
                    }

                    // Now the XKf file...
                    // Create xkf root header.
                    NiSequenceStreamHelperRef xkf_stream_helper = new NiSequenceStreamHelper;
                    xkf_roots.push_back(StaticCast<NiObject>(xkf_stream_helper));

                    // Append NiNodes with a NiKeyFrameController as NiStringExtraData objects.
                    list<pair<NiNodeRef, NiKeyframeControllerRef>> node_controllers;

                    list<NiObjectRef> nodes = GetAllObjectsByType(xnif_root, NiNode::TYPE);
                    for (list<NiObjectRef>::iterator it = nodes.begin(); it != nodes.end(); ++it)
                    {
                        NiNodeRef node = DynamicCast<NiNode>(*it);
                        if (node == null)
                        {
                            continue;
                        }

                        //Find the first NiKeyframeController in the controller list, if any
                        list<NiTimeControllerRef> controllers = node.GetControllers();
                        NiKeyframeControllerRef key_controller;
                        for (list<NiTimeControllerRef>::iterator it = controllers.begin(); it != controllers.end(); ++it)
                        {

                            if ((*it).IsDerivedType(NiKeyframeController::TYPE))
                            {
                                key_controller = StaticCast<NiKeyframeController>(*it);
                            }
                            else if ((*it).IsDerivedType(NiTransformController::TYPE))
                            {
                                NiTransformControllerRef trans = StaticCast<NiTransformController>(*it);
                                NiTransformInterpolatorRef interp = DynamicCast<NiTransformInterpolator>(trans.GetInterpolator());
                                if (interp != null)
                                {
                                    NiTransformDataRef transData = interp.GetData();
                                    if (transData != null)
                                    {
                                        NiKeyframeDataRef data = new NiKeyframeData();
                                        data.SetRotateType(transData.GetRotateType());
                                        data.SetTranslateType(transData.GetTranslateType());
                                        data.SetScaleType(transData.GetScaleType());
                                        data.SetXRotateType(transData.GetXRotateType());
                                        data.SetYRotateType(transData.GetYRotateType());
                                        data.SetZRotateType(transData.GetZRotateType());
                                        data.SetTranslateKeys(transData.GetTranslateKeys());
                                        data.SetQuatRotateKeys(transData.GetQuatRotateKeys());
                                        data.SetScaleKeys(transData.GetScaleKeys());
                                        data.SetXRotateKeys(transData.GetXRotateKeys());
                                        data.SetYRotateKeys(transData.GetYRotateKeys());
                                        data.SetZRotateKeys(transData.GetZRotateKeys());

                                        key_controller = new NiKeyframeController();
                                        key_controller.SetFlags(trans.GetFlags());
                                        key_controller.SetFrequency(trans.GetFrequency());
                                        key_controller.SetPhase(trans.GetPhase());
                                        key_controller.SetStartTime(trans.GetStartTime());
                                        key_controller.SetStopTime(trans.GetStopTime());
                                        key_controller.SetData(data);
                                        break;
                                    }
                                }
                            }
                        }

                        //If this node has a keyframe controller, put it in the list
                        if (key_controller != null)
                        {
                            node_controllers.push_back(pair<NiNodeRef, NiKeyframeControllerRef>(node, key_controller));
                        }
                    }

                    for (list<pair<NiNodeRef, NiKeyframeControllerRef>>::reverse_iterator it = node_controllers.rbegin(); it != node_controllers.rend(); ++it)
                    {
                        //Add string data				
                        NiStringExtraDataRef nodextra = new NiStringExtraData;
                        nodextra.SetData(it.first.GetName());
                        xkf_stream_helper.AddExtraData(StaticCast<NiExtraData>(nodextra), info.version);

                        NiKeyframeControllerRef controller = it.second;
                        (it.first).RemoveController(StaticCast<NiTimeController>(controller));

                        xkf_stream_helper.AddController(StaticCast<NiTimeController>(controller));
                    }

                    // Add a copy of the NiTextKeyExtraData object to the XKf header.
                    NiTextKeyExtraDataRef xkf_txtkey_obj = new NiTextKeyExtraData;
                    xkf_stream_helper.AddExtraData(StaticCast<NiExtraData>(xkf_txtkey_obj), info.version);
                    xkf_txtkey_obj.SetKeys(txtkey_obj.GetKeys());

                }
                else if (kf_type == KF_CIV4)
                {
                    // Construct the Nif file without transform controllers ...
                    xnif_root = CloneNifTree(root_object, info.version, info.userVersion);

                    list<NiObjectRef> mgrs = GetAllObjectsByType(xnif_root, NiControllerManager::TYPE);
                    for (list<NiObjectRef>::iterator it = mgrs.begin(); it != mgrs.end(); ++it)
                    {
                        NiControllerManagerRef mgr = DynamicCast<NiControllerManager>(*it);
                        if (mgr == null)
                        {
                            continue;
                        }
                        NiObjectNETRef target = mgr.GetTarget();
                        target.RemoveController(StaticCast<NiTimeController>(mgr));
                        vector<NiControllerSequenceRef> seqs = mgr.GetControllerSequences();
                        for (vector<NiControllerSequenceRef>::iterator itr = seqs.begin(); itr != seqs.end(); ++itr)
                        {
                            xkf_roots.push_back(StaticCast<NiObject>(*itr));
                        }
                        mgr.ClearSequences();
                    }
                }
                else if (kf_type == KF_FFVT3R)
                {

                    // Construct the Nif file without transform controllers ...
                    xnif_root = CloneNifTree(root_object, info.version, info.userVersion);

                    // Delete all NiMultiTargetTransformController
                    list<NiObjectRef> nodes = GetAllObjectsByType(xnif_root, NiMultiTargetTransformController::TYPE);
                    for (list<NiObjectRef>::iterator it = nodes.begin(); it != nodes.end(); ++it)
                    {
                        if (NiMultiTargetTransformControllerRef ctrl = DynamicCast<NiMultiTargetTransformController>(*it) ) {
                        if (NiNodeRef target = DynamicCast<NiNode>(ctrl.GetTarget())) {
                            target.RemoveController(ctrl);
                        }
                    }
                }

                list<NiObjectRef> mgrs = GetAllObjectsByType(xnif_root, NiControllerManager::TYPE);
                for (list<NiObjectRef>::iterator it = mgrs.begin(); it != mgrs.end(); ++it)
                {
                    NiControllerManagerRef mgr = DynamicCast<NiControllerManager>(*it);
                    if (mgr == null)
                    {
                        continue;
                    }
                    NiObjectNETRef target = mgr.GetTarget();
                    target.RemoveController(StaticCast<NiTimeController>(mgr));
                    vector<NiControllerSequenceRef> seqs = mgr.GetControllerSequences();
                    for (vector<NiControllerSequenceRef>::iterator itr = seqs.begin(); itr != seqs.end(); ++itr)
                    {
                        xkf_roots.push_back(StaticCast<NiObject>(*itr));
                    }
                    mgr.ClearSequences();
                }

            }
            else
            {
                throw runtime_error("KF splitting for the requested game is not yet implemented.");
            }
        } else {
		// no animation groups: nothing to do
		xnif_root = root_object;
	};
}

//TODO:  This was written by Amorilia.  Figure out how to fix it.
static void WriteFileGroup(string const file_name, NiObject root_object, NifInfo info, ExportOptions export_files, NifGame kf_type)
{
    // Get base filename.
    uint file_name_slash = (unsigned int)(file_name.rfind("\\") + 1);
    string file_name_path = file_name.substr(0, file_name_slash);
    string file_name_base = file_name.substr(file_name_slash, file_name.length());
    unsigned int file_name_dot = (unsigned int)(file_name_base.rfind("."));
    file_name_base = file_name_base.substr(0, file_name_dot);

    // Deal with the simple case first
    if (export_files == EXPORT_NIF)
        WriteNifTree(file_name_path + file_name_base + ".nif", root_object, info); // simply export the NIF file!
                                                                                   // Now consider all other cases
    else if (kf_type == Niflib.NifGame.KF_MW)
    {
        if (export_files == EXPORT_NIF_KF)
        {
            // for Morrowind we must also write the full NIF file
            WriteNifTree(file_name_path + file_name_base + ".nif", root_object, info); // simply export the NIF file!
            NiObjectRef xnif_root;
            list<NiObjectRef> xkf_roots;
            Kfm kfm; // dummy
            SplitNifTree(root_object, xnif_root, xkf_roots, kfm, kf_type, info);
            if (xnif_root != null && !xkf_roots.empty())
            {
                WriteNifTree(file_name_path + "x" + file_name_base + ".nif", xnif_root, info);
                WriteNifTree(file_name_path + "x" + file_name_base + ".kf", xkf_roots.front(), info);
            };
        }
        else
            throw runtime_error("Invalid export option.");
    }
    else if (kf_type == KF_CIV4)
    {

        NiObjectRef xnif_root;
        list<NiObjectRef> xkf_roots;
        Kfm kfm; // dummy
        SplitNifTree(root_object, xnif_root, xkf_roots, kfm, kf_type, info);
        if (export_files == EXPORT_NIF || export_files == EXPORT_NIF_KF || export_files == EXPORT_NIF_KF_MULTI)
        {
            WriteNifTree(file_name_path + file_name_base + ".nif", xnif_root, info);
        }
        if (export_files == EXPORT_NIF_KF || export_files == EXPORT_KF)
        {
            WriteNifTree(file_name_path + file_name_base + ".kf", xkf_roots, info);
        }
        else if (export_files == EXPORT_NIF_KF_MULTI || export_files == EXPORT_KF_MULTI)
        {
            for (list<NiObjectRef>::iterator it = xkf_roots.begin(); it != xkf_roots.end(); ++it)
            {
                NiControllerSequenceRef seq = DynamicCast<NiControllerSequence>(*it);
                if (seq == null)
                    continue;
                string path = file_name_path + file_name_base + "_" + CreateFileName(seq.GetTargetName()) + "_" + CreateFileName(seq.GetName()) + ".kf";
                WriteNifTree(path, StaticCast<NiObject>(seq), info);
            }
        }
    }
    else if (kf_type == KF_FFVT3R)
    {

        NiObjectRef xnif_root;
        list<NiObjectRef> xkf_roots;
        Kfm kfm; // dummy
        SplitNifTree(root_object, xnif_root, xkf_roots, kfm, kf_type, info);
        if (export_files == EXPORT_NIF || export_files == EXPORT_NIF_KF || export_files == EXPORT_NIF_KF_MULTI)
        {
            WriteNifTree(file_name_path + file_name_base + ".nif", xnif_root, info);
        }
        if (export_files == EXPORT_NIF_KF || export_files == EXPORT_KF)
        {
            WriteNifTree(file_name_path + file_name_base + ".kf", xkf_roots, info);
        }
        else if (export_files == EXPORT_NIF_KF_MULTI || export_files == EXPORT_KF_MULTI)
        {
            for (list<NiObjectRef>::iterator it = xkf_roots.begin(); it != xkf_roots.end(); ++it)
            {
                NiControllerSequenceRef seq = DynamicCast<NiControllerSequence>(*it);
                if (seq == null)
                    continue;
                string path = file_name_path + file_name_base + "_" + CreateFileName(seq.GetTargetName()) + "_" + CreateFileName(seq.GetName()) + ".kf";
                WriteNifTree(path, StaticCast<NiObject>(seq), info);
            }
        }
    }
    else
        throw runtime_error("Not yet implemented.");
}

static void MapNodeNames(Dictionary<string, NiNodeRef> name_map, NiNode par)
{
    //Add the par node to the map, and then call this function for each of its children
    name_map[par.GetName()] = par;

    List<NiAVObjectRef> links = par.GetChildren();
    for (vector<NiAVObjectRef>::iterator it = links.begin(); it != links.end(); ++it)
    {
        NiNodeRef child_node = DynamicCast<NiNode>(*it);
        if (child_node != null)
        {
            MapNodeNames(name_map, child_node);
        };
    };
}

//This function will merge two scene graphs by attatching new objects to the correct position
//on the existing scene graph.  In other words, it deals only with adding new nodes, not altering
//existing nodes by changing their data or attatched properties
void MergeSceneGraph(map<string, NiNodeRef> & name_map, NiNode* root, NiAVObject* par)
{
    //Check if this object's name exists in the object map
    string name = par.GetName();

    if (name_map.find(name) != name_map.end())
    {
        //This object already exists in the original file, so continue on to its children, if it is a NiNode

        NiNodeRef par_node = DynamicCast<NiNode>(par);
        if (par_node != null)
        {
            vector<NiAVObjectRef> children = par_node.GetChildren();
            for (vector<NiAVObjectRef>::iterator it = children.begin(); it != children.end(); ++it)
            {
                if ((*it) != null)
                {
                    MergeSceneGraph(name_map, root, *it);
                };
            };
        }
        return;
    }

    //This object has a new name and either it has no parent or its parent has a name that is
    // in the list.  Attatch it to the object with the same name as its parent
    //all child objects will follow along.
    NiNodeRef par_par = par.GetParent();

    if (par_par == null)
    {
        //This object has a new name and no parents.  That means it is the root object.
        //of a disimilar Nif file.

        //Check whether we have a NiNode ( a node that might have children) or not.
        NiNodeRef par_node = DynamicCast<NiNode>(par);
        if (par_node == null)
        {
            //This is not a NiNode class, so simply add it as a new child of the
            //target root node
            root.AddChild(par);
        }
        else
        {
            //This is a NiNode class, so merge its child list with that of the root
            vector<NiAVObjectRef> children = par_node.GetChildren();
            for (unsigned int i = 0; i < children.size(); ++i)
            {
                root.AddChild(children[i]);
            }
        }
    }
    else
    {
        //This object has a new name and has a parent with a name that already exists.
        //Attatch it to the object in the target tree that matches the name of its
        //parent

        //TODO:  Implement children
        ////Remove this object from its old parent
        //par_par.GetAttr("Children").RemoveLinks( par );

        //Get the object to attatch to
        NiObjectRef attatch = DynamicCast<NiObject>(name_map[par_par.GetName()]);

        //TODO:  Implement children
        ////Add this object as new child
        //attatch.GetAttr("Children").AddLink( par );
    }
}

void MergeNifTrees(NiNode* target, NiAVObject* right, unsigned version, unsigned user_version)
{
    //For now assume that both are normal Nif trees just to verify that it works

    //Make a clone of the tree to add
    stringstream tmp;
    //WriteNifTree( tmp, right, version );
    tmp.seekg(0, ios_base::beg);
    NiAVObjectRef new_tree = right;// ReadNifTree( tmp ); TODO: Figure out why this doesn't work

    //Create a list of names in the target
    map<string, NiNodeRef> name_map;
    MapNodeNames(name_map, target);

    ////Reassign any cross references in the new tree to point to objects in the
    ////target tree with the same names
    //ReassignTreeCrossRefs( name_map, new_tree );

    //Use the name map to merge the Scene Graphs
    MergeSceneGraph(name_map, target, new_tree);
}

//Version for merging KF Trees rooted by a NiControllerSequence
void MergeNifTrees(NiNode* target, NiControllerSequence* right, unsigned version, unsigned user_version)
{
    //Map the node names
    map<string, NiNodeRef> name_map;
    MapNodeNames(name_map, target);

    //TODO:  Allow this to merge a KF sequence into a file that already has
    //sequences in it by appending all the keyframe data to the end of
    //existing controllers

    //Get the NiTextKeyExtraData, clone it, and attach it to the target node
    NiTextKeyExtraDataRef txt_key = right.GetTextKeyExtraData();
    if (txt_key != null)
    {
        NiObjectRef tx_clone = txt_key.Clone(version, user_version);
        NiExtraDataRef ext_dat = DynamicCast<NiExtraData>(tx_clone);
        if (ext_dat != null)
        {
            target.AddExtraData(ext_dat, version);
        }
    }

    //Atach it to

    //Get the controller data
    vector<ControllerLink> data = right.GetControllerData();

    //Connect a clone of all the interpolators/controllers to the named node
    for (unsigned int i = 0; i < data.size(); ++i)
    {
        //Get strings
        //TODO: Find out if other strings are needed
        string node_name, ctlr_type;
        NiStringPaletteRef str_pal = data[i].stringPalette;
        if (str_pal == null)
        {
            node_name = data[i].nodeName;
            ctlr_type = data[i].controllerType;
        }
        else
        {
            node_name = str_pal.GetSubStr(data[i].nodeNameOffset);
            ctlr_type = str_pal.GetSubStr(data[i].controllerTypeOffset);
        }
        //Make sure there is a node with this name in the target tree
        if (name_map.find(node_name) != name_map.end())
        {
            //See if we're dealing with an interpolator or a controller
            if (data[i].controller != null)
            {
                //Clone the controller and attached data and
                //add it to the named node
                NiObjectRef clone = CloneNifTree(StaticCast<NiObject>(data[i].controller), version, user_version);
                NiTimeControllerRef ctlr = DynamicCast<NiTimeController>(clone);
                if (ctlr != null)
                {
                    name_map[node_name].AddController(ctlr);
                }
            }
            else if (data[i].interpolator != null)
            {
                //Clone the interpolator and attached data and
                //attach it to the specific type of controller that's
                //connected to the named node
                NiNodeRef node = name_map[node_name];
                list<NiTimeControllerRef> ctlrs = node.GetControllers();
                NiSingleInterpControllerRef ctlr;
                for (list<NiTimeControllerRef>::iterator it = ctlrs.begin(); it != ctlrs.end(); ++it)
                {
                    if (*it != null && (*it).GetType().GetTypeName() == ctlr_type)
                    {
                        ctlr = DynamicCast<NiSingleInterpController>(*it);
                        if (ctlr != null)
                        {
                            break;
                        }
                    }
                }

                //If the controller wasn't found, create one of the right type and attach it
                if (ctlr == null)
                {
                    NiObjectRef new_ctlr = ObjectRegistry::CreateObject(ctlr_type);
                    ctlr = DynamicCast<NiSingleInterpController>(new_ctlr);
                    if (ctlr == null)
                    {
                        throw runtime_error("Non-NiSingleInterpController controller found in KF file.");
                    }
                    node.AddController(StaticCast<NiTimeController>(ctlr));
                }

                //Clone the interpolator and attached data and
                //add it to controller of matching type that was
                //found
                NiObjectRef clone = CloneNifTree(StaticCast<NiObject>(data[i].interpolator), version, user_version);
                NiInterpolatorRef interp = DynamicCast<NiInterpolator>(clone);
                if (interp != null)
                {
                    ctlr.SetInterpolator(interp);

                    //Set the start/stop time and frequency of this controller
                    ctlr.SetStartTime(right.GetStartTime());
                    ctlr.SetStopTime(right.GetStopTime());
                    ctlr.SetFrequency(right.GetFrequency());
                    ctlr.SetPhase(0.0f); //TODO:  Is phase somewhere in NiControllerSequence?

                    //Set cycle type as well
                    switch (right.GetCycleType())
                    {
                        case CYCLE_LOOP:
                            ctlr.SetFlags(8); //Active
                            break;
                        case CYCLE_CLAMP:
                            ctlr.SetFlags(12); //Active+Clamp
                            break;
                        case CYCLE_REVERSE:
                            ctlr.SetFlags(10); //Active+Reverse
                            break;
                    }
                }
            }
        }
    }
}

//Version for merging KF Trees rooted by a NiSequenceStreamHelper
void MergeNifTrees(NiNode* target, NiSequenceStreamHelper* right, unsigned version, unsigned user_version)
{
    //Map the node names
    map<string, NiNodeRef> name_map;
    MapNodeNames(name_map, target);

    //TODO: Implement this

}


bool IsSupportedVersion(unsigned int version)
{
    switch (version)
    {
        case VER_2_3:
        case VER_3_0:
        case VER_3_03:
        case VER_3_1:
        case VER_3_3_0_13:
        case VER_4_0_0_0:
        case VER_4_0_0_2:
        case VER_4_1_0_12:
        case VER_4_2_0_2:
        case VER_4_2_1_0:
        case VER_4_2_2_0:
        case VER_10_0_1_0:
        case VER_10_0_1_2:
        case VER_10_0_1_3:
        case VER_10_1_0_0:
        case VER_10_1_0_101:
        case VER_10_1_0_106:
        case VER_10_2_0_0:
        case VER_10_4_0_1:
        case VER_20_0_0_4:
        case VER_20_0_0_5:
        case VER_20_1_0_3:
        case VER_20_2_0_7:
        case VER_20_2_0_8:
        case VER_20_3_0_1:
        case VER_20_3_0_2:
        case VER_20_3_0_3:
        case VER_20_3_0_6:
        case VER_20_3_0_9:
            return true;
    }
    return false;
}

unsigned int ParseVersionString(string version)
{

    unsigned int outver = 0;

    string::size_type start = 0, len, end;
    for (int offset = 3; offset >= 0 && start < version.length(); --offset)
    {
        end = version.find_first_of(".", start);

        if (end == string::npos)
        {
            if (offset > 0)
            {
                //This version has only one period in it.  Take the rest of the numbers one character at a time.
                len = 1;
            }
            else
            {
                //We've already taken two characters one at a time, so take the rest all at once.
                len = end;
            }
        }
        else
        {
            len = end - start;

        }

        int num = 0;
        stringstream sstr(version.substr(start, len) );
        sstr >> num;
        if (num > 0xFF)
        {
            return VER_INVALID;
        }
        outver |= (num << (offset * 8));
        if (len == string::npos)
        {
            break;
        }

        if (end != string::npos)
        {
            //account for length of the period
            start += 1;
        }
        start += len;
    }

    if (outver == 0)
    {
        return VER_INVALID;
    }
    else
    {
        return outver;
    }
}

string FormatVersionString(unsigned version)
{
    //Cast the version to an array of 4 bytes
    char* byte_ver = (char*)&version;

    //Put the version parts into an integer array, reversing their order
    int int_ver[4] = { byte_ver[3], byte_ver[2], byte_ver[1], byte_ver[0] };

    //Format the version string and return it
    stringstream out;

    if (version >= VER_3_3_0_13)
    {
		//Version 3.3.0.13+ is in x.x.x.x format.
		out << int_ver[0] << "." << int_ver[1] << "." << int_ver[2] << "." << int_ver[3];
    }
    else
    {
		//Versions before 3.3.0.13 are in x.x format.
		out << int_ver[0] << "." << int_ver[1];
        if (int_ver[2])
        {
			out << int_ver[2];
            if (int_ver[3])
            {
				out << int_ver[3];
            }
        }
    }

    return out.str();
}


Ref<NiObject> CloneNifTree(NiObject* root, unsigned version, unsigned user_version, NiObject* target_root)
{
    //Create a string stream to temporarily hold the state-save of this tree
    stringstream tmp;
    list<NiObject*> missing_link_stack;
    list<NiObjectRef> resolved_link_stack;

    //Write the existing tree into the stringstream
    WriteNifTree(tmp, root, missing_link_stack, NifInfo(version, user_version));
    //Resolve missing links into target root.
    if (target_root != null)
        resolved_link_stack = ResolveMissingLinkStack(target_root, missing_link_stack);

    //Read the data back out of the stringstream, returning the new tree
    return ReadNifTree(tmp, resolved_link_stack);
}

void SendNifTreeToBindPos(NiNode* root)
{
    //If this node is a skeleton root, send its children to the bind
    //position

    if (root == null)
    {
        throw runtime_error("Attempted to call SendNifTreeToBindPos on a null reference.");
    }

    if (root.IsSkeletonRoot())
    {
        root.GoToSkeletonBindPosition();
    }

    //Call this function on any NiNode children
    vector<NiAVObjectRef> children = root.GetChildren();
    for (unsigned int i = 0; i < children.size(); ++i)
    {
        NiNodeRef child = DynamicCast<NiNode>(children[i]);
        if (child != null)
        {
            SendNifTreeToBindPos(child);
        }
    }
}

list<Ref<NiNode>> ListAncestors(NiAVObject* leaf)
{
    if (leaf == null)
    {
        throw runtime_error("ListAncestors called with a null leaf NiNode Ref");
    }

    list<NiNodeRef> ancestors;

    NiNodeRef current = leaf.GetParent();

    while (current != null)
    {
        ancestors.push_front(current);

        current = current.GetParent();
    }

    return ancestors;
}

Ref<NiNode> FindCommonAncestor( const vector<Ref<NiAVObject>> & objects )
{

    //create lists of nodes that have an influence and this TriBasedGeom
    //as decendents
    size_t obj_count = objects.size();
    vector<list<NiNodeRef>> ancestors(obj_count );

    //Add Ancestors of each object to its corresponding list
    for (size_t i = 0; i < obj_count; ++i)
    {
        ancestors[i] = ListAncestors(objects[i]);
    }

    if (ancestors[0].size() == 0)
    {
        //All objects must have a parent for there to be a common ancestor, so return null
        return null;
    }

    NiNodeRef root = ancestors[0].front();
    //Make sure bone and shapes are part of the same tree
    for (size_t i = 1; i < obj_count; ++i)
    {
        if (ancestors[i].size() == 0)
        {
            //All objects must have a parent for there to be a common ancestor, so return null
            return null;
        }
        if (ancestors[i].front() != root)
        {
            //These objects are not part of the same tree, so return null
            return null;
        }
    }

    //Since the first items have been shown to match, pop all the stacks
    for (size_t i = 0; i < obj_count; ++i)
    {
        ancestors[i].pop_front();
    }

    //Now search for the common ancestor
    while (true)
    {
        bool all_same = true;
        if (ancestors[0].size() == 0)
        {
            //This list is over, so the last top is the common ancestor
            //break out of the loop
            break;
        }
        NiNodeRef first_ancestor = ancestors[0].front();
        for (size_t i = 1; i < obj_count; ++i)
        {
            if (ancestors[i].size() == 0)
            {
                //This list is over, so the last top is the common ancestor
                //break out of the loop
                all_same = false;
                break;
            }
            if (ancestors[i].front() != first_ancestor)
            {
                all_same = false;
            }
        }

        if (all_same == true)
        {
            //They're all the same, so set the top, pop all the stacks
            //and look again

            root = ancestors[0].front();
            for (size_t i = 0; i < obj_count; ++i)
            {
                ancestors[i].pop_front();
            }
        }
        else
        {
            //One is different, so the last top is the common ancestor.
            //break out of the loop
            break;
        }
    }

    //Return result
    return root;
}

}
}