
using UnityEngine;
using UnityEngine.AI;

namespace HOM
{
    public interface IArtificialIntelligence
    {
        NavMeshAgent Agent {get;}

        void SetState(uint state);
    }
}
