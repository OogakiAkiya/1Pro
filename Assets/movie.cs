using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class movie : MonoBehaviour {

    VideoPlayer Video;

	// Use this for initialization
	void Start () {
        Video = GetComponent<VideoPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!Video.isPlaying)
        {
            SceneManager.LoadScene("Result");
        }
	}
}
