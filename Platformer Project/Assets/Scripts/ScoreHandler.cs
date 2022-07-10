using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class ScoreHandler : MonoBehaviour
{
    private int total;
    [SerializeField] private MainMenuController mm;
    [SerializeField] private string fileName;
    [SerializeField] private Text scoreText;
    [SerializeField] private bool bossBeaten;
    /*
    [SerializeField] private int level1PointsCurrent;
    [SerializeField] private int level2PointsCurrent;
    [SerializeField] private int level3PointsCurrent;
    */
    [Header("Total points on the levels")]
    [SerializeField] private int level1PointsTotal;
    [SerializeField] private int level2PointsTotal;
    [SerializeField] private int level3PointsTotal;
    [SerializeField] private int lvl2EnteringPoints;
    [SerializeField] private int lvl3EnteringPoints;
    [SerializeField] private int lvl4EnteringPoints;
    [Header("UI elements")]
    [SerializeField] private GameObject levelNotAvailablePanel;
    [SerializeField] private GameObject arenaNotAvailablePanel;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject authorPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject arenaPanel;
    [SerializeField] private GameObject backPanel;
    [SerializeField] private Text text;


    void Start()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".txt";
        if (!File.Exists(path))
        {
            Debug.Log("HELLLOOO!");
            Refresh();
        }
        RecalculateTotal(path);
    }

    public void EntranceCheck(int level)
    {
        if (level == 1)
        {
            mm.LoadLevel(level);
        }
        if (level == 2) { 
            if (total < lvl2EnteringPoints)
            {
                disableUIelements();
                text.text = (lvl2EnteringPoints - total).ToString();
            }
            else
            {
                mm.LoadLevel(level);
            }
        }

        if (level == 3)
        {
            if (total < lvl3EnteringPoints)
            {
                disableUIelements();
                text.text = (lvl3EnteringPoints - total).ToString();
            }
            else
            {
                mm.LoadLevel(level);
            }
        }

        if (level == 4)
        {
            if (total < lvl4EnteringPoints)
            {
                disableUIelements();
                text.text = (lvl4EnteringPoints - total).ToString();
            }
            else
            {
                mm.LoadLevel(level);
            }
        }
    }

    public void AreanaEntranceCheck(int level)
    {
        if (bossBeaten)
        {
            mm.LoadLevel(level);
        } else
        {
            disableUIelementsArena();
        }
    }


    public void disableUIelements()
    {
        levelNotAvailablePanel.SetActive(true);
        levelsPanel.SetActive(false);
        authorPanel.SetActive(false);
        scorePanel.SetActive(false);
        arenaPanel.SetActive(false);
        backPanel.SetActive(false);
    }

    public void disableUIelementsArena()
    {
        arenaNotAvailablePanel.SetActive(true);
        levelsPanel.SetActive(false);
        authorPanel.SetActive(false);
        scorePanel.SetActive(false);
        arenaPanel.SetActive(false);
        backPanel.SetActive(false);
    }

    public void Refresh()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".txt";
        StreamWriter sw = new StreamWriter(path, false);
        string[] array = new string[] { "0", "0", "0", "NO" };
        for (int i = 0; i < array.Length; i++)
        {
            sw.WriteLine(array[i]);
        }
        sw.Close();
        RecalculateTotal(path);
    }

    public void RecalculateTotal(string path)
    {
            string[] lines = File.ReadAllLines(path);

            total = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (i != lines.Length - 1)
                {
                    total += Int32.Parse(lines[i]);
                }
                else
                {
                    if (lines[i] == "YES")
                    {
                        bossBeaten = true;
                    }
                    else
                    {
                        bossBeaten = false;
                    }
                }
            }
            scoreText.text = total.ToString();
        }

}
