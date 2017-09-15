using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text;

namespace ClientExcelExporter
{
    class SingleExcelExport
    {
        public static bool DEBUG = false;
        public static DataTable dtData;

        public static string[] Export(string exlFilePath, string sheetName, string outBinPath,
            int colStart, int colEnd, int rowStart, int rowEnd)
        {
            if (DEBUG)
            {
                Console.WriteLine("rawFilePath is " + exlFilePath);
                Console.WriteLine("sheetName  is " + sheetName);
                Console.WriteLine("outBinPath  is " + outBinPath);
                Console.WriteLine("setting is " + colStart.ToString() + "," + colEnd.ToString() + "," + rowStart.ToString() + "," + rowEnd.ToString());
            }

            try
            {
                ReadXLS(exlFilePath + ".xls", sheetName);

                if (dtData.Columns.Count > 0 && dtData.Rows.Count > 0)
                {
                    string[] returnValue = ExportData(outBinPath + ".bytes", colStart, colEnd, rowStart, rowEnd);
                    Encrypt(outBinPath + ".bytes");
                    return returnValue;
                }
                else
                    return null;
            }
            catch (Exception exception)
            {
                //if (DEBUG)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                }
                return null;
            }
        }

        public static void ReadXLS(string filetoread, string sheetName)
        {
            // Must be saved as excel 2003 workbook, not 2007, mono issue really
            string con = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq=" + filetoread + ";";

            if (DEBUG)
                Console.WriteLine(con);

            string dataQuery = "SELECT * FROM [" + sheetName + "$]";
            // odbc connector 
            OdbcConnection oCon = new OdbcConnection(con);
            // command object 
            OdbcCommand oCmd = new OdbcCommand(dataQuery, oCon);
            // table to hold the data 
            dtData = new DataTable("Data");
            // open the connection 
            oCon.Open();
            // datareader to fill that table
            OdbcDataReader rData = oCmd.ExecuteReader();
            // load data 
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dtData);
            dataSet.EnforceConstraints = false;
            dtData.Load(rData);
            // close that reader
            rData.Close();
            // close connection
            oCon.Close();
        }

        public static string[] GetColumnHeader()
        {
            string[] result = new string[dtData.Columns.Count - 1];
            for (int i = 0; i < dtData.Columns.Count - 1; i++)
            {
                result[i] = dtData.Columns[i + 1].ColumnName;
            }
            return result;
        }

        public static string[] GetColumnHeaderAll()
        {
            string[] result = new string[dtData.Columns.Count];
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                result[i] = dtData.Columns[i].ColumnName;
            }
            return result;
        }

        public static string[] GetRowHeader()
        {
            string[] result = new string[dtData.Rows.Count];
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                result[i] = dtData.Rows[i][0].ToString();
            }
            return result;
        }

        public static string[] GetColumnData(string columnName)
        {
            int columnIndex = 1;
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                if (columnName.Equals(dtData.Columns[i].ColumnName))
                {
                    columnIndex = i;
                    break;
                }
            }

            string[] result = new string[dtData.Rows.Count];
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                result[i] = dtData.Rows[i][columnIndex].ToString();
            }
            return result;
        }

        public static string[] ExportData(string filetowrite, int colStart, int colEnd, int rowStart, int rowEnd)
        {
            if ((colStart > colEnd) || (colStart == 0 && colEnd == 0))
            {
                colStart = 0;
                colEnd = dtData.Columns.Count - 1;
            }
            if ((rowStart > rowEnd) || (rowStart == 0 && rowEnd == 0))
            {
                rowStart = 0;
                rowEnd = dtData.Rows.Count - 1;
            }

            int actualRowEnd = 0;
            int actualColumnEnd = 0;
            for (int i = rowStart; i <= rowEnd; i++)
            {
                if (!dtData.Rows[i][0].ToString().Equals(""))
                    actualRowEnd = i;
            }

            for (int j = colStart; j <= colEnd; j++)
            {
                if (!dtData.Rows[0][j].ToString().Equals(""))
                    actualColumnEnd = j;
            }

            int ExportRows = actualRowEnd - rowStart + 1;
            int ExportCols = actualColumnEnd - colStart + 1;

            try
            {
                FileStream fs = new FileStream(filetowrite, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs, Encoding.Unicode);
                bw.Write(ExportRows);//dtData.Rows.Count);
                bw.Write(ExportCols);//dtData.Columns.Count);
                for (int i = rowStart; i <= actualRowEnd; i++)
                {
                    for (int j = colStart; j <= actualColumnEnd; j++)
                    {
                        //Debug.Log(dtData.Rows[i][j].GetType().ToString());
                        bw.Write(dtData.Rows[i][j].ToString());
                    }
                }
                fs.Close();
                fs.Dispose();
            }
            catch (IOException exception)
            {
                if (DEBUG)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine(exception.StackTrace);
                }
            }
            string[] headers;
            string[] getHeaders = GetColumnHeaderAll();
            if (ExportCols == getHeaders.Length)
            {
                return getHeaders;
            }
            else if (getHeaders.Length < ExportCols)
            {
                return null;
            }
            else
            {
                headers = new string[ExportCols];
                for (int i = 0; i < ExportCols; i++)
                {
                    headers[i] = getHeaders[i];
                }
                return headers;
            }
        }
        static void Encrypt(string _path)
        {
            byte[] bytes = File.ReadAllBytes(_path);
            bytes = TEA.code(bytes);
            File.WriteAllBytes(_path, bytes);
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
}
