/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;

namespace Niflib
{
    public struct HeaderString
    {
        public string header;
    }

    public struct ShortString
    {
        public string str;
    }

    public struct LineString
    {
        public string line;
    }

    public struct IndexString
    {
        internal string val;
        //public IndexString() { val = null; }
        public IndexString(IndexString r) { val = r.val; }
        public IndexString(string r) { val = r; }
        //IndexString& operator=( const IndexString & ref ) { assign((std::string const &)ref); return *this; }
        //IndexString& operator=( const std::string & ref ) { assign(ref); return *this; }
        //operator std::string const &() const { return *this; }
        //operator std::string &() { return *this; }
    }

    public struct Char8String
    {
        internal string val;
        //public Char8String() { val = null; }
        public Char8String(Char8String r) { val = r.val; }
        public Char8String(string r) { val = r; }
        //Char8String& operator=( const Char8String & ref ) { assign((std::string const &)ref); return *this; }
        //   Char8String& operator=( const std::string & ref ) { assign(ref); return *this; }
        //   operator std::string const &() const { return *this; }
        //   operator std::string &() { return *this; }
    }

    /*!
     * Used to specify optional ways the NIF file is to be written or retrieve information about
     * the way an existing file was stored. 
     */
    public class NifInfo
    {
        public static NifInfo Empty = new NifInfo();
        public NifInfo()
        : this(Niflib.VER_4_0_0_2, 0, 0) { }
        public NifInfo(uint version, uint userVersion = 0, uint userVersion2 = 0)
        {
            this.version = version;
            this.userVersion = userVersion;
            this.userVersion2 = userVersion2;
            endian = EndianType.ENDIAN_LITTLE;
            creator = null;
            exportInfo1 = null;
            exportInfo2 = null;
        }

        public uint version;
        public uint userVersion;
        public uint userVersion2;
        /*! Specifies which low-level number storage format to use. Should match the processor type for the target system. */
        public EndianType endian;
        /*! This is only supported in Oblivion.  It contains the name of the person who created the NIF file. */
        public string creator;
        /*! This is only supported in Oblivion.  It seems to contain the type of script or program used to export the file. */
        public string exportInfo1;
        /*! This is only supported in Oblivion.  It seems to contain the more specific script or options of the above. */
        public string exportInfo2;
    };
}