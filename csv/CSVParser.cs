using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//  CSV.cs
//  Author: Lu Zexi
//  2013-11-13



//csv parse
public class CSVParser
{
    //parse csv from row 3.
    public static List<string> ParseFromRow2(string csvText)
    {
        string[,] vecStr = SplitCsvGrid(csvText);
        List<string> lst = new List<string>();

        for (int j = 2; j < vecStr.GetLength(1)-2; j++)
        {
            for (int i = 0; i < vecStr.GetLength(0)-2; i++)
            {
                lst.Add(vecStr[i, j]);
            }
        }
        return lst;
    }

    // splits a CSV file into a 2D string array
    public static string[,] Parse(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width + 1, lines.Length + 1];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    //split csv line
    public static string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}
