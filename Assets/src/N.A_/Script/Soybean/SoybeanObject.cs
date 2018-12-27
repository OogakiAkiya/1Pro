using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface SoybeanInterface : IEventSystemHandler
{
    void OnRecieve();
}


public class SoybeanObject : MonoBehaviour  , SoybeanInterface
{

    /// <summary>
    /// Variable
    /// </summary>

    SoybeanManager C_soybeanManager;
    //GameObject C_Score;
    int N_index;

    /// <summary>
    /// Function
    /// </summary>

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnRecieve()
    {
        //this.gameObject.SetActive(false);

        //ExecuteEvents.Execute<SoybeanScoreInterface>(
        //    C_Score,
        //    null,
        //    (recieve, y) => recieve.OnRecieve());
        C_soybeanManager.SoybeanScoreAdd();
        C_soybeanManager.SetSoybeanActive(N_index,false);
    }

    public void SetOwner(SoybeanManager _owner,int _index)
    {
        C_soybeanManager = _owner;
        N_index = _index;
        //C_Score = _score;
    }

    //Soybean-Layer is only to get collision to "Player".
    //private void OnCollisionEnter(Collision collision)
    //{
    //    ExecuteEvents.Execute<PlayerInterface>(
    //        collision.gameObject,
    //        null,
    //        (recieve, y) => recieve.OnRecieveEnter(this.gameObject));

    //    Debug.Log("Enter");
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    ExecuteEvents.Execute<PlayerInterface>(
    //        collision.gameObject,
    //        null,
    //        (recieve, y) => recieve.OnRecieveExit(this.gameObject));

    //    Debug.Log("Exit");
    //}
}
