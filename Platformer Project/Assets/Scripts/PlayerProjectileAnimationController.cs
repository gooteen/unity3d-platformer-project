using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileAnimationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerProjectileController projectile;

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
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}
