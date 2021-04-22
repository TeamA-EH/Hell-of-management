using UnityEngine;
using UnityEngine.UI;
namespace HOM
{
    public sealed class ItemSelectedSkillUI : MonoBehaviour
    {
        [SerializeField] Text itemSkillText;

        void Update()
        {
            itemSkillText.text = SkillUsageManager.self.ItemMod == SkillUsageManager.SelectedType.DROP ? "SELECTED SKILL: DROP" : "SELECTED SKILL: THROW";
        }
    }
}
