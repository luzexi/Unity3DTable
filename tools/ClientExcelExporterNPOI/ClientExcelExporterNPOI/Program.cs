using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
// using System.Data.Odbc;
using System.IO;
using System.Text;

/************************************************************************/
/* exl file exporter by cosmosliu
 * to fit the needs of Panda project
 * 
 * input: _TableConfig.xls which includes all configs of exl exporting
/************************************************************************/

namespace ClientExcelExporter
{   
    //this is for data
    class ClientExcelExporter
    {
        static void Main(string[] args)
        {
             try
             {
                if (args != null)
                {
                    if (args.Length < 5)
                    {
                        Console.WriteLine("Not enough param number!");
                        return;
                    }

                    string configFilePath = "";
                    string configFileSheetName = "";
                    string rawDataFilePath = "";
                    string outDataFilePath = "";
                    string outCodeFilePath = "";

                    configFilePath = args[0];
                    configFileSheetName = args[1];
                    rawDataFilePath = args[2];
                    outDataFilePath = args[3];
                    outCodeFilePath = args[4];
                    
                    if (true)
                    {
                        Console.WriteLine("configFilePath is " + configFilePath);
                        Console.WriteLine("configFileSheetName is " + configFileSheetName);
                        Console.WriteLine("rawFilePath  is " + rawDataFilePath);
                        Console.WriteLine("outFilePath  is " + outDataFilePath);
                        Console.WriteLine("outCodeFilePath  is " + outCodeFilePath);
                    }

                    //read config file
                    SingleExcelExport.ReadXLS(configFilePath + ".xls", configFileSheetName);
                    int ColCount = SingleExcelExport.dtData.Columns.Count;
                    int RowCount = SingleExcelExport.dtData.Rows.Count;

                    Console.WriteLine("ColCount is " + ColCount);
                    Console.WriteLine("RowCount is " + RowCount);

                    
                    if (ColCount > 0 && RowCount > 0)
                    {
                        string[,] exportSettings = new string[RowCount, ColCount];
                        for (int i = 0; i < RowCount; i++)
                        {
                            if (SingleExcelExport.dtData.Rows[i][0].ToString() == null)
                                continue;

                            for (int j = 0; j < ColCount; j++)
                            {
                                exportSettings[i, j] = SingleExcelExport.dtData.Rows[i][j].ToString();
                            }
                        }

                        Dictionary<string, int> dicHeaders = new Dictionary<string, int>();
                        //start export
                        for (int i = 1; i < RowCount; i++)
                        {
                            //this excatly fit the settings of _tableConfig.xls
                            if (exportSettings[i, 0] == null)
                                return;
                            if (exportSettings[i, 1] == null)
                                return;
                            if (exportSettings[i, 2] == null)
                                return;

                            string fileName = rawDataFilePath + exportSettings[i, 1];
                            string sheetName = exportSettings[i, 2];
                            string outFileName = outDataFilePath + exportSettings[i, 3];
                            string configFlag = exportSettings[i, 4];
                            string ColRange = exportSettings[i, 5];
                            string RowRange = exportSettings[i, 6];
                            // printf(ColRange);
                            // Console.WriteLine("ColRange is " + ColRange);
                            // printf(RowRange);
                            // Console.WriteLine("RowRange is " + RowRange);
                            // return;
                            string[] colRange = ColRange.Split(',');
                            string[] rowRange = RowRange.Split(',');
                            // return;
                            int startCol = int.Parse(colRange[0]);
                            int endCol = int.Parse(colRange[1]);
                            int startRow = int.Parse(rowRange[0]);
                            int endRow = int.Parse(rowRange[1]);

                            if (configFlag.Equals("S"))//if this table is only for server
                                continue;

                            string [] headers = SingleExcelExport.Export(fileName, sheetName, outFileName, startCol, endCol, startRow, endRow);
                            for(int j=0; j< headers.Length; j++)
                            {
                                dicHeaders.Add(exportSettings[i, 1] + "_" + sheetName + "_" + headers[j], j);
                            }
                        }

                        string filename = outCodeFilePath + "DataDefine.cs";
                        //write to header
                        using (StreamWriter sw = new StreamWriter(filename))
                        {
                            string content;
                            StringBuilder sb = new StringBuilder();

                            sb.Append("//This file is generated by tools!").Append("\n");
                            sb.Append("//Please do not modify this file!").Append("\n");
                            sb.Append("public class DataDefine {").Append("\n").Append("\n");


                            foreach (var item in dicHeaders)
                            {
                                string header = String.Copy(item.Key);
                                if (header.Contains(" "))
                                {
                                    header = header.Replace(' ', '_');
                                }
                                sb.Append("\tpublic const int " + header + " = " + item.Value + ";").Append("\n");
                            }

                            sb.Append("\n");
                            sb.Append("}");

                            content = sb.ToString();
                            sw.Write(content);
                        }
                    }
                }
                    
                
             }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
          }
    }
}

