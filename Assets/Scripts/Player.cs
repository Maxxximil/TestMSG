using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _union;

   public void ShowUnion()
    {
        _union.SetActive(true);
    }
}
