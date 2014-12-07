
About XML-JSON Serialization
============================

XML-JSON Serialization provides pure C# serializers and deserializers for XML and JSON.

These serialization classes are tiny, compatible with Flash, and don't depend on System.dll.

The available classes are:

1.  XMLInStream: deserialize XML strings
2.  XMLOutStream: serialize to XML
3.  JSONInStream: deserialize JSON strings 
4.  JSONOutStream: serialize to JSON
5.  JSONSerializer: serialize classes from/to JSON using C# Reflection (not compatible with Flash)
6.  XMLSerializer: serialize classes from/to XML using C# Reflection (not compatible with Flash)
7.  EscapeString: escape/unescape strings for web browser communication

You can find an HTML version of this readme with syntax highlighting here:
http://www.aworldforus.com/xml-json-serialization/


Contact
=======

To contact us: info@aworldforus.com

XML Deserialization API
==================================================

To deserialize this XML string: ”<root><foo>3</foo><bar>baz</bar></root>”

int foo;
string bar;
XMLInStream stream = new XMLInStream("<root><foo>3</foo><bar>baz</bar></root>");
stream.Content("foo", out foo);
stream.Content("bar", out bar);

You can also chain the calls

int foo;
string bar;
XMLInStream stream = new XMLInStream("<root><foo>3</foo><bar>baz</bar></root>");
stream.Content("foo", out foo)
      .Content("bar", out bar);

To enter a child element use Start and End

int bar, baz;
XMLInStream stream = new XMLInStream("<root><foo><bar>2</bar></foo><baz>3</baz></root>");
stream.Content("baz", out baz)
      .Start("foo")
          .Content("bar", out bar)
      .End();

You can read an attribute

int foo;
int bar;
XMLInStream stream = new XMLInStream("<root><foo bar=\"1\">2</foo></root>");
stream.Start("foo")
         .Attribute("bar", out bar)
     .End();

You can use AttributeOptional and ContentOptional if elements are not mandatory

int bar = -1;
int baz = -1;
XMLInStream stream = new XMLInStream("<root><foo bar=\"1\"><baz>2</baz></foo></root>");
stream.Start("foo")
          .AttributeOptional("bar", out bar)
          .ContentOptional("baz", out baz)
      .End();

You can use these types in the Content, Attribute, ContentOptional and AttributeOptional functions:

1. string
2. int
3. float
4. bool
5. Vector2
6. Vector3
7. Quaternion
8. Color
9. Rect

Complex data structures are serialized with attributes: <position x=”1″ y=”2″ z=”3″/>
They can also be deserialized from arrays like: <position>[1,2,3]</position>

You can test that a child exists with the Has function

XMLInStream stream = new XMLInStream("<root><foo><bar>1</bar></foo></root>");
if(stream.Has("foo")) {
    int bar;
    stream.Start("foo").Content("bar", out bar);
}

You can deserialize list of children with the same tag with the List function:

XMLInStream stream = new XMLInStream("<root><foo>1</foo><foo>2</foo></root>");
stream.List("foo", delegate(XMLInStream fooStream){
    int value;
    fooStream.Content(out value);
});

XML Serialization API
==================================================

To write XML, use the same methods on the OutStream class:

XMLOutStream stream = new OutStream();
stream.Start("root")
           .Content("foo", 2)
           .Start("bar")
              .Attribute("baz", 1)
              .Content("hello")
           .End()
       .End();
 
string xml = stream.Serialize();

It makes this XML string:

<root>
  <foo>2</foo>
  <bar baz="1">
    hello
  </bar>
</root>

JSON API
==================================================

The JSON API is very similar to the XML API. The main differences are:

1. There are no attributes
2. The List method has a different semantics

A simple example:

json = {a: 2, b: {c: 1}}

int a, c;
JSONInStream stream = new JSONInStream(json);
stream.Content("a", out a)
      .Start("b")
          .Content("c", out c)
      .End();

To deserialize a list use the List method:

json = {a:["hello", "world"]}

JSONInStream stream = new JSONInStream(json);
stream.List("a", delegate(int index, JSONInStream elementStream){
     string value;
     elementStream.Content(out value);
});

To serialize a list:

JSONOutStream stream = new JSONOutStream(json);
stream.List("a")
          .Content(0, "hello")
          .Content(1, "world")
      .End();

JSONSerializer and XMLSerializer API
==================================================

JSONSerializer and XMLSerializer provide direct serializers to C# classes using Reflection. These classes don’t work with Flash. The methods are:

1. string JSONSerializer.Serialize<T>(T obj)
2. T JSONSerializer.Deserialize<T>(string s)
3. string XMLSerializer.Serialize<T>(T obj)
4. T XMLSerializer.Deserialize<T>(string s)

How to use it:

private class SampleClassA
{
  public string s;
  public float  f;
  public Vector3 v;
  public List<Color> c;
  public SampleClassB sb;
}

private class SampleClassB
{
  public Quaternion q;
  public Rect r;
  public Vector2 v;
}

public static void TestSimpleSerialization()
{
  SampleClassB sb = new SampleClassB();
  sb.q = new Quaternion(1, 2, 3, 4);
  sb.r = new Rect(0,10,20,30);
  sb.v = new Vector2(1.1f,2.2f);
  SampleClassA sa = new SampleClassA();
  sa.s = "hello";
  sa.f = 3.2f;
  sa.v = Vector3.up;
  sa.c = new List<Color>();
  sa.sb = sb;
  sa.c.Add(Color.red);
  sa.c.Add(Color.green);

  string s = JSONSerializer.Serialize<SampleClassA>(sa);

  SampleClassA sa2 = JSONSerializer.Deserialize<SampleClassA>(s);
}

EscapeString API
==================================================

To escape or unescape use the following functions:

1. string EscapeString.Escape(string text)
2. string EscapeString.Unescape(string text)

These functions encode strings like encodeURI does in javascript, with apostrophe characters ‘ encoded into %27.

Performance
==================================================

Some simple tests in the Unity Editor using a dual core i5 2.3Ghz Macbook Pro:

Parsing time for a Very small XML fragment typical of server or browser communications (7 lines, 126 characters): ~0.15ms
Parsing time for a simple XML file (214 lines, 4.9k characters): ~6ms
Parsing time for a unusually big XML file (4672 lines, 163k characters): ~200ms

Examples in action
==================================================

Check the Examples scene in the Examples directory to see how to use these classes in action.


Copyright
==================================================

Copyright (c) 2012 All Right Reserved, http://www.aworldforus.com

