using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface PlayerInterface : IEventSystemHandler
{

    void OnRecieveEnter(GameObject _soy);
    void OnRecieveExit(GameObject _soy);

}


public class PlayerController : MonoBehaviour, PlayerInterface
{

    /// <summary>
    /// Variable
    /// </summary>

    private List<GameObject> L_contactSoy = new List<GameObject>();

    [SerializeField]
    private GameObject C_ThrowBean;

    private SoybeanManager C_soybeanManager;

    private int N_targetNum = -1;

    [SerializeField]
    private float F_CorrectionHeight = 1.2f;

    private int F_HarvestCounter = 60;
    private int F_HarvestTimer = 0;
    private bool B_HarvestPossible = false;

    [SerializeField]
    private float F_ThrowBeanTimeToLife = 2f;

    [SerializeField]
    private float F_MoveAmountLine = 0.001f;

    private Rigidbody C_rigidBody;

    private bool B_SquatFlag = false;

    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter C_Controller;

    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl C_UserController;

    /// <summary>
    /// Function
    /// </summary>

    // Use this for initialization
    void Start()
    {
        C_soybeanManager = GameObject.Find("SoybeanManager").GetComponent<SoybeanManager>();

        C_UserController = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        C_Controller = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();

        C_rigidBody = GetComponent<Rigidbody>();
        B_SquatFlag = false;
}

    // Update is called once per frame
    void Update()
    {
        float MoveAmount = Mathf.Abs(C_rigidBody.velocity.x + C_rigidBody.velocity.y + C_rigidBody.velocity.z);

        if(Input.GetMouseButtonDown(0))
        {
            if (MoveAmount <= (float.Epsilon) + F_MoveAmountLine)
            {
                Harvest();
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            ThrowBean();
        }

        HarvestWait();

        //Debug.Log(L_contactSoy.Count);
    }


    void Harvest()
    {
        if (!B_HarvestPossible)
        {
            if (L_contactSoy.Count <= 0)
            {
                //Soy is not exist.

                return;
            }
            else
            {
                SearchDistanceToSoy();
                
                ExecuteEvents.Execute<GirlInterface>(
                    this.transform.GetChild(3).gameObject,
                    null,
                    (recieve, y) => recieve.OnRecieveHarvest());
            }
        }
    }

    void HarvestWait()
    {
        if(B_HarvestPossible)
        {
            if(F_HarvestTimer <= F_HarvestCounter)
            {
                F_HarvestTimer++;
            }
            else
            {

                ExecuteEvents.Execute<SoybeanInterface>(
                    L_contactSoy[N_targetNum].gameObject,
                    null,
                    (recieve, y) => recieve.OnRecieve());
                L_contactSoy.RemoveAt(N_targetNum);

                F_HarvestTimer = 0;
                B_HarvestPossible = false;
                //C_Controller.enabled = true;
                //C_UserController.enabled = true;
                C_UserController.m_bMoveFlag = true;
                N_targetNum = -1;
            }
        }
    }

    void ThrowBean()
    {
        if (C_soybeanManager.GetScore() >= 1)
        {
            Vector3 pos = transform.position;
            pos.y += F_CorrectionHeight;
            var obj = Instantiate(C_ThrowBean, pos, transform.rotation);
            obj.SetActive(true);
            C_soybeanManager.SoybeanScoreSub();
            SoybeanManager.N_ThrowBeanNum++;
            Destroy(obj, F_ThrowBeanTimeToLife);

            ExecuteEvents.Execute<GirlInterface>(
                this.transform.GetChild(3).gameObject,
                null,
                (recieve, y) => recieve.OnRecieveThrow());
        }
    }

    void SearchDistanceToSoy()
    {
        //Search
        Vector3 player_pos = transform.position;
        int target_num = -1;
        float min_distance = -1.0f;

        if (L_contactSoy.Count > 1)
        {
            for (int i = 0; i < L_contactSoy.Count; i++)
            {
                Vector3 target = L_contactSoy[i].transform.position;

                float dist = Vector3.Distance(player_pos, target);

                if (i != 0)
                {
                    if (dist < min_distance)
                    {
                        min_distance = dist;
                        target_num = i;
                    }
                }
                else
                {
                    min_distance = dist;
                    target_num = i;
                }
            }
        }
        else
        {
            target_num = 0;
        }

        N_targetNum = target_num;

        B_HarvestPossible = true;

        C_UserController.m_bMoveFlag = false;
        //C_Controller.enabled = false;
        //C_UserController.enabled = false;
        

        //ExecuteEvents.Execute<SoybeanInterface>(
        //    L_contactSoy[target_num].gameObject,
        //    null,
        //    (recieve, y) => recieve.OnRecieve());
        //L_contactSoy.RemoveAt(target_num);
    }


    public void OnRecieveEnter(GameObject _soy)
    {
        L_contactSoy.Add(_soy);
        //Debug.Log(L_contactSoy.Count);
    }
    public void OnRecieveExit(GameObject _soy)
    {
        for(int i = 0; i < L_contactSoy.Count;i++)
        {
            if(L_contactSoy[i].gameObject == _soy.gameObject)
            {
                L_contactSoy.RemoveAt(i);
            }
        }

        //Debug.Log(L_contactSoy.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!B_SquatFlag)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Vector3 p = Camera.main.transform.position;
                ExecuteEvents.Execute<GirlInterface>(
                    this.transform.GetChild(3).gameObject,
                    null,
                    (recieve, y) => recieve.OnRecieveSquat());
                B_SquatFlag = true;
                Debug.Log("Enter");
            }
        }
    }

}
