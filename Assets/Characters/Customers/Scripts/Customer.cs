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

        GameObject pubDoor = null; 
        Chair chair = null;

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

        public void SetChair(Chair chair) => this.chair = chair;
        public void SetDoor(GameObject door) => this.pubDoor = door;

        
    }
}