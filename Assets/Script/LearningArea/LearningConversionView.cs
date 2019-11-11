using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LearningConversionView : MonoBehaviour {

    public GameObject UI_showAnsfeedback, UI_ShowMes,score;
    Text text_score;
    AudioSource ClickBtn;
    int vocabularyID,totalQuesNum, C_correctNum,max_correctNum, correctNum, wrongNum;
    static int p_score;
    public static bool showAchieve;
    string []achievementState;
    LearningManager pm;

    #region ReviewVocabulary UI
    Text text_Define,text_Cotent,text_Species;
    Button btn_pronun,btn_pre, btn_next, btn_gotonext, btn_skip;
    AudioSource VocabularyAS;
    #endregion

    #region PracticeMuitiselect UI
    Text text_totalQues,text_Question,text_Question_ch;
    Button[] btn_option;
    int quesID,correctOption;//quesID:題數;correctOption:正確的選項編號
    int[] randomQuestion, randomOption;//隨機排列後的題目與選項
    #endregion

    #region PracticeCompose UI
    Button btn_alphabet,btn_clear,btn_submit;
    Text text_quescontent;
    GameObject[] CollectBtnObj;
    Color c_original;
    string userAns;
    #endregion

    #region Result UI
    Text text_correct,text_wrong;
    #endregion

    private void Awake()
    {
        achievementState = new string[5];//學習區獎章數量
        pm = new LearningManager();
        StartCoroutine(showReviewVocabulary());
    }

    void Start () {
        UI_ShowMes.SetActive(true);
        UI_ShowMes.GetComponentInChildren<Text>().text = "載入中...";
        text_score = score.GetComponentsInChildren<Text>()[0];
        ClickBtn = GetComponentsInChildren<AudioSource>()[0];
        p_score = 0;
        vocabularyID = 0;
        C_correctNum = -1;//當前連續答對題數
        max_correctNum = -1;//最大連續答對數
        correctNum = 0;//累計正確題數
        wrongNum = 0;//累計錯誤題數
        totalQuesNum = 10;//練習題數
        showAchieve = false;
        UIManager.Instance.CloseAllPanel();
    }
    #region Review function

    void showReviewUI()
    {
        UIManager.Instance.ShowPanel("P_ReviewUI");

        text_Species = GetComponentsInChildren<Text>()[0];
        text_Define = GetComponentsInChildren<Text>()[1];
        text_Cotent = GetComponentsInChildren<Text>()[2];
        btn_pronun = GetComponentsInChildren<Button>()[1];
        btn_pre = GetComponentsInChildren<Button>()[2];
        btn_next = GetComponentsInChildren<Button>()[3];
        btn_gotonext = GetComponentsInChildren<Button>()[4];
        btn_skip = GetComponentsInChildren<Button>()[5];
        btn_gotonext.gameObject.SetActive(false);
        pm.setLearningCount("review_conversion_count");//更新單字學習次數
        btn_skip.gameObject.SetActive(false);
        // if (!pm.getReviewCount())
        // {
        //     btn_skip.gameObject.SetActive(false);
        // }
        // else
        // {
        //     btn_skip.gameObject.SetActive(false);
        // }
        VocabularyAS = GetComponentsInChildren<AudioSource>()[2];
        btn_gotonext.onClick.AddListener(reviewEnd);
        btn_pronun.onClick.AddListener(delegate () { playAudio(vocabularyID); });
        btn_pre.onClick.AddListener(delegate () { changeVocabularyID(-1); });
        btn_next.onClick.AddListener(delegate () { changeVocabularyID(1); });
        btn_skip.onClick.AddListener(skip);

    }


    IEnumerator showReviewVocabulary(){
        StartCoroutine(pm.LoadVocabulary("loadConversionHangout.php"));
        yield return new WaitForSeconds(0.5f);
        UI_ShowMes.SetActive(false);
        showReviewUI();
        // showPracticeUI();
        changeVocabularyID(0);
    }

    void changeVocabularyID(int count) {
        ClickBtn.Play();
        if (vocabularyID >= 0 && (vocabularyID + count)>=0)
        {
            if (pm.Am_speciesDic.ContainsKey(vocabularyID + count))
            {
                vocabularyID += count;
                playAudio(vocabularyID);
                btn_gotonext.gameObject.SetActive(false);
                // text_English.text = pm.E_vocabularyDic[vocabularyID];
                // text_Translation.text = pm.T_vocabularyDic[vocabularyID]+"\n" + pm.EM_vocabularyDic[vocabularyID];
                text_Species.text = pm.Am_speciesDic[vocabularyID];
                text_Define.text = pm.Am_defineDic[vocabularyID];
                text_Cotent.text = pm.Am_contentDic[vocabularyID];
            }
            else
            {
                btn_gotonext.gameObject.SetActive(true);
            }
        }
    }

    void playAudio(int _vocabularyID) {
        ClickBtn.Play();
        VocabularyAS.clip = Resources.Load("Sound/" + pm.Am_speciesDic[_vocabularyID], typeof(AudioClip)) as AudioClip;
        VocabularyAS.Play();
    }
    void reviewEnd()
    {
        string _state = pm.setLearningCount("learning_conversion_count");//更新單字瀏覽完畢次數
        ClickBtn.Play();
        StartCoroutine(LearningEnd());
        SceneManager.LoadScene("Practice_Level");
        UIManager.Instance.CloseAllPanel();
        // showPracticeUI();
    }

    void skip()
    {
        ClickBtn.Play();
        SceneManager.LoadScene("Learning_Level");
        UIManager.Instance.CloseAllPanel();
        // showPracticeUI();
    }
    #endregion

    void showResultUI()
    {
        score.SetActive(false);
        text_score = GetComponentsInChildren<Text>()[0];
        text_correct = GetComponentsInChildren<Text>()[1];
        text_wrong = GetComponentsInChildren<Text>()[2];
        text_score.text = p_score.ToString();
        text_correct.text = correctNum.ToString();
        text_wrong.text = wrongNum.ToString();
        StartCoroutine(LearningEnd());
    }

    IEnumerator LearningEnd() {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.TogglePanel("P_ResultUI",false);
        
        yield return new WaitForSeconds(1f);
            showAchieve = true;
            SceneManager.LoadScene("Practice_Level");
            UIManager.Instance.CloseAllPanel();

    }


    IEnumerator showfeedback(int _state,int pracNum)//pracNum代表練習題型的編號，0:選擇、1:拼字
    {
        GameObject fb  = Instantiate(UI_showAnsfeedback);//Options
        fb.transform.SetParent(this.gameObject.transform);
        fb.transform.localPosition = Vector3.zero;
        fb.transform.localScale = Vector3.one;
        Image img_correct = fb.GetComponentsInChildren<Image>()[0];
        Image img_wrong = fb.GetComponentsInChildren<Image>()[1];
        img_correct.gameObject.SetActive(false);
        img_wrong.gameObject.SetActive(false);
        if (_state == 0)//答對
        {
            C_correctNum++;
            correctNum++;
            img_correct.gameObject.SetActive(true);
            if (pracNum == 0)
            {
                p_score += (int)(p_score * 0.15 + max_correctNum * 0.3 + correctNum * 0.15) + 1;
                text_score.text = p_score.ToString();
            }
            else if (pracNum == 1)
            {
                p_score += (int)(p_score * 0.3 + max_correctNum * 0.3 + correctNum * 0.15) + 2;
                text_score.text = p_score.ToString();
            }
            //Debug.Log("目前累計答對:" + correctNum);

        }
        else//答錯
        {
            if (C_correctNum >= max_correctNum)
            {
                max_correctNum = C_correctNum;
                C_correctNum = 0;
                //Debug.Log("目前最大連續答對:" + max_correctNum);
            }
            wrongNum++;
            img_wrong.gameObject.SetActive(true);
            //Debug.Log("目前累計答錯:"+wrongNum);
        }
        if (C_correctNum >= max_correctNum)//適用於全部作答正確
        {
            max_correctNum = C_correctNum;
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(fb);
    }

}
