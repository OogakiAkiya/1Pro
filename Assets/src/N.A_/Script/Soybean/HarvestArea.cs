using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HarvestArea : MonoBehaviour
{

    /// <summary>
    /// Function
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        GameObject sendObject = this.transform.parent.gameObject;
        ExecuteEvents.Execute<PlayerInterface>(
            other.gameObject,
            null,
            (recieve, y) => recieve.OnRecieveEnter(sendObject));

        //Debug.Log("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject sendObject = this.transform.parent.gameObject;
        ExecuteEvents.Execute<PlayerInterface>(
            other.gameObject,
            null,
            (recieve, y) => recieve.OnRecieveExit(sendObject));

        //Debug.Log("Exit");
    }

}
