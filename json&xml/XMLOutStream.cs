// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com


using System;
using System.Collections.Generic;
using UnityEngine;

//---------------------------------------------------------------------------------
// class XMLOutStream
//---------------------------------------------------------------------------------
public class XMLOutStream
{
    public XMLNode current;
    public string prolog;
    
    //---------------------------------------------------------------------------------
    // Prolog
    //---------------------------------------------------------------------------------
    public XMLOutStream Prolog(string encoding)
    {
        prolog = "<?xml version=\"1.0\" encoding=\"" + encoding + "\"?>";
        return this;
    }
        
    
    //---------------------------------------------------------------------------------
    // Serialize
    //---------------------------------------------------------------------------------
    public string Serialize()
    {
        return Serialize(false);
    }
    public string Serialize(bool newlines)
    {
        string newline = "";
        if(newlines)
            newline = "\n";
        return prolog + newline + current.Serialize(newlines, 0);
    }
    
    //---------------------------------------------------------------------------------
    // Start
    //---------------------------------------------------------------------------------
    public XMLOutStream Start(string tag)
    {
        if(current == null)
            current = new XMLNode(tag);
        else
        {
            XMLNode child = new XMLNode(tag);
            current.AddChild(child);
            current = child;
        }
        return this;
    }
    
    //---------------------------------------------------------------------------------
    // End
    //---------------------------------------------------------------------------------
    public XMLOutStream End()
    {
        if(current.parent != null)
            current = current.parent;
        return this;
    }
    
    //---------------------------------------------------------------------------------
    // Content
    //---------------------------------------------------------------------------------
    public XMLOutStream Content(string value)
    {
        current.content = value;
        return this;
    }
    public XMLOutStream Content(float value)
    {
        current.content = value.ToString();
        return this;
    }
    public XMLOutStream Content(Int32 value)
    {
        current.content = value.ToString();
        return this;
    }
    public XMLOutStream Content(bool value)
    {
        current.content = value.ToString();
        return this;
    }
    public XMLOutStream Content(string tag, string value)
    {
        return this.Start(tag).Content(value).End();
    }
    public XMLOutStream Content(string tag, bool value)
    {
        return this.Start(tag).Content(value).End();
    }
    public XMLOutStream Content(string tag, Int32 value)
    {
        return this.Start(tag).Content(value).End();
    }
    public XMLOutStream Content(string tag, float value)
    {
        return this.Start(tag).Content(value).End();
    }
    public XMLOutStream Content(Vector2 value)
    {
        this.Content("["+value.x+","+value.y+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Vector2 value)
    {
        this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).End();
        return this;
    }
    public XMLOutStream Content(Vector3 value)
    {
        this.Content("["+value.x+","+value.y+","+value.z+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Vector3 value)
    {
        this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).End();
        return this;
    }
    public XMLOutStream Content(Vector4 value)
    {
        this.Content("["+value.x+","+value.y+","+value.z+","+value.w+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Vector4 value)
    {
        this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).Attribute("w", value.w).End();
        return this;
    }
    public XMLOutStream Content(Quaternion value)
    {
        this.Content("["+value.x+","+value.y+","+value.z+","+value.w+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Quaternion value)
    {
        this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("z", value.z).Attribute("w", value.w).End();
        return this;
    }
    public XMLOutStream Content(Color value)
    {
        this.Content("["+value.r+","+value.g+","+value.b+","+value.a+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Color value)
    {
        this.Start(tag).Attribute("r", value.r).Attribute("g", value.g).Attribute("b", value.b).Attribute("a", value.a).End();
        return this;
    }
    public XMLOutStream Content(Rect value)
    {
        this.Content("["+value.x+","+value.y+","+value.width+","+value.height+"]");
        return this;
    }
    public XMLOutStream Content(string tag, Rect value)
    {
        this.Start(tag).Attribute("x", value.x).Attribute("y", value.y).Attribute("width", value.width).Attribute("height", value.height).End();
        return this;
    }
    
    //---------------------------------------------------------------------------------
    // Attribute
    //---------------------------------------------------------------------------------
    public XMLOutStream Attribute(string name, string value)
    {
        current.attributes[name] = value;
        return this;
    }
    public XMLOutStream Attribute(string name, Int32 value)
    {
        current.attributes[name] = value.ToString();
        return this;
    }
    public XMLOutStream Attribute(string name, float value)
    {
        current.attributes[name] = value.ToString();
        return this;
    }
    public XMLOutStream Attribute(string name, bool value)
    {
        current.attributes[name] = value.ToString();
        return this;
    }
}