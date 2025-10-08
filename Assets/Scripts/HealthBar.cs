
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using System;

public class HealthBar : MonoBehaviour
{

    [SerializeField] public Image healthBarSprite;

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}
