using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] private float damage;
    [SerializeField] private string tagName;
    [SerializeField] private float pushForce;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == tagName)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = new Vector3(col.gameObject.transform.position.x - transform.position.x, 0f, 0f).normalized;
            Debug.Log(direction);
            rb.AddForce(direction * pushForce);

            if (gameObject.tag == "Projectile")
            {
                Destroy(gameObject);
            }
        }
    }

}
