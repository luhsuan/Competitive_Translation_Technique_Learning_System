using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageLevel_L : MonoBehaviour {

    Button amplification_btn,omission_btn,means_btn,word_conversion_btn,btn_exit;
    // Button btn_LPractice, btn_LCompete;//排行榜
    AudioSource ClickBtn;

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
        amplification_btn = GetComponentsInChildren<Button>()[2];
        omission_btn = GetComponentsInChildren<Button>()[3];
        means_btn = GetComponentsInChildren<Button>()[4];
        word_conversion_btn = GetComponentsInChildren<Button>()[5];
        // btn_compete = GetComponentsInChildren<Button>()[1];
        // btn_LPractice = GetComponentsInChildren<Button>()[2];
        // btn_LCompete = GetComponentsInChildren<Button>()[3];

        amplification_btn.onClick.AddListener(goAmplificationLearning);
        omission_btn.onClick.AddListener(goOmissionLearning);
        means_btn.onClick.AddListener(goMeansLearning);
        word_conversion_btn.onClick.AddListener(goWordConversionLearning);
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

    void goAmplificationLearning() {
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Learning", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("LearningAmplification");
    }

    void goOmissionLearning() {
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Learning", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("LearningOmission");
    }

    void goMeansLearning() {
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Learning", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("LearningMeans");
    }

    void goWordConversionLearning() {
        ClickBtn.Play();
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "Learning", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("LearningWordConversion");
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
