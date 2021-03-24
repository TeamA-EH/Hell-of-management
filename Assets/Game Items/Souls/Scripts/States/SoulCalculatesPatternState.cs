using UnityEngine;

namespace HOM
{
    public sealed class SoulCalculatesPatternState : StateMachineBehaviour
    {
        Soul target = null;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(!target) target = animator.gameObject.GetComponent<Soul>();

            Vector3 direction = ( animator.gameObject.transform.position - C_Garth.self.gameObject.gameObject.transform.position).normalized;
            Debug.DrawLine(animator.gameObject.transform.position, animator.gameObject.transform.position + direction, Color.red, 1f);
            
            if(isOutsideTheRange(animator.gameObject.transform.position + direction))
            {
                target.SetAIDirection(animator.gameObject.transform.right);
                target.SetAIGoal(animator.gameObject.transform.position + animator.gameObject.transform.right);
            }
            else
            {
                target.SetAIDirection(direction);
                target.SetAIGoal(animator.gameObject.transform.position + direction);
            }

            animator.SetTrigger("Rotate Soul");
        }

        bool isOutsideTheRange(Vector3 position)
        {
            Vector3 center = SoulsStorage.self.gameObject.transform.position;
            Vector3 size = new Vector3(5,0,5);

            if(position.x > center.x + size.x && position.x < center.x - size.x || 
                position.z > center.z + size.z && position.z < center.z - size.z) return false;
                else return true;
        }
    }
}
