using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {
    public Image panel;
    private bool flg = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.anyKeyDown){
            flg = true;
        }
        if (flg)
        {
            Color color = panel.color;
            color.a += 0.01f;
            panel.color = color;
            if (panel.color.a >= 1)
            {
                SceneManager.LoadScene("Game");
            }
        }
	}
}
