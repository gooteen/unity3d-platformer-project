using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectileController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float step;
    [SerializeField] private float pushForce;
    [SerializeField] private Animator anim;
    [SerializeField] private EyeProjectileAnimationController projAnim;
    [SerializeField] private Transform referencePoint;
    [SerializeField] private float timeToExplode;
    private Transform player;
    //private Rigidbody2D rb;
    private CircleCollider2D circle;
    private bool follow;
    
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        follow = true;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    public void Update()
    {
        if (Time.time - startTime >= timeToExplode)
        {
            follow = false;
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (player != null)
        {
            if (follow)
            {
                Debug.Log("I am born");
                transform.position = Vector3.MoveTowards(transform.position, player.position, step * Time.deltaTime);
                transform.LookAt(player);
            }
        } else
        {
            follow = false;
            anim.SetTrigger("Break");
            SetStatic();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            follow = false;
            col.gameObject.GetComponent<Health>().TakeDamage(damage);
            Rigidbody2D rbCol = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = (col.gameObject.transform.position - referencePoint.position).normalized;
 
            rbCol.AddForce(direction * pushForce);
            follow = false;
            anim.SetTrigger("Break");
            SetStatic();
        }
        if (col.gameObject.tag == "Destructor")
        {
            follow = false;
            anim.SetTrigger("Break");
            SetStatic();
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        follow = false;
        anim.SetTrigger("Break");
        SetStatic();
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    public void CallSetGraphics(bool cond)
    {
        projAnim.SetGraphics(cond);
    }

    public void SetStatic()
    {
        circle.enabled = false;
        //rb.bodyType = RigidbodyType2D.Static;
    }
}
