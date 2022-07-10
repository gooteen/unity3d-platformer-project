using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    private BoxCollider2D hitbox;
    [SerializeField] private AnimationHandler anim;
    [SerializeField] private Transform pointLeft;
    [SerializeField] private Transform pointRight;

    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        hitbox.enabled = false;
    }

    void Update()
    {
        if (anim.isFacingRight)
        {
            transform.position = pointRight.position;
        }
        else
        {
            transform.position = pointLeft.position;
        }
    }

    public void Enable()
    {
        hitbox.enabled = true;
    }

    public void Disable()
    {
        hitbox.enabled = false;
    }

}
