using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProduct : MonoBehaviour
{
    public Product product;
    public GameObject _checkMark;


    private bool _isClicked = false;
    private SpriteRenderer _renderer;
    

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (!_isClicked && PlayerControl.Instance.canClick)
        {
            PlayerControl.Instance.AddChoosenProduct(this);
            _isClicked = true;
            Color color = _renderer.color;
            color.a = 0.3f;
            _renderer.color = color;
            _checkMark.SetActive(true);
        }
        else if(_isClicked)
        {
            PlayerControl.Instance.RemoveChoosenProduct(this);
            _isClicked = false;
            Color color = _renderer.color;
            color.a = 1f;
            _renderer.color = color;
            _checkMark.SetActive(false);
        }
    }
}
