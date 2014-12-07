// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using System;
using System.Collections.Generic;
using UnityEngine;

public class JSONInStream
{
	public JSONNode node;
	
	//---------------------------------------------------------------------------------
  	// Constructor
  	//---------------------------------------------------------------------------------
	public JSONInStream (string input)
	{
		JSONParser parser = new JSONParser();
		node = parser.Parse(new FlashCompatibleTextReader(input));
	}
	
	public JSONInStream (JSONNode node)
	{
		this.node = node;
	}
	
	
	//-----------------------------------------------------------------------
	// Has
	//-----------------------------------------------------------------------
	public bool Has(string tag)
	{
		return node.GetField(tag) != null;
	}

	//---------------------------------------------------------------------------------
  	// Content
  	//---------------------------------------------------------------------------------
	public JSONInStream Content(string tag, out string value)
	{			
		try
		{
			JSONStringFieldValue fieldValue = (JSONStringFieldValue) node.GetField(tag);
			value = fieldValue.value;
		}
		catch(Exception e)
		{
			try
			{
				#pragma warning disable
				JSONNullFieldValue fieldValue2 = (JSONNullFieldValue) node.GetField(tag);
				#pragma warning restore
				value = null;
			}
			catch(Exception e2)
			{
				Debug.LogError("Error JSONInStream " + tag + " " + e.ToString() + " " + e2.ToString());
				value = null;
				return null;
			}
		}
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref string value)
	{			
		try
		{
			JSONStringFieldValue fieldValue = (JSONStringFieldValue) node.GetField(tag);
			if(fieldValue == null)
				return this;
			value = fieldValue.value;
		}
		catch(Exception e)
		{
			try
			{
				#pragma warning disable
				JSONNullFieldValue fieldValue2 = (JSONNullFieldValue) node.GetField(tag);
				#pragma warning restore
				value = null;
			}
			catch(Exception e2)
			{
				Debug.LogError("Error JSONInStream " + tag + " " + e.ToString() + " " + e2.ToString());
				return null;
			}
		}
		return this;
	}
	
	public JSONInStream Content(out string value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out string value)
	{
		try
		{
			JSONStringFieldValue fieldValue = (JSONStringFieldValue) node.GetField(idx);
			value = fieldValue.value;
		}
		catch(Exception e)
		{
			try
			{
				#pragma warning disable
				JSONNullFieldValue fieldValue2 = (JSONNullFieldValue) node.GetField(idx);
				#pragma warning restore
				value = null;
			}
			catch(Exception e2)
			{
				Debug.LogError("Error JSONInStream " + idx + " " + e.ToString() + " " + e2.ToString());
				value = null;
				return null;
			}
		}
		return this;
	}
	
	public JSONInStream Content(string tag, out float value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		value = (float) fieldValue.value;
		return this;
	}

	public JSONInStream Content(out float value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out float value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(idx);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(idx);
		}
		value = (float) fieldValue.value;
		return this;
	}

	public JSONInStream Content(string tag, out double value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		value = (double) fieldValue.value;
		return this;
	}

	public JSONInStream Content(out double value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out double value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(idx);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(idx);
		}
		value = (double) fieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref double value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		if(fieldValue != null)
			value = (double) fieldValue.value;
		return this;
	}
	
	public JSONInStream Content(string tag, out int value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		value = (int) fieldValue.value;
		return this;
	}

	public JSONInStream Content(out int value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out int value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(idx);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(idx);
		}
		value = (int) fieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref int value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		if(fieldValue != null)
			value = (int) fieldValue.value;
		return this;
	}
	
	public JSONInStream Content(string tag, out bool value)
	{
		JSONBooleanFieldValue fieldValue = null;
		try{
			fieldValue = (JSONBooleanFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		value = fieldValue.value;
		return this;
	}

	public JSONInStream Content(out bool value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out bool value)
	{
		JSONBooleanFieldValue fieldValue = null;
		try{
			fieldValue = (JSONBooleanFieldValue) node.GetField(idx);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(idx);
		}
		value = fieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref bool value)
	{
		JSONBooleanFieldValue fieldValue = null;
		try{
			fieldValue = (JSONBooleanFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		if(fieldValue != null)
			value = fieldValue.value;
		return this;
	}

	// VECTOR2

	public JSONInStream Content(string tag, out Vector2 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[2];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector2 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;

		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[2];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}

	public JSONInStream Content(out Vector2 value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Vector2 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[2];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}
	
	
	// VECTOR3

	public JSONInStream Content(string tag, out Vector3 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[3];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector3 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;

		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[3];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream Content(out Vector3 value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Vector3 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[3];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	// VECTOR4
	
	public JSONInStream Content(string tag, out Vector4 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector4 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Vector4 value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out Vector4 value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	// QUATERNION
	
	public JSONInStream Content(string tag, out Quaternion value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Quaternion value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Quaternion value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out Quaternion value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	// COLOR

	public JSONInStream Content(string tag, out Color value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Color value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Color value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Color value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	// RECT

	public JSONInStream Content(string tag, out Rect value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = new Rect();
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Rect value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(tag);
		if(fieldValue == null)
			return this;
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = new Rect();
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Rect value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Rect value)
	{
		// null ?
		IJSONFieldValue fieldValue = node.GetField(idx);
		if(fieldValue.GetType() == typeof(JSONNullFieldValue))
		{
			value = new Rect();
			return this;
		}
		
		// unpack values
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream){
			float f;
			stream.Content(out f);
			fs[i] = f;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out XorInt value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		value = (XorInt)fieldValue.value;
		return this;
	}
	
	public JSONInStream Content(out XorInt value)
	{
		return Content(0, out value);
	}
	
	public JSONInStream Content(int idx, out XorInt value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(idx);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(idx);
		}
		value = (XorInt)fieldValue.value;
		return this;
	}
	
	public JSONInStream ContentOptional(string tag, ref XorInt value)
	{
		JSONNumberFieldValue fieldValue = null;
		try{
			fieldValue = (JSONNumberFieldValue) node.GetField(tag);
		}
		catch(Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		if(fieldValue != null)
			value = (XorInt)fieldValue.value;
		return this;
	}
	
	//---------------------------------------------------------------------------------
  // List
  //---------------------------------------------------------------------------------
	public JSONInStream List(string tag, Action<int, JSONInStream> callback)
	{
		JSONListFieldValue fieldValue = null;
		try{
			fieldValue = (JSONListFieldValue) node.GetField(tag);
		}
		catch(System.Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}

		int i = 0;
		foreach(IJSONFieldValue val in fieldValue.value)
		{
			JSONNode n = new JSONNode(val);
			JSONInStream stream = new JSONInStream(n);

  		try
  		{
        JSONObjectFieldValue valObject = (JSONObjectFieldValue) val;
        if(valObject != null)
          stream = stream.Start(0);
  		}
  		catch{}

			callback(i++, stream);
		}
		return this;
	}
	
	public JSONInStream List(int idx, Action<int, JSONInStream> callback)
	{
		JSONListFieldValue fieldValue = (JSONListFieldValue) node.GetField(idx);
		int i = 0;
		foreach(IJSONFieldValue val in fieldValue.value)
		{
			JSONNode n = new JSONNode(val);
			JSONInStream stream = new JSONInStream(n);
      
  		try
  		{
        JSONObjectFieldValue valObject = (JSONObjectFieldValue) val;
        if(valObject != null)
          stream = stream.Start(0);
  		}
  		catch{}


			callback(i++, stream);
		}
		return this;
	}
	
	//---------------------------------------------------------------------------------
  	// Start
  	//---------------------------------------------------------------------------------
	public JSONInStream Start(string tag)
	{
		try{
			JSONObjectFieldValue fieldValue = (JSONObjectFieldValue) node.GetField(tag);
			fieldValue.value.parent = node;
			node = fieldValue.value;
		}
		catch(System.Exception e){
			Debug.LogError(e);
			Debug.LogError(tag);
		}
		return this;
	}
	
	public JSONInStream Start(int idx)
	{
		JSONObjectFieldValue fieldValue = (JSONObjectFieldValue) node.GetField(idx);
		fieldValue.value.parent = node;
		node = fieldValue.value;
		return this;
	}
	
	//---------------------------------------------------------------------------------
  	// End
  	//---------------------------------------------------------------------------------
	public JSONInStream End()
	{
		if(node.parent != null)
			node = node.parent;
		return this;
	}
	
	//---------------------------------------------------------------------------------
  	// Count
  	//---------------------------------------------------------------------------------
	public int Count
	{get
		{
			return node.GetFieldCount();
		}
	}
}
