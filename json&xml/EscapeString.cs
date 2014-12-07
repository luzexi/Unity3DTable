// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;

public class EscapeString
{
    private static string[] conversions = new string[]{
       "%20",
       "%22",
       "%27",
       "%3C",
       "%3E",
       "%23",
       "%25",
       "%7B",
       "%7D",
       "%7C",
       "%5E",
       "%7E",
       "%5B",
       "%5D",
       "%24",
       "%26",
       "%2B",
       "%2C",
       "%2F",
       "%3A",
       "%3B",
       "%3D",
       "%3F",
       "%40",
       "%5F",
       "%0A",
       "%2E",
       "%09",
       "%C2%A1",
       "%C2%A2",
       "%C2%A3",
       "%C2%A4",
       "%C2%A5",
       "%C2%A6",
       "%C2%A7",
       "%C2%A8",
       "%C2%A9",
       "%C2%AA",
       "%C2%AB",
       "%C2%AC",
       "%C2%AD",
       "%C2%AE",
       "%C2%AF",
       "%C2%B0",
       "%C2%B1",
       "%C2%B2",
       "%C2%B3",
       "%C2%B4",
       "%C2%B5",
       "%C2%B6",
       "%C2%B7",
       "%C2%B8",
       "%C2%B9",
       "%C2%BA",
       "%C2%BB",
       "%C2%BC",
       "%C2%BD",
       "%C2%BE",
       "%C2%BF",
       "%C3%80",
       "%C3%81",
       "%C3%82",
       "%C3%83",
       "%C3%84",
       "%C3%85",
       "%C3%86",
       "%C3%87",
       "%C3%88",
       "%C3%89",
       "%C3%8A",
       "%C3%8B",
       "%C3%8C",
       "%C3%8D",
       "%C3%8E",
       "%C3%8F",
       "%C3%90",
       "%C3%91",
       "%C3%92",
       "%C3%93",
       "%C3%94",
       "%C3%95",
       "%C3%96",
       "%C3%97",
       "%C3%98",
       "%C3%99",
       "%C3%9A",
       "%C3%9B",
       "%C3%9C",
       "%C3%9D",
       "%C3%9E",
       "%C3%9F",
       "%C3%A0",
       "%C3%A1",
       "%C3%A2",
       "%C3%A3",
       "%C3%A4",
       "%C3%A5",
       "%C3%A6",
       "%C3%A7",
       "%C3%A8",
       "%C3%A9",
       "%C3%AA",
       "%C3%AB",
       "%C3%AC",
       "%C3%AD",
       "%C3%AE",
       "%C3%AF",
       "%C3%B0",
       "%C3%B1",
       "%C3%B2",
       "%C3%B3",
       "%C3%B4",
       "%C3%B5",
       "%C3%B6",
       "%C3%B7",
       "%C3%B8",
       "%C3%B9",
       "%C3%BA",
       "%C3%BB",
       "%C3%BC",
       "%C3%BD",
       "%C3%BE",
       "%C3%BF",
       "%2D",
       "%2A",
       "%E2%82%AC",
       "%60",
       "%21",
       "%28",
       "%29",
       "%5C",
       "%E2%80%99",
       "%E2%80%A6"
    };
    
    private static char[] symbols = new char[]{
        ' ',  
        '\"', 
        '\'', 
        '<',  
        '>',  
        '#',  
        '%',  
        '{',  
        '}',  
        '|',  
        '^',  
        '~',  
        '[',  
        ']',  
        '$',  
        '&',  
        '+',  
        ',',  
        '/',  
        ':',  
        ';',  
        '=',  
        '?',  
        '@',
        '_',
        '\n',
        '.',
        ' ',
        '\u00A1',
        '\u00A2',
        '\u00A3',
        '\u00A4',
        '\u00A5',
        '\u00A6',
        '\u00A7',
        '\u00A8',
        '\u00A9',
        '\u00AA',
        '\u00AB',
        '\u00AC',
        '\u00AD',
        '\u00AE',
        '\u00AF',
        '\u00B0',
        '\u00B1',
        '\u00B2',
        '\u00B3',
        '\u00B4',
        '\u00B5',
        '\u00B6',
        '\u00B7',
        '\u00B8',
        '\u00B9',
        '\u00BA',
        '\u00BB',
        '\u00BC',
        '\u00BD',
        '\u00BE',
        '\u00BF',
        '\u00C0',
        '\u00C1',
        '\u00C2',
        '\u00C3',
        '\u00C4',
        '\u00C5',
        '\u00C6',
        '\u00C7',
        '\u00C8',
        '\u00C9',
        '\u00CA',
        '\u00CB',
        '\u00CC',
        '\u00CD',
        '\u00CE',
        '\u00CF',
        '\u00D0',
        '\u00D1',
        '\u00D2',
        '\u00D3',
        '\u00D4',
        '\u00D5',
        '\u00D6',
        '\u00D7',
        '\u00D8',
        '\u00D9',
        '\u00DA',
        '\u00DB',
        '\u00DC',
        '\u00DD',
        '\u00DE',
        '\u00DF',
        '\u00E0',
        '\u00E1',
        '\u00E2',
        '\u00E3',
        '\u00E4',
        '\u00E5',
        '\u00E6',
        '\u00E7',
        '\u00E8',
        '\u00E9',
        '\u00EA',
        '\u00EB',
        '\u00EC',
        '\u00ED',
        '\u00EE',
        '\u00EF',
        '\u00F0',
        '\u00F1',
        '\u00F2',
        '\u00F3',
        '\u00F4',
        '\u00F5',
        '\u00F6',
        '\u00F7',
        '\u00F8',
        '\u00F9',
        '\u00FA',
        '\u00FB',
        '\u00FC',
        '\u00FD',
        '\u00FE',
        '\u00FF',
        '-',
        '*',
        '\u20AC',
        '\u0060',
        '!',
        '(',
        ')',
        '\\',
        '\'',
        '\u2026'
    };
    
    public static string Escape(string input)
    {
         StringBuilder builder = new StringBuilder("");
         for(int i = 0; i < input.Length; ++i)
         {
             //Debug.Log("char: " + input[i]);
             bool found = false;
             for(int j = 0; j < conversions.Length; ++j)
             {
                 if(input[i] == symbols[j])
                 {
                    //Debug.Log("  found: " + input[i] + " " + symbols[j]);
                    builder.Append(conversions[j]);
                    found = true;
                    break;
                 }
             }
             if(! found)
                builder.Append(input[i]);
         }
         return builder.ToString();
    }
    
    public static string Unescape(string input)
    {
        StringBuilder builder = new StringBuilder("");
        char[] chars = input.ToCharArray();
        int i = 0;
        while(i < chars.Length)
        {
            if(chars[i] == '%')
            {
                StringBuilder builder2 = new StringBuilder("");
                builder2.Append(chars[i++]);
                builder2.Append(chars[i++]);
                builder2.Append(chars[i++]);
                string symbol = builder2.ToString();
                if(symbol == "%C2" || symbol == "%C3")
                {
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    symbol = builder2.ToString();
                }
                else if(symbol == "%E2")
                {
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    builder2.Append(chars[i++]);
                    symbol = builder2.ToString();
                }
                bool found = false;
                for(int j = 0; j < conversions.Length; ++j)
                    if(conversions[j] == symbol)
                    {
                        builder.Append(symbols[j]);
                        found = true;
                        break;
                    }
                if(!found)
                    builder.Append(symbol);
            }
            else
                builder.Append(chars[i++]);
        }
        return builder.ToString();
    }
}