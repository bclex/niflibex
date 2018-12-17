/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;

namespace Niflib
{
    /*! Stores 2D texture coordinates as two floating point variables, u and v. */
    public struct TexCoord
    {
        public float u; /*!< The U value in this coordinate pair. */
        public float v; /*!< The V value in this coordinate pair. */

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] u The value to set U to.
         * \param[in] v The value to set V to.
         */
        public TexCoord(float u = 0f, float v = 0f)
        {
            this.u = u;
            this.v = v;
        }
        public TexCoord(TexCoord src) : this(src.u, src.v) { }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] u The value to set U to.
         * \param[in] v The value to set V to.
         */
        public void Set(float u, float v)
        {
            this.u = u;
            this.v = v;
        }

        public static TexCoord operator +(TexCoord ret, TexCoord rhs) { ret.u += rhs.u; ret.v += rhs.v; return ret; }
        public static TexCoord operator -(TexCoord ret, TexCoord rhs) { ret.u -= rhs.u; ret.v -= rhs.v; return ret; }
        public static TexCoord operator *(TexCoord ret, float rhs) { ret.u *= rhs; ret.v *= rhs; return ret; }
        public static bool operator ==(TexCoord t, TexCoord n) => t.u == n.u && t.v == n.v;
        public static bool operator !=(TexCoord t, TexCoord n) => t.u != n.u || t.v != n.v;
    }

    /*! Represents a triangle face formed between three vertices referenced by number */
    public struct Triangle
    {
        public ushort v1; /*!< The index of the first vertex. */
        public ushort v2; /*!< The index of the second vertex. */
        public ushort v3; /*!< The index of the third vertex. */

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] v1 The index of the first vertex.
         * \param[in] v2 The index of the second vertex.
         * \param[in] v3 The index of the third vertex.
         */
        public Triangle(ushort v1 = 0, ushort v2 = 0, ushort v3 = 0)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] v1 The index of the first vertex.
         * \param[in] v2 The index of the second vertex.
         * \param[in] v3 The index of the third vertex.
         */
        public void Set(ushort v1, ushort v2, ushort v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        /*! The bracket operator makes it possible to use this structure like a C++ array.
         * \param[in] n The index into the data array.  Should be 0, 1, or 2.
         * \return The value at the given array index by reference so it can be read or set via the bracket operator.
         */
        public ushort this[int n] { get { switch (n) { case 0: return v1; case 1: return v2; case 2: return v3; default: throw new IndexOutOfRangeException("Index out of range for Triangle"); } } }
    }

    /*!Represents a position or direction in 3D space*/
    public struct Vector3
    {
        public float x; /*!< The X component of this vector. */
        public float y; /*!< The Y component of this vector. */
        public float z; /*!< The Z component of this vector. */

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] x The value to set X to.
         * \param[in] y The value to set Y to.
         * \param[in] z The value to set Z to.
         */
        public Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /*! This ructor can be used to initialize this Vector3 with another Vector3
         * \param[in] v The Vector3 to ruct this one from
         */
        public Vector3(Vector3 v) : this(v.x, v.y, v.z) { }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] x The value to set X to.
         * \param[in] y The value to set Y to.
         * \param[in] z The value to set Z to.
         */
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /* This function calculates the magnitude of this vector
         * \return the magnitude of the vector; its length.
         */
        public float Magnitude() => (float)Math.Sqrt(x * x + y * y + z * z);

        /* This function returns a normalization of this vector.  A vector pointing in the same
         * direction but with a magnitude, or length, of 1.
         */
        public Vector3 Normalized() { float m = Magnitude(); return new Vector3(x / m, y / m, z / m); }

        /* Allows the addition of vectors.  Each component, x, y, y, is added with
         * the same component of the other vector.
         * \return The result of the addition.
         */
        public static Vector3 operator +(Vector3 t, Vector3 rh) { t += rh; return t; }
        public static Vector3 operator +(Vector3 t, float rh) { t += rh; return t; }

        /* Adds the two vectors and then sets the result to the left-hand vector.
         * \return This vector is returned.
         */
        //public static Vector3 operator+=(Vector3 t, Vector3 rh) { t.x += rh.x; t.y += rh.y; t.z += rh.z; return t; }
        //public static Vector3 operator+=(Vector3 t, float rh) { t.x += rh; t.y += rh; t.z += rh; return t; }

        /* Allows the subtraction of vectors.  Each component, x, y, y, is subtracted from
         * the same component of the other vector.
         * \return The result of the subtraction.
         */
        public static Vector3 operator -(Vector3 t, Vector3 rh) { t -= rh; return t; }
        public static Vector3 operator -(Vector3 t, float rh) { t -= rh; return t; }

        /* This operator subtracts the two vectors and then sets the result to the left-hand vector.
         * \return This vector is returned.
         */
        //public static Vector3 operator-=(Vector3 t, Vector3 rh) { t.x -= rh.x; t.y -= rh.y; t.z -= rh.z; return t; }
        //public static Vector3 operator-=(Vector3 t, float rh) { t.x -= rh; t.y -= rh; t.z -= rh; return t; }

        /* Allows scaler multiplication, that is multipying all components of the
         * vector, x, y and z, by the same number.
         * \return The result of the multiplication.
         */
        public static Vector3 operator *(Vector3 t, float rh) { t *= rh; return t; } //Scalar Multiply

        /* Multipies a vector by a scalar and then sets the result to the left-hand vector.
         * \return This vector is returned.
         */
        public static Vector3 operator *=(Vector3 t, float rh) { t.x *= rh; t.y *= rh; t.z *= rh; return t; }

        /* Multiplies a vector by a vector using the dot product
         * \return The dot product of the two vectors.
         */
        public static float operator *(Vector3 t, Vector3 v) => DotProduct(v);

        /* Multiplies a vector by a vector using the cross product
         * \return The cross product of the two vectors.
         */
        public static Vector3 operator ^(Vector3 t, Vector3 v) => CrossProduct(v);

        /* Allows scaler division, that is dividing all components of the
         * vector, x, y and z, by the same number.
         * \return The result of the division.
         */
        public static Vector3 operator /(Vector3 t, float rh) { t /= rh; return t; }

        /* Divides a vector by a scalar and then sets the result to the left-hand vector.
         * \return This vector is returned.
         */
        public static Vector3 operator/=(Vector3 t, float rh) { t.x /= rh; t.y /= rh; t.z /= rh; return t; }

        /* Sets the components of this Vector3 to those of another Vector3 
         * \return This vector is returned.
         */
        public static Vector3 operator=(Vector3 t, Vector3 v) { t.x = v.x; t.y = v.y; t.z = v.z; return t; }

        /* Tests the equality of two Vector3 structures.  Vectors are considered equal if all
         * three components are equal.
         */
        public static bool operator ==(Vector3 t, Vector3 rh) => rh.x == t.x && rh.y == t.y && rh.z == t.z;

        /* Tests the inequality of two Vector3 structures.  Vectors are considered equal if all
         * three components are equal.
         */
        public static bool operator !=(Vector3 t, Vector3 rh) => !(rh.x == t.x && rh.y == t.y && rh.z == t.z);

        /*! The bracket operator makes it possible to use this structure like a C++ array.
        * \param[in] n The index into the data array.  Should be 0, 1, or 2.
        * \return The value at the given array index by reference so it can be read or set via the bracket operator.
        */
        public float this[int n] { get { switch (n) { case 0: return x; case 1: return y; case 2: return z; default: throw new IndexOutOfRangeException("Index out of range for Vector3"); } } }

        /* Computes the dot product of two vectors; the angle between two vectors.
         * \param[in] rh The vector to perform the dot product with
         * \return The angle in radians between this vector and the one given
         */
        public float DotProduct(Vector3 rh) => x * rh.x + y * rh.y + z * rh.z;

        /* Computes the cross product of two vectors; a vector perpendicular to both of them.
         * \param[in] The vector to perform the cross product with
         * \return A vector perpendicular to this vector and the one given
         */
        public Vector3 CrossProduct(Vector3 rh) => new Vector3(y * rh.z - z * rh.y, z * rh.x - x * rh.z, x * rh.y - y * rh.x);
    }

    /*!Represents a position or direction in 3D space*/
    public struct Vector4
    {
        public float x; /*!< The X component of this vector. */
        public float y; /*!< The Y component of this vector. */
        public float z; /*!< The Z component of this vector. */
        public float w; /*!< The W component of this vector. */

        /*! This ructor can be used to set all values in this structure during initialization
        * \param[in] x The value to set X to.
        * \param[in] y The value to set Y to.
        * \param[in] z The value to set Z to.
        * \param[in] w The value to set W to.
        */
        public Vector4(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /*! This ructor can be used to initialize this Vector4 with another Vector4
        * \param[in] v The Vector4 to ruct this one from
        */
        public Vector4(Vector4 v) { x = v.x; y = v.y; z = v.z; w = v.w; }

        /*! This ructor can be used to initialize this Vector4 with a Vector3
        * \param[in] v The Vector3 to ruct this one from
        */
        public Vector4(Vector3 v) { x = v.x; y = v.y; z = v.z; w = 0.0f; }

        /*! This ructor can be used to initialize this Vector4 with a Float4
        * \param[in] v The Float4 to ruct this one from
        */
        public Vector4(Float4 v) { x = v[0]; y = v[1]; z = v[2]; w = v[3]; }

        /*! This function can be used to set all values in the structure at the same time.
        * \param[in] x The value to set X to.
        * \param[in] y The value to set Y to.
        * \param[in] z The value to set Z to.
        * \param[in] w The value to set W to.
        */
        public void Set(float x, float y, float z, float w = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /* This function calculates the magnitude of this vector
        * \return the magnitude of the vector; its length.
        */
        public float Magnitude() => (float)Math.Sqrt(x * x + y * y + z * z + w * w);

        /* This function returns a normalization of this vector.  A vector pointing in the same
        * direction but with a magnitude, or length, of 1.
        */
        public Vector4 Normalized() { float m = Magnitude(); return new Vector4(x / m, y / m, z / m, w / m); }

        /* Allows the addition of vectors.  Each component, x, y, z, w is added with
        * the same component of the other vector.
        * \return The result of the addition.
        */
        public static Vector4 operator +(Vector4 t, Vector4 rh) { t += rh; return t; }

        /* Adds the two vectors and then sets the result to the left-hand vector.
        * \return This vector is returned.
        */
        public static Vector4 operator+=(Vector4 t, Vector4 rh) { t.x += rh.x; t.y += rh.y; t.z += rh.z; t.w += rh.w; return t; }

        /* Allows the subtraction of vectors.  Each component, x, y, y, is subtracted from
        * the same component of the other vector.
        * \return The result of the subtraction.
        */
        public static Vector4 operator -(Vector4 t, Vector4 rh) { t -= rh; return t; }

        /* This operator subtracts the two vectors and then sets the result to the left-hand vector.
        * \return This vector is returned.
        */
        public static Vector4 operator-=(Vector4 t, Vector4 rh) { t.x -= rh.x; t.y -= rh.y; t.z -= rh.z; t.w -= rh.w; return t; }

        /* Allows scaler multiplication, that is multiplying all components of the
        * vector, x, y and z, by the same number.
        * \return The result of the multiplication.
        */
        public static Vector4 operator *(Vector4 t, float rh) { t *= rh; return t; }

        /* Multiplies a vector by a scalar and then sets the result to the left-hand vector.
        * \return This vector is returned.
        */
        static Vector4 operator*=(Vector4 t, float rh) { t.x *= rh; t.y *= rh; t.z *= rh; t.w *= rh; return t; }

        /* Multiplies a vector by a vector using the dot product
        * \return The dot product of the two vectors.
        */
        public static float operator *(Vector4 t, Vector4 v);

        /* Multiplies a vector by a vector using the cross product
        * \return The cross product of the two vectors.
        */
        public static Vector4 operator ^(Vector4 v);

        /* Allows scaler division, that is dividing all components of the
        * vector, x, y and z, by the same number.
        * \return The result of the division.
        */
        public static Vector4 operator /(Vector4 t, float rh) { t /= rh; return t; }

        /* Divides a vector by a scalar and then sets the result to the left-hand vector.
        * \return This vector is returned.
        */
        static Vector4 operator/=(Vector4 t, float rh) { t.x /= rh; t.y /= rh; t.z /= rh; t.w /= rh; return t; }

        /* Sets the components of this Vector4 to those of another Vector4 
        * \return This vector is returned.
        */
        public static Vector4 operator=(Vector4 t, Vector4 v) { t.x = v.x; t.y = v.y; t.z = v.z; t.w = v.w; return t; }

        /* Tests the equality of two Vector4 structures.  Vectors are considered equal if all
        * three components are equal.
        */
        public static bool operator ==(Vector4 t, Vector4 rh) => rh.x == t.x && rh.y == t.y && rh.z == t.z && rh.w == t.w;

        /* Tests the inequality of two Vector4 structures.  Vectors are considered equal if all
        * three components are equal.
        */
        public static bool operator !=(Vector4 t, Vector4 rh) => !(rh.x == t.x && rh.y == t.y && rh.z == t.z && rh.w == t.w);

        /*! The bracket operator makes it possible to use this structure like a C++ array.
        * \param[in] n The index into the data array.  Should be 0, 1, 2, or 3.
        * \return The value at the given array index by reference so it can be read or set via the bracket operator.
        */
        public float this[int n] { get { switch (n) { case 0: return x; case 1: return y; case 2: return z; case 3: return w; default: throw new ArgumentOutOfRangeException("Invalid index"); } } }
    }

    /* Stores two floating point numbers.  Used as a row of a Matrix22 */
    public struct Float2
    {
        public float[] data; /*!< The two floating point numbers stored as an array. */

        /*! The bracket operator makes it possible to use this structure like a C++ array.
         * \param[in] n The index into the data array.  Should be 0 or 1.
         * \return The value at the given array index by reference so it can be read or set via the bracket operator.
         */
        public float this[int n] { get { return data[n]; } }

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         */
        public Float2(float f1 = 0.0f, float f2 = 0.0f)
        {
            data = new float[] { f2, f2 };
        }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         */
        public void Set(float f1, float f2)
        {
            data[0] = f1;
            data[1] = f2;
        }
    }


    /*! Stores a 2 by 2 matrix used for bump maps. */
    public struct Matrix22
    {
        /*! The 2x2 identity matrix ant */
        public static Matrix22 IDENTITY = new Matrix22(
            1.0f, 0.0f,
            0.0f, 1.0f);

        public Float2[,] rows;  /*!< The two rows of Float2 structures which hold two floating point numbers each. */

        /*! The bracket operator makes it possible to use this structure like a 2x2 C++ array.
         * \param[in] n The index into the row array.  Should be 0 or 1.
         * \return The Float2 structure for the given row index by reference so it can be read or set via the bracket operator.
         */
        public Float2 this[int n] { get { return rows[n]; } }

        /*! This ructor can be used to set all values in this matrix during initialization
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         */
        public Matrix22(
            float m11, float m12,
            float m21, float m22)
        {
            rows = new float[2, 2]();
            rows[0][0] = m11; rows[0][1] = m12;
            rows[1][0] = m21; rows[1][1] = m22;
        }

        /*! This function can be used to set all values in this matrix at the same time.
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         */
        public void Set(
            float m11, float m12,
            float m21, float m22
        )
        {
            rows[0][0] = m11; rows[0][1] = m12;
            rows[1][0] = m21; rows[1][1] = m22;
        }
    }

    /* Stores three floating point numbers.  Used as a row of a Matrix33 and to store the data in attr_vector3 and attr_color3 type attributes. */
    public struct Float3
    {
        public float[] data; /*!< The three floating point numbers stored as an array. */

        /*! The bracket operator makes it possible to use this structure like a C++ array.
         * \param[in] n The index into the data array.  Should be 0, 1, or 2.
         * \return The value at the given array index by reference so it can be read or set via the bracket operator.
         */
        public float this[int n] { get { return data[n]; } }

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         * \param[in] f3 The value to set the third floating point number to.
         */
        public Float3(float f1 = 0f, float f2 = 0f, float f3 = 0f)
        {
            data = new float[3];
            data[0] = f1;
            data[1] = f2;
            data[2] = f3;
        }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         * \param[in] f3 The value to set the third floating point number to.
         */
        public void Set(float f1, float f2, float f3)
        {
            data[0] = f1;
            data[1] = f2;
            data[2] = f3;
        }
    }

    /*! Stores a 3 by 3 matrix used for rotation. */
    public struct Matrix33
    {
        /*! The 3x3 identity matrix ant*/
        public static Matrix33 IDENTITY = new Matrix33(
            1.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 1.0f);

        public Float3[,] rows; /*!< The three rows of Float3 structures which hold three floating point numbers each. */

        /*! The bracket operator makes it possible to use this structure like a 3x3 C++ array.
         * \param[in] n The index into the row array.  Should be 0, 1, or 2.
         * \return The Float3 structure for the given row index by reference so it can be read or set via the bracket operator.
         */
        public Float3 this[int n] { get { return rows[n]; } }

        ///*! Copy ructor.   */
        //NIFLIB_API Matrix33(Matrix33& src);

        /*! This ructor can be used to set all values in this matrix during initialization
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m13 The value to set at row 1, column 3.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         * \param[in] m23 The value to set at row 2, column 3.
         * \param[in] m31 The value to set at row 3, column 1.
         * \param[in] m32 The value to set at row 3, column 2.
         * \param[in] m33 The value to set at row 3, column 3.
         */
        public Matrix33(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33
        )
        {
            rows = new float[3, 3];
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33;
        }

        /*! This function can be used to set all values in this matrix at the same time.
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m13 The value to set at row 1, column 3.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         * \param[in] m23 The value to set at row 2, column 3.
         * \param[in] m31 The value to set at row 3, column 1.
         * \param[in] m32 The value to set at row 3, column 2.
         * \param[in] m33 The value to set at row 3, column 3.
         */
        public void Set(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33
        )
        {
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33;
        }

        /*! Returns a quaternion representation of the rotation stored in this matrix. 
         * \return A quaternion with an equivalent rotation to the one stored in this matrix.
         */
        public Quaternion AsQuaternion()
        {
            Quaternion quat;
            float tr, s; float[] q = new float[4];
            int i, j, k;

            int[] nxt = new int[3] { 1, 2, 0 };

            Matrix33 m = this;

            // compute the trace of the matrix
            tr = m[0][0] + m[1][1] + m[2][2];

            // check if the trace is positive or negative
            if (tr > 0.0)
            {
                s = (float)Math.Sqrt(tr + 1.0f);
                quat.w = s / 2.0f;
                s = 0.5f / s;
                quat.x = (m[1][2] - m[2][1]) * s;
                quat.y = (m[2][0] - m[0][2]) * s;
                quat.z = (m[0][1] - m[1][0]) * s;
            }
            else
            {
                // trace is negative
                i = 0;
                if (m[1][1] > m[0][0])
                    i = 1;
                if (m[2][2] > m[i][i])
                    i = 2;
                j = nxt[i];
                k = nxt[j];
                s = (float)Math.Sqrt((m[i][i] - (m[j][j] + m[k][k])) + 1.0f);
                q[i] = s * 0.5f;
                if (s != 0.0f) s = 0.5f / s;
                q[3] = (m[j][k] - m[k][j]) * s;
                q[j] = (m[i][j] + m[j][i]) * s;
                q[k] = (m[i][k] + m[k][i]) * s;
                quat.x = q[0];
                quat.y = q[1];
                quat.z = q[2];
                quat.w = q[3];
            }
            return quat;
        }

        /*! Calculates the determinant of this matrix.
         * \return The determinant of this matrix.
         */
        public float Determinant()
        {
            return (this)[0]
                [0] * ((this)[1]
                [1] * (this)[2]
                [2] - (this)[1]
                [2] * (this)[2]
                [1])
        - (this)[0]
                [1] * ((this)[1]
                [0] * (this)[2]
                [2] - (this)[1]
                [2] * (this)[2]
                [0])
        + (this)[0]
                [2] * ((this)[1]
                [0] * (this)[2]
                [1] - (this)[1]
                [1] * (this)[2]
                [0]);
        }

        //Undocumented
        public void AsFloatArr(out float o[3][3])
        {
            o[0][0] = rows[0][0]; o[0][1] = rows[0][1]; o[0][2] = rows[0][2];
            o[1][0] = rows[1][0]; o[1][1] = rows[1][1]; o[1][2] = rows[1][2];
            o[2][0] = rows[2][0]; o[2][1] = rows[2][1]; o[2][2] = rows[2][2];
        }

        public static Matrix33 operator *(Matrix33 t, Matrix33 m)
        {
            Matrix33 m3 = new Matrix33();
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                {
                   m3[r][c] = t[r]
                   [0] * m[0]
                   [c] + t[r]
                   [1] * m[1]
                   [c] + t[r]
                   [2] * m[2]
                   [c];
                }
            return m3;
        }
    }

    /* Stores four floating point numbers.  Used as a row of a Matrix44. */
    public struct Float4
    {
        public float[] data; /*!< The four floating point numbers stored as an array. */

        /*! The bracket operator makes it possible to use this structure like a C++ array.
         * \param[in] n The index into the data array.  Should be 0, 1, 2, or 3.
         * \return The value at the given array index by reference so it can be read or set via the bracket operator.
         */
        public float this[int n] { get { return data[n]; } }

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         * \param[in] f3 The value to set the third floating point number to.
         * \param[in] f4 The value to set the fourth floating point number to.
         */
        public Float4(float f1 = 0f, float f2 = 0f, float f3 = 0f, float f4 = 0f)
        {
            data = new float[4];
            data[0] = f1;
            data[1] = f2;
            data[2] = f3;
            data[3] = f4;
        }

        /*! This ructor can be used to initialize this Float4 with a Vector4
        * \param[in] v The Vector4 to ruct this one from
        */
        public Float4(Vector4 v) : this(v.x, v.y, v.z, v.w) { }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] f1 The value to set the first floating point number to.
         * \param[in] f2 The value to set the second floating point number to.
         * \param[in] f3 The value to set the third floating point number to.
         * \param[in] f4 The value to set the fourth floating point number to.
         */
        public void Set(float f1, float f2, float f3, float f4)
        {
            data[0] = f1;
            data[1] = f2;
            data[2] = f3;
            data[3] = f4;
        }
    }

    /*! Stores a 4 by 4 matrix used for combined transformations. */
    public struct Matrix44
    {
        /*! The 4x4 identity matrix ant */
        public static Matrix44 IDENTITY = new Matrix44(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);

        public Float4[] rows[]; /*!< The three rows of Float3 structures which hold three floating point numbers each. */

        /*! The bracket operator makes it possible to use this structure like a 4x4 C++ array.
         * \param[in] n The index into the row array.  Should be 0, 1, 2, or 3.
         * \return The Float4 structure for the given row index by reference so it can be read or set via the bracket operator.
         */
        public Float4 this[int n] { get { return rows[n]; } }

        /*! Copy ructor.  Initializes Matrix to another Matrix44.
         * \param[in] m The matrix to initialize this one to. 
         */
        public Matrix44(Matrix44 m) { memcpy(rows, m.rows, sizeof(Float4) * 4); }

        /*! This ructor can be used to set all values in this matrix during initialization
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m13 The value to set at row 1, column 3.
         * \param[in] m14 The value to set at row 1, column 4.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         * \param[in] m23 The value to set at row 2, column 3.
         * \param[in] m24 The value to set at row 2, column 4.
         * \param[in] m31 The value to set at row 3, column 1.
         * \param[in] m32 The value to set at row 3, column 2.
         * \param[in] m33 The value to set at row 3, column 3.
         * \param[in] m34 The value to set at row 3, column 4.
         * \param[in] m41 The value to set at row 4, column 1.
         * \param[in] m42 The value to set at row 4, column 2.
         * \param[in] m43 The value to set at row 4, column 3.
         * \param[in] m44 The value to set at row 4, column 4.
         */
        public Matrix44(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
        )
        {
            rows = new float[4, 4];
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13; rows[0][3] = m14;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23; rows[1][3] = m24;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33; rows[2][3] = m34;
            rows[3][0] = m41; rows[3][1] = m42; rows[3][2] = m43; rows[3][3] = m44;
        }

        /*! This ructor allows a 4x4 transform matrix to be initalized from a
         * translate vector, a 3x3 rotation matrix, and a scale factor.
         * \param[in] translate The translation vector that specifies the new x, y, and z coordinates.
         * \param[in] rotation The 3x3 rotation matrix.
         * \param[in] scale The scale factor.
         */
        public Matrix44(Vector3 translate, Matrix33 rotation, float scale);

        /*! This ructor allows a 4x4 transform matrix to be initalized from a
         * a 3x3 rotation matrix.
         * \param[in] rotation The 3x3 rotation matrix.
         */
        public Matrix44(Matrix33 rotation);

        /*! This function can be used to set all values in this matrix at the same time.
         * \param[in] m11 The value to set at row 1, column 1.
         * \param[in] m12 The value to set at row 1, column 2.
         * \param[in] m13 The value to set at row 1, column 3.
         * \param[in] m14 The value to set at row 1, column 4.
         * \param[in] m21 The value to set at row 2, column 1.
         * \param[in] m22 The value to set at row 2, column 2.
         * \param[in] m23 The value to set at row 2, column 3.
         * \param[in] m24 The value to set at row 2, column 4.
         * \param[in] m31 The value to set at row 3, column 1.
         * \param[in] m32 The value to set at row 3, column 2.
         * \param[in] m33 The value to set at row 3, column 3.
         * \param[in] m34 The value to set at row 3, column 4.
         * \param[in] m41 The value to set at row 4, column 1.
         * \param[in] m42 The value to set at row 4, column 2.
         * \param[in] m43 The value to set at row 4, column 3.
         * \param[in] m44 The value to set at row 4, column 4.
         */
        public void Set(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44
        )
        {
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13; rows[0][3] = m14;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23; rows[1][3] = m24;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33; rows[2][3] = m34;
            rows[3][0] = m41; rows[3][1] = m42; rows[3][2] = m43; rows[3][3] = m44;
        }

        /* Multiplies this matrix by another.
         * \param[in] rh The matrix to multiply this one with.
         * \return The result of the multiplication.
         */
        public static Matrix44 operator *(Matrix44 t, Matrix44 rh);

        /* Multiplies this matrix by another and sets the result to itself.
         * \param[in] rh The matrix to multiply this one with.
         * \return This matrix is returned.
         */
        public static Matrix44 operator*=(Matrix44 t, Matrix44 rh);

        /* Multiplies this matrix by a scalar value.
         * \param[in] rh The scalar value to multiply each component of this matrix by.
         * \return The result of the multiplication.
         */
        public static Matrix44 operator *(Matrix44 t, float rh);

        /* Multiplies this matrix by a scalar value and sets the resutl to itself.
         * \param[in] rh The scalar value to multiply each component of this matrix by.
         * \return This matrix is returned.
         */
        public static Matrix44 operator*=(Matrix44 t, float rh);

        /* Multiplies this matrix by a vector with x, y, and z components.
         * \param[in] rh The vector to multiply this matrix with.
         * \return The result of the multiplication.
         */
        public static Vector3 operator *(Matrix44 t, Vector3 rh);

        /* Adds this matrix to another.
         * \param[in] rh The matrix to be added to this one.
         * \return The result of the addition.
         */
        public static Matrix44 operator +(Matrix44 t, Matrix44 rh);

        /* Adds this matrix to another and sets the result to itself.
         * \param[in] rh The matrix to be added to this one.
         * \return This matrix is returned.
         */
        public static Matrix44 operator+=(Matrix44 t, Matrix44 rh);

        /* Adds this matrix to another.
        * \param[in] rh The matrix to be added to this one.
        * \return The result of the addition.
        */
        public static Matrix44 operator -(Matrix44 t, Matrix44 rh);

        /* Adds this matrix to another and sets the result to itself.
        * \param[in] rh The matrix to be added to this one.
        * \return This matrix is returned.
        */
        public static Matrix44 operator-=(Matrix44 t, Matrix44 rh);

        /* Sets the values of this matrix to those of the given matrix.
         * \param[in] rh The matrix to copy values from.
         * \return This matrix is returned.
         */
        public static Matrix44 operator=(Matrix44 t, Matrix44 rh);

        /* Allows the contents of the matrix to be printed to an ostream.
         * \param[in] lh The ostream to insert the text into.
         * \param[in] rh The matrix to insert into the stream.
         * \return The given ostream is returned.
         */
        internal static ostream operator <<(ostream lh, Matrix44 rh);

        /* Compares two 4x4 matricies.  They are considered equal if all components are equal.
         * \param[in] rh The matrix to compare this one with.
         * \return true if the matricies are equal, false otherwise.
         */
        public static bool operator ==(Matrix44 t, Matrix44 rh);

        /* Compares two 4x4 matricies.  They are considered inequal if any corresponding
         * components are inequal.
         * \param[in] rh The matrix to compare this one with.
         * \return true if the matricies are inequal, false otherwise.
         */
        public static bool operator !=(Matrix44 t, Matrix44 rh);

        /*! Calculates the transpose of this matrix.
         * \return The transpose of this matrix.
         */
        public Matrix44 Transpose();

        /*! Calculates the determinant of this matrix.
         * \return The determinant of this matrix.
         */
        public float Determinant();

        /*! Calculates the inverse of this matrix.
         * \return The inverse of this matrix.
         */
        public Matrix44 Inverse();

        /*! Returns a 3x3 submatrix of this matrix created by skipping the indicated row and column.
         * \param[in] skip_r The row to skip.  Must be a value between 0 and 3.
         * \param[in] skip_c The colum to skip.  Must be a value between 0 and 3.
         * \return The 3x3 submatrix obtained by skipping the indicated row and column.
         */
        public Matrix33 Submatrix(int skip_r, int skip_c);

        /*! Calculates the adjunct of this matrix created by skipping the indicated row and column.
         * \param[in] skip_r The row to skip.  Must be a value between 0 and 3.
         * \param[in] skip_c The colum to skip.  Must be a value between 0 and 3.
         * \return The adjunct obtained by skipping the indicated row and column.
         */
        public float Adjoint(int skip_r, int skip_c);

        public Matrix33 GetRotation();
        public float GetScale();
        public Vector3 GetTranslation();

        //undocumented, may be removed
        public void AsFloatArr(out float o[4][4] )
        {
            o[0][0] = rows[0][0]; o[0][1] = rows[0][1]; o[0][2] = rows[0][2]; o[0][3] = rows[0][3];
            o[1][0] = rows[1][0]; o[1][1] = rows[1][1]; o[1][2] = rows[1][2]; o[1][3] = rows[1][3];
            o[2][0] = rows[2][0]; o[2][1] = rows[2][1]; o[2][2] = rows[2][2]; o[2][3] = rows[2][3];
            o[3][0] = rows[3][0]; o[3][1] = rows[3][1]; o[3][2] = rows[3][2]; o[3][3] = rows[3][3];
        }

        // undocumented
        public void Decompose(Vector3 translate, Matrix33 rotation, float scale);
    }

    /*! Stores a color along with alpha translucency */
    public struct Color3
    {
        public float r; /*!< The red component of this color.  Should be between 0.0f and 1.0f. */
        public float g; /*!< The green component of this color.  Should be between 0.0f and 1.0f. */
        public float b; /*!< The blue component of this color.  Should be between 0.0f and 1.0f. */

        /*! Copy ructor */
        public Color3(Color3 src)
        {
            r = src.r;
            g = src.g;
            b = src.b;
        }

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] r The value to set the red component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] g The value to set the green component of this color to. Should be between 0.0f and 1.0f.
         * \param[in] b The value to set the blue component of this color to.  Should be between 0.0f and 1.0f.
         */
        public Color3(float r = 0f, float g = 0f, float b = 0f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] r The value to set the red component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] g The value to set the green component of this color to. Should be between 0.0f and 1.0f.
         * \param[in] b The value to set the blue component of this color to.  Should be between 0.0f and 1.0f.
         */
        public void Set(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    };

    /*! Stores a color along with alpha translucency */
    public struct Color4
    {
        public float r; /*!< The red component of this color.  Should be between 0.0f and 1.0f. */
        public float g; /*!< The green component of this color.  Should be between 0.0f and 1.0f. */
        public float b; /*!< The blue component of this color.  Should be between 0.0f and 1.0f. */
        public float a; /*!< The alpha translucency component of this color.  Should be between 0.0f and 1.0f. */

        public static Color4 operator +(Color4 t, Color4 rhs)
        {
            t.r += rhs.r;
            t.g += rhs.g;
            t.b += rhs.b;
            t.a += rhs.a;
            return t;
        }

        public static Color4 operator -(Color4 t, Color4 rhs)
        {
            t.r -= rhs.r;
            t.g -= rhs.g;
            t.b -= rhs.b;
            t.a -= rhs.a;
            return t;
        }

        public static Color4 operator +(Color4 t, float rhs)
        {
            t.r += rhs;
            t.g += rhs;
            t.b += rhs;
            t.a += rhs;
            return t;
        }

        public static Color4 operator -(Color4 t, float rhs)
        {
            t.r -= rhs;
            t.g -= rhs;
            t.b -= rhs;
            t.a -= rhs;
            return t;
        }

        public static Color4 operator *(Color4 t, float rhs)
        {
            t.r *= rhs;
            t.g *= rhs;
            t.b *= rhs;
            t.a *= rhs;
            return t;
        }

        public static Color4 operator /(Color4 t, float rhs)
        {
            t.r /= rhs;
            t.g /= rhs;
            t.b /= rhs;
            t.a /= rhs;
            return t;
        }

        /*! Copy ructor */
        public Color4(Color4 src) : this(src.r, src.g, src.b, src.a) { }

        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] r The value to set the red component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] g The value to set the green component of this color to. Should be between 0.0f and 1.0f.
         * \param[in] b The value to set the blue component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] a The value to set the alpha translucency component of this color to.  Should be between 0.0f and 1.0f.
         */
        public Color4(float r = 0f, float g = 0f, float b = 0f, float a = 1f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] r The value to set the red component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] g The value to set the green component of this color to. Should be between 0.0f and 1.0f.
         * \param[in] b The value to set the blue component of this color to.  Should be between 0.0f and 1.0f.
         * \param[in] a The value to set the alpha translucency component of this color to.  Should be between 0.0f and 1.0f.
         */
        public void Set(float r, float g, float b, float a = 1f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static bool operator ==(Color4 t, Color4 n) => t.r == n.r && t.g == n.g && t.b == n.b && t.a == n.a;

        public static bool operator !=(Color4 t, Color4 & n ) => t.r != n.r || t.g != n.g || t.b != n.b || t.a != n.a;
    }

    /*! Represents a quaternion - a 4D extention of complex numbers used as an alternitive to matrices to represent rotation.*/
    public struct Quaternion
    {
        public float w; /*!< The W scalar component of this Quaternion. */
        public float x; /*!< The X vector component of this Quaternion. */
        public float y; /*!< The Y vector component of this Quaternion. */
        public float z; /*!< The Z vector component of this Quaternion. */


        /*! This ructor can be used to set all values in this structure during initialization
         * \param[in] w The value to set the W scalar component of this quaternion to.
         * \param[in] x The value to set the X vector component of this quaternion to.
         * \param[in] y The value to set the Y vector component of this quaternion to.
         * \param[in] z The value to set the Z vector component of this quaternion to.
         */
        public Quaternion(float w = 0f, float x = 0f, float y = 0f, float z = 0f)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /* Dot-product */
        public float Dot(Quaternion rhs) => x * rhs.x + y * rhs.y + z * rhs.z + w * rhs.w;

        /* Multiplication */
        public static Quaternion operator *(Quaternion t, float rhs)
        {
            t.x *= rhs;
            t.y *= rhs;
            t.z *= rhs;
            t.w *= rhs;
            return t;
        }

        /* Addition */
        public static Quaternion operator +(Quaternion t, Quaternion rhs)
        {
            t.x += rhs.x;
            t.y += rhs.y;
            t.z += rhs.z;
            t.w += rhs.w;
            return t;
        }

        /* Equality */
        public static bool operator ==(Quaternion t, Quaternion n) => t.x == n.x && t.y == n.y && t.z == n.z && t.w == n.w;

        /*! This function can be used to set all values in the structure at the same time.
         * \param[in] w The value to set the W scalar component of this quaternion to.
         * \param[in] x The value to set the X vector component of this quaternion to.
         * \param[in] y The value to set the Y vector component of this quaternion to.
         * \param[in] z The value to set the Z vector component of this quaternion to.
         */
        public void Set(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /*! This function returns a 3x3 matrix representation of the rotation stored in this quaternion.
         * \return a Matrix33 structure with an equivalent rotation to this quaternion.
         */
        public Matrix33 AsMatrix();

        /*! This function returns a Euler Angle representation of the rotation stored in this quaternion.
         * The angles returned correspond to yaw, pitch, and roll and are in radiens.
         * \return a Float3 structure with the first value containing the yaw, the second the pitch,
         * and the third the roll.  The values are in radians.
         */
        public Float3 AsEulerYawPitchRoll();

        /*! Calculates the inverse of this quaternion.
        * \return The inverse of this quaternion.
        */
        public Quaternion Inverse();
    }

    /*! Stores a 4 by 3 matrix used for tensors. */
    public struct InertiaMatrix
    {
        /*! The 4x3 identity matrix ant */
        public static InertiaMatrix IDENTITY = new InertiaMatrix(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f);


        Float4[] rows[]; /*!< The three rows of Float3 structures which hold three floating point numbers each. */

        /*! The bracket operator makes it possible to use this structure like a 4x4 C++ array.
        * \param[in] n The index into the row array.  Should be 0, 1, 2, or 3.
        * \return The Float4 structure for the given row index by reference so it can be read or set via the bracket operator.
        */
        public Float4 this[int n] => rows[n];

        /*! Copy ructor.  Initializes Matrix to another InertiaMatrix.
        * \param[in] m The matrix to initialize this one to. 
        */
        public InertiaMatrix(InertiaMatrix m) { memcpy(rows, m.rows, sizeof(Float4) * _countof(rows)); }

        /*! This ructor can be used to set all values in this matrix during initialization
        * \param[in] m11 The value to set at row 1, column 1.
        * \param[in] m12 The value to set at row 1, column 2.
        * \param[in] m13 The value to set at row 1, column 3.
        * \param[in] m14 The value to set at row 1, column 4.
        * \param[in] m21 The value to set at row 2, column 1.
        * \param[in] m22 The value to set at row 2, column 2.
        * \param[in] m23 The value to set at row 2, column 3.
        * \param[in] m24 The value to set at row 2, column 4.
        * \param[in] m31 The value to set at row 3, column 1.
        * \param[in] m32 The value to set at row 3, column 2.
        * \param[in] m33 The value to set at row 3, column 3.
        * \param[in] m34 The value to set at row 3, column 4.
        * \param[in] m41 The value to set at row 4, column 1.
        * \param[in] m42 The value to set at row 4, column 2.
        * \param[in] m43 The value to set at row 4, column 3.
        * \param[in] m44 The value to set at row 4, column 4.
        */
        public InertiaMatrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34
            )
        {
            rows = new float[4, 3];
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13; rows[0][3] = m14;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23; rows[1][3] = m24;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33; rows[2][3] = m34;
        }

        /*! This ructor allows a 4x4 transform matrix to be initalized from a
        * a 3x3 rotation matrix.
        * \param[in] rotation The 3x3 rotation matrix.
        */
        public InertiaMatrix(Matrix33 rotation);

        /*! This function can be used to set all values in this matrix at the same time.
        * \param[in] m11 The value to set at row 1, column 1.
        * \param[in] m12 The value to set at row 1, column 2.
        * \param[in] m13 The value to set at row 1, column 3.
        * \param[in] m14 The value to set at row 1, column 4.
        * \param[in] m21 The value to set at row 2, column 1.
        * \param[in] m22 The value to set at row 2, column 2.
        * \param[in] m23 The value to set at row 2, column 3.
        * \param[in] m24 The value to set at row 2, column 4.
        * \param[in] m31 The value to set at row 3, column 1.
        * \param[in] m32 The value to set at row 3, column 2.
        * \param[in] m33 The value to set at row 3, column 3.
        * \param[in] m34 The value to set at row 3, column 4.
        */
        public void Set(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34
            )
        {
            rows[0][0] = m11; rows[0][1] = m12; rows[0][2] = m13; rows[0][3] = m14;
            rows[1][0] = m21; rows[1][1] = m22; rows[1][2] = m23; rows[1][3] = m24;
            rows[2][0] = m31; rows[2][1] = m32; rows[2][2] = m33; rows[2][3] = m34;
        }

        /* Multiplies this matrix by another.
        * \param[in] rh The matrix to multiply this one with.
        * \return The result of the multiplication.
        */
        public static InertiaMatrix operator *(InertiaMatrix t, InertiaMatrix rh);

        /* Multiplies this matrix by another and sets the result to itself.
        * \param[in] rh The matrix to multiply this one with.
        * \return This matrix is returned.
        */
        public static InertiaMatrix operator*=(InertiaMatrix t, InertiaMatrix rh);

        /* Multiplies this matrix by a scalar value.
        * \param[in] rh The scalar value to multiply each component of this matrix by.
        * \return The result of the multiplication.
        */
        public static InertiaMatrix operator *(InertiaMatrix t, float rh);

        /* Multiplies this matrix by a scalar value and sets the resutl to itself.
        * \param[in] rh The scalar value to multiply each component of this matrix by.
        * \return This matrix is returned.
        */
        public static InertiaMatrix operator*=(InertiaMatrix t, float rh);

        /* Multiplies this matrix by a vector with x, y, and z components.
        * \param[in] rh The vector to multiply this matrix with.
        * \return The result of the multiplication.
        */
        public static Vector3 operator *(InertiaMatrix t, Vector3 rh);

        /* Adds this matrix to another.
        * \param[in] rh The matrix to be added to this one.
        * \return The result of the addition.
        */
        public static InertiaMatrix operator +(InertiaMatrix t, InertiaMatrix rh);

        /* Adds this matrix to another and sets the result to itself.
        * \param[in] rh The matrix to be added to this one.
        * \return This matrix is returned.
        */
        public static InertiaMatrix operator+=(InertiaMatrix t, InertiaMatrix rh);

        /* Sets the values of this matrix to those of the given matrix.
        * \param[in] rh The matrix to copy values from.
        * \return This matrix is returned.
        */
        public static InertiaMatrix operator=(InertiaMatrix t, InertiaMatrix rh);

        /* Allows the contents of the matrix to be printed to an ostream.
        * \param[in] lh The ostream to insert the text into.
        * \param[in] rh The matrix to insert into the stream.
        * \return The given ostream is returned.
        */
        internal static ostream operator <<(ostream lh, InertiaMatrix rh);

        /* Compares two 4x4 matricies.  They are considered equal if all components are equal.
        * \param[in] rh The matrix to compare this one with.
        * \return true if the matricies are equal, false otherwise.
        */
        public static bool operator ==(InertiaMatrix t, InertiaMatrix rh);

        /* Compares two 4x4 matricies.  They are considered inequal if any corresponding
        * components are inequal.
        * \param[in] rh The matrix to compare this one with.
        * \return true if the matricies are inequal, false otherwise.
        */
        public static bool operator !=(InertiaMatrix t, InertiaMatrix rh);

        /*! Calculates the transpose of this matrix.
        * \return The transpose of this matrix.
        */
        public InertiaMatrix Transpose();

        /*! Calculates the determinant of this matrix.
        * \return The determinant of this matrix.
        */
        public float Determinant();

        /*! Calculates the inverse of this matrix.
        * \return The inverse of this matrix.
        */
        public InertiaMatrix Inverse();

        /*! Returns a 3x3 submatrix of this matrix created by skipping the indicated row and column.
        * \param[in] skip_r The row to skip.  Must be a value between 0 and 3.
        * \param[in] skip_c The colum to skip.  Must be a value between 0 and 3.
        * \return The 3x3 submatrix obtained by skipping the indicated row and column.
        */
        public Matrix33 Submatrix(int skip_r, int skip_c);

        /*! Calculates the adjunct of this matrix created by skipping the indicated row and column.
        * \param[in] skip_r The row to skip.  Must be a value between 0 and 3.
        * \param[in] skip_c The colum to skip.  Must be a value between 0 and 3.
        * \return The adjunct obtained by skipping the indicated row and column.
        */
        public float Adjoint(int skip_r, int skip_c);
    }


























































    Matrix44::Matrix44(Matrix33 & r ) {
	//Set this matrix with rotate and translate information
	Matrix44 & m = * this;
	m[0][0] = r[0][0];  m[0][1] = r[0][1];  m[0][2] = r[0][2];  m[0][3] = 0.0f;

    m[1][0] = r[1][0];  m[1][1] = r[1][1];  m[1][2] = r[1][2];  m[1][3] = 0.0f;

    m[2][0] = r[2][0];  m[2][1] = r[2][1];  m[2][2] = r[2][2];  m[2][3] = 0.0f;

    m[3][0] = 0.0f;     m[3][1] = 0.0f;     m[3][2] = 0.0f;     m[3][3] = 1.0f;
}

    Matrix44::Matrix44(Vector3 & t,  Matrix33 & r, float scale ) {
	//Set up a matrix with rotate and translate information
	Matrix44 rt;
    rt[0][0] = r[0][0];	rt[0][1] = r[0][1];	rt[0][2] = r[0][2];	rt[0][3] = 0.0f;
	rt[1][0] = r[1][0];	rt[1][1] = r[1][1];	rt[1][2] = r[1][2];	rt[1][3] = 0.0f;
	rt[2][0] = r[2][0];	rt[2][1] = r[2][1];	rt[2][2] = r[2][2];	rt[2][3] = 0.0f;
	rt[3][0] = t.x;     rt[3][1] = t.y;     rt[3][2] = t.z;     rt[3][3] = 1.0f;

	//Set up another matrix with the scale information
	Matrix44 s;
    s[0][0] = scale;	s[0][1] = 0.0f;		s[0][2] = 0.0f;		s[0][3] = 0.0f;
	s[1][0] = 0.0f;		s[1][1] = scale;	s[1][2] = 0.0f;		s[1][3] = 0.0f;
	s[2][0] = 0.0f;		s[2][1] = 0.0f;		s[2][2] = scale;	s[2][3] = 0.0f;
	s[3][0] = 0.0f;		s[3][1] = 0.0f;		s[3][2] = 0.0f;		s[3][3] = 1.0f;

    //Multiply the two for the combined transform
    * this = s* rt;
}

    Matrix33 Matrix44::GetRotation()
    {
        Matrix44 & t = *this;

        Matrix33 m(t[0][0], t[0][1], t[0][2],
                    t[1][0], t[1][1], t[1][2],
                    t[2][0], t[2][1], t[2][2]


                   );

        //--Extract Scale from first 3 rows--//
        float scale[3];
        for (int r = 0; r < 3; ++r)
        {
            //Get scale for this row
            scale[r] = Vector3(m[r][0], m[r][1], m[r][2]).Magnitude();

            //Normalize the row by dividing each factor by scale
            m[r][0] /= scale[r];
            m[r][1] /= scale[r];
            m[r][2] /= scale[r];
        }

        //Return result
        return m;
    }

    float Matrix44::GetScale()
    {
        Matrix44 & m = *this;
        float scale[3];
        for (int r = 0; r < 3; ++r)
        {
            //Get scale for this row
            scale[r] = Vector3(m[r][0], m[r][1], m[r][2]).Magnitude();
        }

        //averate the scale since NIF doesn't support discreet scaling
        return (scale[0] + scale[1] + scale[2]) / 3.0f;
    }


    Vector3 Matrix44::GetTranslation()
    {
        Matrix44 & m = *this;
        return Vector3(m[3][0], m[3][1], m[3][2]);
    }

    Matrix44 Matrix44::operator*(Matrix44 & rh )
    {
        return Matrix44(*this) *= rh;
    }
    Matrix44 & Matrix44::operator*=(Matrix44 & rh )
    {
        Matrix44 r;
        Matrix44 & lh = *this;
        float t;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                t = 0.0f;
                for (int k = 0; k < 4; k++)
                {
                    t += lh[i][k] * rh[k][j];
                }
                r[i][j] = t;
            }
        }

        *this = r;
        return *this;
    }

    Matrix44 Matrix44::operator*(float rh )  {
        return Matrix44(*this) *= rh;
    }

    Matrix44 & Matrix44::operator*=(float rh)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                (*this)[i][j] *= rh;
            }
        }
        return *this;
    }

    Vector3 Matrix44::operator*(Vector3 & rh )
    {
        Matrix44 & t = *this;
        Vector3 v;
        //Multiply, ignoring w
        v.x = rh.x * t[0][0] + rh.y * t[1][0] + rh.z * t[2][0] + t[3][0];

        v.y = rh.x * t[0][1] + rh.y * t[1][1] + rh.z * t[2][1] + t[3][1];

        v.z = rh.x * t[0][2] + rh.y * t[1][2] + rh.z * t[2][2] + t[3][2];
        //answer[3] = rh[0] * t(0,3) + rh[1] * t(1,3) + rh[2] * t(2,3) + t(3,3);

        return v;
    }

    Matrix44 Matrix44::operator+(Matrix44 & rh )
    {
        return Matrix44(*this) += rh;
    }

    Matrix44 & Matrix44::operator+=(Matrix44 & rh )
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                (*this)[i][j] += rh[i][j];
            }
        }
        return *this;
    }

    Matrix44 Matrix44::operator-(Matrix44 & rh )
    {
        return Matrix44(*this) -= rh;
    }

    Matrix44 & Matrix44::operator-=(Matrix44 & rh )
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                (*this)[i][j] -= rh[i][j];
            }
        }
        return *this;
    }

    Matrix44 & Matrix44::operator=(Matrix44 & rh )
    {
        memcpy(rows, rh.rows, sizeof(Float4) * 4);
        return *this;
    }

    bool Matrix44::operator==(Matrix44 & rh )
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((*this)[i]
    [j] != rh[i]
    [j])
                    return false;
            }
        }
        return true;
    }

    bool Matrix44::operator!=(Matrix44 & rh )
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((*this)[i]
    [j] != rh[i]
    [j])
                    return true;
            }
        }
        return false;
    }

    Matrix44 Matrix44::Transpose()
    {
        Matrix44 & t = *this;
        return Matrix44(t[0][0], t[1][0], t[2][0], t[3][0],
                         t[0][1], t[1][1], t[2][1], t[3][1],
                         t[0][2], t[1][2], t[2][2], t[3][2],
                         t[0][3], t[1][3], t[2][3], t[3][3]);
    }

    Matrix33 Matrix44::Submatrix(int skip_r, int skip_c)
    {
        Matrix33 sub;
        int i = 0, j = 0;
        for (int r = 0; r < 4; r++)
        {
            if (r == skip_r)
                continue;
            for (int c = 0; c < 4; c++)
            {
                if (c == skip_c)
                    continue;
                sub[i][j] = (*this)[r]
    [c];
                j++;
            }
            i++;
            j = 0;
        }
        return sub;
    }

    float Matrix44::Adjoint(int skip_r, int skip_c)
    {
        Matrix33 sub = Submatrix(skip_r, skip_c);
        return pow(-1.0f, float(skip_r + skip_c)) * sub.Determinant();
    }

    Matrix44 Matrix44::Inverse()
    {
        Matrix44 result;

        float det = Determinant();
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                result[c][r] = Adjoint(r, c) / det;
            }
        }

        return result;
    }

    float Matrix44::Determinant()
    {
        Matrix44 & t = *this;
        return t[0][0] * Submatrix(0, 0).Determinant()
              - t[0]
    [1] * Submatrix(0, 1).Determinant()
              + t[0]
    [2] * Submatrix(0, 2).Determinant()
              - t[0]
    [3] * Submatrix(0, 3).Determinant();
    }

    void Matrix44::Decompose(Vector3 & translate, Matrix33 & rotation, float & scale )
    {
        translate = Vector3((*this)[3][0], (*this)[3]
     [1], (*this)[3]
     [2]);
        Matrix33 rotT;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                rotation[i][j] = (*this)[i][j];
                rotT[j][i] = (*this)[i][j];
            }
        }
        Matrix33 mtx = rotation * rotT;
        Float3 scale3(sqrt(mtx[0]
        [0]), sqrt(mtx[1]
        [1]), sqrt(mtx[2]
        [2]) );
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                rotation[i][j] /= scale3[i];
            }
        }

        //averate the scale since NIF doesn't support discreet scaling
        scale = (scale3[0] + scale3[1] + scale3[2]) / 3.0f;
    }
    /*
     * Quaternion Methods
     */

    Float3 Quaternion::AsEulerYawPitchRoll()
    {
        float yaw, pitch, roll;

        if (x * y + z * w == 0.5)
        {
            //North Pole
            yaw = 2 * atan2(x, w);
            pitch = asin(2 * x * y + 2 * z * w);
            roll = 0.0f;
        }
        else if (x * y + z * w == -0.5)
        {
            //South Pole
            yaw = -2 * atan2(x, w);
            pitch = asin(2 * x * y + 2 * z * w);
            roll = 0.0f;
        }
        else
        {
            yaw = atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            pitch = asin(2 * x * y + 2 * z * w);
            roll = atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        }

        return Float3(yaw, pitch, roll);
    }

    Matrix33 Quaternion::AsMatrix()
    {
        Matrix33 m;

        float w2 = w * w;
        float x2 = x * x;
        float y2 = y * y;
        float z2 = z * z;

        m[0][0] = w2 + x2 - y2 - z2;
        m[0][1] = 2.0f * x * y - 2.0f * w * z;
        m[0][2] = 2.0f * x * z + 2.0f * w * y;

        m[1][0] = 2.0f * x * y + 2.0f * w * z;
        m[1][1] = w2 - x2 + y * y - z2;
        m[1][2] = 2.0f * y * z - 2.0f * w * x;

        m[2][0] = 2.0f * x * z - 2.0f * w * y;
        m[2][1] = 2.0f * y * z + 2.0f * w * x;
        m[2][2] = w2 - x2 - y2 + z2;

        return m;
    }

    Quaternion Quaternion::Inverse()
    {
        return Quaternion(w, -x, -y, -z);
    }


    /*
    * InertiaMatrix Methods
    */

    InertiaMatrix::InertiaMatrix() {

    * this = InertiaMatrix::IDENTITY;
}

    InertiaMatrix::InertiaMatrix(Matrix33 & r ) {
	//Set this matrix with rotate and translate information
	InertiaMatrix & m = * this;
	m[0][0] = r[0][0];  m[0][1] = r[0][1];  m[0][2] = r[0][2];  m[0][3] = 0.0f;

    m[1][0] = r[1][0];  m[1][1] = r[1][1];  m[1][2] = r[1][2];  m[1][3] = 0.0f;

    m[2][0] = r[2][0];  m[2][1] = r[2][1];  m[2][2] = r[2][2];  m[2][3] = 0.0f;
}

    InertiaMatrix InertiaMatrix::operator*(InertiaMatrix & rh )
    {
        return InertiaMatrix(*this) *= rh;
    }

    InertiaMatrix & InertiaMatrix::operator*=(InertiaMatrix & rh )
    {
        InertiaMatrix r;
        InertiaMatrix & lh = *this;
        float t;
        for (int i = 0; i < _countof(rows); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                t = 0.0f;
                for (int k = 0; k < _countof(rows); k++)
                {
                    t += lh[i][k] * rh[k][j];
                }
                r[i][j] = t;
            }
        }

        *this = r;
        return *this;
    }

    InertiaMatrix InertiaMatrix::operator*(float rh )  {
        return InertiaMatrix(*this) *= rh;
    }

    InertiaMatrix & InertiaMatrix::operator*=(float rh)
    {
        for (int i = 0; i < _countof(rows); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                (*this)[i][j] *= rh;
            }
        }
        return *this;
    }

    Vector3 InertiaMatrix::operator*(Vector3 & rh )
    {
        InertiaMatrix & t = *this;
        Vector3 v;
        //Multiply, ignoring w
        v.x = rh.x * t[0][0] + rh.y * t[1][0] + rh.z * t[2][0];

        v.y = rh.x * t[0][1] + rh.y * t[1][1] + rh.z * t[2][1];

        v.z = rh.x * t[0][2] + rh.y * t[1][2] + rh.z * t[2][2];
        return v;
    }

    InertiaMatrix InertiaMatrix::operator+(InertiaMatrix & rh )
    {
        return InertiaMatrix(*this) += rh;
    }

    InertiaMatrix & InertiaMatrix::operator+=(InertiaMatrix & rh )
    {
        for (int i = 0; i < _countof(rows); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                (*this)[i][j] += rh[i][j];
            }
        }
        return *this;
    }

    InertiaMatrix & InertiaMatrix::operator=(InertiaMatrix & rh )
    {
        memcpy(rows, rh.rows, sizeof(Float4) * _countof(rows));
        return *this;
    }

    bool InertiaMatrix::operator==(InertiaMatrix & rh )
    {
        for (int i = 0; i < _countof(rows); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((*this)[i]
    [j] != rh[i]
    [j])
                    return false;
            }
        }
        return true;
    }

    bool InertiaMatrix::operator!=(InertiaMatrix & rh )
    {
        for (int i = 0; i < _countof(rows); i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if ((*this)[i]
    [j] != rh[i]
    [j])
                    return true;
            }
        }
        return false;
    }

    InertiaMatrix InertiaMatrix::Transpose()
    {
        InertiaMatrix & t = *this;
        return InertiaMatrix(t[0][0], t[1][0], t[2][0], t[3][0],
            t[0][1], t[1][1], t[2][1], t[3][1],
            t[0][2], t[1][2], t[2][2], t[3][2]);
    }

    Matrix33 InertiaMatrix::Submatrix(int skip_r, int skip_c)
    {
        Matrix33 sub;
        int i = 0, j = 0;
        for (int r = 0; r < _countof(rows); r++)
        {
            if (r == skip_r)
                continue;
            for (int c = 0; c < 4; c++)
            {
                if (c == skip_c)
                    continue;
                sub[i][j] = (*this)[r]
    [c];
                j++;
            }
            i++;
            j = 0;
        }
        return sub;
    }

    float InertiaMatrix::Adjoint(int skip_r, int skip_c)
    {
        Matrix33 sub = Submatrix(skip_r, skip_c);
        return pow(-1.0f, float(skip_r + skip_c)) * sub.Determinant();
    }

    InertiaMatrix InertiaMatrix::Inverse()
    {
        InertiaMatrix result;

        float det = Determinant();
        for (int r = 0; r < _countof(rows); r++)
        {
            for (int c = 0; c < 4; c++)
            {
                result[c][r] = Adjoint(r, c) / det;
            }
        }

        return result;
    }

    float InertiaMatrix::Determinant()
    {
        InertiaMatrix & t = *this;
        return t[0][0] * Submatrix(0, 0).Determinant()
            - t[0]
    [1] * Submatrix(0, 1).Determinant()
            + t[0]
    [2] * Submatrix(0, 2).Determinant()
            - t[0]
    [3] * Submatrix(0, 3).Determinant();
    }

    /*
     * ostream functions for printing with cout
     */
    namespace Niflib
    {
ostream & operator <<(ostream & out, TexCoord  & val )
        {
            return out << "(" << setw(6) << val.u << "," << setw(6) << val.v << ")";
        }

        ostream & operator <<(ostream & out, Triangle  & val )
        {
            return out << "{" << setw(6) << val.v1 << "," << setw(6) << val.v2 << "," << setw(6) << val.v3 << "}";
        }

        ostream & operator <<(ostream & out, Vector3  & val )
        {
            return out << "(" << setw(6) << val.x << "," << setw(6) << val.y << "," << setw(6) << val.z << ")";
        }

        ostream & operator <<(ostream & out, Float2  & val )
        {
            return out << "{" << setw(6) << val.data[0] << "," << setw(6) << val.data[1] << "}";
        }

        ostream & operator <<(ostream & out, Matrix22  & val )
        {
	out << endl
        << "   |" << setw(6) << val[0][0] << "," << setw(6) << val[0][1] << " |" << endl
        << "   |" << setw(6) << val[1][0] << "," << setw(6) << val[1][1] << " |";
            return out;
        }

        ostream & operator <<(ostream & out, Float3  & val )
        {
            return out << "{" << setw(6) << val.data[0] << "," << setw(6) << val.data[1] << "," << setw(6) << val.data[2] << "}";
        }

        ostream & operator <<(ostream & out, Matrix33  & val )
        {
	out << endl
        << "   |" << setw(6) << val[0][0] << "," << setw(6) << val[0][1] << "," << setw(6) << val[0][2] << " |" << endl
        << "   |" << setw(6) << val[1][0] << "," << setw(6) << val[1][1] << "," << setw(6) << val[1][2] << " |" << endl
        << "   |" << setw(6) << val[2][0] << "," << setw(6) << val[2][1] << "," << setw(6) << val[2][2] << " |";
            return out;
        }

        ostream & operator <<(ostream & out, Float4  & val )
        {
            return out << "{" << setw(6) << val.data[0] << "," << setw(6) << val.data[1] << "," << setw(6) << val.data[2] << "," << setw(6) << val.data[3] << "}";
        }

        ostream & operator <<(ostream & out, Matrix44  & val )
        {
	out << endl
        << "   |" << setw(6) << val[0][0] << "," << setw(6) << val[0][1] << "," << setw(6) << val[0][2] << "," << setw(6) << val[0][3] << " |" << endl
        << "   |" << setw(6) << val[1][0] << "," << setw(6) << val[1][1] << "," << setw(6) << val[1][2] << "," << setw(6) << val[1][3] << " |" << endl
        << "   |" << setw(6) << val[2][0] << "," << setw(6) << val[2][1] << "," << setw(6) << val[2][2] << "," << setw(6) << val[2][3] << " |" << endl
        << "   |" << setw(6) << val[3][0] << "," << setw(6) << val[3][1] << "," << setw(6) << val[3][2] << "," << setw(6) << val[3][3] << " |";
            return out;
        }

        ostream & operator <<(ostream & out, Color3  & val )
        {
            return out << "{R:" << setw(6) << val.r << " G:" << setw(6) << val.g << " B:" << setw(6) << val.b << "}";
        }

        ostream & operator <<(ostream & out, Color4  & val )
        {
            return out << "{R:" << setw(6) << val.r << " G:" << setw(6) << val.g << " B:" << setw(6) << val.b << " A:" << setw(6) << val.a << "}";
        }

        ostream & operator <<(ostream & out, Quaternion  & val )
        {
            return out << "[" << setw(6) << val.w << ",(" << setw(6) << val.x << "," << setw(6) << val.y << "," << setw(6) << val.z << ")]";
        }

        ostream & operator <<(ostream & out, InertiaMatrix  & val )
        {
	out << endl
        << "   |" << setw(6) << val[0][0] << "," << setw(6) << val[0][1] << "," << setw(6) << val[0][2] << "," << setw(6) << val[0][3] << " |" << endl
        << "   |" << setw(6) << val[1][0] << "," << setw(6) << val[1][1] << "," << setw(6) << val[1][2] << "," << setw(6) << val[1][3] << " |" << endl
        << "   |" << setw(6) << val[2][0] << "," << setw(6) << val[2][1] << "," << setw(6) << val[2][2] << "," << setw(6) << val[2][3] << " |";
            return out;
        }

    }
}