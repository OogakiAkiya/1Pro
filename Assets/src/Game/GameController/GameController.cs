using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public float fog=0;
    public Image fade;
    private bool startFlg = true;
    public bool endFlg = false;
    public bool badEndFlg = false;
    //タイマー関係
    public Text timer;
    private int minute;
    public static float seconds;
    private float oldSeconds;
    private float startTime;


    //BGM
    private AudioSource[] m_CAudio;
    private bool m_bESCAPEChange;
    private bool m_bNORMALChange;

    enum BGM
    {
        NORMAL,
        ESCAPE,

        MAX,
    }

    [SerializeField] SoybeanManager soybeanManager;

    // Use this for initialization
    void Start () {
        //フォグ設定
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.9f, 0.9f, 0.9f);
        RenderSettings.fogDensity = 0f;

        //フェードイン
        Color color = fade.GetComponent<Image>().color;
        color.a = 1;
        fade.GetComponent<Image>().color = color;

        //タイマー
        startTime = Time.time;

        //AudioSource
        m_CAudio = GetComponents<AudioSource>();
        m_CAudio[(int)BGM.NORMAL].Play();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update () {


        ChangeBGM();

        FogSetting();
        if(startFlg){
            Color color = fade.GetComponent<Image>().color;
            color.a -= 0.02f;
            fade.GetComponent<Image>().color = color;
            if (fade.color.a <= 0) startFlg = false;
        }
        if (endFlg==true){
            Color color = fade.GetComponent<Image>().color;
            color.a+=0.01f;
            fade.GetComponent<Image>().color = color;
            if(color.a>0.9f){
                SceneManager.LoadScene("Movie");
            }
        }
        if (badEndFlg == true)
        {
            Color color = fade.GetComponent<Image>().color;
            color.a += 0.01f;
            fade.GetComponent<Image>().color = color;
            if (color.a >= 0.9f)
            {
                SceneManager.LoadScene("BadEnd");
            }
        }
        RenderSettings.fogDensity = fog;


        //タイマー関係
        //　Time.timeでの時間計測
        seconds = Time.time - startTime;

        minute = (int)seconds / 60;

        if ((int)seconds != (int)oldSeconds)
        {
            timer.text = ": "+minute.ToString("00") + ":" + ((int)(seconds % 60)).ToString("00");
        }
        oldSeconds = seconds;
    }

    private void FogSetting(){
        switch(soybeanManager.N_Score){
            case 0:
                fog = 0.5f;
                break;

            case 1:
                fog = 0.35f;
                break;
            case 3:
                fog = 0.3f;
                break;
            case 5:
                fog = 0.25f;
                break;
            case 7:
                fog = 0.2f;
                break;
            case 9:
                fog = 0.1f;
                break;
        }
    }

    void ChangeBGM()
    {
        //Flag検索
        if (SearchFlag())
        {
            if (!m_bESCAPEChange)
            {
                m_CAudio[(int)BGM.NORMAL].Stop();
                m_CAudio[(int)BGM.ESCAPE].Play();
                m_bNORMALChange = false;
                m_bESCAPEChange = true;
            }
        }
        else
        {
            if (!m_bNORMALChange)
            {
                m_CAudio[(int)BGM.ESCAPE].Stop();
                m_CAudio[(int)BGM.NORMAL].Play();
                m_bNORMALChange = true;
                m_bESCAPEChange = false;
            }
        }
    }

    bool SearchFlag()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i].GetComponent<ManController>().targetFindFlg)
            {
                return true;
            }
        }

        return false;
    }

}
