using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Odbc;
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
    //this is for multi language text
    class ClientMultiTextExporter
    {
        static void Main(string[] args)
        {
            try
            {
                if (args != null)
                {
                    if (args.Length < 5)
                        return;

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

                    string[] rawTextFileNames = new string[RowCount];
                    string[] rawTextFileSheetName = new string[RowCount];
                    for (int i = 0; i < RowCount; i++)
                    {
                        if (SingleExcelExport.dtData.Rows[i][0].ToString() == null)
                            continue;
                        rawTextFileNames[i] = SingleExcelExport.dtData.Rows[i][0].ToString();
                        rawTextFileSheetName[i] = SingleExcelExport.dtData.Rows[i][1].ToString();
                    }

                    ClientMultiLanguageTextExporter.ExportMLTextFiles(rawTextFileNames, rawTextFileSheetName, outDataFilePath, outCodeFilePath);

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}

