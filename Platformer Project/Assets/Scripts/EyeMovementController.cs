using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovementController : MonoBehaviour
{
	[SerializeField] public bool canShoot = false;
	[SerializeField] private GameObject player;
	[SerializeField] private float thrust;
	[SerializeField] private EyeAnimationController EyeAnim;
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
			if (player.transform.position.x >= transform.position.x)
			{
				EyeAnim.Flip(true);
			}
			else
			{
				Debug.Log("FlippedBack");
				EyeAnim.Flip(false);
			}
		}
	}

	void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(overlapSpot.position, offset, groundedSurfacesMask);
		Debug.Log("IS GROUNDED " + isGrounded);
		if (isGrounded)
        {
			EyeAnim.SetTrigger();
        }

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
		}
		else
		{
			playerOnTrigger = false;
		}

		if (!EyeAnim.isDead)
		{

			if (player != null && !EyeAnim.isDead)
			{
				if (!EyeAnim.isFalling)
				{
					distance = player.transform.position.x - transform.position.x;
					playerInRadius = Physics2D.OverlapCircle(transform.position, radius, lm);
					//Debug.Log("PlayerInRadius: " + playerInRadius);
					//Debug.Log("distance is ok to move: " + (Mathf.Abs(distance) > distanceUntilPlayer));
					//Debug.Log("PLayer on trigger " + playerOnTrigger);

					if (playerInRadius && (Mathf.Abs(distance) > distanceUntilPlayer) && playerOnTrigger)
					{
						//Debug.Log("AAAAA");
						Vector2 direction = new Vector2(distance, 0f).normalized;
						rb.velocity = direction * thrust;
						canShoot = false;
					}
					else if (playerInRadius && (Mathf.Abs(distance) <= distanceUntilPlayer) && playerOnTrigger)
					{
						rb.velocity = new Vector2(0f, 0f);
						canShoot = true;
					}
					else
					{
						canShoot = false;
					}
				}
			}
			else
			{
				rb.velocity = new Vector2(0f, 0f);
				canShoot = false;
			}
		}

	}

	public void setMovementTrigger()
	{
		mov[0] = GameObject.FindGameObjectWithTag("Trigger").GetComponent<MovementTrigger>();
	}

	public float GetDistance()
	{
		return distance;
	}
}
