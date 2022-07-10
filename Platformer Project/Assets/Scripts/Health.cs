using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isAlive;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private SkeletonAnimationController skeleton;
    [SerializeField] private SlimeAnimationController slime;
    [SerializeField] private EyeAnimationController eye;
    [SerializeField] private BossAnimationController boss;
    [SerializeField] private AnimationHandler player;
    [SerializeField] private Animator effect;
    
    void Awake()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = currentHealth - damage;
        /*if (gameObject.tag != "Player")*/
        if (skeleton != null)
        {
            skeleton.CallDamageAnim();
        } else if (slime != null)
        {
            slime.CallDamageAnim();
        } else if (eye != null)
        {
            eye.CallDamageAnim();

        } else if (boss != null)
        {
            boss.CallDamageAnim();
        } else
        {
            player.ShowImpact();
        }
        CheckIfAlive();
    }

    private void CheckIfAlive()
    {
        if (currentHealth <= 0)
        {
            isAlive = false;
            
        } else
        {
            isAlive = true;
        }
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void AddHealth(int hp)
    {
        if (currentHealth >= (maxHealth - hp))
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth = currentHealth + hp;
        }
        effect.Play("HealingSparkles");
    }

}
