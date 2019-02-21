using System;
using UnityEngine;
public class MessageBuffer
{
    private byte[] buffer;
    private int position, maxSize;
    public int Size { get; private set; }
    const int minSize = 128;
    public byte[] Bytes { get { return buffer; } }
    public MessageBuffer(int startingSize)
    {
        if(startingSize < minSize)
        {
            startingSize = minSize;
        }
        maxSize = startingSize;
        buffer = new byte[maxSize];
        position = 0;
        Size = 0;
    }

    public MessageBuffer()
    {
        maxSize = minSize;
        buffer = new byte[maxSize];
        position = 0;
        Size = 0;
    }

    public MessageBuffer(byte[] input, int writtenBytes)
    {
        buffer = input;
        position = 0;
        Size = writtenBytes;
        maxSize = input.Length;
    }

    private void Resize(int newSize)
    {
        System.Diagnostics.Debug.Assert(position <= newSize);
        if(newSize < minSize)
        {
            newSize = minSize;
        }
        maxSize = newSize;
        byte[] temp = new byte[maxSize];
        Array.Copy(buffer, 0, temp, 0, position);
        buffer = temp;
    }
    
    public void Reset()
    {
        position = 0;
    }

    public void Clear()
    {
        position = 0;
        Size = 0;
    }

    public bool HasUnreadData()
    {
        return position < Size;
    }

    public void Write(byte b)
    {
        if(maxSize < position + 1)
        {
            Resize(maxSize * 2);
        }
        buffer[position++] = b;
        if (position > Size)
        {
            Size = position;
        }
    }


    public void Write(short s)
    {
        if(maxSize < position + 2)
        {
            Resize(maxSize * 2);
        }
        Array.Copy(BitConverter.GetBytes(s), 0, buffer, position, 2);
        position += 2;
        if(position > Size)
        {
            Size = position;
        }
    }

    public void Write(int i)
    {
        if (maxSize < position + 4)
        {
            Resize(maxSize * 2);
        }
        Array.Copy(BitConverter.GetBytes(i), 0, buffer, position, 4);
        position += 4;
        if (position > Size)
        {
            Size = position;
        }
    }

    public void Write(float f)
    {
        if (maxSize < position + 4)
        {
            Resize(maxSize * 2);
        }
        Array.Copy(BitConverter.GetBytes(f), 0, buffer, position, 4);
        position += 4;
        if (position > Size)
        {
            Size = position;
        }
    }

    public void Write(double d)
    {
        if (maxSize < position + 8)
        {
            Resize(maxSize * 2);
        }
        Array.Copy(BitConverter.GetBytes(d), 0, buffer, position, 8);
        position += 8;
        if (position > Size)
        {
            Size = position;
        }
    }

    public void Write(Vector2 v)
    {
        Write(v.x);
        Write(v.y);
    }

    public void Write(Vector3 v)
    {
        Write(v.x);
        Write(v.y);
        Write(v.z);
    }

    public void Write(Vector4 v)
    {
        Write(v.x);
        Write(v.y);
        Write(v.z);
        Write(v.w);
    }

    public byte ReadByte()
    {
        if(position + 1 <= maxSize)
        {
            return buffer[position++];
        }
        else
        {
            return buffer[maxSize - 1];
        }

    }

    public short ReadInt16()
    {
        short value = 0;
        if(position + 2 <= maxSize)
        {
            value = BitConverter.ToInt16(buffer, position);
            position += 2;
        }
        return value;
    }

    public int ReadInt()
    {
        int value = 0;
        if(position + 4 <= maxSize)
        {
            value = BitConverter.ToInt32(buffer, position);
            position += 4;
        }
        return value;
    }

    public float ReadFloat32()
    {
        float value = 0;
        if(position + 4 <= maxSize)
        {
            value = BitConverter.ToSingle(buffer, position);
            position += 4;
        }
        return value;
    }

    public double ReadFloat64()
    {
        double value = 0;
        if (position + 8 <= maxSize)
        {
            value = BitConverter.ToDouble(buffer, position);
            position += 8;
        }
        return value;
    }

    public Vector2 ReadVector2()
    {
        return new Vector2(ReadFloat32(), ReadFloat32());
    }

    public Vector3 ReadVector3()
    {
        return new Vector3(ReadFloat32(), ReadFloat32(), ReadFloat32());
    }

    public Vector4 ReadVector4()
    {
        return new Vector4(ReadFloat32(), ReadFloat32(), ReadFloat32(), ReadFloat32());
    }
}
