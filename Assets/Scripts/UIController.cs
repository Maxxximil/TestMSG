using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TMP_Text _money;
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    [SerializeField] private GameObject _musicOn;
    [SerializeField] private GameObject _musicOff;

    public int startMoney = 500;
    public int music = 1;
    public int sound = 1;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", startMoney);
        }
        _money.text ="$ " + PlayerPrefs.GetInt("Money").ToString();
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        music = PlayerPrefs.GetInt("Music");
        sound = PlayerPrefs.GetInt("Sound");
        MusicController.instance.StopMusic();
        CheckSettings();
    }

    private void CheckSettings()
    {
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            _musicOn.SetActive(true);
            _musicOff.SetActive(false);
        }
        else
        {
            _musicOn.SetActive(false);
            _musicOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            _soundOn.SetActive(true);
            _soundOff.SetActive(false);
        }
        else
        {
            _soundOn.SetActive(false);
            _soundOff.SetActive(true);
        }
    }

    public void AddMoney(int money)
    {
        money += PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", money);
        _money.text= "$ " + money.ToString();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Music", music);
        PlayerPrefs.SetInt("Sound", sound);
        MusicController.instance.StopMusic();
    }

    public void EnableMusic()
    {
        music = 1;
    }
    public void DisableMusic()
    {
        music = 0;
    }
    public void EnableSound()
    {
        sound = 1;
    }
    public void DisableSound()
    {
        sound = 0;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
