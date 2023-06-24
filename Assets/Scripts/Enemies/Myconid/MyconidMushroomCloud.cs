using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Enemies.Myconid
{
    public class MyconidMushroomCloud : MonoBehaviour
    {
        private CircleCollider2D cloudCollider;

        [SerializeField]
        private VisualEffect cloudVFX;
        private float healAmount;

        private void Awake()
        {
            cloudCollider = GetComponent<CircleCollider2D>();
        }

        public void SetupCloud(float cloudRange, float healAmount)
        {
            cloudCollider.radius = cloudRange;
            this.healAmount = healAmount;
        }

        public void ToggleCloud(bool enable)
        {
            cloudCollider.enabled = enable;
            if (enable)
            {
                cloudVFX.Play();
            }
            else
            {
                cloudVFX.Stop();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!cloudCollider.enabled)
            {
                return;
            }

            if (
                other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)
                && !healthSystem.isPlayer
            )
            {
                healthSystem.Heal(healAmount * Time.deltaTime);
            }
        }
    }
}
