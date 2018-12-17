using System;
using System.Collections.Generic;
using System.IO;

namespace Niflib
{
    //KFM Data Structure
    public class KfmEventString
    {
        uint unk_int;
        string @event;

        public KfmEventString() { unk_int = 0; @event = null; }
        public void Read(IStream s, uint version)
        {
            unk_int = Nif.ReadUInt(s);
            @event = Nif.ReadString(s);
        }
        public void Write(OStream s, uint version)
        {
            Nif.WriteUInt(unk_int, s);
            Nif.WriteString(@event, s);
        }
    }

    public class KfmEvent
    {
        uint id;
        uint type;
        float unk_float;
        List<KfmEventString> event_strings;
        uint unk_int3;

        public KfmEvent() { id = 0; type = 0; unk_float = 0.5f; event_strings = new List<KfmEventString>(); unk_int3 = 0; }
        public void Read(IStream s, uint version)
        {
            id = Nif.ReadUInt(s);
            type = Nif.ReadUInt(s);
            if (type != 5)
            {
                unk_float = Nif.ReadFloat(s);
                event_strings.Resize(Nif.ReadUInt(s));
                foreach (var it in event_strings) it.Read(s, version);
                unk_int3 = Nif.ReadUInt(s);
            }
        }
    }

    public class KfmAction
    {
        string action_name;
        string action_filename;
        uint unk_int1;
        List<KfmEvent> events;
        uint unk_int2;

        public void Read(IStream s, uint version)
        {
            if (version <= Kfm.VER_KFM_1_2_4b) action_name = Nif.ReadString(s);
            action_filename = Nif.ReadString(s);
            unk_int1 = Nif.ReadUInt(s);
            events.Resize(Nif.ReadUInt(s));
            foreach (var it in events) it.Read(s, version);
            unk_int2 = Nif.ReadUInt(s);
        }
    }

    public class Kfm
    {
        //--KFM File Format--//

        //KFM Versions
        public const uint VER_KFM_1_0 = 0x01000000; /*!< Kfm Version 1.0 */
        public const uint VER_KFM_1_2_4b = 0x01020400; /*!< Kfm Version 1.2.4b */
        public const uint VER_KFM_2_0_0_0b = 0x02000000; /*!< Kfm Version 2.0.0.0b */

        uint version;
        byte unk_byte;
        string nif_filename;
        string master;
        uint unk_int1;
        uint unk_int2;
        float unk_float1;
        float unk_float2;
        uint unk_int3;
        List<KfmAction> actions;

        // Reads the given file and returns the KFM version.
        public uint Read(string file_name) // returns Kfm version
        {
            var s = new IStream(File.OpenRead(file_name));
            uint version = Read(s);
            if (s.Eof())
                throw new Exception("End of file reached prematurely. This KFM may be corrupt or improperly supported.");
            Nif.ReadByte(s); // this should fail, and trigger the in.eof() flag
            if (!s.Eof())
                throw new Exception("End of file not reached. This KFM may be corrupt or improperly supported.");
            return version;
        }

        public uint Read(IStream s) // returns Kfm version
        {
            //--Read Header--//
            byte[] header_string = new byte[64];
            string headerstr = s.GetLine(header_string, 64);
            // make sure this is a KFM file
            if (headerstr.Substring(0, 26) != ";Gamebryo KFM File Version")
            {
                version = Niflib.VER_INVALID;
                return version;
            }
            // supported versions
            if (headerstr == ";Gamebryo KFM File Version 2.0.0.0b") version = VER_KFM_2_0_0_0b;
            else if (headerstr == ";Gamebryo KFM File Version 1.2.4b") version = VER_KFM_1_2_4b;
            //else if ( headerstr == ";Gamebryo KFM File Version 1.0" ) version = VER_KFM_1_0;
            //else if ( headerstr == ";Gamebryo KFM File Version 1.0\r" ) version = VER_KFM_1_0; // Windows eol style
            else
            {
                version = Niflib.VER_UNSUPPORTED;
                return version;
            }
            //--Read remainder--//
            if (version == VER_KFM_1_0)
            {
                // TODO write a parser
            }
            else
            {
                if (version >= VER_KFM_2_0_0_0b) unk_byte = Nif.ReadByte(s);
                else unk_byte = 1;
                nif_filename = Nif.ReadString(s);
                master = Nif.ReadString(s);
                unk_int1 = Nif.ReadUInt(s);
                unk_int2 = Nif.ReadUInt(s);
                unk_float1 = Nif.ReadFloat(s);
                unk_float2 = Nif.ReadFloat(s);
                actions.Resize(Nif.ReadUInt(s));
                unk_int3 = Nif.ReadUInt(s);
                foreach (var it in actions) it.Read(s, version);
            }

            // Retrieve action names
            if (version >= VER_KFM_2_0_0_0b)
            {
                string model_name = nif_filename.Substring(0, nif_filename.Length - 4); // strip .nif extension
                foreach (var it in actions)
                {
                    string action_name = it.action_filename.Substring(0, it.action_filename.Length - 3); // strip .kf extension
                    if (action_name.Find(model_name + "_") == 0)
                        action_name = action_name.Substring(model_name.Length + 1, string.npos);
                    if (action_name.Find(master + "_") == 0)
                        action_name = action_name.Substring(master.Length + 1, string.npos);
                    it.action_name = action_name;
                }
            }
            return version;
        }

        /*
        void Kfm::Write(OStream s, uint version ) {
            if ( version == VER_KFM_1_0 ) {
                // handle this case seperately
                out << ";Gamebryo KFM File Version 1.0" << endl;
                // TODO write the rest of the data
            } else {
                if ( version == VER_KFM_1_2_4b )
                    out.write(";Gamebryo KFM File Version 1.2.4b\n", 34);
                else if ( version == VER_KFM_2_0_0_0b )
                    out.write(";Gamebryo KFM File Version 2.0.0.0b\n", 37);
                else throw runtime_error("Cannot write KFM file of this version.");
            }
        }
        */

        // Reads the NIF file and all KF files referred to in this KFM, and returns the root object of the resulting NIF tree.
        public Ref<NiObject> MergeActions(string path)
        {
            // Read NIF file
            NiObjectRef nif = ReadNifTree(path + '\\' + nif_filename);

            // Read Kf files
            List<NiObjectRef> kf = new List<NiObjectRef>();
            foreach (var it in actions)
            {
                string action_filename = path + '\\' + it.action_filename;
                // Check if the file exists.
                // Probably we should check some other field in the Kfm file to determine this...
                bool exists = File.Exists(action_filename);
                // Import it, if it exists.
                if (exists) kf.Add(ReadNifTree(action_filename));
            }
            // TODO: merge everything into the nif file
            return nif;
        }
    }
}