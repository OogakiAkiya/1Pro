using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ThrowbeanInterface : IEventSystemHandler
{
    void OnRecieve();
}

public class Throwbean : MonoBehaviour,ThrowbeanInterface
{

    /// <summary>
    /// Variable
    /// </summary>
    [SerializeField]
    float F_forceRateUpward = 2.0f;
    [SerializeField]
    float F_forceRateForward = 10f;

    Vector3 V_Force;
    Rigidbody C_rigidBody;

    /// <summary>
    /// Function
    /// </summary>

    // Use this for initialization
    void Start () {
        Vector3 Forward = Vector3.Normalize(transform.forward);
        Vector3 Upward = Vector3.Normalize(transform.up);

        V_Force = Forward * F_forceRateForward + Upward * F_forceRateUpward;
        C_rigidBody = GetComponent<Rigidbody>();
        C_rigidBody.AddForce(V_Force, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            C_rigidBody.velocity = Vector3.zero;
            C_rigidBody.isKinematic = true;
            C_rigidBody.useGravity = false;
        }
        Debug.Log(collision.gameObject.name);
    }

    public void OnRecieve()
    {
        Destroy(this.gameObject);
    }

}
