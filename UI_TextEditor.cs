using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UI_Text))]
public class UI_TextEditor : Editor
{
	// private int mTextID;
	private string mTextStr;

	void OnEnable ()
	{
		serializedObject.Update();
		UI_Text text = target as UI_Text;
		// this.mTextID = text.mTextID;
		this.mTextStr = text.mTextStr;
	}

	// [MenuItem("GameObject/UI/UIText")]
	// static void CreateUIText()
	// {
	// 	Type[] types = new Type[]{typeof(Text),typeof(UI_Text)};
	// 	GameObject obj = EditorUtility.CreateGameObjectWithHideFlags("Text",HideFlags.None,types);
	// 	//
	// }

	// [InitializeOnLoadMethod]
	// static void StartInitializeOnLoadMethod()
	// {
	// 	EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
	// }

	// static void OnHierarchyGUI(int instanceID, Rect selectionRect)
	//  {
	//  	 Event evt = Event.current;
	//  	 // Debug.LogError("kkk " + evt.button + " " + evt.type);
	//      if (evt != null && selectionRect.Contains(evt.mousePosition)
	//          && evt.button == 1 && evt.type == EventType.MouseDown)
	//      {
	//      	Debug.LogError("in");
	//          GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
	// 		 //这里可以判断selectedGameObject的条件
	//          if (selectedGameObject)
	//          {
	//          	Debug.LogError("in2");
	// 			Vector2 mousePosition = Event.current.mousePosition;
 
	// 			EditorUtility.DisplayPopupMenu(new Rect(mousePosition.x, mousePosition.y, 0, 0), "GameObject/",null);
	//             evt.Use();
	//          }			
	//      }
	//  }

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		UI_Text text = target as UI_Text;
		text.mTextStr = EditorGUILayout.TextField("Text",text.mTextStr);
		if (this.mTextStr != text.mTextStr)
		{
			this.mTextStr = text.mTextStr;
			// TextManager.instance.LoadLanguage(TextManager.CN);
			// text.SetText(text.mTextStr);
		}
		if(GUILayout.Button("Change text"))
		{
			TextManager.instance.LoadLanguage(TextManager.LANGUAGE.CN);
			text.SetText(text.mTextStr);
		}
		serializedObject.ApplyModifiedProperties();
	}
}



