/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float projectileSpeed;
    public int damage;

    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * projectileSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Projectile")
        {
            gameObject.transform.parent = collision.gameObject.transform;
            Destroy(gameObject);
            GetComponent<CircleCollider2D>().enabled = false;
        }

        if (collision.tag == "Enemy")
        {
            var healthComponet = collision.GetComponent<Health>();
            if (healthComponet != null)
            {
                healthComponet.TakeDamage(damage);
            }

        }    
    }

}
*/