using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private bool _canSell = false;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            Color color = _renderer.color;
            color.a = 1f;
            _renderer.color = color;
            _canSell = isActive;
        }
        else
        {
            Color color = _renderer.color;
            color.a = 0.5f;
            _renderer.color = color;
            _canSell = isActive;
        }
    }

    private void OnMouseDown()
    {
        if (_canSell)
        {
            PlayerControl.Instance.SellProducts();
        }
    }
}
