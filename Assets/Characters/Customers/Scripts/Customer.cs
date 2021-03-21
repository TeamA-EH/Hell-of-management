using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HOM
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Customer : MonoBehaviour, IArtificialIntelligence
    {
        NavMeshAgent agent = null;
        public NavMeshAgent Agent => agent;

        Transform pubDoor = null, Chair = null;

        #region Unity Callbacks
        void Start()
        {
            Initialize();
        }
        #endregion

        void Initialize()
        {
            if(!agent) agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public void SetState(uint state)
        {

        }

        public void SetChair(Transform chair) => this.Chair = chair;
        public void SetDoor(Transform door) => this.pubDoor = door;

        
    }
}