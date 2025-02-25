using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private TextMeshProUGUI cooldownText;

    private Ability trackedAbility;

    public void Initialize(Ability ability)
    {
        trackedAbility = ability;

        if (iconImage != null && ability.icon != null)
        {
            iconImage.sprite = ability.icon;
            iconImage.enabled = true;
        }

        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f;
            cooldownOverlay.enabled = true;
        }

        if (cooldownText != null)
        {
            cooldownText.text = "";
        }
    }

    public void UpdateCooldown(float timeLeft) // TODO Is there a more efficient way to do this?
    {
        if (trackedAbility == null)
            return;

        float fillValue = timeLeft / trackedAbility.cooldownTime;
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = fillValue;
        }

        if (cooldownText != null)
        {
            if (timeLeft > 0)
            {
                cooldownText.text = Mathf.Ceil(timeLeft).ToString();
            }
            else
            {
                cooldownText.text = "";
            }
        }
    }

    public Ability GetTrackedAbility() => trackedAbility;
}
