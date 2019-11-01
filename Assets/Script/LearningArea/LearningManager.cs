using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class LearningManager {
    private string serverlink = "http://140.115.126.167/translate/";
    HttpWebRequest request;
    Xmlprocess xmlprocess;
    public Dictionary<int, string> Am_speciesDic = new Dictionary<int, string>();//key=單字ID,val=英文單字
    public Dictionary<int, string> Am_defineDic = new Dictionary<int, string>();//key=單字ID,val=英文中譯
    public Dictionary<int, string> Am_contentDic = new Dictionary<int, string>();//key=單字ID,val=英文意思


    public LearningManager() {
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

                Am_speciesDic.Add(i, tmp2[0]);//類別
                tmp2[1] = tmp2[1].Replace('/', ',');
                Am_defineDic.Add(i, tmp2[1]);//定義
                tmp2[2] = tmp2[2].Replace('/', ',');
                Am_contentDic.Add(i, tmp2[2]);//例子
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

        int randomindex = 0, dicLength = Am_speciesDic.Count;
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

    public int[] randomOption(int optionCount,int correctID)
    {
        int randomindex = 0, dicLength = Am_defineDic.Count;
        int[] i_indexRand = new int[dicLength];
        for (int i = 0; i < dicLength; i++)
        {
            //將正確答案ID移到陣列第一個
            if (i == correctID)
            {
                i_indexRand[0] = correctID;
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

    /// <summary>
    /// 取得單字預習次數
    /// </summary>
    public bool getReviewCount()
    {
       return xmlprocess.getReviewCount();
    }
   

    /// <summary>
    /// 更新單字學習與完畢次數
    /// </summary>
    /// <param name="eventname">要更新的attribute</param>
    public string setLearningCount(string eventname) {
        return xmlprocess.setLearningCount(eventname);
    }

}
