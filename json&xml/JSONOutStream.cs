// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using System;
using UnityEngine;
using System.Collections.Generic;



public class JSONOutStream
{
	public JSONNode node = new JSONNode();
	
	//---------------------------------------------------------------------------------
  	// Serialize
  	//---------------------------------------------------------------------------------
	public string Serialize()
	{
		return node.Serialize();
	}
	
	//---------------------------------------------------------------------------------
  	// Content
  	//---------------------------------------------------------------------------------
	public JSONOutStream Content(string tag, string value)
	{
		node.AddField(tag, new JSONStringFieldValue(value));
		return this;
	}
	public JSONOutStream Content(int idx, string value)
	{
		node.AddField(idx, new JSONStringFieldValue(value));
		return this;
	}
	public JSONOutStream Content(string value)
	{
		node.AddField(new JSONStringFieldValue(value));
		return this;
	}
	
	public JSONOutStream Content(string tag, double value)
	{
		node.AddField(tag, new JSONNumberFieldValue(value));
		return this;
	}
	public JSONOutStream Content(int idx, double value)
	{
		node.AddField(idx, new JSONNumberFieldValue(value));
		return this;
	}
	public JSONOutStream Content(double value)
	{
		node.AddField(new JSONNumberFieldValue(value));
		return this;
	}
	
	public JSONOutStream Content(string tag, bool value)
	{
		node.AddField(tag, new JSONBooleanFieldValue(value));
		return this;
	}
	public JSONOutStream Content(int idx, bool value)
	{
		node.AddField(idx, new JSONBooleanFieldValue(value));
		return this;
	}
	public JSONOutStream Content(bool value)
	{
		node.AddField(new JSONBooleanFieldValue(value));
		return this;
	}
	public JSONOutStream Content(int idx, XorInt value)
	{
		node.AddField(idx, new JSONNumberFieldValue(value));
		return this;
	}
	public JSONOutStream Content(XorInt value)
	{
		node.AddField(new JSONNumberFieldValue(value));
		return this;
	}

	// VECTOR2
	
	public JSONOutStream Content(string tag, Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(int idx, Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Vector2 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	// VECTOR3
	
	public JSONOutStream Content(string tag, Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(int idx, Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Vector3 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	// VECTOR4
	
	public JSONOutStream Content(string tag, Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(int idx, Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Vector4 value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	// QUATERNION
	
	public JSONOutStream Content(string tag, Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(int idx, Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Quaternion value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.z));
		list.Add(new JSONNumberFieldValue(value.w));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	// COLOR

	public JSONOutStream Content(string tag, Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.r));
		list.Add(new JSONNumberFieldValue(value.g));
		list.Add(new JSONNumberFieldValue(value.b));
		list.Add(new JSONNumberFieldValue(value.a));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	
	public JSONOutStream Content(int idx, Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.r));
		list.Add(new JSONNumberFieldValue(value.g));
		list.Add(new JSONNumberFieldValue(value.b));
		list.Add(new JSONNumberFieldValue(value.a));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Color value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.r));
		list.Add(new JSONNumberFieldValue(value.g));
		list.Add(new JSONNumberFieldValue(value.b));
		list.Add(new JSONNumberFieldValue(value.a));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}

	// RECT

	public JSONOutStream Content(string tag, Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.width));
		list.Add(new JSONNumberFieldValue(value.height));
		node.AddField(tag, new JSONListFieldValue(list));
		return this;
	}
	
	public JSONOutStream Content(int idx, Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.width));
		list.Add(new JSONNumberFieldValue(value.height));
		node.AddField(idx, new JSONListFieldValue(list));
		return this;
	}
	public JSONOutStream Content(Rect value)
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		list.Add(new JSONNumberFieldValue(value.x));
		list.Add(new JSONNumberFieldValue(value.y));
		list.Add(new JSONNumberFieldValue(value.width));
		list.Add(new JSONNumberFieldValue(value.height));
		node.AddField(new JSONListFieldValue(list));
		return this;
	}
	
	//---------------------------------------------------------------------------------
  // List
  //---------------------------------------------------------------------------------
	public JSONOutStream List(string tag)
	{
		JSONNode n = new JSONNode(node);
		n.isList = true;
		n.listName = tag;
		node = n;
		return this;
	}
	public JSONOutStream List()
	{
		JSONNode n = new JSONNode(node);
		n.isList = true;
		n.listName = null;
		node = n;
		return this;
	}
	
	//---------------------------------------------------------------------------------
  // Start
  //---------------------------------------------------------------------------------
	public JSONOutStream Start(string tag)
	{
		JSONNode n = new JSONNode(node);
		node.AddField(tag, new JSONObjectFieldValue(n));
		node = n;
		return this;
	}
	
	public JSONOutStream Start(int idx)
	{
		JSONNode n = new JSONNode(node);
		node.AddField(idx, new JSONObjectFieldValue(n));
		node = n;
		return this;
	}
  
	public JSONOutStream Start()
	{
		JSONNode n = new JSONNode(node);
		node.AddField(new JSONObjectFieldValue(n));
		node = n;
		return this;
	}
				
	//---------------------------------------------------------------------------------
  // End
  //---------------------------------------------------------------------------------
	public JSONOutStream End()
	{
		if(node.parent != null)
		{
			if(node.isList && node.listName != null)
				node.parent.AddField(node.listName, node.GetListFieldValue());
      else if(node.isList)
        node.parent.AddField(node.GetListFieldValue());
			node = node.parent;
		}
		return this;
	}
}
