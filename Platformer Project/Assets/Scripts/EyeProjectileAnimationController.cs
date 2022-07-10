using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeProjectileAnimationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private EyeProjectileController projectile;

    public void CallBreak()
    {
        projectile.Break();
    }

    public void SetGraphics(bool cond)
    {
        if (sprite == null)
        {
            Debug.Log("null");
        }

        if (cond)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
