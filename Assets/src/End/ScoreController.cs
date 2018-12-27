using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private int harvestAmount;
    private int throwBeanAmount;
    private float time;
    private int minute;
    private int totalScore;
    // Use this for initialization
    void Start () {
        harvestAmount=global::SoybeanManager.N_ScoreAmount;
        throwBeanAmount=global::SoybeanManager.N_ThrowBeanNum;
        time = global::GameController.seconds;
        minute = (int)time / 60;
        totalScore=(600-(int)time)*harvestAmount-(throwBeanAmount*100);
        if (totalScore < 0) totalScore = 0;
    }

    // Update is called once per frame
    void Update () {
        this.transform.GetChild(5).GetComponent<Text>().text= minute.ToString("00") + ":" + ((int)(time % 60)).ToString("00");
        this.transform.GetChild(6).GetComponent<Text>().text = harvestAmount.ToString();
        this.transform.GetChild(7).GetComponent<Text>().text = throwBeanAmount.ToString();
        this.transform.GetChild(8).GetComponent<Text>().text =totalScore.ToString();
    }
}
