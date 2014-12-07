// Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

using UnityEngine;
using System;
using System.Collections;

//---------------------------------------------------------------------------------
// class XMLParser
//---------------------------------------------------------------------------------
public class XMLParser
{
  private FlashCompatibleTextReader reader;
	private Stack elements;
	private XMLNode currentElement;

	//---------------------------------------------------------------------------------
  	// Constructor
  	//---------------------------------------------------------------------------------
	public XMLParser()
	{
		elements = new Stack();
		currentElement = null;
	}

  	//---------------------------------------------------------------------------------
  	// Parse
  	//---------------------------------------------------------------------------------
  	public XMLNode Parse( string txt )
  	{
  		FlashCompatibleTextReader reader = new FlashCompatibleTextReader(txt);
  		return Parse(reader);
  	}
	public XMLNode Parse(FlashCompatibleTextReader reader)
	{
		this.reader = reader;

		//check empty string
		if(reader.Peek() == -1)
			return null;

		// skip xml declaration or DocTypes
		SkipPrologs();

		//check empty string
		if(reader.Peek() == -1)
			return null;
		
		while (true)
		{
			SkipWhitespace();
			int index;
			string tagName;

			// remove the prepend or trailing white spaces
			bool startingBracket = (char)reader.Peek() == '<';
			string currentTag = ReadTag(startingBracket).Trim();
			if(currentTag.StartsWith("<!"))
			{
				// Nothing to do, it's a comment
			}
			else if (currentTag.StartsWith("</"))
			{
				// close tag
				tagName = currentTag.Substring(2, currentTag.Length-3);

				// no open tag
				if (currentElement == null)
				{
					Debug.LogError("Got close tag '" + tagName +
							"' without open tag.");
					return null;
				}

				// close tag does not match with open tag
				if (tagName != currentElement.tag)
				{
					Debug.LogError("Expected close tag for '" +
				    			currentElement.tag + "' but got '" +
							 tagName + "'.");
					return null;
				}

				if (elements.Count == 0)
					return currentElement;
				else
					currentElement = (XMLNode)elements.Pop();

			}
			else
			{
				// open tag or tag with both open and close tags
				index = currentTag.IndexOf('"');
				if(index < 0)
					index = currentTag.IndexOf('\'');

				if (index < 0) {
					// tag with no attributes
					if (currentTag.EndsWith("/>"))
					{
						// close tag as well
						tagName = currentTag.Substring(1, currentTag.Length-3).Trim();
						currentTag = "/>";
					}
					else 
					{
						// open tag
						tagName = currentTag.Substring(1, currentTag.Length-2).Trim();
						currentTag = "";
					}
				}
				else
				{
					// tag with attributes
					int endtagIndex = currentTag.IndexOf(" ");
					tagName = currentTag.Substring(1, endtagIndex).Trim();
					currentTag = currentTag.Substring(endtagIndex+1);
				}

				// create new element
				XMLNode element = new XMLNode(tagName);

				// parse the attributes
				bool isTagClosed = false;
				while (currentTag.Length > 0)
				{
					// remove the prepend or trailing white spaces
					currentTag = currentTag.Trim();

					if (currentTag == "/>")
					{
						// close tag
						isTagClosed = true;
						break;
					} 
					else if (currentTag == ">")
					{
						// open tag
						break;
					}

					index = currentTag.IndexOf("=");
					if (index < 0)
					{
						Debug.LogError("Invalid attribute for tag '" +
								tagName + "'.");
						return null;
					}

					// get attribute name
					string attributeName = currentTag.Substring(0, index);
					currentTag = currentTag.Substring(index+1);

					// get attribute value
					string attributeValue;
					bool isQuoted = true;
					if (currentTag.StartsWith("\""))
					{
						index = currentTag.IndexOf('"', 1);
					} 
					else if (currentTag.StartsWith("'"))
					{
						index = currentTag.IndexOf('\'', 1);
					}
					else 
					{
						isQuoted = false;
						index = currentTag.IndexOf(' ');
						if (index < 0) {
							index = currentTag.IndexOf('>');
							if (index < 0) {
								index = currentTag.IndexOf('/');
							}
						}
					}

					if (index < 0)
					{
						Debug.LogError("Invalid attribute for tag '" +
								tagName + "'.");
						return null;
					}

					if (isQuoted)
						attributeValue = currentTag.Substring(1, index -1);
					else
						attributeValue = currentTag.Substring(0, index - 1);

					// add attribute to the new element
					element.attributes[attributeName]= attributeValue;

					currentTag = currentTag.Substring(index+1);
				}

				// read the text between the open and close tag
				if (!isTagClosed)
					element.content = ReadText();

				// add new element as a child element of
				// the current element
				if (currentElement != null)
					currentElement.AddChild(element);

				if (!isTagClosed)
				{
					if (currentElement != null)
						elements.Push(currentElement);

					currentElement = element;
				}
				else if (currentElement == null)
				{
					// only has one tag in the document
					return element;
				}
			}
		}
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
  	// SkipProlog
  	// the first "<" has been read by SkipPrologs
  	//---------------------------------------------------------------------------------
	private void SkipProlog()
	{
		// skip "?" or "!"
		reader.Read();

		while (true)
		{
			int next = reader.Peek();

			if (next == '>')
			{
				reader.Read();
				break;
			} else if (next == '<')
			{
				// nesting prolog
				SkipProlog();
			} else
			{
				reader.Read();
			}
		}
	}

  	//---------------------------------------------------------------------------------
  	// SkipPrologs
  	// returns having read the first '<'
  	//---------------------------------------------------------------------------------
	private void SkipPrologs()
	{
		while (true) {
			SkipWhitespace();

			int next = reader.Read();
			if(next == -1)
				return;
			if (((char)next) != '<')
			{
				Debug.LogError("Expected '<' but got '" + next + "'.");
				return;
			}
			int afterNext = reader.Peek();
			if(next == -1)
				return;
			if ((((char)afterNext) == '?') || (((char)afterNext) == '!'))
			{
				SkipProlog();
			}
			else
			{
				break;
			}
		}
	}
  	//---------------------------------------------------------------------------------
  	// ReadTag
  	// includes the brackets
  	//---------------------------------------------------------------------------------
		private string ReadTag(bool startingBracket)
		{
			SkipWhitespace();
	
			string result = "";
	
			char next = (char) reader.Peek();
			if(startingBracket && next != '<')
			{
				Debug.LogError("Expected < but got " + next);
				return null;
			}
			else if(! startingBracket)
				result += "<";
	
			result += ((char)reader.Read()).ToString();
			while(reader.Peek() != '>')
				result += ((char)reader.Read()).ToString();

			result += ((char)reader.Read()).ToString();
	
			return result;
		}

  	//---------------------------------------------------------------------------------
  	// ReadText
  	//---------------------------------------------------------------------------------
	private String ReadText()
	{
		string result = "";

		while (true)
		{
			int peek = reader.Peek();
			if(peek == -1)
				return result.Trim();
			if( ((char)peek) != '<')
				result += ((char)reader.Read()).ToString();
			else
				return result.Trim();
		}
	}
}
