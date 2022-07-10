using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovementController : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private SlimeAnimationController slimeAnim;
	[SerializeField] private bool isGrounded;
	[SerializeField] private Transform overlapSpot;
	[SerializeField] private float offset;
	[SerializeField] private LayerMask groundedSurfacesMask;
	[SerializeField] private float jumpThrust;
	[SerializeField] private float jumpHeight;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask lm;
	[SerializeField] private float coolDownRate;
	[SerializeField] private MovementTrigger[] mov;
	[SerializeField] private bool playerOnTrigger;

	private bool isCoolingDown;
	private float startTime;

	private Rigidbody2D rb;
	private bool playerInRadius;

	//private int coef;
	/*
	
	[SerializeField] private float thrust;

	[SerializeField] private float distanceUntilPlayer;

	*/

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		startTime = Time.deltaTime;
		isCoolingDown = true;
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		
		if (isCoolingDown && Time.time - startTime >= coolDownRate)
		{
			startTime = Time.time;
			isCoolingDown = false;
		}
		Debug.Log(isCoolingDown);


	}

	void FixedUpdate()
	{
		slimeAnim.ManageJump(rb.velocity.y);
		isGrounded = Physics2D.OverlapCircle(overlapSpot.position, offset, groundedSurfacesMask);
		slimeAnim.Land(isGrounded);

		if (!isCoolingDown)
		{
			if (!slimeAnim.isDead && isGrounded)
			{
				if (player != null)
				{
					playerInRadius = Physics2D.OverlapCircle(transform.position, radius, lm);

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

					if (playerInRadius && playerOnTrigger)
					{
						slimeAnim.Squeeze();
						
					}
				}
			}
		}

	}

	public void setMovementTrigger()
    {
		mov[0] = GameObject.FindGameObjectWithTag("Trigger").GetComponent<MovementTrigger>();
    }

	public void Jump()
    {
		int coef = 1;
		if (player != null)
		{
			if (player.transform.position.x <= transform.position.x)
			{
				coef = -1;
			}
			else
			{
				coef = 1;
			}
		}
		rb.velocity = new Vector2(jumpThrust * coef, jumpHeight);
		//CoolDown();
	}

	public void CoolDown()
    {
		isCoolingDown = true;
	}

	/*
	public float GetDistance()
	{
		return distance;
	}
	*/
}
