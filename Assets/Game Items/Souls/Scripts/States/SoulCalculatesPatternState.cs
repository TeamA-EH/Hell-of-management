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
            RaycastHit hit;
            if(Physics.Raycast(animator.gameObject.transform.position, direction, out hit, 1))  //Se colpisco qualcosa
            {
                Debug.Log($"wall: {hit.collider.gameObject.name}");
                direction = GetFreeDirection(animator.gameObject);
                target.SetAIDirection(direction);
                target.SetAIGoal(animator.gameObject.transform.position + direction);
                animator.SetTrigger("Rotate Soul");
            } 
            else
            {
                target.SetAIDirection(direction);
                target.SetAIGoal(animator.gameObject.transform.position + direction);
                animator.SetTrigger("Rotate Soul");
            }
           
        }

        bool isOutsideTheRange(Vector3 position)
        {
            Vector3 center = SoulsStorage.self.gameObject.transform.position;
            Vector2 size = SoulsStorage.self.RoomSize;

            if((position.x > center.x + size.x && position.x < center.x - size.x) &&
                (position.z > center.z + size.y && position.z < center.z - size.y)) return false;
                else return true;
        }

        Vector3 GetFreeDirection(GameObject target)
        {
            RaycastHit hit;
            if(!Physics.Raycast(target.transform.position, target.transform.forward, out hit, 1))
            {
                return target.transform.forward;
            }
            else if(!Physics.Raycast(target.transform.position, target.transform.right, out hit, 1))
            {
                return target.transform.right;
            }
            else if(!Physics.Raycast(target.transform.position, -target.transform.forward, out hit, 1))
            {
                return -target.transform.forward;
            }
            else if(!Physics.Raycast(target.transform.position, -target.transform.right, out hit, 1))
            {
                return -target.transform.right;
            }
            else return Vector3.zero;
            
        }
    }
}
