using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class PracticeManager {
    private string serverlink = "http://140.115.126.167/translate/";
    HttpWebRequest request;
    Xmlprocess xmlprocess;
    public Dictionary<int, string> E_vocabularyDic = new Dictionary<int, string>();//key=單字ID,val=英文單字
    public Dictionary<int, string> T_vocabularyDic = new Dictionary<int, string>();//key=單字ID,val=英文中譯
    public Dictionary<int, string> EM_vocabularyDic = new Dictionary<int, string>();//key=單字ID,val=英文意思
    public Dictionary<int, string> EM_vocabularyDic2 = new Dictionary<int, string>();//key=單字ID,val=英文意思
    public Dictionary<int, string> UsernameDic = new Dictionary<int, string>();//key=ID,val=使用者名字
    public Dictionary<int, string> HighscoreDic = new Dictionary<int, string>();//key=ID,val=使用者練習最高分
    public Dictionary<int, string> OptionDic = new Dictionary<int, string>();//key=單字ID,val=選項
    public Dictionary<int, string> OptionDic2 = new Dictionary<int, string>();//key=單字ID,val=選項
    public Dictionary<int, string> OptionDic3 = new Dictionary<int, string>();//key=單字ID,val=選項

    public PracticeManager() {
        xmlprocess = new Xmlprocess();
    }

    public IEnumerator LoadVocabulary(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] tmp, tmp2;
        if (reg.error == null)
        {
            tmp = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                tmp2 = tmp[i].Split(',');

                tmp2[0] = tmp2[0].Replace('/', ',');
                E_vocabularyDic.Add(i, tmp2[0]);//題目
                T_vocabularyDic.Add(i, tmp2[1]);//答案
                tmp2[2] = tmp2[2].Replace('/', ',');
                EM_vocabularyDic.Add(i, tmp2[2]);//中譯
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

     public IEnumerator LoadMeansVocabulary(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] tmp, tmp2;
        if (reg.error == null)
        {
            tmp = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                tmp2 = tmp[i].Split(',');

                E_vocabularyDic.Add(i, tmp2[0]);//題目id
                tmp2[1] = tmp2[1].Replace('/', ',');
                T_vocabularyDic.Add(i, tmp2[1]);//題目
                EM_vocabularyDic.Add(i, tmp2[2]);//答案
                
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

    public IEnumerator LoadConversionVocabulary(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] tmp, tmp2;
        if (reg.error == null)
        {
            
            tmp = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                tmp2 = tmp[i].Split(',');

                E_vocabularyDic.Add(i, tmp2[0]);//題目id
                tmp2[1] = tmp2[1].Replace('/', ',');
                T_vocabularyDic.Add(i, tmp2[1]);//題目
                EM_vocabularyDic.Add(i, tmp2[2]);//答案
                tmp2[3] = tmp2[3].Replace('/', ',');
                EM_vocabularyDic2.Add(i, tmp2[3]);//英文題目
                
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

    ///<summary>
    ///將題目用索引值亂數重新排序
    ///</summary>
    public int[] randomQuestion() {

        int randomindex = 0, dicLength = E_vocabularyDic.Count;
        int[] i_indexRand = new int[dicLength];
        for (int i = 0; i < dicLength; i++)
        {
            i_indexRand[i] = i;
        }
        int tmp =0;
        //亂數排列key(0~dicLength)
        for (int i = 0; i < i_indexRand.Length; i++)
        {
            randomindex = UnityEngine.Random.Range(i, i_indexRand.Length- 1);
            tmp = i_indexRand[randomindex];
            i_indexRand[randomindex] = i_indexRand[i];
            i_indexRand[i] = tmp;
        }
        return i_indexRand;
    }

    ///<summary>
    ///根據選項數量進行n次亂數排列，randomOption[0]為正解(correctID)
    ///</summary>

    public IEnumerator getOption(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] option;
        if (reg.error == null)
        {
            option = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < option.Length - 1; i++)
            {
                
                OptionDic.Add(i, option[i]);//選項
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

    public IEnumerator getMeansOption(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] option,tmp2;
        if (reg.error == null)
        {
            option = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < option.Length - 1; i++)
            {
                
                tmp2 = option[i].Split(',');
                OptionDic.Add(i,  tmp2[0]);
                // optionList.Add(new List<string>(){tmp2[0], tmp2[1], tmp2[3]});
                       
                OptionDic2.Add(i,  tmp2[1]);
                OptionDic3.Add(i,  tmp2[2]);//選項
            
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

    public int[] randomOption(int optionCount,int correctID)
    {
        int randomindex = 0, dicLength = OptionDic.Count;
        int[] i_indexRand = new int[dicLength];
        for (int i = 0; i < dicLength; i++)
        {
            if( T_vocabularyDic[correctID] == OptionDic[i]) //將正確答案ID移到陣列第一個
            {
                i_indexRand[0] = i;
                i_indexRand[i] = 0;
            }
            else
            {
                i_indexRand[i] = i;
            }
        }
        //將正確答案ID剔除後,進行optionCount-1次亂數排序
        int tmp = 0;
        for (int i = 1; i < optionCount; i++)
        {
            randomindex = UnityEngine.Random.Range(i, i_indexRand.Length - 1);
            tmp = i_indexRand[randomindex];
            i_indexRand[randomindex] = i_indexRand[i];
            i_indexRand[i] = tmp;
        }
        return i_indexRand;
    }

    public IEnumerator LoadRank(string fileName)
    {
        WWWForm phpform = new WWWForm();
        phpform.AddField("action", fileName);
        WWW reg = new WWW(serverlink + fileName, phpform);
        yield return reg;
        string[] tmp, tmp2;
        if (reg.error == null)
        {
            tmp = reg.text.Split('|');//最後一個是空的
            for (int i = 0; i < tmp.Length - 1; i++)
            {
                tmp2 = tmp[i].Split(',');

                UsernameDic.Add(i, tmp2[0]);//使用者名字
                HighscoreDic.Add(i, tmp2[1]);//使用者在練習中的最高分
                // Debug.Log("rank name msg : " + i + UsernameDic[i]);
                // Debug.Log("rank score msg" + HighscoreDic[i]);
                // Debug.Log("rank msg" + i);
            }
        }
        else
        {
            Debug.Log("error msg" + reg.error);
        }
    }

    /// <summary>
    /// 取得單字預習次數
    /// </summary>
    public bool getReviewCount()
    {
       return xmlprocess.getReviewCount();
    }
    /// <summary>
    /// 新增回合單字練習紀錄
    /// </summary>
    public void startPractice() {
        xmlprocess.createPracticeRecord();
    }
    /// <summary>
    /// 更新回合單字練習不同題型的成績紀錄
    /// </summary>
    public void setPracticeTypeScore(string TypeName,int score)
    {
        xmlprocess.setPracticeTypeScoreRecord(TypeName,score);
    }

        /// <summary>
    /// 更新練習主題與難度 0:easy;1:hard
    /// </summary>
    public void setPracticeInfo(string theme,string level)
    {
        xmlprocess.setPracticeInfo(theme,level);
    }

    /// <summary>
    /// 更新單字學習與完畢次數
    /// </summary>
    /// <param name="eventname">要更新的attribute</param>
    public string setPracticeCount(string eventname) {
        return xmlprocess.setPracticeCount(eventname);
    }

    /// <summary>
    /// 回合單字練習的成績紀錄 0:highscore;1:improve
    /// </summary>
    public string[] setPracticeScore(int score)
    {
        return xmlprocess.setPracticeScoreRecord(score);
    }

    /// <summary>
    /// 一回合答對題數
    /// </summary>
    public string setPracticeCorrect(int correctCount,int worngCount)
    {
        return xmlprocess.setPracticeCorrect(correctCount, worngCount);
    }

    /// <summary>
    /// 連續答對題數
    /// </summary>
    public string setPracticeMaxCorrect(int max_correctCount)
    {
        return  xmlprocess.setPracticeMaxCorrect(max_correctCount);
    }

}
