using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        Rigidbody rig;
        Animator animator;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            animator = GetComponent<Animator>();
            rig = GetComponent<Rigidbody>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if(agent.enabled){
                if (target != null)
                    agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);
            }
        }

        Coroutine dieCor;
        public void Die(){
            if(dieCor==null)dieCor = StartCoroutine(DieCor());
        }

        IEnumerator DieCor(){
            agent.enabled = false;
            rig.isKinematic = false;
            animator.enabled = false;
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
