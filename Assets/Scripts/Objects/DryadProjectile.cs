using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadProjectile : MonoBehaviour
{
    private int damage;
    private int speed;
    private Vector3 direction;
    private float lifetime = 5f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetupProjectile(int damage, int speed, Vector2 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction;
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.TryGetComponent<HealthSystem>(out HealthSystem healthSystem)
            && healthSystem.isPlayer
        )
        {
            healthSystem.TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
