// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using System.Collections.Generic;
using System;

//---------------------------------------------------------------------------------
// class XMLXMLNode
//---------------------------------------------------------------------------------
public class XMLNode
{
    public XMLNode parent;
    public string tag = "";
    public List<XMLNode> children = new List<XMLNode>();
    public string content = "";
    public Dictionary<string, string> attributes = new Dictionary<string, string>();
    
    //---------------------------------------------------------------------------------
	// Constructor
	//---------------------------------------------------------------------------------
    public XMLNode(string tagArg)
	{
		tag = tagArg;
	}
    
    //---------------------------------------------------------------------------------
	// Serialize
	//---------------------------------------------------------------------------------
    public string Serialize(bool newlines, int spacesNumber)
    {
    	string newline = "";
    	string spaces = "";
    	string contentSpaces = "";
    	if(newlines)
    	{
    		contentSpaces += "  ";
    	 	for(int i = 0; i < spacesNumber; ++i)
    			spaces += " ";
    		newline = "\n";
    	}
    	string result = spaces + "<" + tag;
    	foreach(string name in attributes.Keys)
    		result += " " + name + "=\"" + attributes[name] + "\"";
    	result += ">";
    	foreach(XMLNode child in children)
    		result += newline + child.Serialize(newlines, spacesNumber + 2);
    	if(content != "")
    		result += content;
    	result += newline;
    	result += spaces + "</" + tag + ">";
     	return result;
    }
    
    //---------------------------------------------------------------------------------
	// AddChild
	//---------------------------------------------------------------------------------
	public void AddChild(XMLNode child)
	{
		child.parent = this;
		children.Add(child);
	}
}