using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageLevel_C : MonoBehaviour {

    Button amplification_btn,omission_btn,means_btn,conversion_btn,integrate_btn,btn_exit;
    // Button btn_LPractice, btn_LCompete;//排行榜
    AudioSource ClickBtn;

    Xmlprocess xmlprocess;
    public static bool showAchieve;
    public static string level = "";//關卡主題

    void Awake()
    {
        showAchieve = true;
    }
    void Start () {
        xmlprocess = new Xmlprocess();
        ClickBtn = GetComponents<AudioSource>()[1];
        btn_exit = GetComponentsInChildren<Button>()[1];
        integrate_btn = GetComponentsInChildren<Button>()[2];
        // amplification_btn = GetComponentsInChildren<Button>()[3];
        // omission_btn = GetComponentsInChildren<Button>()[4];
        means_btn = GetComponentsInChildren<Button>()[3];
        conversion_btn = GetComponentsInChildren<Button>()[4];
        // btn_compete = GetComponentsInChildren<Button>()[1];
        // btn_LPractice = GetComponentsInChildren<Button>()[2];
        // btn_LCompete = GetComponentsInChildren<Button>()[3];

        integrate_btn.onClick.AddListener(goIntegrateCompete);
        // amplification_btn.onClick.AddListener(goAmplificationCompete);
        // omission_btn.onClick.AddListener(goOmissionCompete);
        means_btn.onClick.AddListener(goMeansCompete);
        conversion_btn.onClick.AddListener(goConversionCompete);
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

    // void goAmplificationCompete() {
    //     ClickBtn.Play();
    //     level = "amplification";
    //     //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
    //     xmlprocess.ScceneHistoryRecord( "AmplificationCompete", DateTime.Now.ToString("HH:mm:ss"));
    //     SceneManager.LoadScene("CompeteArea");
    // }

    // void goOmissionCompete() {
    //     ClickBtn.Play();
    //     level = "omission";
    //     //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
    //     xmlprocess.ScceneHistoryRecord( "OmissionCompete", DateTime.Now.ToString("HH:mm:ss"));
    //     SceneManager.LoadScene("CompeteArea");
    // }

    void goMeansCompete() {
        ClickBtn.Play();
        level = "means";
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "MeansCompete", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("CompeteArea");
    }

    void goConversionCompete() {
        ClickBtn.Play();
        level = "conversion";
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "ConversionCompete", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("CompeteArea");
    }

    void goIntegrateCompete() {
        ClickBtn.Play();
        level = "integrate";
        //xmlprocess.New_timeHistoryRecord(levelName + "_Practice", System.DateTime.Now.ToString("HH-mm-ss"));
        xmlprocess.ScceneHistoryRecord( "IntegrateCompete", DateTime.Now.ToString("HH:mm:ss"));
        SceneManager.LoadScene("CompeteArea");
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
