using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectileController : MonoBehaviour
{
    //THE TWO RBs
    [SerializeField] private float damage;
    [SerializeField] private float pushForce;
    [SerializeField] private Animator anim;
    [SerializeField] private SkeletonProjectileAnimationController swordAnim;
    [SerializeField] private Transform referencePoint;
    private Rigidbody2D rb;
    private CircleCollider2D circle; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = (col.gameObject.transform.position - referencePoint.position).normalized;
           
            rb.AddForce(direction * pushForce);
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (col.gameObject.tag == "Destructor")
        {
            anim.SetTrigger("Break");
            SetStatic();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        anim.SetTrigger("Break");
        SetStatic();
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    public void CallSetGraphics(bool cond)
    {
        swordAnim.SetGraphics(cond);
    }

    public void SetStatic()
    {
        circle.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
