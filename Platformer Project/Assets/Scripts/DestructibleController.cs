using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleController : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject destructible;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Destructor" || col.gameObject.tag == "PlayerProjectile")
        {
            Debug.Log("Hey!");
            image.SetActive(false);
            anim.Play("Smoke_burst");
        }
    }

    public void callDestroy()
    {
        Destroy(destructible);
    }

    public void DisableParticles()
    {
        particles.SetActive(false);
    }
}
