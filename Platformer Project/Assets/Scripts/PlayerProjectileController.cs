using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float pushForce;
    [SerializeField] private float pushForceSpecial;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerProjectileAnimationController orbAnim;
    [SerializeField] private ContactFilter2D soulWallBlocks;
    [SerializeField] private float overlapCircleRadius;
    //[SerializeField] private Transform referencePoint;
    private Rigidbody2D rb;
    private CircleCollider2D circle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Damageable")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = (col.gameObject.transform.position - transform.position).normalized;
            Debug.Log(col.gameObject.transform.position);
            Debug.Log(direction);
            rb.AddForce(direction * pushForce);
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (col.gameObject.tag == "BossNonDamageable")
        {
            anim.SetTrigger("Break");
            SetStatic();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "SoulBlock")
        {
            Collider2D[] result = new Collider2D[10];
            Physics2D.OverlapCircle(gameObject.transform.position, overlapCircleRadius, soulWallBlocks, result);
            int i = 0;
            while (result[i] != null)
            {
                Rigidbody2D colRb = result[i].gameObject.GetComponent<Rigidbody2D>();
                colRb.bodyType = RigidbodyType2D.Dynamic;
                Vector3 direction = (result[i].gameObject.transform.position - transform.position);
                Debug.Log("direction done");
                colRb.AddForce(direction* pushForceSpecial);
                SoulWallBlockController controller = result[i].gameObject.GetComponent<SoulWallBlockController>();
                controller.SetBreak();
                Debug.Log("anim done");
                i++;
            }
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (col.gameObject.tag != "Player")
        {
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (col.gameObject.tag == "Damageable")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = (col.gameObject.transform.position - transform.position).normalized;
            Debug.Log(col.gameObject.transform.position);
            Debug.Log(direction);
            rb.AddForce(direction * pushForce);
            anim.SetTrigger("Break");
            SetStatic();
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    public void CallSetGraphics(bool cond)
    {
        orbAnim.SetGraphics(cond);
    }

    public void SetStatic()
    {
        circle.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
