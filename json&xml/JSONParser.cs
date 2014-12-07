// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// class JSONParser
//---------------------------------------------------------------------------------
public class JSONParser
{
	
  	private FlashCompatibleTextReader reader;

	//---------------------------------------------------------------------------------
  	// Constructor
  	//---------------------------------------------------------------------------------
	public JSONParser()
	{
	}

  	//---------------------------------------------------------------------------------
  	// Parse
  	//---------------------------------------------------------------------------------
  	public JSONNode Parse(string txt)
	{
		FlashCompatibleTextReader reader = new FlashCompatibleTextReader(txt);
		return Parse(reader);
	}
	public JSONNode Parse(FlashCompatibleTextReader reader)
	{
		this.reader = reader;
		
		//check empty string
		if(reader.Peek() == -1)
			return null;
		
		
		return ReadObject();
	}
	
	//---------------------------------------------------------------------------------
  	// ReadObject
  	//---------------------------------------------------------------------------------
	private JSONNode ReadObject()
	{
		JSONNode node = new JSONNode();
		
		SkipWhitespace();
		// read '{'
		if(reader.Peek() != '{')
		{
			Debug.LogError("malformed json: no starting '{'");
			return null;
		}
		reader.Read();
		while(reader.Peek() != '}')
		{	
			if(reader.Peek() == ',')
				reader.Read();
			if(reader.Peek() == '}')
				break;
			
			SkipWhitespace();
			// read field name
			string fieldName = ReadFieldName().Trim();
			
			// read ':'
			SkipWhitespace();
			reader.Read();
			//Console.WriteLine("after : found " + ((char) reader.Peek()));

			// read value
			IJSONFieldValue val = ReadValue();
			
			//Console.WriteLine("adding field " + fieldName + " " + val);
		 	node.AddField(fieldName, val);
			SkipWhitespace();
		}
		reader.Read(); // remove trailing "}" 
		
		return node;
	}
	
	//---------------------------------------------------------------------------------
  	// ReadValue
  	//---------------------------------------------------------------------------------
	private IJSONFieldValue ReadValue()
	{
		SkipWhitespace();
		char peek = (char) reader.Peek();
		// Is it a string ?
		//Console.WriteLine("character found at beginning of value " + peek);
		if(peek == '"' || peek == '\'')
		{
			return  new JSONStringFieldValue(ReadString());
		}
		else if(FlashCompatibleConvert.IsDigit(peek) || peek == '-')
		{
			return  new JSONNumberFieldValue(ReadNumber());
		}
		else if(peek == '[')
		{
			return  new JSONListFieldValue(ReadList());
		}
		else if(peek == '{')
		{
			return new JSONObjectFieldValue(ReadObject());
		}
		else if(peek == 't' || peek == 'f')
		{
			return new JSONBooleanFieldValue(ReadBoolean());
		}
		else if(peek == 'n')
		{
			ReadNull();
			return new JSONNullFieldValue();
		}
		return null;
	}

  	//---------------------------------------------------------------------------------
  	// SkipWhitespace
  	//---------------------------------------------------------------------------------
	private void SkipWhitespace()
	{
		while(true)
		{
			int peek = reader.Peek();
			if(peek == -1 || ! Char.IsWhiteSpace((char)peek))
				return;
			reader.Read();
		}
	}

  	//---------------------------------------------------------------------------------
  	// ReadFieldName
  	//---------------------------------------------------------------------------------
	private string ReadFieldName()
	{
		SkipWhitespace();

		string result = "";

		char peek = (char) reader.Peek();
		bool quoted = (peek == '\'') || (peek == '"');
		SkipWhitespace();
		while(peek != ':')
		{
			if(peek == '}')
			{
				if(result == "")
					return "";
				else
				{
					Debug.LogError("malformed json: read '}' before reading ':'");
					return null;
				}
			}
			result += ((char)reader.Read()).ToString();
			SkipWhitespace();
			peek = (char) reader.Peek();
		}

		if(quoted && (result.EndsWith("'") || result.EndsWith("\"")))
		{
			result = result.Substring(1, result.Length-2);
		}
		return result;
	}

  	//---------------------------------------------------------------------------------
  	// ReadNumber
  	//---------------------------------------------------------------------------------
	private double ReadNumber()
	{
		string result = "";
		
		while (true)
		{
			int peek = reader.Peek();
			if(peek == -1 || peek == ',' || peek == '}' || peek == ']'|| Char.IsWhiteSpace((char)peek))
			{
				return FlashCompatibleConvert.ToDouble(result);
			}
			else
				result += ((char)reader.Read()).ToString();
		}
	}
	
	//---------------------------------------------------------------------------------
  	// ReadBoolean
  	//---------------------------------------------------------------------------------
	private bool ReadBoolean()
	{
		char peek = (char) reader.Peek();
		bool t = peek == 't';
		for(int i = 0; i < 4; ++i)
			reader.Read();
		if(! t)
			reader.Read();
		return t;
	}
	
	//---------------------------------------------------------------------------------
  	// ReadNull
  	//---------------------------------------------------------------------------------
	private void ReadNull()
	{
		for(int i = 0; i < 4; ++i)
			reader.Read();
	}
	
	//---------------------------------------------------------------------------------
  	// ReadString
  	//---------------------------------------------------------------------------------
	private String ReadString()
	{
		string result = "";
		
		bool simpleQuoted = ((char)reader.Peek()) == '\'';
		reader.Read(); // read starting "'"
		
		while (true)
		{
			char peek = (char) reader.Peek();
			if( (! simpleQuoted && peek == '"')
			 || (simpleQuoted   && peek == '\''))
			{
				reader.Read(); //read the ending '"'
				return result;
			}
			else
			{
				result += ((char)reader.Read()).ToString();
			}
		}
	}
	
	//---------------------------------------------------------------------------------
  	// ReadList
  	//---------------------------------------------------------------------------------
	private List<IJSONFieldValue> ReadList()
	{
		List<IJSONFieldValue> result = new List<IJSONFieldValue>();
		
		reader.Read(); // read starting "["
		
		while (true)
		{
			char peek = (char) reader.Peek();
			if(peek == ']')
			{
				reader.Read(); //read the ending ']'
				return result;
			}
			else if(peek == ',')
			{
				reader.Read(); // read the ','
				SkipWhitespace();
			}
			else
			{				
				IJSONFieldValue val = ReadValue();
				result.Add(val);
				SkipWhitespace();
			}
		}
	}
}