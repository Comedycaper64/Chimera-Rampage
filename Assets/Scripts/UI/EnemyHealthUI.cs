using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private Slider healthSlider;
    private HealthSystem enemyHealth;

    [SerializeField]
    private GameObject healthVisual;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
        enemyHealth = GetComponentInParent<HealthSystem>();

        enemyHealth.OnNewHealth += ChangeSlider;
    }

    private void OnDisable()
    {
        enemyHealth.OnNewHealth -= ChangeSlider;
    }

    private void ChangeSlider(object sender, float newValue)
    {
        healthSlider.value = newValue;
        if (newValue <= 0f)
        {
            healthVisual.SetActive(false);
        }
        else
        {
            healthVisual.SetActive(true);
        }
    }
}
