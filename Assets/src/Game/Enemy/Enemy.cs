using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] UnityStandardAssets.Characters.ThirdPerson.AICharacterControl AICharacterControl;
    private int level=0;
    private const int AREALENGE = 8;
    // Use this for initialization
    void Start () {
        this.transform.localScale = new Vector3(AREALENGE + (level + 1), 0.2f, AREALENGE + (level + 1));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (level != AICharacterControl.GetLevel())
        {
            level = AICharacterControl.GetLevel();
            this.transform.localScale=new Vector3(AREALENGE + (level + 1), 0.2f, AREALENGE + (level + 1));
            //Vector3 targetPos = GameObject.Find();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "ThirdPersonController")
        {
            AICharacterControl.SetFindFlg(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name== "ThirdPersonController"){
            AICharacterControl.SetTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "ThirdPersonController")
        {
            AICharacterControl.SetFindFlg(false);
        }
    }
}
