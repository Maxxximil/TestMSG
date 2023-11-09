using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт отвечает за игрока
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _union;

   public void ShowUnion(bool t)//Активируем у игрока появление облачка
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
