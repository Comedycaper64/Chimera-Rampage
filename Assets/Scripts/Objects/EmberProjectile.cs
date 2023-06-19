using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject emberVisual;

    [SerializeField]
    private Collider2D areaOfEffectCollider;

    private int damage;

    //private float areaOfEffect;

    [SerializeField]
    private float projectileSpeed;

    private void Update()
    {
        emberVisual.transform.Translate(Vector3.down * projectileSpeed * Time.deltaTime);
        if (
            (emberVisual.transform.position - areaOfEffectCollider.transform.position).magnitude
            < 0.5f
        )
        {
            Explode();
        }
    }

    public void SetupProjectile(Vector3 targetPosition, int damage, float areaOfEffect)
    {
        this.damage = damage;
        areaOfEffectCollider.transform.localScale = new Vector3(
            areaOfEffect,
            areaOfEffect,
            areaOfEffect
        );
        transform.position = targetPosition;
        emberVisual.transform.position = targetPosition + new Vector3(0, 20f, 0);
    }

    private void Explode()
    {
        List<Collider2D> hitUnits = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        areaOfEffectCollider.OverlapCollider(filter, hitUnits);
        foreach (Collider2D hitUnit in hitUnits)
        {
            if (hitUnit.TryGetComponent<HealthSystem>(out HealthSystem health))
            {
                health.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
