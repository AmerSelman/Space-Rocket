using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    AudioSource musicSource;
    // Start is called before the first frame update

    private string ON = "ON";
    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        CheckMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckMusic()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetString("Music", ON);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetString("Music") == ON)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }
}
