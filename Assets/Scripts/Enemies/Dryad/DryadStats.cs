using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Dryad
{
    public class DryadStats : MonoBehaviour
    {
        public int health;
        public int attackDamage;
        public int attackProjectileSpeed;
        public float attackIntervals;

        [Range(0, 1)]
        public float attackTiming;
    }
}
