using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTreeController : MonoBehaviour
{
    [SerializeField] private FruitController[] fruits;
    [SerializeField] private Animator anim;
    private BoxCollider2D box;
    private CapsuleCollider2D capsule;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        if (GetComponent<CapsuleCollider2D>())
        {
            capsule = GetComponent<CapsuleCollider2D>();
        }
        if (GetComponent<CapsuleCollider2D>())
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (box.enabled == true && (col.gameObject.tag == "Destructor" || col.gameObject.tag == "PlayerProjectile"))
        {
            
            if (gameObject.tag == "Tree")
            {
                
                anim.Play("Tree_hit");
                capsule.enabled = false;
            } else
            {
                anim.Play("Branch_hit");
            }
            box.enabled = false;
            
            rb.bodyType = RigidbodyType2D.Static;
            for (int i = 0; i < fruits.Length; i++)
            {
                fruits[i].ToDynamic();
            }
        }
    }
}
