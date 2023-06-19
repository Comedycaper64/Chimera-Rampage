using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraStats : MonoBehaviour
    {
        [Header("Abilities")]
        [Header("Ember Ability")]
        public float emberDamage;
        public float emberAreaOfEffect;
        public float emberCooldown;

        [Header("Flame Breath Ability")]
        public float flameBreathDamage;

        [Range(0, 1)]
        public float flameBreathAreaArc;
        public float flameBreathCooldown;

        [Header("Swipe Ability")]
        public float swipeDamage;
        public float swipeAreaOfEffect;
        public float swipeCooldown;

        [Header("Devour Ability")]
        public float devourDamage;
        public float devourRange;
        public float devourCooldown;

        [Header("Ram Ability")]
        public float ramDamage;
        public float ramKnockback;
        public float ramDashSpeed;
        public float ramDashDistance;
        public float ramCooldown;

        [Header("Wail Ability")]
        public float wailDamage;
        public float wailRange;
        public float wailCooldown;

        [Header("Misc")]
        public float movementSpeed;
    }
}
