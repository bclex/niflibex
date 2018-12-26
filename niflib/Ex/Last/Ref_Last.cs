/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;

namespace Niflib.Last
{
    public class Ref<T> : IDisposable
        where T : RefObject
    {
        public Ref(T obj = null)
        {
            _obj = obj;
            //If object isn't null, increment reference count
            if (_obj != null)
                _obj.AddRef();
        }

        public Ref(Ref<T> ref_to_copy)
        {
            _obj = ref_to_copy._obj;
            //If object isn't null, increment reference count
            if (_obj != null)
                _obj.AddRef();
        }

        public void Dispose()
        {
            //if object insn't null, decrement reference count
            if (_obj != null)
                _obj.SubtractRef();
        }

        //public T operator *() => _object;

        //public bool operator <(const Ref & ref) const;

        //public bool operator ==(T object) => (_obj < ref._obj);
        //public bool operator !=(T object) => (_obj != obj);
        //public bool operator ==(Ref<T> ref) => (_obj == ref._obj);
        //public bool operator !=(Ref<T> ref) => (_obj != ref._obj);

        //internal Stream operator <<(Stream s, Ref<T> ref_)
        //{
        //    s << (ref_._obj != null ? ref_->GetIDString() : "NULL");
        //}

        //public static Ref<T> operator=(Ref<T> t, T obj)
        //{
        //    //Check if referenced objects are already the same
        //    if (t._obj == obj)
        //        return t; //Do nothing
        //    //Increment reference count on new object if it is not NULL
        //    if (obj != null)
        //        obj.AddRef();
        //    //Decrement reference count on previously referenced object, if any
        //    if (t._obj != null)
        //        t._obj.SubtractRef();
        //    //Change reference to new object
        //    t._obj = obj;
        //    return t;
        //}
        //public Ref & operator=( const Ref & ref );

        //public operator T ()  => _object;
        //public T operator->() => _object;

        protected T _obj;

    }
}