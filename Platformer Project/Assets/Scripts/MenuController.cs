using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject canvasEnd;
    [SerializeField] private Text scoreText;
    [SerializeField] private InputProcessing ip;
    [SerializeField] private LevelController lc;
    [SerializeField] private string fileName;
    private string path;

    void Start()
    {
        
        path = Application.streamingAssetsPath + "/" + fileName + ".txt";
    }

    public void Pause()
    {
        Time.timeScale = 0;
        canvasMain.gameObject.SetActive(false);
        canvasPause.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        canvasMain.gameObject.SetActive(true);
        canvasPause.gameObject.SetActive(false);

    }
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SetMode()
    {
        ip.endLevelMenuMode = true;
        canvasEnd.SetActive(true);
        scoreText.text = lc.GetScore().ToString();
    }

    public void SetModeBoss()
    {
        ip.endBossLevelMenuMode = true;
        canvasEnd.SetActive(true);
    }

    public void FileUpdate(int level)
    {
        string[] lines = File.ReadAllLines(path);
        
        StreamReader sr = new StreamReader(path);
        int currentScore = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == level)
            {
                currentScore = Int32.Parse(sr.ReadLine());
            }
            else
            {
                sr.ReadLine();
            }
        }
        sr.Close();

        StreamWriter sw = new StreamWriter(path, false);

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == level)
            {
                if(currentScore < lc.GetScore())
                {
                    Debug.Log("Writing");
                    sw.WriteLine(lc.GetScore().ToString());
                } else
                {
                    sw.WriteLine(lines[i]);
                }
            }
            else
            {
                sw.WriteLine(lines[i]);
            }
        }
        sw.Close();
        

    }

    public void FileUpdateBoss(int level)
    {

        string[] lines = File.ReadAllLines(path);

        StreamWriter sw = new StreamWriter(path, false);

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == level)
            {
                sw.WriteLine("YES");
            }
            else
            {
                sw.WriteLine(lines[i]);
            }
        }
        sw.Close();

    }
}
