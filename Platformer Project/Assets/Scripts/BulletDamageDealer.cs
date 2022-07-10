using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string tagName;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == tagName)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject); 
    }
}
