using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chimera
{
    public class ChimeraCooldowns : MonoBehaviour
    {
        ChimeraStats stats;

        public float emberCooldown = 0f;
        public float breathCooldown = 0f;
        public float swipeCooldown = 0f;
        public float devourCooldown = 0f;
        public float ramCooldown = 0f;
        public float wailCooldown = 0f;

        private void Awake()
        {
            stats = GetComponent<ChimeraStats>();
        }

        void Update()
        {
            if (emberCooldown > 0f)
            {
                emberCooldown -= Time.deltaTime;
            }
            if (breathCooldown > 0f)
            {
                breathCooldown -= Time.deltaTime;
            }
            if (swipeCooldown > 0f)
            {
                swipeCooldown -= Time.deltaTime;
            }
            if (devourCooldown > 0f)
            {
                devourCooldown -= Time.deltaTime;
            }
            if (ramCooldown > 0f)
            {
                ramCooldown -= Time.deltaTime;
            }
            if (wailCooldown > 0f)
            {
                wailCooldown -= Time.deltaTime;
            }
        }

        public float GetEmberCooldownNormalised()
        {
            return emberCooldown / stats.emberCooldown;
        }

        public float GetBreathCooldownNormalised()
        {
            return breathCooldown / stats.flameBreathCooldown;
        }

        public float GetSwipeCooldownNormalised()
        {
            return swipeCooldown / stats.swipeCooldown;
        }

        public float GetDevourCooldownNormalised()
        {
            return devourCooldown / stats.devourCooldown;
        }

        public float GetRamCooldownNormalised()
        {
            return ramCooldown / stats.ramCooldown;
        }

        public float GetWailCooldownNormalised()
        {
            return wailCooldown / stats.wailCooldown;
        }

        public void SetEmberCooldown()
        {
            emberCooldown = stats.emberCooldown;
        }

        public void SetBreathCooldown()
        {
            breathCooldown = stats.flameBreathCooldown;
        }

        public void SetSwipeCooldown()
        {
            swipeCooldown = stats.swipeCooldown;
        }

        public void SetDevourCooldown()
        {
            devourCooldown = stats.devourCooldown;
        }

        public void SetRamCooldown()
        {
            ramCooldown = stats.ramCooldown;
        }

        public void SetWailCooldown()
        {
            wailCooldown = stats.wailCooldown;
        }
    }
}
