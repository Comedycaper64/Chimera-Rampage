using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Twig
{
    public class TwigStats : MonoBehaviour
    {
        public int health;
        public float movementSpeed;
        public int attackDamage;
        public float attackRange;
        public float damageRange;

        [Range(0, 1)]
        public float attackTiming;
    }
}
