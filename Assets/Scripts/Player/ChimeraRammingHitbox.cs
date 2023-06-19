using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraRammingHitbox : MonoBehaviour
    {
        private CircleCollider2D rammingCollider;
        private int damage;
        private float knockback;

        private void Awake()
        {
            rammingCollider = GetComponent<CircleCollider2D>();
            rammingCollider.enabled = false;
        }

        public void ToggleHitbox(bool toggle)
        {
            rammingCollider.enabled = toggle;
        }

        public void ToggleHitbox(bool toggle, int damage, float knockback, float colliderArea)
        {
            rammingCollider.enabled = toggle;
            this.damage = damage;
            rammingCollider.radius = colliderArea;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Badly gets enemy layer, because unsure how layermasks work
            if (
                other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)
                && (other.gameObject.layer == 6)
            )
            {
                healthSystem.TakeDamage(damage);
            }
        }
    }
}
