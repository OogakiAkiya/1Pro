using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    public Text text;
    public bool flg=false;
	// Use this for initialization
	void Start () {
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
        Color color = text.color;
        if (flg)
        {
            color.a += 0.02f;
            if (color.a > 0.9f) flg = !flg;
        }
        else
        {
            color.a -= 0.02f;
            if (color.a < 0.1) flg = !flg;
        }
        text.color = color;
	}
}
