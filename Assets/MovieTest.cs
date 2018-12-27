using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieTest : MonoBehaviour {

    enum Mode
    {
        Plane,
        RawImage
    };
    //　PlaneとRawImageで切り替える
    [SerializeField]
    private Mode mode;
    //　再生する動画
    [SerializeField]
    private MovieTexture[] movies;
    //　動画番号
    private int num;
    void Start()
    {
        //　最初の動画を設定
        num = 0;
        if (mode == Mode.Plane)
        {
            GetComponent<MeshRenderer>().material.mainTexture = movies[num];
        }
        else
        {
            GetComponent<RawImage>().texture = movies[num];
        }
        //　動画をループ設定
        movies[num].loop = true;
    }
    void Update()
    {
        //　Sキーを押したら動画の再生とポーズを繰り返す
        if (Input.GetKeyDown("s"))
        {
            if (movies[num].isPlaying)
            {
                movies[num].Pause();
                Debug.Log("pause");
            }
            else
            {
                movies[num].Play();
                Debug.Log("start");
            }
            //　Qキーを押したら動画をストップ
        }
        else if (Input.GetKeyDown("q"))
        {
            movies[num].Stop();
            Debug.Log("stop");
        }
        else if (Input.GetKeyDown("n"))
        {
            //　動画を切り替える前に今再生している動画を止める
            movies[num].Stop();
            //　次の動画を指す
            num++;
            if (num >= movies.Length)
            {
                num = 0;
            }
            //　Textureを次のMovieTextureに変える
            if (mode == Mode.Plane)
            {
                GetComponent<MeshRenderer>().material.mainTexture = movies[num];
            }
            else
            {
                GetComponent<RawImage>().texture = movies[num];
            }
            //　動画をループに設定
            movies[num].loop = true;
            Debug.Log("change movie");
        }
    }
}
