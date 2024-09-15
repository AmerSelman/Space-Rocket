using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private int rocket = 0;

    [SerializeField]
    private GameObject OptionPanel;

    [SerializeField]
    private GameObject HighscorePanel;

    [SerializeField]
    private TextMeshProUGUI highScore;

    [SerializeField]
    AudioSource musicSource;

    void Start()
    {
        SetHighscore();
        SetAdsTime();
    }

    public void PlayGame()
    {

        GameManager.instance.RocketIndex = rocket;

        SceneManager.LoadScene("Gameplay");
    }

    public void GameOptions()
    {
        OptionPanel.SetActive(true);
    }
    public void OpenHighscorePanel()
    {
        HighscorePanel.SetActive(true);
    }

    public void CloseGameOptions()
    {
        OptionPanel.SetActive(false);
    }
    public void CloseHighscorePanel()
    {
        HighscorePanel.SetActive(false);
    }

    public void SetHighscore()
    {
        if (!PlayerPrefs.HasKey("PlayerScore"))
        {
            PlayerPrefs.SetFloat("PlayerScore", 0.0f);
            PlayerPrefs.Save();

        }
        highScore.text = PlayerPrefs.GetFloat("PlayerScore").ToString();
    }
    public void SetAdsTime()
    {
        if (!PlayerPrefs.HasKey("GameOver"))
        {
            PlayerPrefs.SetInt("GameOver", 0);
            PlayerPrefs.Save();
        }
    }

    public void MusicOn()
    {
        PlayerPrefs.SetString("Music", "ON");
        PlayerPrefs.Save();
        musicSource.Play();
    }
    public void MusicOff()
    {
        PlayerPrefs.SetString("Music", "OFF");
        PlayerPrefs.Save();
        musicSource.Stop();

    }
    public void GameExit()
    {
        Application.Quit();
    }
}