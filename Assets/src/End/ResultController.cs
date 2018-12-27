using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour {
    public Image panel;
    private int count = 0;

    // Use this for initialization
    void Start () {
        Color color = panel.color;
        color.a = 1f;
        panel.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        Color color = panel.color;
        color.a -= 0.01f;
        panel.color = color;
        if (panel.color.a <= 0.01)
        {
            if(Input.anyKeyDown)SceneManager.LoadScene("Title");
        }
    }
}
