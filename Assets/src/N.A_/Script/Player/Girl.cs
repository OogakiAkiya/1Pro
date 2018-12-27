using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface GirlInterface : IEventSystemHandler
{
    void OnRecieveHarvest();
    void OnRecieveThrow();

    void OnRecieveSquat();
}

public class Girl : MonoBehaviour , GirlInterface
{

    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;

    Animation anima;
    //Rigidbody rigidbody;

    AudioSource[] audio;

    public float m_fHarvestDelayTime = 1.5f;
    public float m_fThrowDelayTime = 0.0f;


    enum SE
    {
        RUN,
        THROW,
        HARVEST,
        SCREAM,

        MAX,
    }

    bool m_bIsWalk = false;
    bool m_bSquat = false;

	// Use this for initialization
	void Start () {

       //m_Cam = Camera.main.transform;
        //rigidbody = GetComponent<Rigidbody>();
        anima = GetComponent<Animation>();
        //anima.Play("Walk");
        anima["Run"].speed = 1.5f;
        anima["Harvest"].speed = 0.3f;
        anima["Throw"].speed = 0.6f;
        anima["Squat"].speed = 1.5f;

        audio = GetComponents<AudioSource>();
        audio[(int)SE.RUN].pitch = 0.8f;

        audio[(int)SE.HARVEST].pitch = 0.6f;

	}

    // Update is called once per frame
    void Update()
    {
        if (!m_bSquat)
        {
            if (!(anima.IsPlaying("Harvest") || anima.IsPlaying("Throw")))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    //anima.Play("Run");
                    anima.Blend("Run");
                    if (!m_bIsWalk)
                    {
                        audio[(int)SE.RUN].Play();
                        m_bIsWalk = true;
                    }
                }
                else
                {
                    //anima.Play("Wait");
                    anima.Blend("Wait");
                    audio[(int)SE.RUN].Stop();
                    m_bIsWalk = false;
                }
            }
        }
    }

    public void OnRecieveHarvest()
    {
        anima.Play("Harvest");

        audio[(int)SE.RUN].Stop();
        m_bIsWalk = false;

        audio[(int)SE.HARVEST].PlayDelayed(m_fHarvestDelayTime);
    }

    public void OnRecieveThrow()
    {
        anima.Play("Throw");

        audio[(int)SE.RUN].Stop();
        m_bIsWalk = false;

        audio[(int)SE.THROW].PlayDelayed(m_fThrowDelayTime);
    }

    public void OnRecieveSquat()
    {
        anima.Play("Squat");

        audio[(int)SE.RUN].Stop();
        audio[(int)SE.SCREAM].Play();
        m_bIsWalk = false;
        m_bSquat = true;
    }

}
