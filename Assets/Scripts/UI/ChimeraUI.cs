using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chimera
{
    public class ChimeraUI : MonoBehaviour
    {
        private ChimeraStateMachine chimera;

        [Header("Ability UI Containers")]
        [SerializeField]
        private RectTransform dragonAbilityContainer;

        [SerializeField]
        private RectTransform lionAbilityContainer;

        [SerializeField]
        private RectTransform goatAbilityContainer;

        [SerializeField]
        private RectTransform centralUIAnchor;

        [SerializeField]
        private RectTransform leftUIAnchor;

        [SerializeField]
        private RectTransform rightUIAnchor;

        private Vector3 reducedContainerScale = new Vector3(0.7f, 0.7f, 0.7f);

        [SerializeField]
        private Slider healthBar;

        private void Awake()
        {
            chimera = GameObject
                .FindGameObjectWithTag("Player")
                .GetComponent<ChimeraStateMachine>();
            SetHealth(1f);
            chimera.OnHeadChanged += ChimeraStateMachine_OnHeadChanged;
            chimera.chimeraHealth.OnNewHealth += HealthSystem_OnNewHealth;
        }

        private void OnDisable()
        {
            chimera.OnHeadChanged -= ChimeraStateMachine_OnHeadChanged;
            chimera.chimeraHealth.OnNewHealth -= HealthSystem_OnNewHealth;
        }

        public void SetHealth(float newHealth)
        {
            //Takes in % value of maxhealth
            healthBar.value = newHealth;
        }

        public void ChangeActiveHead(ChimeraHead newHead)
        {
            dragonAbilityContainer.localScale = reducedContainerScale;
            lionAbilityContainer.localScale = reducedContainerScale;
            goatAbilityContainer.localScale = reducedContainerScale;
            switch (newHead)
            {
                case ChimeraHead.Dragon:
                    //dragonUI = centre, lionUI = right, goatUI = left
                    dragonAbilityContainer.SetParent(centralUIAnchor);
                    lionAbilityContainer.SetParent(rightUIAnchor);
                    goatAbilityContainer.SetParent(leftUIAnchor);
                    dragonAbilityContainer.localScale = Vector3.one;
                    break;
                case ChimeraHead.Lion:
                    //lionUI = centre, goaatUI = right, dragonUI = left
                    dragonAbilityContainer.SetParent(leftUIAnchor);
                    lionAbilityContainer.SetParent(centralUIAnchor);
                    goatAbilityContainer.SetParent(rightUIAnchor);
                    lionAbilityContainer.localScale = Vector3.one;
                    break;
                case ChimeraHead.Goat:
                    //goatI = centre, dragonUI = rught, lkionUI = left
                    dragonAbilityContainer.SetParent(rightUIAnchor);
                    lionAbilityContainer.SetParent(leftUIAnchor);
                    goatAbilityContainer.SetParent(centralUIAnchor);
                    goatAbilityContainer.localScale = Vector3.one;
                    break;
            }
            dragonAbilityContainer.localPosition = Vector3.zero;
            lionAbilityContainer.localPosition = Vector3.zero;
            goatAbilityContainer.localPosition = Vector3.zero;
        }

        private void ChimeraStateMachine_OnHeadChanged(object sender, ChimeraHead newHead)
        {
            ChangeActiveHead(newHead);
        }

        private void HealthSystem_OnNewHealth(object sender, float newHealth)
        {
            SetHealth(newHealth);
        }
    }
}
