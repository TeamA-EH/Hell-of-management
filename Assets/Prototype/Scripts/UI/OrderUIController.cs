using UnityEngine;
using UnityEngine.UI;

public class OrderUIController : MonoBehaviour
{
    [Header("Setup"), Space(20)]
    [SerializeField] GameObject vignette;

    public void ActivateVignette()
    {
        vignette.SetActive(true);
        //vignette.GetComponent<OrderVignette>().UpdateUI();
    }
    public void DeactivateVignette() => vignette.SetActive(false);

}
