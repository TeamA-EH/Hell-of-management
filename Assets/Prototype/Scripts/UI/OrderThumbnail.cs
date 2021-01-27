using UnityEngine;
using UnityEngine.UI;

public class OrderThumbnail : MonoBehaviour
{
    [SerializeField] Text orderName;
    [SerializeField] Text redSoulsText;
    [SerializeField] Text greenSoulsText;
    [SerializeField] Text orangeSoulsText;

    /// <summary>
    /// Aggiorna le informazioni della thumbnail
    /// </summary>
    /// <param name="name"></param>
    /// <param name="redSouls"></param>
    /// <param name="greenSouls"></param>
    /// <param name="orangeSouls"></param>
    public void SetThumbnailInfos(string name, uint redSouls, uint greenSouls, uint orangeSouls)
    {
        orderName.text =                name;
        redSoulsText.text =             redSouls.ToString();
        greenSoulsText.text =           greenSouls.ToString();
        orangeSoulsText.text =          orangeSouls.ToString();
    }
}
