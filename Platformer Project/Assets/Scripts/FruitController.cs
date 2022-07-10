using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    [SerializeField] private int hp;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ToDynamic()
    {
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void setHealth(int health)
    {
        hp = health;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (rb != null)
        {
            if (rb.bodyType != RigidbodyType2D.Static && col.gameObject.tag == "Player")
            {
                
                Health health = col.gameObject.GetComponent<Health>();
                health.AddHealth(hp);
                Destroy(gameObject);
            }
        }
    }
}
