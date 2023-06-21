using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraStats : MonoBehaviour
    {
        [Header("Abilities")]
        [Header("Ember Ability")]
        public int emberDamage;
        public float emberAreaOfEffect;
        public float emberCooldown;

        [Header("Flame Breath Ability")]
        public int flameBreathDamage;

        [Range(0, 1)]
        public float flameBreathAreaArc;
        public float flameBreathAreaRange;
        public float flameBreathCooldown;

        [Header("Swipe Ability")]
        public int swipeDamage;

        [Range(0, 1)]
        public float swipeArc;
        public float swipeRange;
        public float swipeCooldown;

        [Header("Devour Ability")]
        public int devourDamage;

        [Range(0, 1)]
        public float devourArc;
        public float devourRange;
        public float devourCooldown;

        [Header("Ram Ability")]
        public int ramDamage;
        public float ramKnockback;
        public float ramHitboxSize;
        public float ramDashTime;
        public float ramDashDistance;
        public float ramCooldown;

        [Header("Wail Ability")]
        public int wailDamage;
        public float wailDebuffDuration;
        public float wailRange;
        public float wailCooldown;

        [Header("Misc")]
        public float movementSpeed;
        public int health;
    }
}
