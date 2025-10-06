using UnityEngine;
using UnityEngine.UI; // <-- Required for Image and fillAmount

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthbarsprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthbarsprite.fillAmount = currentHealth / maxHealth;
    }
}
