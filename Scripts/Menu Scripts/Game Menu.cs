using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI showTime, score, highScore;

    //[SerializeField]
    //private SpriteRenderer rocketSr;

    [SerializeField]
    private GameObject GamePanel;

    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private GameObject GameOverPanel;

    [SerializeField]
    AudioSource musicSource;

    private GameObject rocket;

    private float time = 0.0f;
    private double clock;
    private int gameOverNumber;

    private SpriteRenderer rocketSr;

    void Start()
    {
        FindRocket();
        SetScore();
    }
    void Update()
    {

        ClockTime();
    }
    void ClockTime()
    {
        if (rocketSr)
        {
            if (rocketSr.enabled)
            {
                time += Time.deltaTime;
                clock = System.Math.Round(time, 1);

                if (clock == (int)clock)
                {
                    showTime.text = clock.ToString() + ",0";
                }
                else
                {
                    showTime.text = clock.ToString();
                }
            }
            else
            {
                NewHighscore();
                SetNewScore();
                GameOverPanel.SetActive(true);
            }
        }
    }

    public void MenuPressed()
    {
        Time.timeScale = 0;
        menuButton.SetActive(false);
        GamePanel.SetActive(true);
    }
    public void GameResumed()
    {
        Time.timeScale = 1;
        menuButton.SetActive(true);
        GamePanel.SetActive(false);
    }
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    public void SetScore()
    {
        highScore.text = PlayerPrefs.GetFloat("PlayerScore").ToString();
    }
    public void SetNewScore()
    {
        score.text = clock.ToString();
    }
    public void NewHighscore()
    {

        double savedScore = PlayerPrefs.GetInt("PlayerScore");
        if (clock > savedScore)
        {
            float c = (float)clock;
            PlayerPrefs.SetFloat("PlayerScore", c);
            PlayerPrefs.Save();
        }
    }
    public void FindRocket()
    {
        rocket = GameObject.FindWithTag("Player");

        if (rocket != null)
        {
            rocketSr = rocket.GetComponent<SpriteRenderer>();
        }
    }
}
