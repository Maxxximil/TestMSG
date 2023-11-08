using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _union;

   public void ShowUnion(bool t)
    {
        _union.SetActive(t);
        if (t)
        {
            MusicController.instance.PlayBubbleAppeared();
        }
        else
        {
            MusicController.instance.PlayBubbleDisappeared();
        }
    }
}
