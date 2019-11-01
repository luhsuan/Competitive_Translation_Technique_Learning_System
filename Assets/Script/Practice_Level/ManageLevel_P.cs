using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageLevel_P : MonoBehaviour {

    Button amplification_easy_btn,amplification_hard_btn,omission_easy_btn,
    omission_hard_btn,means_easy_btn,means_hard_btn,word_conversion_easy_btn,
    word_conversion_hard_btn,btn_exit;
    // Button btn_LPractice, btn_LCompete;//排行榜
    AudioSource ClickBtn;
    public static string levelDifficulty = "";//關卡難度

    Xmlprocess xmlprocess;
    public static bool showAchieve;

    void Awake()
    {
        showAchieve = true;
    }
    void Start () {
        xmlprocess = new Xmlprocess();
        ClickBtn = GetComponents<AudioSource>()[1];
        btn_exit = GetComponentsInChildren<Button>()[1];
        amplification_easy_btn = GetComponentsInChildren<Button>()[2];
        amplification_hard_btn = GetComponentsInChildren<Button>()[3];
        omission_easy_btn = GetComponentsInChildren<Button>()[4];
        omission_hard_btn = GetComponentsInChildren<Button>()[5];
        means_easy_btn = GetComponentsInChildren<Button>()[6];
        means_hard_btn = GetComponentsInChildren<Button>()[7];
        word_conversion_easy_btn = GetComponentsInChildren<Button>()[8];
        word_conversion_hard_btn = GetComponentsInChildren<Button>()[9];
        // btn_compete = GetComponentsInChildren<Button>()[1];
        // btn_LPractice = GetComponentsInChildren<Button>()[2];
        // btn_LCompete = GetComponentsInChildren<Button>()[3];

        amplification_easy_btn.onClick.AddListener(goAmplificationPractice_easy);
        amplification_hard_btn.onClick.AddListener(goAmplificationPractice_hard);
        omission_easy_btn.onClick.AddListener(goOmissionPractice_easy);
        omission_hard_btn.onClick.AddListener(goOmissionPractice_hard);
        means_easy_btn.onClick.AddListener(goMeansPractice_easy);
        means_hard_btn.onClick.AddListener(goMeansPractice_hard);
        word_conversion_easy_btn.onClick.AddListener(goWordConversionPractice_easy);
        word_conversion_hard_btn.onClick.AddListener(goWordConversionPractice_hard);
        // btn_compete.onClick.AddListener(goCompete);
        // btn_LPractice.onClick.AddListener(delegate() { showLeaderboard(0); });
        // btn_LCompete.onClick.AddListener(delegate () { showLeaderboard(1); });
        btn_exit.onClick.AddListener(UploadData);

        /*//必須先完成練習1次才可以進入對戰區
        if (!xmlprocess.getLearningCount())
        {
            btn_compete.interactable = false;
            btn_compete.image.color = Color.gray;
        }
        else {
            btn_compete.interactable = true;
            btn_compete.onClick.AddListener(goCompete);
        }
        */

    }

    void goAmplificationPractice_easy() {
        levelDifficulty = "easy";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeAmplification");
    }

    void goAmplificationPractice_hard() {
        levelDifficulty = "hard";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeAmplification");
    }

    void goOmissionPractice_easy() {
        levelDifficulty = "easy";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeOmission");
    }

     void goOmissionPractice_hard() {
        levelDifficulty = "hard";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeOmission");
    }

    void goMeansPractice_easy() {
        levelDifficulty = "easy";
        // ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeMeans");
    }

     void goMeansPractice_hard() {
        levelDifficulty = "hard";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeMeans");
    }

    void goWordConversionPractice_easy() {
        levelDifficulty = "easy";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeWordConversion");
    }

    void goWordConversionPractice_hard() {
        levelDifficulty = "hard";
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Practice", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("PracticeWordConversion");
    }

    // void goCompete()
    // {
    //     ClickBtn.Play();
    //     //xmlprocess.New_timeHistoryRecord(levelName + "_Compete", System.DateTime.Now.ToString("HH-mm-ss"));
    //     xmlprocess.ScceneHistoryRecord( "Compete", DateTime.Now.ToString("HH:mm:ss"));
    //     SceneManager.LoadScene("CompeteArea");
    // }

    // void showLeaderboard(int i) {
    //     switch (i) {
    //         case 0:
    //             break;
    //         case 1:
    //             break;
    //     }
    // }

    void UploadData() {
        ClickBtn.Play();
        gameObject.AddComponent<UpdateSQL>();
        SceneManager.LoadScene("Home");
    }
}
