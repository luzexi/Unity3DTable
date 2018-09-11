using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TextManager
{
	public enum LANGUAGE{
		EN = 0,
		FR,
		SP,
		PT,
		IT,
		GM,
		RU,
		CN,
		JP,
		KR,
		TCN,
		NA,
	};
	
	public const int LANGUAGE_COUNT = 11;
	static string HEADERNAME = "TEXT_HEAD";
	
	// Should not changed, it is also used in the protocol: GetUserInfoRequest
	public static string[] FILENAME = new string[11]{
		"EN",
		"FR",
		"SP",
		"PT",
		"IT",
		"GM",
		"RU",
		"CH",
		"JP",
		"KR",
		"TCN",};
	
	public static bool USE_EASTEN_CHARACTER = false;
	
	public LANGUAGE currentLanguage;
	
	string[] texts;
	int textsCount;
	Dictionary<string, int> headerDictionary = new Dictionary<string, int>();
	
	private bool mHasLoadHeader;
	private LANGUAGE mCurrentLoadedLanguage;
	
	static TextManager instance;
	
	public static TextManager GetInstance()
	{
		if (instance == null)
		{
			instance = new TextManager();
		}
		return instance;
	}
	
	private TextManager()
	{
		currentLanguage = LANGUAGE.NA;
		Clear();
	}

	public int GetTextCount(){return textsCount;}

	public void Clear()
	{
		mHasLoadHeader = false;
		mCurrentLoadedLanguage = LANGUAGE.NA;
		USE_EASTEN_CHARACTER = false;
	}
	
	private void LoadHeader()
	{
		bool result = ReadTextFile(HEADERNAME);
		
		if (result)
		{
			headerDictionary.Clear();
			for (int i=0; i<textsCount; i++)
			{
#if UNITY_EDITOR
				if (headerDictionary.ContainsKey(texts[i]))
					Debug.LogError(texts[i] + " duplicated!");
#endif
				headerDictionary.Add(texts[i], i);
			}
			
			texts = null;
			textsCount = 0;
		}
		
		mHasLoadHeader = true;
	}
	
	public bool LoadLanguage(LANGUAGE _lan)
	{
		bool result = true;
		
		if (!mHasLoadHeader)
		{
			LoadHeader();
		}
		if (mCurrentLoadedLanguage != _lan && _lan != LANGUAGE.NA)
		{
			currentLanguage = _lan;
			mCurrentLoadedLanguage = _lan;
			int languageId = (int)mCurrentLoadedLanguage;
			if (languageId < LANGUAGE_COUNT)
			{
				result = ReadTextFile(FILENAME[languageId]);
			}
			else
			{
				result = false;
			}
			if (!result)
			{
				texts = null;
				textsCount = 0;
			}
		}
		
		return result;
	}
	
	public bool ReloadLanguage()
	{
		bool result = true;
		
		if (!mHasLoadHeader)
		{
			LoadHeader();
		}
		if (mCurrentLoadedLanguage != currentLanguage && currentLanguage != LANGUAGE.NA)
		{
			mCurrentLoadedLanguage = currentLanguage;
			if (mCurrentLoadedLanguage == LANGUAGE.CN 
				|| mCurrentLoadedLanguage == LANGUAGE.JP 
				|| mCurrentLoadedLanguage == LANGUAGE.KR 
				|| mCurrentLoadedLanguage == LANGUAGE.RU				 
				|| mCurrentLoadedLanguage == LANGUAGE.TCN)
			{
				USE_EASTEN_CHARACTER = true;
			}
			else
			{
				USE_EASTEN_CHARACTER = false;
			}
			
			int languageId = (int)currentLanguage;
			if (languageId < LANGUAGE_COUNT)
			{
				result = ReadTextFile(FILENAME[languageId]);
			}
			else
			{
				result = false;
			}
			if (!result)
			{
				texts = null;
				textsCount = 0;
			}
		}
		
		return result;
	}
	
	public void ReloadLanguageFromAssetBundle(AssetBundle assetBundle)
	{
		if (assetBundle == null)
			return;
		
		// load header
		if (!mHasLoadHeader)
		{
			texts = null;
			textsCount = 0;
			bool result = ReadTextFileAssetBundle(assetBundle, HEADERNAME);
			
			if (result)
			{
				headerDictionary.Clear();
				for (int i=0; i<textsCount; i++)
				{
					headerDictionary.Add(texts[i], i);
				}
				
				texts = null;
				textsCount = 0;
			}
			
			mHasLoadHeader = true;
		}
		
		if (mCurrentLoadedLanguage != currentLanguage && currentLanguage != LANGUAGE.NA)
		{
			mCurrentLoadedLanguage = currentLanguage;
			if (mCurrentLoadedLanguage == LANGUAGE.CN 
				|| mCurrentLoadedLanguage == LANGUAGE.JP 
				|| mCurrentLoadedLanguage == LANGUAGE.KR 
				|| mCurrentLoadedLanguage == LANGUAGE.RU
				|| mCurrentLoadedLanguage == LANGUAGE.TCN)
			{
				USE_EASTEN_CHARACTER = true;
			}
			else
			{
				USE_EASTEN_CHARACTER = false;
			}
			
			// load current language
			int languageId = (int)currentLanguage;
			bool result = ReadTextFileAssetBundle(assetBundle, FILENAME[languageId]);
			if (!result)
			{
				texts = null;
				textsCount = 0;
			}
		}
	}
	
	public void SetCurLanguage(LANGUAGE _lan)
    {
        if (_lan != currentLanguage)
        {
            currentLanguage = _lan;
#if UNITY_EDITOR
            ReloadLanguage();
#endif
        }
    }
	
	public string GetText(int _id)
	{
		if (texts!=null && _id >=0 && _id < textsCount)
		{
			return texts[_id];
		}
		else
		{
			return string.Empty;
		}
	}
	
	public string GetText(string _key)
	{
		if (_key == null || _key == string.Empty) return string.Empty;
		int _id = -1;
		if(!headerDictionary.TryGetValue(_key, out _id))
		{
			return _key;
		}
		return GetText(_id);
	}
	
	public Dictionary<string, int> GetTextHeader()
	{
		return headerDictionary;
	}
	
	bool ReadTextFile(string filename)
	{
		filename = "dat/" + filename;
		TextAsset binaryStream = (TextAsset)Resources.Load(filename);
        MemoryStream ms = new MemoryStream(binaryStream.bytes);
        BinaryReader br = new BinaryReader(ms, Encoding.Unicode);

		int rows = 0;
        rows = br.ReadInt32();        
		if(rows == 0)
		{
			Debug.LogError("Error reading tablesize  "+filename);
			return false;
		}
		
		textsCount = rows;
		texts = new string[textsCount];
		for (int i = 0; i < textsCount; i++)
		{
			texts[i] = br.ReadString();
			//Debug.LogError ("filename  " + texts[i]);
		}
#if !UNITY_METRO
		br.Close();
#endif		
		return true;
	}
	
	
	bool ReadTextFileAssetBundle(AssetBundle assetBundle, string filename)
	{
		TextAsset binaryStream = (TextAsset)assetBundle.LoadAsset(filename);
        MemoryStream ms = new MemoryStream(binaryStream.bytes);
        BinaryReader br = new BinaryReader(ms, Encoding.Unicode);
		
		int rows = 0;
        rows = br.ReadInt32();        
		if(rows == 0)
		{
			Debug.LogError("Error reading tablesize  "+filename);
			return false;
		}
		
		textsCount = rows;
		texts = new string[textsCount];
		for (int i = 0; i < textsCount; i++)
		{
			texts[i] = br.ReadString();
		}
#if !UNITY_METRO
		br.Close();
#endif
		
		return true;
	}
}