using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManController : MonoBehaviour
{
    [SerializeField] SoybeanManager soybeanManager;
    [SerializeField] GameController gameController;
    private const float SERCHAREA = 13;
    private const float SPEED = 1.4f;               //1.3f
    private Animation anim;
    private Transform target;
    public bool targetFindFlg = false;
    public bool contactFlg = false;
    public int count = 0;

    AudioSource m_CAudio;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;
        anim["Walk"].speed = 0.9f;
        anim["Run"].speed = 0.7f;

        m_CAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        List<Collider> targetList = new List<Collider>();                                   //プレイヤーとthrowBeanを入れる

        //ターゲット捜索
        if (SerchTarget(targetList) == true)
        {
            if(!targetFindFlg)
            {
                m_CAudio.Play();
            }
            //見つかった
            targetFindFlg = true;
            anim.Play("Run");
            DecisionTarget(targetList);
        }
        else
        {
            //見つからなかった
            targetFindFlg = false;
            target = null;
        }

        //ターゲットが見つからなかった時の処理
        if (target == null)
        {
            anim.Play("Walk");
            this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward) * SPEED;
            return;
        }

        //追尾処理
        Vector3 diff = target.position - this.transform.position;
        diff.y = 0;
        if (contactFlg == false)
        {
            this.GetComponent<Rigidbody>().transform.forward = diff.normalized;
            if (soybeanManager.GetScore() <= 4) this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward) * (SPEED + 0.1f) * 1f;
            if (soybeanManager.GetScore() > 4 && soybeanManager.GetScore() < 9) this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward) * (SPEED + 0.1f) * 2.6f;
            if (soybeanManager.GetScore() >= 9) this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward) * (SPEED + 0.1f) * 3f;

        }
        //一定の距離を離れると追尾をやめる
        if (diff.magnitude > SERCHAREA)
        {
            targetFindFlg = false;
            target = null;
            contactFlg = false;
        }
    }

    private bool SerchTarget(List<Collider> _targetList)
    {
        Collider[] targets = Physics.OverlapSphere(this.transform.position, SERCHAREA);     //範囲内のオブジェクト一括取得

        bool Flg = false;                                                                   //追い掛ける対象があるかの判定

        foreach (Collider collider in targets)
        {
            if (collider.gameObject.name == "Alice" || collider.gameObject.tag == "ThrowBean")
            {
                _targetList.Add(collider);                                                   //追加
                if (Flg == false) Flg = true;
            }
        }
        return Flg;
    }

    private void DecisionTarget(List<Collider> _targetList)
    {
        foreach (Collider element in _targetList)
        {
            //初めの一つのみ代入
            if (target == null)
            {
                target = element.gameObject.transform;
                continue;
            }

            //二つ目以降
            if ((target.transform.position - this.transform.position).magnitude > (element.transform.position - this.transform.position).magnitude)
            {
                target = element.gameObject.transform;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "ThrowBean"){
            contactFlg = true;
            return;
        }

        if(collision.gameObject.tag=="Player"){
            gameController.badEndFlg = true;
            return;
        }
        Vector3 temp = new Vector3(Random.Range(-180, 180), Random.Range(-1, -1), Random.Range(-180, 180)) - this.transform.position;
        temp.y = 0;
        this.GetComponent<Rigidbody>().transform.forward = temp.normalized;
        this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().transform.TransformDirection(Vector3.forward) * (SPEED + 0.1f) * (soybeanManager.GetScore() + 1);

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ThrowBean")
        {
            contactFlg = false;
            targetFindFlg = false;
            target = null;
        }
    }
}
