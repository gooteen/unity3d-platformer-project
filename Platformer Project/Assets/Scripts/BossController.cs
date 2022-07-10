using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] public bool canBreathe = false;
	[SerializeField] private GameObject player;
	[SerializeField] private float thrust;
	[SerializeField] private BossAnimationController bossAnim;
	[SerializeField] private float radius;
	[SerializeField] private float distanceUntilPlayer;
	[SerializeField] private LayerMask lm;
	private bool playerInRadius;
	private BreathController breath;
	private float distance;
	private Rigidbody2D rb;

	void Start()
	{
		breath = GetComponent<BreathController>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (player != null)
		{
			if (player.transform.position.x <= transform.position.x)
			{
				Debug.Log("Should be looking to the left");
				bossAnim.Flip(true);
			}
			else
			{
				Debug.Log("Should be looking to the right");
				bossAnim.Flip(false);
			}
		}
	}

	void FixedUpdate()
	{

		int count = 0;

		if (!bossAnim.isDead)
		{
			if (player != null)
			{

				
				playerInRadius = Physics2D.OverlapCircle(transform.position, radius, lm);
				
				distance = player.transform.position.x - transform.position.x;
				Debug.Log("playerInRadius " + playerInRadius);
				Debug.Log($"Distance between the player and the demon ({Mathf.Abs(distance)}) > distance to stop at ({distanceUntilPlayer})? - " + (Mathf.Abs(distance) > distanceUntilPlayer));
				Debug.Log("not breathing " + !breath.isBreathing);
				if (playerInRadius && (Mathf.Abs(distance) > distanceUntilPlayer) && !breath.isBreathing)
				{
					Vector2 direction = new Vector2(distance, 0f).normalized;
					rb.velocity = direction * thrust;
					canBreathe = false;
				}
				else if (playerInRadius && (Mathf.Abs(distance) <= distanceUntilPlayer))
				{
					Debug.Log("herei");
					rb.velocity = new Vector2(0f, 0f);
					canBreathe = true;
				}
				else
				{
					canBreathe = false;
				}
			}
			else
			{
				canBreathe = false;
			}
		} else
        {
			rb.velocity = new Vector2(0f, 0f);
			Debug.Log("I am dead");
		}

	}

	public float GetDistance()
	{
		return distance;
	}
}
