using UnityEngine;
using UnityEngine.UI;

public class OrderUIController : MonoBehaviour
{


    [Header("Setup"), Space(20)]
    [SerializeField] GameObject vignette;
    [Space(20)]
    [SerializeField] Text recipeTypeText;
    [SerializeField] Text redSoulText;
    [SerializeField] Text greenSoulText;
    [SerializeField] Text orangeSoulText;

    private void Update()
    {
        if(OrderManager.GetIstance.ActiveOrder == null)
        {
            DeactivateVignette();
            return;
        }
    }

    public void ActivateVignette()
    {
        vignette.SetActive(true);
        OnActivation();
    }
    public void DeactivateVignette() => vignette.SetActive(false);

    protected void OnActivation()
    {
        recipeTypeText.text = OrderManager.GetIstance.ActiveOrder.orderType.ToString();
        redSoulText.text = OrderManager.GetIstance.ActiveOrder.redSouls.ToString();
        greenSoulText.text = OrderManager.GetIstance.ActiveOrder.greenSouls.ToString();
        orangeSoulText.text = OrderManager.GetIstance.ActiveOrder.orangeSouls.ToString();
    }

}
