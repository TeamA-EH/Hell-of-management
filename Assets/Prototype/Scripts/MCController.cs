using UnityEngine;
using DG.Tweening;
using System.Collections;

public class MCController : MonoBehaviour, IDragger
{
    CharacterController cc;

    [Header("Walking Settings"), Space(20)]
    [Tooltip("Velocita di movimento del personaggio")]
    [SerializeField] float walkingSpeed = 5;
    [Tooltip("La forza di gravita applicata al personaggio quando si muove")]
    [SerializeField] Vector3 gravityForce = new Vector3(0, -9.81f, 0);

    [Header("Dash Settings"), Space(20)]
    [Tooltip("Indica quanto spazio percorre il personaggio in base alla direzine di movimento")]
    [SerializeField] float dashDistance = 5;
    [Tooltip("Indica in quanto tempo il personaggio percorre la distanza")]
    [SerializeField] float dashDuration = .25f;
    [Tooltip("Indica quanto tempo bisogni attendere prima di poter utilizzare nuovamente il DASH")]
    [SerializeField] float dashDowntime = 3f;
    bool dash = false;
    bool dashEnable = true;

    [Header("Object Holding Movement"), Space(20)]
    [Tooltip("Definisce la scalabilita del movimento del personaggio rispetto al peso dell'oggetto")]
    [SerializeField] float inertialSpeed = 0;
    [SerializeField] bool holdingItem = false;

    [Header("Drag Operation"), Space(20)]
    [Tooltip("La distanza massima entro la quale si puo prendere un oggetto")]
    [SerializeField] float grabberDistance = 5;
    
    [System.Serializable]
    struct Hand
    {
        public Transform transform;
        public bool available;
    }
    [SerializeField] Hand[] hands = new Hand[2];
    public bool hasFreeHand
    {
        get
        {
            foreach(var hand in hands)
            {
                if (hand.available) return true;
            }

            return false;
        }
    }

    void Move(Vector3 _direction, float speed)
    {
        cc.Move(((_direction * (speed - inertialSpeed)) + gravityForce) * Time.deltaTime);
    }
    void Dash(Vector3 _direction)
    {
        Debug.Log("Dash");

        Vector3 endpoint = gameObject.transform.position + (new Vector3(_direction.x, 0, _direction.z).normalized * dashDistance);
        gameObject.transform.DOMove(endpoint, dashDuration)
            .OnComplete(() =>
            {
                dash = false;
                StartCoroutine(PlayDashCooldown());
            });
        dash = false;
    }
    IEnumerator PlayDashCooldown()
    {
        dashEnable = false;
        Debug.Log("Dash Disabled!");
        yield return new WaitForSeconds(dashDowntime);
        dashEnable = true;
        Debug.Log("Dash Enabled!");
    }

    public void Drag(GameObject item, float dragTime)
    {
        Hand freeHand = hands[0].available ? hands[0] : hands[1];
        Vector3 position = freeHand.transform.position;

        item.transform.DOJump(position, 1, 1, dragTime)
            .OnComplete(() =>
            {
                item.transform.SetParent(freeHand.transform);
            });

        freeHand.available = false;
    }


    #region UnityCallbacks
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift)) dash = true;

        if(dashEnable && dash && !holdingItem)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) Dash(Camera.main.transform.forward);
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)) Dash(-Camera.main.transform.forward);
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) Dash(Camera.main.transform.right);
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) Dash(-Camera.main.transform.right);
        }
        else
        {
            /* Walking */
            if (Input.GetKey(KeyCode.W)) Move(Camera.main.transform.forward, walkingSpeed);
            else if (Input.GetKey(KeyCode.S)) Move(-Camera.main.transform.forward * 0.5f, walkingSpeed);

            if (Input.GetKey(KeyCode.D)) Move(Camera.main.transform.right, walkingSpeed);
            else if (Input.GetKey(KeyCode.A)) Move(-Camera.main.transform.right, walkingSpeed);
        }
        
    }
    #endregion

}
