using UnityEngine;
using UnityEngine.Animations;

namespace HOM
{
    public class GarthDropAnimationStartState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject);
            C_Garth.self.AnimationController.SetFloat("speed",0);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            C_Garth.self.gameObject.GetComponent<Animator>().SetTrigger("Drop Item");
        }
    } 
}