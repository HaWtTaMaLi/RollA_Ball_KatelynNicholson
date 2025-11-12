using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Full")]
    [SerializeField] public Image healthBarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
