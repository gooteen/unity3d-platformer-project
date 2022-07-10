using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWallBlockController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    
    public void SetBreak()
    {
        anim.SetTrigger("Broken");
    }
}
