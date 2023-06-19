using System.Collections;
using System.Collections.Generic;
using Chimera;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    private ChimeraCooldowns cooldowns;
    private Slider cooldownSlider;

    [SerializeField]
    private AbilityType abilityType;

    private void Awake()
    {
        cooldowns = GameObject.FindGameObjectWithTag("Player").GetComponent<ChimeraCooldowns>();
        cooldownSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        //Fill in ChimeraCooldowns gets and adjust slider accordingly
        switch (abilityType)
        {
            case AbilityType.dragonBreath:
                cooldownSlider.value = cooldowns.GetBreathCooldownNormalised();
                break;
            case AbilityType.dragonEmber:
                cooldownSlider.value = cooldowns.GetEmberCooldownNormalised();
                break;
            case AbilityType.lionSwipe:
                cooldownSlider.value = cooldowns.GetSwipeCooldownNormalised();
                break;
            case AbilityType.lionDevour:
                cooldownSlider.value = cooldowns.GetDevourCooldownNormalised();
                break;
            case AbilityType.goatRam:
                cooldownSlider.value = cooldowns.GetRamCooldownNormalised();
                break;
            case AbilityType.goatWail:
                cooldownSlider.value = cooldowns.GetWailCooldownNormalised();
                break;
        }
    }
}
