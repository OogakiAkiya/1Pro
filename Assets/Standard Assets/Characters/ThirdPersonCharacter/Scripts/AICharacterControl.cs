using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]


    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling



        private Transform target;                                    // target to aim for
        private const float Speed=0.9f;
        public bool targetFindFlg = false;
        private int level = 0;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (targetFindFlg)
            {
                Vector3 diff = target.position - this.transform.position;
                //Vector3 currentPos = this.transform.position + (diff * (Time.deltaTime * 0.5f));
                character.Move(diff.normalized * (Speed*(level+1)), false, false);
            }else{
                character.Move(new Vector3(0,0,0), false, false);
            }

        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void SetFindFlg(bool _flg){
            targetFindFlg = _flg;
        }

        public void SetLevel(int _level){
            level = _level;   
        }

        public int GetLevel(){
            return level;
        }
    }
}
