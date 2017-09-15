using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TableData
{
	// table
	protected RawTable mTableData;
    protected uint mHash = 0;
    static Dictionary<int, uint> s_hashDictionary = new Dictionary<int, uint>();
    public static void Reset()
    {
        s_hashDictionary.Clear();
    }
    static void CheckHash(string _path, uint _hash)
    {
        int key = _path.GetHashCode();
        uint oldHash;
        if (s_hashDictionary.TryGetValue(key, out oldHash))
        {
            if (oldHash != _hash)
            {
                Application.Quit();
            }
        }
        else
            s_hashDictionary.Add(key, _hash);
    }

    public static uint ComputeHash(byte[] s)
    {
        uint h = 0;
        for (int i = s.Length - 1; i >= 0; --i)
        {
            h = (h << 5) - h + s[i];
        }
        return h;
    }
    public static uint ComputeHash(char[] s)
    {
        uint h = 0;
        for (int i = s.Length - 1; i >= 0; --i)
        {
            h = (h << 5) - h + s[i];
        }
        return h;
    }
    protected virtual uint GetHash()
    {
        return 0;
    }
    protected abstract string GetPath();
    protected abstract void _ParseData();

    public uint CheckHash()
    {
        uint hash = GetHash();

        CheckHash(GetPath(), hash);

        return hash;
    }
    public void ReadTable()
    {
        if (mTableData == null)
            mTableData = new RawTable();

        mTableData.readBinary(GetPath());
    }
    public void ParseData()
    {
        _ParseData();
        CheckHash();
        mTableData = null;
    }
}


public class TableAchievementInfo : TableData
{
    // file path
    public readonly string sFilePath        = "tAchievement";
    
    public const int RATE_ACHIEVEMENT_ID = 10000;
    public const int FB_ACHIEVEMENT_ID = 21;
    
    // data
    public class Data
    {
        public int          mAchId;
        public string       mTitle;
        public string       mDetail;
        public string       mIcon;
        public int          mType;
        public int          mConditionValue;
        public string       mBonus;
        public string       mProductId;
        
        public string GetTitle()
        {
            return TextManager.GetInstance().GetText(mTitle);
        }
        public string GetDetail()
        {
            return TextManager.GetInstance().GetText(mDetail);
        }
    }
    
    public Dictionary<int, Data> mData;

    protected override string GetPath()
    {
        return sFilePath;
    }
    
    protected override void _ParseData()
    {
        mData = new Dictionary<int, Data>();
        bool parseBonusSuccess = true;
        for (int i=0; i<mTableData._nRows; i++)
        {
            Data data = new Data();
            data.mAchId = mTableData.GetInt(i, DataDefine.Achievement_Achievement_achId);

            data.mTitle = mTableData.GetStr(i, DataDefine.Achievement_Achievement_title);
            data.mDetail = mTableData.GetStr(i, DataDefine.Achievement_Achievement_desc);
            data.mIcon = mTableData.GetStr(i, DataDefine.Achievement_Achievement_icon);
            data.mType = mTableData.GetInt(i, DataDefine.Achievement_Achievement_cond1_type);
            data.mConditionValue = mTableData.GetInt(i, DataDefine.Achievement_Achievement_cond1_value);
            data.mProductId = mTableData.GetStr(i, DataDefine.Achievement_Achievement_productId);
            
            data.mBonus = mTableData.GetStr(i, DataDefine.Achievement_Achievement_cond1_bonus);
            
            
            mData.Add(data.mAchId, data);
        }
        
        mTableData = null;
    }
}


public class TableAchievementInfo2 : TableData
{
    // file path
    public readonly string sFilePath        = "tAchievement";
    
    public const int RATE_ACHIEVEMENT_ID = 10000;
    public const int FB_ACHIEVEMENT_ID = 21;
    
    // data
    public class Data
    {
        public int          mAchId;
        public string       mTitle;
        public string       mDetail;
        public string       mIcon;
        public int          mType;
        public int          mConditionValue;
        public string       mBonus;
        public string       mProductId;
        
        public string GetTitle()
        {
            return TextManager.GetInstance().GetText(mTitle);
        }
        public string GetDetail()
        {
            return TextManager.GetInstance().GetText(mDetail);
        }
    }
    
    public Dictionary<int, Data> mData;

    protected override string GetPath()
    {
        return sFilePath;
    }
    
    protected override void _ParseData()
    {
        mData = new Dictionary<int, Data>();
        bool parseBonusSuccess = true;
        for (int i=0; i<mTableData._nRows; i++)
        {
            Data data = new Data();
            data.mAchId = mTableData.GetInt(i, DataDefine.Achievement_Achievement_achId);

            data.mTitle = mTableData.GetStr(i, DataDefine.Achievement_Achievement_title);
            data.mDetail = mTableData.GetStr(i, DataDefine.Achievement_Achievement_desc);
            data.mIcon = mTableData.GetStr(i, DataDefine.Achievement_Achievement_icon);
            data.mType = mTableData.GetInt(i, DataDefine.Achievement_Achievement_cond1_type);
            data.mConditionValue = mTableData.GetInt(i, DataDefine.Achievement_Achievement_cond1_value);
            data.mProductId = mTableData.GetStr(i, DataDefine.Achievement_Achievement_productId);
            
            data.mBonus = mTableData.GetStr(i, DataDefine.Achievement_Achievement_cond1_bonus);
            
            
            mData.Add(data.mAchId, data);
        }
        
        mTableData = null;
    }
}
