/*
 * RawDataTableReader 
 * Cosmosliu: 2011-10-9
 * define basic data of a table
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class RawTable
{
    static List<ulong> s_hashTable;

    static List<ulong> HashTable
    {
        get
        {
            if (s_hashTable == null)
            {
                s_hashTable = new List<ulong>();
                TextAsset text = null;
// #if UNITY_EDITOR
//                 if (Constants.USE_ASSETBUNDLE)
//                 {
//                     text = ResourceLibrary.instance.Load(AssetBundleManager.EBundleType.eDat, "hash") as TextAsset;
//                 }
//                 else
//                 {
//                     text = (TextAsset)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/dat/hash.txt", typeof(TextAsset));
//                 }
// #else
//                 text = ResourceLibrary.instance.Load(AssetBundleManager.EBundleType.eDat, "hash") as TextAsset;
// #endif
                string[] hashList = text.text.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string hash in hashList)
                {
                    ulong uint_hash = ulong.Parse(hash);
                    s_hashTable.Add(~uint_hash);
                }

            }
            return s_hashTable;
        }
    }
    public static ulong GetHash(int index)
    {
        return ~s_hashTable[index];
    }

	public string[,]		_data;
	public int		   		_nRows;
	public int		   		_nColumns;

	//read binary data
	public void readBinary(string tableName)
	{
		ClearData();
		
		TextAsset binaryStream = null;
		
// #if UNITY_EDITOR
// 		if (Constants.USE_ASSETBUNDLE)
// 		{
// 			binaryStream = ResourceLibrary.instance.Load(AssetBundleManager.EBundleType.eDat, tableName) as TextAsset;	
// 		}
// 		else
// 		{
// 			binaryStream = (TextAsset)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Resources/dat/"+tableName+".bytes", typeof(TextAsset));
// 		}
// #else
// 		binaryStream = ResourceLibrary.instance.Load(AssetBundleManager.EBundleType.eDat, tableName) as TextAsset;
// #endif		
		
		if (binaryStream == null)
		{
			Debug.Log("Error reading table:" + tableName);
			return;
		}
        if (!HashTable.Contains(~ComputeHash(binaryStream.bytes)))
        {
            Application.Quit();
        }

        byte[] bytes = TEA.decode(binaryStream.bytes);

        MemoryStream ms = new MemoryStream(bytes);
        BinaryReader br = new BinaryReader(ms, Encoding.Unicode);
		
		int columns = 0, rows = 0;
        rows = br.ReadInt32();        
		columns = br.ReadInt32();   
		
		_nRows = rows;
		_nColumns = columns;
		
		if(_nRows == 0 || _nColumns==0)
			Debug.Log("Error reading tablesize Rows is "+_nRows.ToString()+" and _nColumns is "+_nColumns.ToString()+".");
        _data = new string[_nRows, _nColumns];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				_data[i,j] = br.ReadString();
			}
		}
	}	
	
	public string GetStr(int row, int column)
	{
		if (row < _nRows && column < _nColumns)
			return _data[row,column];
		else
		{
			Debug.Log("Error reading Row: "+row.ToString()+" Columns: "+column.ToString()+".");			
			return string.Empty;
		}
	}
	
	public int GetInt(int row, int column)
	{
		string result = GetStr(row, column);
		if (result != string.Empty)
		{
			return int.Parse(result);
		}
		else
		{
			Debug.Log("Error when try int.parse in Row: "+row.ToString()+" Columns: "+column.ToString()+".");	
			return 0;
		}
	}
	
	public short GetShort(int row, int column)
	{
		string result = GetStr(row, column);
		if (result != string.Empty)
		{
			return short.Parse(result);
		}
		else
		{
			Debug.Log("Error when try short.parse in Row: "+row.ToString()+" Columns: "+column.ToString()+".");	
			return 0;
		}
	}
	
	public byte GetByte(int row, int column)
	{
		string result = GetStr(row, column);
		if (result != string.Empty)
		{
			return byte.Parse(result);
		}
		else
		{
			Debug.Log("Error when try byte.parse in Row: "+row.ToString()+" Columns: "+column.ToString()+".");	
			return 0;
		}
	}
	
	public float GetFloat(int row, int column)
	{
		string result = GetStr(row, column);
		if (result != string.Empty)
		{
			return float.Parse(result);
		}
		else
		{
			Debug.Log("Error when try float.parse in Row: "+row.ToString()+" Columns: "+column.ToString()+".");	
			return 0;
		}
	}
	
	public void ClearData()
	{
		_data = null;
	}

    ulong ComputeHash(byte[] s)
    {
        ulong hash = 0x9A9AA99A;
        for (int i = 0; i < s.Length; i++)
        {
            if ((i & 1) == 0)
            {
                hash ^= ((hash << 7) ^ s[i] ^ (hash >> 3));
            }
            else
            {
                hash ^= (~((hash << 11) ^ s[i] ^ (hash >> 5)));
            }
        }
        return hash;
    }
}

class TEA
{
    readonly static uint[] KP =
		{
			0x243F6A88, 0x95A308D3, 0x13198A2E, 0x03707344,
			0xA4093832, 0x299F31D0, 0x082EFA98, 0xEC7E6C89,
			0x452821E6, 0x38D01377, 0xBE5466CF, 0x34E90C6D,
			0xC0AC29B7, 0xC97A50DD, 0x3F84D4B5, 0xB5470917,
			0x9216D5D9, 0x7979FB1B
		};

    static void code(uint y, uint z, uint[] k, out uint o1, out uint o2)
    {
        uint sum = 0;
        uint delta = 0x9e3779b9;
        uint n = 32;

        while (n-- > 0)
        {
            y += (z << 4 ^ z >> 5) + z ^ sum + k[(sum & 3) % k.Length];
            sum += delta;
            z += (y << 4 ^ y >> 5) + y ^ sum + k[(sum >> 11 & 3) % k.Length];
        }

        o1 = y;
        o2 = z;
    }
    static void decode(uint y, uint z, uint[] k, out uint o1, out uint o2)
    {
        uint n = 32;
        uint sum;
        uint delta = 0x9e3779b9;

        sum = delta << 5;

        while (n-- > 0)
        {
            z -= (y << 4 ^ y >> 5) + y ^ sum + k[(sum >> 11 & 3) % k.Length];
            sum -= delta;
            y -= (z << 4 ^ z >> 5) + z ^ sum + k[(sum & 3) % k.Length];
        }

        o1 = y;
        o2 = z;
    }

    public static byte[] code(byte[] input)
    {
        byte[] output = new byte[8 * (1 + (input.Length - 1) / 8)];
        for (int i = 0; i < input.Length; i += 8)
        {
            uint y = byteToUint(input, i);
            uint z = byteToUint(input, i + 4);
            uint o1, o2;
            code(y, z, KP, out o1, out o2);
            uintToByte(o1, output, i);
            uintToByte(o2, output, i + 4);
        }
        return output;
    }
    public static byte[] decode(byte[] input)
    {
        byte[] output = new byte[input.Length];
        for (int i = 0; i < input.Length; i += 8)
        {
            uint y = byteToUint(input, i);
            uint z = byteToUint(input, i + 4);
            uint o1, o2;
            decode(y, z, KP, out o1, out o2);
            uintToByte(o1, output, i);
            uintToByte(o2, output, i + 4);
        }
        return output;
    }

    static uint byteToUint(byte[] input, int index)
    {
        uint output = 0;
        if (index < input.Length)
            output += ((uint)input[index++]);
        if (index < input.Length)
            output += ((uint)input[index++] << 8);
        if (index < input.Length)
            output += ((uint)input[index++] << 16);
        if (index < input.Length)
            output += ((uint)input[index++] << 24);
        return output;
    }

    static void uintToByte(uint input, byte[] output, int index)
    {
        if (index < output.Length)
            output[index++] = ((byte)((input & 0xFF)));
        if (index < output.Length)
            output[index++] = ((byte)((input >> 8) & 0xFF));
        if (index < output.Length)
            output[index++] = ((byte)((input >> 16) & 0xFF));
        if (index < output.Length)
            output[index++] = ((byte)((input >> 24) & 0xFF));
    }
}