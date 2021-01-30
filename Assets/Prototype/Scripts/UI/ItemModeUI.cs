using UnityEngine;
using UnityEngine.UI;

public class ItemModeUI : MonoBehaviour
{
    [SerializeField] Text modeText;

    private void Update()
    {
        modeText.text = ItemUsabilityManager.GetInstance.ActiveMode.ToString();
    }
}
