// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using System;
using System.Collections.Generic;
using UnityEngine;

public interface IJSONFieldValue
{
	string Serialize();
}

public class JSONField
{
	public string name;
	public IJSONFieldValue value;
	
	public JSONField(string n, IJSONFieldValue val)
	{
		name  = n;
		value = val;
	}
}

public class JSONStringFieldValue: IJSONFieldValue
{
	public string value;
	
	public JSONStringFieldValue(string val)
	{
		value = val;
	}
	
	public string Serialize()
	{
		return "\"" + value + "\"";
	}
}

public class JSONNumberFieldValue: IJSONFieldValue
{
	public double value;
	
	public JSONNumberFieldValue(double val)
	{
		value = val;
	}
	
	public string Serialize()
	{
		return value.ToString();
	}
}

public class JSONListFieldValue: IJSONFieldValue
{
	public List<IJSONFieldValue> value;
	
	public JSONListFieldValue()
	{
		value = new List<IJSONFieldValue>();
	}
	
	public JSONListFieldValue(List<IJSONFieldValue> val)
	{
		value = val;
	}
	
	public string Serialize()
	{
		string result = "[";
		if(value.Count > 0)
			result += value[0].Serialize();
		for(int i = 1; i < value.Count; ++i)
			result += "," + value[i].Serialize();
		return result + "]";
	}
}

public class JSONObjectFieldValue: IJSONFieldValue
{
	public JSONNode value;
	
	public JSONObjectFieldValue(JSONNode val)
	{
		value = val;
	}
	
	public string Serialize()
	{
		return value.Serialize();
	}
}

public class JSONBooleanFieldValue: IJSONFieldValue
{
	public bool value;
	
	public JSONBooleanFieldValue(bool val)
	{
		value = val;
	}
	
	public string Serialize()
	{
		return value.ToString().ToLower();
	}
}

public class JSONNullFieldValue: IJSONFieldValue
{	
	public string Serialize()
	{
		return "null";
	}
}