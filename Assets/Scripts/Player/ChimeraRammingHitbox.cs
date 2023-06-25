using System.Collections;
using System.Collections.Generic;
using Enemies.Twig;
using UnityEngine;

namespace Chimera
{
    public class ChimeraRammingHitbox : MonoBehaviour
    {
        private CircleCollider2D rammingCollider;
        private List<HealthSystem> unitsHit = new List<HealthSystem>();
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
            unitsHit.Clear();
        }

        public void ToggleHitbox(bool toggle, int damage, float knockback, float colliderArea)
        {
            rammingCollider.enabled = toggle;
            this.damage = damage;
            this.knockback = knockback;
            rammingCollider.radius = colliderArea;
            unitsHit.Clear();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Badly gets enemy layer, because unsure how layermasks work
            if (
                other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)
                && (other.gameObject.layer == 6)
                && !unitsHit.Contains(healthSystem)
            )
            {
                healthSystem.TakeDamage(damage);

                // if (other.TryGetComponent<TwigStateMachine>(out TwigStateMachine stateMachine))
                // {
                //     Vector2 directionToHitUnit = (
                //         other.transform.position - transform.position
                //     ).normalized;
                //     stateMachine.Knockback(directionToHitUnit, knockback);
                // }

                Vector2 directionToHitUnit = (
                    other.transform.position - transform.position
                ).normalized;
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                rb.AddForce(directionToHitUnit * knockback, ForceMode2D.Impulse);
                unitsHit.Add(healthSystem);
            }
        }
    }
}
