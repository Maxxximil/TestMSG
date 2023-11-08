using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _bubbleSound_appeared;
    [SerializeField] private AudioSource _bubbleSound_disappeared;
    [SerializeField] private AudioSource _click;
    [SerializeField] private AudioSource _cash;
    [SerializeField] private AudioSource _productSelect;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBubbleAppeared()
    {
        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            _bubbleSound_appeared.Play();
        }
        
    }

    public void PlayBubbleDisappeared()
    {
        if (PlayerPrefs.GetInt("Sound") == 1) _bubbleSound_disappeared.Play();
    }

    public void PlayClick()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)  _click.Play();
    }

    public void PlayCash()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)  _cash.Play();
    }

    public void PlayProductSelect()
    {
        if (PlayerPrefs.GetInt("Sound") == 1) _productSelect.Play();
    }

    public void StopMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 1) _music.Play();
        else _music.Stop();
    }


}
