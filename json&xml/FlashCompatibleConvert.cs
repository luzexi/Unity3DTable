// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


  public class FlashCompatibleConvert : MonoBehaviour {

  	// Use this for initialization
  	public static int ToInt32(string s)
    {
      if(s == null)
        throw new Exception("FlashCompatibleConvert.ToInt32 was passed a null string as argument");

      if(s.Length == 0)
        throw new Exception("FlashCompatibleConvert.ToInt32 was passed an empty string as argument");

      bool negative = s[0] == '-';
      int offsetIfNegative = negative ? 1 : 0;

      double result = 0;
  	  for(int i = offsetIfNegative; i < s.Length; ++i)
      {
        int n = CharToInt32(s[i]);
        if(n == -1)
          throw new Exception("FlashCompatibleConvert.ToInt32 was passed a wrong argument: " + s);
        result += n * Math.Pow(10, s.Length - i - 1);
      }
      return (int) (result * (negative ? -1 : 1));
  	}

    static int CharToInt32(char s)
    {
      switch(s)
      {
        case '0': return 0;
        case '1': return 1;
        case '2': return 2;
        case '3': return 3;
        case '4': return 4;
        case '5': return 5;
        case '6': return 6;
        case '7': return 7;
        case '8': return 8;
        case '9': return 9;
        case '.': return -2;
        case ',': return -2;
        case 'e': return -3;
        case 'E': return -3;
        default: return -1;
      }
    }

    public static bool ToBoolean(string s)
    {
      if(s == null)
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed a null string as argument");

      if(s.Length == 0)
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed an empty string as argument");

      if(s.ToLower() == "true")
        return true;
      else if(s.ToLower() == "false")
        return false;
      else
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed a wrong argument: " + s);
    }

    public static double ToDouble(string s)
    {
      if(s == null)
        throw new Exception("FlashCompatibleConvert.ToDouble was passed a null string as argument");

      if(s.Length == 0)
        throw new Exception("FlashCompatibleConvert.ToDouble was passed an empty string as argument");
      
      // Search the index of the coma
      int indexComa = -1;
      for(int i = 0; i < s.Length; ++i)
        if(CharToInt32(s[i]) == -2)
        {
          indexComa = i;
          break;
        }
      
      // End of the string to be parsed
      int end = s.Length;
      
      // Search the index of the exponent
      int indexExponent = -1;
      for(int i = 0; i < s.Length; ++i)
        if(CharToInt32(s[i]) == -3)
        {
          indexExponent = i;
          end = i;
          break;
        }

      // Check if negative
      bool negative = s[0] == '-';
      int offsetIfNegative = negative ? 1 : 0;
      
      int intPartLength = s.Length;
      if(indexComa != -1)
        intPartLength = indexComa;

      // Manage the main value with decimal
      double result = 0;

      for(int i = offsetIfNegative; i < end; ++i)
      {
        if(i == indexComa)
          continue;

        int n = CharToInt32(s[i]);
        if(n == -1)
          throw new Exception("FlashCompatibleConvert.ToDouble was passed a wrong argument: " + s);
        
        if(indexComa != -1 && i > indexComa)
        {
          result += n * Math.Pow(0.1, i - indexComa);
        }
        else
        {
          result += n * Math.Pow(10, intPartLength - i - 1);
        }
        
      }
      
      // Manage the exponent
      if(indexExponent != -1)
      {
        double exp = FlashCompatibleConvert.ToDouble(s.Substring(indexExponent+1));
        result = result * Math.Pow(10, exp);
      }
      
      return result * (negative ? -1 : 1);
    }
  
    public static bool IsDigit(char c)
    {
      return c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9'; 
    }

  }
