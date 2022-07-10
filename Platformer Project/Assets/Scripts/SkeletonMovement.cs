using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
	[SerializeField] public bool canShoot = false;
	[SerializeField] private GameObject player; 
	[SerializeField] private float thrust;
	[SerializeField] private SkeletonAnimationController skeletonAnim;
	[SerializeField] private float radius;
	[SerializeField] private float distanceUntilPlayer;
	[SerializeField] private LayerMask lm;
	[SerializeField] private float offset;
	[SerializeField] private LayerMask groundedSurfacesMask;
	[SerializeField] private Transform overlapSpot;
	[SerializeField] private bool isGrounded;
	[SerializeField] private MovementTrigger[] mov;
	private bool playerOnTrigger;
	private bool playerInRadius;
	private float distance;
	private Rigidbody2D rb;

	void Start() 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerOnTrigger = false;
		rb = GetComponent<Rigidbody2D>();
	}		

    void Update()
	{
		if (player != null) 
		{
		if (player.transform.position.x <= transform.position.x) 
		{
			skeletonAnim.Flip(true);
		} else 
		{
			skeletonAnim.Flip(false);
		}
		}
	}

	void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(overlapSpot.position, offset, groundedSurfacesMask);

		int count = 0;
		for (int i = 0; i < mov.Length; i++)
		{
			if (mov[i].GetBool())
            {
				count++;
            }
		}
		if (count >= 1)
        {
			playerOnTrigger = true;
        } else
        {
			playerOnTrigger = false;
		}

		if (!skeletonAnim.isDead && isGrounded)
		{
			if (Mathf.Abs(rb.velocity.x) > 0)
			{
				skeletonAnim.Walk(true);
			}
			else
			{
				skeletonAnim.Walk(false);
			}

			if (player != null)
			{
				distance = player.transform.position.x - transform.position.x;
				playerInRadius = Physics2D.OverlapCircle(transform.position, radius, lm);

				if (playerInRadius && (Mathf.Abs(distance) > distanceUntilPlayer) && playerOnTrigger)
				{
					Vector2 direction = new Vector2(distance, 0f).normalized;
					rb.velocity = direction * thrust;
					canShoot = false;
                }
                else if (playerInRadius && (Mathf.Abs(distance) <= distanceUntilPlayer) && playerOnTrigger)
                {
					canShoot = true;
                }
				else
                {
					canShoot = false;
				}
			}
            else
            {
				canShoot = false;
            }
		}
	
    }

	public float GetDistance()
    {
		return distance;
    }

	public void setMovementTrigger()
	{
		mov[0] = GameObject.FindGameObjectWithTag("Trigger").GetComponent<MovementTrigger>();
	}
}
