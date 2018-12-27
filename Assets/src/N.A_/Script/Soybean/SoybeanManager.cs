using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoybeanManager : MonoBehaviour {

    /// <summary>
    /// Variable
    /// </summary>

    [SerializeField]
    private int N_SoyBeanTimerCount = 120;
    [SerializeField]
    private bool B_SoyBeanAwakePop = true;
    GameObject[] A_SoyBeanArray;
    bool[] A_SoyBeanActiveArray;
    int[] A_SoyBeanActiveTimer;

    GameObject C_SoybeanScore;
    public int N_Score = 0;
    public static int N_ScoreAmount = 0;
    public static int N_ThrowBeanNum = 0;

    /// <summary>
    /// Function
    /// </summary>

    // Use this for initialization
    void Start () {
        N_Score = 0;
        N_ScoreAmount = 0;
        N_ThrowBeanNum = 0;
        SoybeanInit();
    }
	
    void Count()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SoybeanScoreAdd();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SoybeanScoreSub();
        }
    }

	// Update is called once per frame
	void Update () {
        SoybeanActivationProcess();
        Count();
	}

    void SoybeanInit()
    {

        A_SoyBeanArray = GameObject.FindGameObjectsWithTag("Soybean");
        A_SoyBeanActiveArray = new bool[A_SoyBeanArray.Length];
        A_SoyBeanActiveTimer = new int[A_SoyBeanArray.Length];
        C_SoybeanScore = GameObject.Find("SoybeanScore").gameObject;

        Debug.Log(A_SoyBeanArray.Length);
        for (int i = 0; i < A_SoyBeanArray.Length; i++)
        {
            A_SoyBeanArray[i].GetComponent<SoybeanObject>().SetOwner(this,i);
            if (!B_SoyBeanAwakePop)
            {
                A_SoyBeanArray[i].SetActive(false);
                A_SoyBeanActiveArray[i] = false;
                A_SoyBeanActiveTimer[i] = Random.Range(0, N_SoyBeanTimerCount);
            }
        }

    }

    void SoybeanActivationProcess()
    {
        for(int i = 0; i < A_SoyBeanArray.Length;i++)
        {
            if (A_SoyBeanActiveArray[i] == false)
            {
                if (A_SoyBeanActiveTimer[i] <= N_SoyBeanTimerCount)
                {
                    A_SoyBeanActiveTimer[i]++;
                }
                else
                {
                    A_SoyBeanActiveTimer[i] = 0;
                    A_SoyBeanActiveArray[i] = true;
                    A_SoyBeanArray[i].SetActive(true);
                }
            }
        }
    }

    public void SoybeanScoreAdd()
    {
        N_Score++;
        N_ScoreAmount++;
        ExecuteEvents.Execute<SoybeanScoreInterface>(
            C_SoybeanScore,
            null,
            (recieve, y) => recieve.OnRecieveScore(N_Score));
    }

    public void SoybeanScoreSub()
    {
        N_Score--;
        ExecuteEvents.Execute<SoybeanScoreInterface>(
            C_SoybeanScore,
            null,
            (recieve, y) => recieve.OnRecieveScore(N_Score));
    }

    public void SetSoybeanActive(int _index, bool _flag)
    {
        A_SoyBeanArray[_index].SetActive(_flag);
        A_SoyBeanActiveArray[_index] = false;

        if (_flag == false)
        {
            A_SoyBeanActiveTimer[_index] = 0;
        }
    }

    public int GetScore()
    {
        return N_Score;
    }

}
