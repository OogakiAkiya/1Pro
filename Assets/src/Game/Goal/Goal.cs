using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {
    //----------------------------------
    //変数
    //----------------------------------

    // Use this for initialization
    void Start()
    {
        //ゴール範囲を高さ0にする
        Vector3 pos = this.transform.position;
        pos.y = 0;
        this.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GameController").GetComponent<GameController>().endFlg = true;
        }
    }
}
