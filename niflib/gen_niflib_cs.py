#!/usr/bin/python

# gen_niflib_cs.py
#
# This script generates C# code for Niflib.
#
# --------------------------------------------------------------------------
# Command line options
#
# -p /path/to/niflib : specifies the path where niflib can be found
#
# -b : enable bootstrap mode (generates templates)
#
# -i : do NOT generate implmentation; place all code in defines.h
#
# -a : generate accessors for data in classes
#
# -n <block>: generate only files which match the specified name
#
# --------------------------------------------------------------------------
# ***** BEGIN LICENSE BLOCK *****
#
# Copyright (c) 2005, NIF File Format Library and Tools
# All rights reserved.
#
# Redistribution and use in source and binary forms, with or without
# modification, are permitted provided that the following conditions
# are met:
#
#    * Redistributions of source code must retain the above copyright
#      notice, this list of conditions and the following disclaimer.
#
#    * Redistributions in binary form must reproduce the above
#      copyright notice, this list of conditions and the following
#      disclaimer in the documentation and/or other materials provided
#      with the distribution.
#
#    * Neither the name of the NIF File Format Library and Tools
#      project nor the names of its contributors may be used to endorse
#      or promote products derived from this software without specific
#      prior written permission.
#
# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
# "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
# LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
# FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE
# COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
# INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
# BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
# LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
# CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
# LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
# ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
# POSSIBILITY OF SUCH DAMAGE.
#
# ***** END LICENSE BLOCK *****
# --------------------------------------------------------------------------
from __future__ import unicode_literals
from nifxml_cs import *
from distutils.dir_util import mkpath
import os
import io
import itertools

#
# global data
#
ROOT_DIR = "Ex/_"
BOOTSTRAP = True
GENIMPL = True
GENACCESSORS = False
GENBLOCKS = []
GENALLFILES = True

prev = ""
for i in sys.argv:
    if prev == "-p":
        ROOT_DIR = i
    elif i == "-b":
        BOOTSTRAP = True
    elif i == "-i":
        GENIMPL = False
    elif i == "-a":
        GENACCESSORS = True
    elif prev == "-n":
        GENBLOCKS.append(i)
        GENALLFILES = False
        
    prev = i


# Fix known manual update attributes.  For now hard code here.
block_types["NiKeyframeData"].find_member("Num Rotation Keys").is_manual_update = True
#block_types["NiTriStripsData"].find_member("Num Triangles").is_manual_update =
#True

#
# Function to extract custom code from existing file
#
def ExtractCustomCode(file_name):
    custom_lines = {}
    custom_lines['MISC'] = []
    custom_lines['FILE HEAD'] = []
    custom_lines['FILE FOOT'] = []
    custom_lines['PRE-READ'] = []
    custom_lines['POST-READ'] = []
    custom_lines['PRE-WRITE'] = []
    custom_lines['POST-WRITE'] = []
    custom_lines['PRE-STRING'] = []
    custom_lines['POST-STRING'] = []
    custom_lines['PRE-FIXLINKS'] = []
    custom_lines['POST-FIXLINKS'] = []
    custom_lines['CONSTRUCTOR'] = []
    custom_lines['DESTRUCTOR'] = []
    
    if True: # | os.path.isfile(file_name) == False:
        custom_lines['MISC'].append("\n")
        custom_lines['FILE HEAD'].append("\n")
        custom_lines['FILE FOOT'].append("\n")
        custom_lines['PRE-READ'].append("\n")
        custom_lines['POST-READ'].append("\n")
        custom_lines['PRE-WRITE'].append("\n")
        custom_lines['POST-WRITE'].append("\n")
        custom_lines['PRE-STRING'].append("\n")
        custom_lines['POST-STRING'].append("\n")
        custom_lines['PRE-FIXLINKS'].append("\n")
        custom_lines['POST-FIXLINKS'].append("\n")
        custom_lines['CONSTRUCTOR'].append("\n")
        custom_lines['DESTRUCTOR'].append("\n")
        return custom_lines
    
    f = io.open(file_name, 'rt', 1, 'utf-8')
    lines = f.readlines()
    f.close()
   
    custom_flag = False
    custom_name = ""
    
    for l in lines:
        if custom_flag == True:
            if l.find('//--END CUSTOM CODE--//') != -1:
                custom_flag = False
            else:
                if not custom_lines[custom_name]:
                    custom_lines[custom_name] = [l]
                else:
                    custom_lines[custom_name].append(l)
        if l.find('//--BEGIN MISC CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'MISC'
        elif l.find('//--BEGIN FILE HEAD CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'FILE HEAD'
        elif l.find('//--BEGIN FILE FOOT CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'FILE FOOT'
        elif l.find('//--BEGIN PRE-READ CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'PRE-READ'
        elif l.find('//--BEGIN POST-READ CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'POST-READ'
        elif l.find('//--BEGIN PRE-WRITE CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'PRE-WRITE'
        elif l.find('//--BEGIN POST-WRITE CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'POST-WRITE'
        elif l.find('//--BEGIN PRE-STRING CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'PRE-STRING'
        elif l.find('//--BEGIN POST-STRING CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'POST-STRING'
        elif l.find('//--BEGIN PRE-FIXLINKS CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'PRE-FIXLINKS'
        elif l.find('//--BEGIN POST-FIXLINKS CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'POST-FIXLINKS'
        elif l.find('//--BEGIN CONSTRUCTOR CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'CONSTRUCTOR'
        elif l.find('//--BEGIN DESTRUCTOR CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'DESTRUCTOR'
        elif l.find('//--BEGIN INCLUDE CUSTOM CODE--//') != -1:
            custom_flag = True
            custom_name = 'INCLUDE'
    
    return custom_lines

#
# Function to compare two files
#
def OverwriteIfChanged(original_file, candidate_file):
    files_differ = False

    if os.path.isfile(original_file):
        f1 = file(original_file, 'r')
        f2 = file(candidate_file, 'r')

        s1 = f1.read()
        s2 = f2.read()

        f1.close()
        f2.close()
        
        if s1 != s2:
            files_differ = True
            #remove original file
            os.unlink(original_file)
    else:
        files_differ = True

    if files_differ:
        #Files differ, so overwrite original with candidate
        os.rename(candidate_file, original_file)
   
#
# generate compound code
#
mkpath(os.path.join(ROOT_DIR, "obj"))
mkpath(os.path.join(ROOT_DIR, "gen"))

for n in compound_names:
    x = compound_types[n]
    
    # skip natively implemented types
    if x.name in NATIVETYPES.keys(): continue
    
    if not GENALLFILES and not x.cname in GENBLOCKS:
            continue
        
    #Get existing custom code
    file_name = ROOT_DIR + '/gen/' + x.cname + '.cs'
    custom_lines = ExtractCustomCode(file_name)

    cs = CSFile(io.open(file_name, 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs Python script.')
    cs.code()
    cs.code('using mylib;')
    if n in ["Header", "Footer"]:
        cs.code('using mylib2')
    cs.code(x.code_using())
    cs.write("namespace Niflib {\n")
    cs.code()
    # header
    cs.comment(x.description)
    hdr = "struct %s" % x.cname
    if x.template: hdr = "template <class T >\n%s" % hdr
    hdr += " {"
    cs.code(hdr)
    
    #constructor/destructor/assignment
    #if not x.template:
    #    cs.code( '/*!  Default Constructor */' )
    #    cs.code( "NIFLIB_API %s();"%x.cname )
    #    cs.code( '/*!  Default Destructor */' )
    #    cs.code( "NIFLIB_API ~%s();"%x.cname )
    #    cs.code( '/*!  Copy Constructor */' )
    #    cs.code( 'NIFLIB_API %s( const %s & src );'%(x.cname, x.cname) )
    #    cs.code( '/*!  Copy Operator */' )
    #    cs.code( 'NIFLIB_API %s & operator=( const %s & src );'%(x.cname,
    #    x.cname) )

    # declaration
    cs.declare(x)

    # header and footer functions
    #if n == "Header":
    #    cs.code( 'NIFLIB_HIDDEN NifInfo Read( istream& in );' )
    #    cs.code( 'NIFLIB_HIDDEN void Write( ostream& out, const NifInfo & info
    #    = NifInfo() ) const;' )
    #    cs.code( 'NIFLIB_HIDDEN string asString( bool verbose = false )
    #    const;' )
    
    #if n == "Footer":
    #    cs.code( 'NIFLIB_HIDDEN void Read( istream& in, list<unsigned int> &
    #    link_stack, const NifInfo & info );' )
    #    cs.code( 'NIFLIB_HIDDEN void Write( ostream& out, const
    #    map<NiObjectRef,unsigned int> & link_map, list<NiObject *> &
    #    missing_link_stack, const NifInfo & info ) const;' )
    #    cs.code( 'NIFLIB_HIDDEN string asString( bool verbose = false )
    #    const;' )

    if not x.template:

        cs.code('//Constructor')
        
        # constructor
        x_code_construct = x.code_construct()
        #if x_code_construct:
        cs.code("%s::%s()" % (x.cname,x.cname) + x_code_construct + " {};")
        cs.code()

        #cs.code('//Copy Constructor')
        #cs.code( '%s(%s src) {'%(x.cname,x.cname) )
        #cs.code( '*this = src;' )
        #cs.code('};')
        #cs.code()

        cs.code('//Copy Operator')
        cs.code('%s & %s::operator=( const %s & src ) {' % (x.cname,x.cname,x.cname))
        for m in x.members:
            if not m.is_duplicate:
                cs.code('this->%s = src.%s;' % (m.cname, m.cname))
        cs.code('return *this;')
        cs.code('};')
        cs.code()

        ## header and footer functions
        #if n == "Header":
        #    cs.code( 'NifInfo ' + x.cname + '::Read( istream& in ) {' )
        #    cs.code( '//Declare NifInfo structure' )
        #    cs.code( 'NifInfo info;' )
        #    cs.code()
        #    cs.stream(x, ACTION_READ)
        #    cs.code()
        #    cs.code( '//Copy info.version to local version var.' )
        #    cs.code( 'version = info.version;' )
        #    cs.code()
        #    cs.code( '//Fill out and return NifInfo structure.' )
        #    cs.code( 'info.userVersion = userVersion;' )
        #    cs.code( 'info.userVersion2 = userVersion2;' )
        #    cs.code( 'info.endian = EndianType(endianType);' )
        #    cs.code( 'info.creator = exportInfo.creator.str;' )
        #    cs.code( 'info.exportInfo1 = exportInfo.exportInfo1.str;' )
        #    cs.code( 'info.exportInfo2 = exportInfo.exportInfo2.str;' )
        #    cs.code()
        #    cs.code( 'return info;' )
        #    cs.code()
        #    cs.code( '}' )
        #    cs.code()
        #    cs.code( 'void ' + x.cname + '::Write( ostream& out, const NifInfo
        #    & info ) const {' )
        #    cs.stream(x, ACTION_WRITE)
        #    cs.code( '}' )
        #    cs.code()
        #    cs.code( 'string ' + x.cname + '::asString( bool verbose ) const
        #    {' )
        #    cs.stream(x, ACTION_OUT)
        #    cs.code( '}' )
        
        #if n == "Footer":
        #    cs.code()
        #    cs.code( 'void ' + x.cname + '::Read( istream& in, list<unsigned
        #    int> & link_stack, const NifInfo & info ) {' )
        #    cs.stream(x, ACTION_READ)
        #    cs.code( '}' )
        #    cs.code()
        #    cs.code( 'void ' + x.cname + '::Write( ostream& out, const
        #    map<NiObjectRef,unsigned int> & link_map, list<NiObject *> &
        #    missing_link_stack, const NifInfo & info ) const {' )
        #    cs.stream(x, ACTION_WRITE)
        #    cs.code( '}' )
        #    cs.code()
        #    cs.code( 'string ' + x.cname + '::asString( bool verbose ) const
        #    {' )
        #    cs.stream(x, ACTION_OUT)
        #    cs.code( '}' )

    cs.code('//--BEGIN MISC CUSTOM CODE--//')

    #Preserve Custom code from before
    for l in custom_lines['MISC']:
        cs.write(l)
        
    cs.code('//--END CUSTOM CODE--//')

    # done
    cs.code("};")
    cs.code()
    cs.write("}\n")
    cs.close()


    # Write out Public Enumeration header Enumerations
if GENALLFILES:
    cs = CSFile(io.open(ROOT_DIR + '/gen/Enums.cs', 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs.py Python script.')
    cs.code()
    cs.code('using System;')
    cs.code()
    cs.write('namespace Niflib {\n')
    cs.code()
    for n, x in itertools.chain(enum_types.items(), flag_types.items()):
      if x.options:
        if x.description:
          cs.comment(x.description)
        cs.code('public enum %s : uint {' % (x.cname))
        for o in x.options:
          cs.code('%s = %s, /*!< %s */' % (o.cname, o.value, o.description))
        cs.code('};')
        cs.code()
        #: cpp
        #cs.code()
        #cs.code('//--' + x.cname + '--//')
        #cs.code()
        #cs.code('void NifStream( %s & val, istream& in, const NifInfo & info )
        #{' % (x.cname))
        #cs.code('%s temp;' % (x.storage))
        #cs.code('NifStream( temp, in, info );')
        #cs.code('val = %s(temp);' % (x.cname))
        #cs.code('}')
        #cs.code()
        #cs.code('void NifStream( %s const & val, ostream& out, const NifInfo &
        #info ) {' % (x.cname))
        #cs.code('NifStream( (%s)(val), out, info );' % (x.storage))
        #cs.code('}')
        #cs.code()
        #cs.code('ostream & operator<<( ostream & out, %s const & val ) { ' %
        #(x.cname))
        #cs.code('switch ( val ) {')
        #for o in x.options:
        #  cs.code('case %s: return out << "%s";' % (o.cname, o.name))
        #cs.code('default: return out << "Invalid Value!  - " << (unsigned
        #int)(val);')
        #cs.code('}')
        #cs.code('}')
        #cs.code()

    cs.write('}\n')
    cs.close()
        
    # Write out Internal Enumeration header (NifStream functions)
if GENALLFILES:
    cs = CSFile(io.open(ROOT_DIR + '/gen/Enums_intl.cs', 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs.py Python script.')
    cs.code()
    cs.code('using std;')
    cs.code()
    cs.write('namespace Niflib {\n')
    cs.code()
    for n, x in itertools.chain(enum_types.items(), flag_types.items()):
      if x.options:
        if x.description:
            cs.code()
            cs.code('//---' + x.cname + '---//')
            cs.code()
        cs.code('void NifStream( %s & val, istream& in, const NifInfo & info = NifInfo() );' % x.cname)
        cs.code('void NifStream( %s const & val, ostream& out, const NifInfo & info = NifInfo() );' % x.cname)
        cs.code()

    cs.write('}\n')
    cs.close()


    #
    # NiObject Registration Function
    #
    cs = CSFile(io.open(ROOT_DIR + '/gen/Register.cs', 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs.py Python script.')
    cs.code()
    cs.code('namespace Niflib {')
    cs.code('void RegisterObjects() {')
    cs.code()
    for n in block_names:
        x = block_types[n]
        cs.code('ObjectRegistry.RegisterObject( "' + x.name + '", ' + x.cname + '.Create );')
    cs.code()
    cs.code('}')
    cs.code('}')
    cs.close()
    









#
# NiObject Files
#
for n in block_names:
    x = block_types[n]
    x_define_name = define_name(x.cname)

    if not GENALLFILES and not x.cname in GENBLOCKS:
        continue
    
    #
    # NiObject Header File
    #

    #Get existing custom code
    file_name = ROOT_DIR + '/obj/' + x.cname + '.cs'
    custom_lines = ExtractCustomCode(file_name)

    #output new file
    cs = CSFile(io.open(file_name, 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//-----------------------------------NOTICE----------------------------------//')
    cs.code('// Some of this file is automatically filled in by a Python script.  Only    //')
    cs.code('// add custom code in the designated areas or it will be overwritten during  //')
    cs.code('// the next update.                                                          //')
    cs.code('//-----------------------------------NOTICE----------------------------------//')
    cs.code()
    cs.code(x.code_using())
    cs.code('using ' + x.cname + 'Ref = Niflib.Ref<Niflib.' + x.cname + '>;')
    cs.code()
    #Preserve Custom code from before
    cs.code('//--BEGIN FILE HEAD CUSTOM CODE--//')
    for l in custom_lines['FILE HEAD']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()
    cs.write("namespace Niflib {\n")
    cs.code()
    cs.comment(x.description)
    if x.inherit:
        cs.code('public class ' + x.cname + ' : ' + x.inherit.cname + ' {')
    else:
        cs.code('public class ' + x.cname + ' : RefObject {')
    cs.code('//Definition of TYPE constant')
    if x.inherit:
        cs.code('public static readonly Type_ TYPE = new Type_(\"' + x.name + '\", ' + x.inherit.cname + '.TYPE);')
    else:
        cs.code('public static readonly Type_ TYPE = new Type_(\"' + x.name + '\", RefObject.TYPE);')

    #
    # Show example naive implementation if requested
    #
    
    # Create a list of members eligable for functions
    #if GENACCESSORS:
    #    func_members = []
    #    for y in x.members:
    #        if not y.arr1_ref and not y.arr2_ref and
    #        y.cname.lower().find("unk") == -1:
    #            func_members.append(y)
    
    #    if len(func_members) > 0:
    #        cs.code('/***Begin Example Naive Implementation****')
    #        cs.code()
    #        for y in func_members:
    #            cs.comment(y.description + "\n\\return The current value.",
    #            False)
    #            cs.code(y.getter_declare("", ";"))
    #            cs.code()
    #            cs.comment(y.description + "\n\\param[in] value The new
    #            value.", False)
    #            cs.code(y.setter_declare("", ";"))
    #            cs.code()
    #        cs.code('****End Example Naive Implementation***/')
    #    else:
    #        cs.code('//--This object has no eligable attributes.  No example
    #        implementation generated--//')
    #    cs.code()
    
    #Preserve Custom code from before
    cs.code('//--BEGIN MISC CUSTOM CODE--//')
    for l in custom_lines['MISC']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')

    #if x.members:
    #    cs.code('protected:')
    cs.declare(x)

    cs.code()
    x_code_construct = x.code_construct()
    if x_code_construct:
        cs.code('public ' + x.cname + '()' + x_code_construct + ' {')
    else:
        cs.code('public ' + x.cname + '() {')
    
    #Preserve Custom code from before
    cs.code('//--BEGIN CONSTRUCTOR CUSTOM CODE--//')
    for l in custom_lines['CONSTRUCTOR']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code('}')
    cs.code()

    cs.code('/*!')
    cs.code(' * Used to determine the type of a particular instance of this object.')
    cs.code(' * \\return The type constant for the actual type of the object.')
    cs.code(' */')
    cs.code('public override Type_ GetType() => TYPE;')
    cs.code()
    cs.code('/*!')
    cs.code(' * A factory function used during file reading to create an instance of this type of object.')
    cs.code(' * \\return A pointer to a newly allocated instance of this type of object.')
    cs.code(' */')
    cs.code('public static NiObject Create() => new ' + x.cname + '();')
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("protected override Read(StreamReader s, List<uint> link_stack, NifInfo info) {")

    #Preserve Custom code from before
    cs.code('//--BEGIN PRE-READ CUSTOM CODE--//')
    for l in custom_lines['PRE-READ']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()
    cs.stream(x, ACTION_READ)
    cs.code()

    #Preserve Custom code from before
    cs.code('//--BEGIN POST-READ CUSTOM CODE--//')
    for l in custom_lines['POST-READ']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code("}")
    cs.code()
      
    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("protected override Write(StreamWriter s, Dictionary<NiObjectRef, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {")

    #Preserve Custom code from before
    cs.code('//--BEGIN PRE-WRITE CUSTOM CODE--//')
    for l in custom_lines['PRE-WRITE']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()
    cs.stream(x, ACTION_WRITE)
    cs.code()
    #Preserve Custom code from before
    cs.code('//--BEGIN POST-WRITE CUSTOM CODE--//')
    for l in custom_lines['POST-WRITE']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code("}")
    cs.code()
      
    cs.code('/*!')
    cs.code(' * Summarizes the information contained in this object in English.')
    cs.code(' * \\param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.')
    cs.code(' * \\return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.')
    cs.code(' */')
    cs.code("public override string asString(bool verbose = false) {")

    #Preserve Custom code from before
    cs.code('//--BEGIN PRE-STRING CUSTOM CODE--//')
    for l in custom_lines['PRE-STRING']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()
    cs.stream(x, ACTION_OUT)
    cs.code()

    #Preserve Custom code from before
    cs.code('//--BEGIN POST-STRING CUSTOM CODE--//')
    for l in custom_lines['POST-STRING']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("protected override void FixLinks(Dictionary<uint, NiObjectRef> objects, List<uint> link_stack, List<NiObjectRef> missing_link_stack, NifInfo info) {")

    #Preserve Custom code from before
    cs.code('//--BEGIN PRE-FIXLINKS CUSTOM CODE--//')
    for l in custom_lines['PRE-FIXLINKS']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()
    cs.stream(x, ACTION_FIXLINKS)
    cs.code()

    #Preserve Custom code from before
    cs.code('//--BEGIN POST-FIXLINKS CUSTOM CODE--//')
    for l in custom_lines['POST-FIXLINKS']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("protected override List<NiObjectRef> GetRefs() {")
    cs.stream(x, ACTION_GETREFS)
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("protected override List<NiObject> GetPtrs() {")
    cs.stream(x, ACTION_GETPTRS)
    cs.code("}")
    cs.code()

    # Output example implementation of public getter/setter Mmthods if
    # requested
    #if GENACCESSORS:
    #    func_members = []
    #    for y in x.members:
    #        if not y.arr1_ref and not y.arr2_ref and
    #        y.cname.lower().find("unk") == -1:
    #            func_members.append(y)
    
    #    if len(func_members) > 0:
    #        cs.code('/***Begin Example Naive Implementation****')
    #        cs.code()
    #        for y in func_members:
    #            cs.code(y.getter_declare(x.name + "::", " {"))
    #            cs.code("return %s;" % y.cname)
    #            cs.code("}")
    #            cs.code()
                
    #            cs.code(y.setter_declare(x.name + "::", " {"))
    #            cs.code("%s = value;" % y.cname)
    #            cs.code("}")
    #            cs.code()
    #        cs.code('****End Example Naive Implementation***/')
    #    else:
    #        cs.code('//--This object has no eligable attributes.  No example
    #        implementation generated--//')
    #    cs.code()
        
    #Preserve Custom code from before
    cs.code('//--BEGIN MISC CUSTOM CODE--//')
    for l in custom_lines['MISC']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    
    #Preserve Custom code from before
    cs.code('//--BEGIN FILE FOOT CUSTOM CODE--//')
    for l in custom_lines['FILE FOOT']:
        cs.write(l)
    cs.code('//--END CUSTOM CODE--//')
    cs.code()

    cs.code('};')
    cs.code()

    cs.write("}")
    cs.close()

    ##Check if the temp file is identical to the target file
    #OverwriteIfChanged( file_name, 'temp' )