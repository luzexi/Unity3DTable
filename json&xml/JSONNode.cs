// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com


using System;
using System.Collections.Generic;

public class JSONNode : IJSONFieldValue
{
	public JSONNode parent;
	public List<JSONField> fields_ = new List<JSONField>();
	
	// Only used to OutStream lists
	public bool isList = false;
	public string listName = "";
	
	public JSONNode()
	{
	}
	
	public JSONNode(JSONNode parent)
	{
		this.parent = parent;
	}
	
	public JSONNode(IJSONFieldValue val)
	{
		fields_.Add(new JSONField("0", val));
	}
	
	public JSONNode(List<IJSONFieldValue> list)
	{
		for(int i = 0; i < list.Count; ++i)
			fields_.Add(new JSONField(i.ToString(), list[i]));
	}
	
	public void AddField(string fieldName, IJSONFieldValue val)
	{
		fields_.Add(new JSONField(fieldName, val));
	}
	
	public void AddField(int idx, IJSONFieldValue val)
	{
		fields_.Add(new JSONField(idx.ToString(), val));
	}
  
	public void AddField(IJSONFieldValue val)
	{
		fields_.Add(new JSONField(null, val));
	}
	
	public IJSONFieldValue GetField(string name)
	{
		foreach(JSONField field in fields_)
			if(field.name == name)
				return field.value;
		return null;
	}
	
	public IJSONFieldValue GetField(int index)
	{
		return fields_[index].value;
	}
	
	public int GetFieldCount()
	{
		return fields_.Count;
	}
	
	public JSONListFieldValue GetListFieldValue()
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		for(int i = 0; i < fields_.Count; ++i)
		{
			list.Add(fields_[i].value);
		}
		return new JSONListFieldValue(list);
	}
	
	public string Serialize()
	{
    if(fields_.Count == 1 && (fields_[0].name == "" || fields_[0].name == null))
    {
  		return fields_[0].value.Serialize();
    }
    else
    {
  		string result = "{";
    	if(fields_.Count > 0)
    		result+= "\"" + fields_[0].name + "\":" + fields_[0].value.Serialize();	
    	for(int i = 1; i < fields_.Count; ++i)
    	{
    		result += ",\"" + fields_[i].name + "\":" + fields_[i].value.Serialize();	
    	}
    	result += "}";
  		return result; 
    }
	}
}
