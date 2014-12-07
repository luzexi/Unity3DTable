// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using System;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------
// class XMLInStream
//---------------------------------------------------------------------------------
public class XMLInStream
{
    public XMLNode current;
    
    //---------------------------------------------------------------------------------
	// Constructor
	//---------------------------------------------------------------------------------
    public XMLInStream(XMLNode node)
    {
        current = node;
    }
    
    public XMLInStream(string input)
    {
    	XMLParser parser = new XMLParser();
    	current = parser.Parse(new FlashCompatibleTextReader(input));
    }
	
	//---------------------------------------------------------------------------------
	// Clone
	//---------------------------------------------------------------------------------
    public XMLInStream Clone()
	{
		return new XMLInStream(current);
	}
	

    //---------------------------------------------------------------------------------
    // Tag
    //---------------------------------------------------------------------------------
    public string Tag
    {
        get {
            return current.tag;
        }
    }
	
    //---------------------------------------------------------------------------------
	// Children
	//---------------------------------------------------------------------------------
    public List<XMLInStream> Children
    { get
    {
		List<XMLInStream> res = new List<XMLInStream>();
		foreach(XMLNode child in current.children)
			res.Add(new XMLInStream(child));
		return res;
    }
    }
    
    //---------------------------------------------------------------------------------
	// Has
	//---------------------------------------------------------------------------------
    public bool Has(string tag)
	{
	    foreach(XMLNode child in current.children)
            if(child.tag == tag)
            {
                return true;
            }
	    return false;
	}

    //---------------------------------------------------------------------------------
    // NumberChildren
    //---------------------------------------------------------------------------------
    public int NumberChildren(string tag)
    {
        int count = 0;
        foreach(XMLNode child in current.children)
            if(child.tag == tag)
            {
                count++;
            }
        return count;
    }

    //---------------------------------------------------------------------------------
    // HasAttribute
    //---------------------------------------------------------------------------------
    public bool HasAttribute(string tag)
    {
        return current.attributes.ContainsKey(tag);
    }
	
    //---------------------------------------------------------------------------------
	// Start
	//---------------------------------------------------------------------------------
    public XMLInStream Start(string tag)
    {
        foreach(XMLNode child in current.children)
            if(child.tag == tag)
            {
                current = child;
                return this;
            }
        Debug.LogError("No child node named: " + tag + " in node " + current.tag);
        return null;
    }
    
    //---------------------------------------------------------------------------------
	// End
	//---------------------------------------------------------------------------------
    public XMLInStream End()
    {
    	if(current.parent == null)
        {
            Debug.LogError("No parent node for tag: " + current.tag);
            return null;
        }
        current = current.parent;
        return this;
    }
    
    //---------------------------------------------------------------------------------
	// Content
	//---------------------------------------------------------------------------------
    
    // string

    public XMLInStream Content(string tag, out string value)
    {
    	return this.Start(tag).Content(out value).End();
    }

    public XMLInStream Content(out string value)
    {
        value = current.content;
        return this;
    }

    // bool

    public XMLInStream Content(string tag, out bool value)
    {
    	return this.Start(tag).Content(out value).End();
    }

    public XMLInStream Content(out bool value)
    {
        value = FlashCompatibleConvert.ToBoolean(current.content);
        return this;
    }


    // Int32

    public XMLInStream Content(string tag, out Int32 value)
    {
    	return this.Start(tag).Content(out value).End();
    }

    public XMLInStream Content(out Int32 value)
    {
        value = FlashCompatibleConvert.ToInt32(current.content);
        return this;
    }

    // float

    public XMLInStream Content(string tag, out float value)
    {
    	return this.Start(tag).Content(out value).End();
    }

    public XMLInStream Content(out float value)
    {
        value = (float) GetDouble(current.content);
        return this;
    }

    // Color

    public XMLInStream Content(string tag, out Color value)
    {
        float r, g, b, a;
        if(tag != null)
            this.Start(tag);
        if(HasAttribute("r"))
        {
           this.Attribute("r", out r).Attribute("g", out g).Attribute("b", out b).Attribute("a", out a).End();       
        }
        else
        { 
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "");
           string[] vals = txt.Split(',');
           r = (float) GetDouble(vals[0]);
           g = (float) GetDouble(vals[1]);
           b = (float) GetDouble(vals[2]);
           a = (float) GetDouble(vals[3]);
        }
        value = new Color(r, g, b, a);
        return this;
    }

    public XMLInStream Content(out Color value)
    {
        return Content(null, out value);
    }

    // Vector2

    public XMLInStream Content(string tag, out Vector2 value)
    {
        float x, y;
        if(tag != null)
            this.Start(tag);
        if(HasAttribute("x"))
        {
           this.Attribute("x", out x).Attribute("y", out y).End();
        }
        else
        {
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "").Replace(" ", "");
           string[] vals = txt.Split(',');
           x = (float) GetDouble(vals[0]);
           y = (float) GetDouble(vals[1]);
        }
        value = new Vector2(x, y);
        return this;
    }

    public XMLInStream Content(out Vector2 value)
    {
        return Content(null, out value);
    }

    // Vector3

    public XMLInStream Content(string tag, out Vector3 value)
    {
    	float x, y, z;
        if(tag != null)
            this.Start(tag);
    	if(HasAttribute("x"))
        {
           this.Attribute("x", out x).Attribute("y", out y).Attribute("z", out z).End();
    	}
        else
        {
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "").Replace(" ", "");
           string[] vals = txt.Split(',');
           x = (float) GetDouble(vals[0]);
           y = (float) GetDouble(vals[1]);
           z = (float) GetDouble(vals[2]);
        }
        value = new Vector3(x, y, z);
    	return this;
    }

    public XMLInStream Content(out Vector3 value)
    {
        return Content(null, out value);
    }

    // Quaternion

    public XMLInStream Content(string tag, out Quaternion value)
    {
    	float x, y, z, w;
        if(tag != null)
            this.Start(tag);
    	if(HasAttribute("x"))
        {
            this.Attribute("x", out x).Attribute("y", out y).Attribute("z", out z).Attribute("w", out w).End();
    	}
        else
        {
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "");
           string[] vals = txt.Split(',');
           x = (float) GetDouble(vals[0]);
           y = (float) GetDouble(vals[1]);
           z = (float) GetDouble(vals[2]);
           w = (float) GetDouble(vals[3]);
        }
        value = new Quaternion(x, y, z, w);
    	return this;
    }

    public XMLInStream Content(out Quaternion value)
    {
        return Content(null, out value);
    }

    // Vector4

    public XMLInStream Content(string tag, out Vector4 value)
    {
        float x, y, z, w;
        if(tag != null)
            this.Start(tag);
        if(HasAttribute("x"))
        {
            this.Attribute("x", out x).Attribute("y", out y).Attribute("z", out z).Attribute("w", out w).End();
        }
        else
        {
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "");
           string[] vals = txt.Split(',');
           x = (float) GetDouble(vals[0]);
           y = (float) GetDouble(vals[1]);
           z = (float) GetDouble(vals[2]);
           w = (float) GetDouble(vals[3]);
        }
        value = new Vector4(x, y, z, w);
        return this;
    }

    public XMLInStream Content(out Vector4 value)
    {
        return Content(null, out value);
    }

    // Rect

    public XMLInStream Content(string tag, out Rect value)
    {
        float x, y, width, height;
        if(tag != null)
            this.Start(tag);
        if(HasAttribute("x"))
        {
            this.Attribute("x", out x).Attribute("y", out y).Attribute("width", out width).Attribute("height", out height).End();
        }
        else
        {
           string txt;
           if(tag != null)
           {
            this.End();  
            Content(tag, out txt);
           }
           else
            Content(out txt);
           txt = txt.Replace("[", "").Replace("]", "");
           string[] vals = txt.Split(',');
           x = (float) GetDouble(vals[0]);
           y = (float) GetDouble(vals[1]);
           width = (float) GetDouble(vals[2]);
           height = (float) GetDouble(vals[3]);
        }
        value = new Rect(x, y, width, height);
        return this;
    }

    public XMLInStream Content(out Rect value)
    {
        return Content(null, out value);
    }

    // ContentOptional
    
    public XMLInStream ContentOptional(string tag, ref string value)
    {
    	if(Has(tag))
    	   return Content(tag, out value);
    	else
    		return this;			
    }

    public XMLInStream ContentOptional(string tag, ref bool value)
    {
		if(Has(tag))
		  return Content(tag, out value);
		else
			return this;			
    }

    public XMLInStream ContentOptional(string tag, ref Int32 value)
    {
		if(Has(tag))
		  return Content(tag, out value);
		else
			return this;			
    }

    public XMLInStream ContentOptional(string tag, ref float value)
    {
		if(Has(tag))
		  return Content(tag, out value);
		else
			return this;			
    }

    public XMLInStream ContentOptional(string tag, ref Vector2 value)
    {
        if(Has(tag))
          return Content(tag, out value);
        else
            return this;            
    }

    public XMLInStream ContentOptional(string tag, ref Vector3 value)
    {
		if(Has(tag))
		  return Content(tag, out value);
		else
			return this;			
    }

    public XMLInStream ContentOptional(string tag, ref Quaternion value)
    {
		if(Has(tag))
		      return Content(tag, out value);
		else
		      return this;			
    }

    public XMLInStream ContentOptional(string tag, ref Color value)
    {
        if(Has(tag))
              return Content(tag, out value);
        else
              return this;                      
    }

    public XMLInStream ContentOptional(string tag, ref Rect value)
    {
        if(Has(tag))
              return Content(tag, out value);
        else
              return this;                      
    }
    
    //---------------------------------------------------------------------------------
	// Attribute
	//---------------------------------------------------------------------------------
    public XMLInStream AttributeOptional(string name, ref string value)
    {
    	if(current.attributes.ContainsKey(name))
    		value = current.attributes[name];
    	return this;
    }
    public XMLInStream AttributeOptional(string name, ref bool value)
    {
    	if(current.attributes.ContainsKey(name))
    		value = FlashCompatibleConvert.ToBoolean(GetAttribute(name));
    	return this;
    }
    public XMLInStream AttributeOptional(string name, ref int value)
    {
        if(current.attributes.ContainsKey(name))
            value = FlashCompatibleConvert.ToInt32(GetAttribute(name));
        return this;
    }
    public XMLInStream AttributeOptional(string name, ref float value)
    {
        if(current.attributes.ContainsKey(name))
            value = (float) GetDouble(GetAttribute(name));
        return this;
    }
	public XMLInStream Attribute(string name, out string value)
    {
    	value = GetAttribute(name);
    	return this;
    }
    public XMLInStream Attribute(string name, out Int32 value)
    {
    	value = FlashCompatibleConvert.ToInt32(GetAttribute(name));
    	return this;
    }
    public XMLInStream Attribute(string name, out float value)
    {
    	value = (float) GetDouble(GetAttribute(name));
    	return this;
    }
    public XMLInStream Attribute(string name, out bool value)
    {
    	value = FlashCompatibleConvert.ToBoolean(GetAttribute(name));
    	return this;
    }
    private string GetAttribute(string name)
    {
    	if(current.attributes.ContainsKey(name))
    		return current.attributes[name];
    	else
        {
    		Debug.LogError("Attribute " + name + " don't exist in node " + current.tag);
            return null;
        }
    }
    
    //---------------------------------------------------------------------------------
	// List
	//---------------------------------------------------------------------------------
    public XMLInStream List(string tag, Action<XMLInStream> callback)
    {
    	foreach(XMLNode child in current.children)
    	{
    		if(child.tag == tag)
    			callback(new XMLInStream(child));
    	}
    	return this;
    }
    
    //---------------------------------------------------------------------------------
	// GetDouble Utility
	//---------------------------------------------------------------------------------
    private double GetDouble(string val)
    {
    	try
    	{
    		return FlashCompatibleConvert.ToDouble(val);
    	}
    	catch
    	{
    		if(val.Contains("."))
    			return FlashCompatibleConvert.ToDouble(val.Replace('.', ','));
    		return FlashCompatibleConvert.ToDouble(val.Replace(',', '.'));
    	}
    }
}
