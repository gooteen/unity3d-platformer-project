using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private InputProcessing input;
    [SerializeField] private int score;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject player;
    [SerializeField] private Text scoreIndicator;
    [SerializeField] private MenuController menu;
    [SerializeField] private Text arenaScoreText;
    private Health health;

    void Awake()
    {
        health = player.GetComponent<Health>();
    }
    
    void Start()
    {
        score = 0;
        UpdateScoreIndicator(score);
        panel.SetActive(false);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Arena")
        {
            arenaScoreText.text = score.ToString();
        }
        if (!health.isAlive)
        {
            popUp();
        }
        if (input == null)
        {
            if (Input.GetButtonDown(Buttons.SLASH_BUTTON))
            {
                Reload();
            }
            if (Input.GetButtonDown(Buttons.FIRE_BUTTON) && SceneManager.GetActiveScene().name == "Arena")
            {
                menu.ToMenu();
            }
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreIndicator(score);
    }

    public void UpdateScoreIndicator(int points)
    {
        scoreIndicator.text = points.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void popUp()
    {
        panel.SetActive(true);
    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
