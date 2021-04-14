using UnityEngine;

namespace HOM
{
    public class GarthThrowAnimationStartState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Throw Item");
        }
    }
}
