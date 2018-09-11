
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//  UI_FontNum.cs
//  Author: Lu Zexi
//  2015-03-31


//font num
[AddComponentMenu("UI/UI Text")]
[RequireComponent (typeof(Text))]
public class UI_Text : MonoBehaviour
{
    public string mTextStr;
    private Text mText;

    void Awake()
    {
        this.mText = this.GetComponent<Text>();
		SetText (this.mTextStr);
    }

	public void SetText(string _str)
	{
        if(this.mText == null)
        {
            this.mText = this.GetComponent<Text>();
        }
		string txt = TextManager.instance.GetText(_str);
		Debug.Log("txt " + txt + " " +_str);
		if(txt != null && txt != string.Empty)
		{
			this.mText.text = txt;
		}
		else
		{
			this.mText.text = _str;
		}
	}
}

