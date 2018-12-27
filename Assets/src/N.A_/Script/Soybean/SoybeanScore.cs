using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface SoybeanScoreInterface : IEventSystemHandler
{
    void OnRecieve();
    void OnRecieveScore(int _score);
}

public class SoybeanScore : MonoBehaviour,SoybeanScoreInterface
{

    /// <summary>
    /// Variable
    /// </summary>

    int N_score = 0;


    /// <summary>
    /// Function
    /// </summary>

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text =": "+N_score.ToString();
	}

    public void OnRecieve()
    {
        N_score++;
    }

    public void OnRecieveScore(int _score)
    {
        N_score = _score;
    }

}
