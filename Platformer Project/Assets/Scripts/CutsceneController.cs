using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneController : MonoBehaviour
{
    public bool readyToStartTheFight;
    public bool passive;
    [SerializeField] GameObject player;
    [SerializeField] GameObject boss;
    [SerializeField] private GameObject crow;
    [SerializeField] private float crowSpeed;
    [SerializeField] private Animator crowAnim;
    [SerializeField] private Transform crowDestination;
    [SerializeField] private GameObject[] borders;
    [SerializeField] private GameObject playerHP;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject bossHP;
    [SerializeField] private GameObject playerSP;
    [SerializeField] private GameObject obelisk;
    [SerializeField] private CinemachineVirtualCamera camMain;
    [SerializeField] private CinemachineVirtualCamera camCrow;
    [SerializeField] private bool readyToFly;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        readyToFly = false;
        camMain.Priority = 1;
        camCrow.Priority = 0;
    }

    
    void Update()
    {
        if (!passive)
        {
            if (readyToFly)
            {
                Move();

            }

            if (crow.transform.position == crowDestination.position)
            {
                crowAnim.SetTrigger("Landed!");
                readyToFly = false;
            }

            if (readyToStartTheFight)
            {
                playerHP.SetActive(true);
                bossHP.SetActive(true);
                scorePanel.SetActive(true);
                playerSP.SetActive(true);
                obelisk.SetActive(false);
                boss.SetActive(true);
                player.GetComponent<InputProcessing>().disabled = false;
                crow.SetActive(false);
                GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

                for (int i = 0; i < spawners.Length; i++)
                {
                    spawners[i].GetComponent<SpawnController>().spawnable = true;
                }
                camMain.Priority = 1;
                camCrow.Priority = 0;
                for (int i = 0; i < borders.Length; i++)
                {
                    borders[i].SetActive(false);
                }
                box.enabled = false;
                passive = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        
        player.GetComponent<InputProcessing>().disabled = true;
        readyToFly = true;
        crowAnim.SetTrigger("TakeOff!");
        if (col.gameObject.tag == "Player")
        {
            camMain.Priority = 0;
            camCrow.Priority = 1;
        }
        
        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].SetActive(true);
        }
        box.enabled = false;
    }

    public void Move()
    {
        crow.transform.position = Vector3.MoveTowards(crow.transform.position, crowDestination.position, crowSpeed*Time.deltaTime);
    }
}
