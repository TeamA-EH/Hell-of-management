using System.Collections;
using UnityEngine;

public class DrunkenDemonController : MonoBehaviour
{
    PlayerDetector detector;
    MCController pc;

    [Header("Demon Effect Settings"), Space(10)]
    [SerializeField] float m_effectCooldown = 3f;
    [SerializeField] bool m_canActivate = true;

    #region Unity Callbacks
    private void Awake()
    {
        if (!detector) detector = gameObject.GetComponentInChildren<PlayerDetector>();
    }
    private void OnEnable()
    {
        detector.OnPlayerDetected += OnPlayerDetected;
    }
    private void OnDisable()
    {
        detector.OnPlayerDetected -= OnPlayerDetected;
    }
    #endregion

    #region Detector Callbacks
    void OnPlayerDetected(GameObject _player)
    {
        if(m_canActivate)
        {
            pc = _player.GetComponent<MCController>();

            if(pc.hands[0].holdedItem || pc.hands[1].holdedItem) StartCoroutine(ExecuteEffect(m_effectCooldown));
        }
    }
    #endregion

    IEnumerator ExecuteEffect(float _cooldown)
    {
        m_canActivate = false;

        if (pc.hands[0].holdedItem && pc.hands[1].holdedItem)
        {
            pc.DropItem((uint)Random.Range(0, 2), (gameObject.transform.position - pc.gameObject.transform.position).normalized);
        }
        else if(pc.hands[0].holdedItem)
        {
            pc.DropItem(0, (gameObject.transform.position - pc.gameObject.transform.position).normalized);
        }
        else
        {
            pc.DropItem(1, (gameObject.transform.position - pc.gameObject.transform.position).normalized);
        }

        yield return new WaitForSeconds(_cooldown);
        m_canActivate = true;

    }
}
