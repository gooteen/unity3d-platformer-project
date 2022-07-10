using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject flame;
    [SerializeField] private BoxCollider2D box;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void Stop()
    {
        RemoveCollider();
        anim.SetTrigger("Finished");
    }
    
    public void JumpToStart()
    {
        box.enabled = true;
        anim.Play("Firebreath_start");
    }

    public void RemoveCollider()
    {
        box.enabled = false;
    }

    public void RemoveFlame()
    {
        flame.SetActive(false);
    }
}
