
using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace HOM
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Soul : Item
    {
        [Header("Settings"), Space(10)]
        [SerializeField] GameObject green_soul_mesh;
        [SerializeField] GameObject blue_soul_mesh;
        [SerializeField] GameObject red_soul_mesh;

        [Header("Movement"), Space(10)]
        [Tooltip("Determinates the minimum distance from the player before this soul starts to escape")]
        [SerializeField] float maxDistanceFromPlayer = 3;
        ///<summary> Determinates the minimum distance from the player before this soul starts to escape </summary>
        public float MaxDistanceFromPlayer => maxDistanceFromPlayer;
        [SerializeField] CharacterMovementData movementData;
        public CharacterMovementData MovementData => movementData;
        bool canIteract => (gameObject.transform.position - C_Garth.self.gameObject.transform.position).magnitude <= iteractionData.DistanceFromPlayer;
        Material originalMaterial = null;
        public Rigidbody rb {private set; get;}

        [SerializeField] uint m_tag = 0;     //Rimuovere serializzazione
        public uint Tag 
        {
            set
            {
                m_tag = value;
                OnTagChanged(value);
            }
            get
            {
                return m_tag;
            }
        }

        #region  Unity Callbacks
        void Start()
        {
            Init();
        }
        void Update()
        {
            if(canIteract)
            {
               
                uint type = 0;
                if(Tag == SoulsManager.SOUL_TAG_RED) type = 0;
                else if(Tag == SoulsManager.SOUL_TAG_GREEN) type = 1;
                else if(Tag == SoulsManager.SOUL_TAG_BLUE) type = 2;

                if(Input.GetMouseButtonDown(0) && GetSelectedSoul() != null)
                {
                    var selection = GetSelectedSoul();
                    SoulsStorage.self.storedSouls.Remove(selection);
                    
                    StopBehaviourTree();
                    SetEnvironment(false);

                    SkillManager.SendPickupSkillRequest(GetSelectedSoul(), type, C_Garth.self.gameObject, 0, () => MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject), null);
                }
                else if(Input.GetMouseButtonDown(1) && GetSelectedSoul() != null)
                {
                     var selection = GetSelectedSoul();
                    SoulsStorage.self.storedSouls.Remove(selection);

                    StopBehaviourTree();
                    SetEnvironment(false);

                    SkillManager.SendPickupSkillRequest(GetSelectedSoul(), type, C_Garth.self.gameObject, 1, () => MovementHandler.DisableCharacterRotation(C_Garth.self.gameObject), null);
                }
            }
        }
        void OnMouseEnter()
        {
            if(canIteract)
            {
                ChangeMaterial(iteractionData.HighlightShader);
            }
        }
        void OnMouseExit()
        {
            if(gameObject.GetComponentInChildren<MeshRenderer>().material != originalMaterial)
            {
                ChangeMaterial(originalMaterial);
            }
        }
        #endregion

        public void Init()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.enabled = true;
            agent.isStopped = true;
            rb = gameObject.GetComponent<Rigidbody>();
            originalMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        }

        ///<summary> Changes the mesh material </summary>
        ///<param name="newMat"> The new mesh material </param>
        void ChangeMaterial(Material newMat)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = newMat;
        }

        ///<summary> Get the soul selected from the mouse cursor </summary>
        GameObject GetSelectedSoul()
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit))
            {
                if(hit.collider.gameObject.GetComponent<Soul>())
                {
                    return hit.collider.gameObject;       
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        ///<summary> Bind methods for being called when the soul tag changes </summary>
        ///<param name="tag"> The new tag </param>
        void OnTagChanged(uint tag)
        {
            SwapMeshLayer(tag);
        }
        ///<summary> Swaps displayed mesh using unique tags  </summary>
        ///<param name="tag"> The unique tag for the soul object </param>
        void SwapMeshLayer(uint tag)
        {
            switch(tag)
            {
                case SoulsManager.SOUL_TAG_GREEN:

                green_soul_mesh.SetActive(true);
                blue_soul_mesh.SetActive(false);
                red_soul_mesh.SetActive(false);

                break;
                case SoulsManager.SOUL_TAG_BLUE:

                green_soul_mesh.SetActive(false);
                blue_soul_mesh.SetActive(true);
                red_soul_mesh.SetActive(false);

                break;
                case SoulsManager.SOUL_TAG_RED:

                green_soul_mesh.SetActive(false);
                blue_soul_mesh.SetActive(false);
                red_soul_mesh.SetActive(true);

                break;
                default:
                Debug.LogError("Soul Tag Error!");
                break;
            }
        }
        public void EnablePhysics()
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        public void DisablePhysic()
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        ///<summary> Set the rigidbody velocity attached to this object </summary>
        ///<param name="velocity"> Value to set </param>
        public void SetVelocity(Vector3 velocity)
        {
            rb.velocity = velocity;
        }
        public void SetForce(Vector3 force)
        {
            //Debug.Log($"rigidbody: {rb}");
            rb.AddForce(force, ForceMode.Impulse);
        }

        #region Artificial Intelligence
        NavMeshAgent agent = null;
        public Vector3 NavigationDirection {private set; get;} = Vector3.zero;
        public Vector3 Goal => agent.destination;
        public bool InsideRoom {private set; get;} = true;
        public bool BehaviourTreeActivated {private set; get;} = false;

        ///<summary> Enables this artificial intelligence for navigating on the navmesh  </summary>
        public void ActivatesArtificialIntelligence() => agent.isStopped = false;
        ///<summary> Disables this artificial intelligence for navigating on the navmesh </summary>
        public void DeactivatesArtificialIntelligence() => agent.isStopped = true;
        ///<summary> Sets the goal position on the navmesh </summary>
        ///<param name="goal"> The final destination where to move </param>
        public void SetAIGoal(Vector3 goal)
        {
            agent.destination = goal;
        }
        ///<summary> Sets the direction for this artificial intelligence </summary>
        ///<param name="direction"> The direction for this artificial intelligence </param>
        public void SetAIDirection(Vector3 direction)
        {
            NavigationDirection = direction;
        }
        ///<summary> Rotates this AI towards the given direction </summary>
        ///<param name="time"> The time in seconds to perform the rotation </param>
        ///<param name="OnComplete"> Callback called when the rotation has been completed</param>
        public void AIRotate(float time, Action OnComplete = null)
        {
            float dot = Vector3.Dot(gameObject.transform.forward, NavigationDirection);
            float length = gameObject.transform.forward.magnitude * NavigationDirection.magnitude;
            float angle = Mathf.Acos(dot/length) * Mathf.Rad2Deg;
            gameObject.transform.DORotate(new Vector3(0,angle, 0), time)
            .OnComplete(() => OnComplete?.Invoke());
        }
        ///<summary> Returns the current speed for this artificial ingelligence </summary>
        public float GetAISpeed() => agent.speed;
        ///<summary> Returns the current accelleration for this artificial ingelligence </summary>
        public float GetAIAccelleration() => agent.acceleration;
        ///<summary> Returns the remaining distance between the AI and GOAL </summary>
        public float GetAIRemainingDistance()=> agent.remainingDistance;
        ///<summary> Returns TRUE if the AI reached the designed destination, otherwise returns FALSE </summary>
        public bool AIReachedGoal() => agent.remainingDistance <= .1f;
        ///<summary> Define if the soul is inside the storage room or outside </summary>
        public bool SetEnvironment(bool insideRoom) => InsideRoom = insideRoom;
        public void ExecuteBehaviourTree() => BehaviourTreeActivated = true;
        public void StopBehaviourTree() => BehaviourTreeActivated = false;
        #endregion

    }
}