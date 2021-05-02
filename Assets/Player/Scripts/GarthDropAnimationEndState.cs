using UnityEngine;

namespace HOM
{
    public class GarthDropAnimationEndState : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            MovementHandler.EnableCharacterRotation(C_Garth.self.gameObject);
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Wait");
        }
    }
}
