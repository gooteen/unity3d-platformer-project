using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour
{
    public bool spawnable;
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject skeleton;
    [SerializeField] private GameObject eye;
    [SerializeField] private GameObject fruit;
    [SerializeField] private Animator spawnAnim;
    [SerializeField] private float spawnRateAtStart;
    [SerializeField] private float spawnRate;
    [SerializeField] private int fruitQuantity;
    [SerializeField] private int fruitChance;
    [SerializeField] private int healRate;
    [SerializeField] private int enemyNumberLimit;
    [SerializeField] private float spawnRateDecrease;
    private float startTime;
    private System.Random random;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name != "Arena")
        {
            spawnable = false;
        }
        spawnRate = spawnRateAtStart;
    }

    public void Update()
    {
        if((Time.time - startTime) >= spawnRate)
        {
            Spawn();
        }
    }

    
    public void Spawn()
    {
        if (spawnable)
        {
            spawnRate += spawnRateDecrease;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Damageable");
            Debug.Log($"Size: {enemies.Length}");
            if (enemies.Length < enemyNumberLimit + 1)
            {
                startTime = Time.time;
                random = new System.Random();
                int condFruit = random.Next(fruitChance);
                if (condFruit != 0)
                {
                    int cond = random.Next(3);
                    GameObject currentEnemy;
                    spawnAnim.Play("Spawn_burst");
                    if (cond == 0)
                    {
                        currentEnemy = Instantiate(skeleton, transform.position, Quaternion.identity);
                        currentEnemy.GetComponent<SkeletonMovement>().setMovementTrigger();
                    }
                    else if (cond == 1)
                    {
                        currentEnemy = Instantiate(slime, transform.position, Quaternion.identity);
                        currentEnemy.GetComponent<SlimeMovementController>().setMovementTrigger();
                    } else
                    {
                        currentEnemy = Instantiate(eye, transform.position, Quaternion.identity);
                        currentEnemy.GetComponent<EyeMovementController>().setMovementTrigger();
                    }
                    
                }
                else
                {
                    if (SceneManager.GetActiveScene().name != "Arena")
                    {
                        spawnAnim.Play("Spawn_burst");
                        SpawnFruits();
                    }
                }
            }

            else
            {
                if (SceneManager.GetActiveScene().name != "Arena")
                {
                    startTime = Time.time;
                    random = new System.Random();
                    int condFruit = random.Next(fruitChance);
                    if (condFruit == 0)
                    {
                        spawnAnim.Play("Spawn_burst");
                        SpawnFruits();
                    }
                }

            }
        }
    }

    public void SpawnFruits()
    {
        for (int i = 0; i < fruitQuantity; i++)
        {
            GameObject currentFruit = Instantiate(fruit, transform.position, Quaternion.identity);
            currentFruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            currentFruit.GetComponent<FruitController>().setHealth(healRate);
        }
    }
}
