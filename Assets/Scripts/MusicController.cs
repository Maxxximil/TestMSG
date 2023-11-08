using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    
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
        _bubbleSound_appeared.Play();
    }

    public void PlayBubbleDisappeared()
    {
        _bubbleSound_disappeared.Play();
    }

    public void PlayClick()
    {
        _click.Play();
    }

    public void PlayCash()
    {
        _cash.Play();
    }

    public void PlayProductSelect()
    {
        _productSelect.Play();
    }

}
