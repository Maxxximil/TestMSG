using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProduct : MonoBehaviour
{
    public Product product;
    public GameObject _checkMark;
    public GameObject _missMark;


    public bool isClicked = false;
    private SpriteRenderer _renderer;
    

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        product.isRight = false;
    }

    private void OnMouseDown()
    {
        if (!isClicked && PlayerControl.Instance.canClick)
        {
            PlayerControl.Instance.AddChoosenProduct(this);
            isClicked = true;
            ChangeAlpha(0.3f);
            _checkMark.SetActive(true);
        }
        else if(isClicked)
        {
            PlayerControl.Instance.RemoveChoosenProduct(this);
            isClicked = false;
            ChangeAlpha(1f);
            _checkMark.SetActive(false);
        }
    }

    public void ShowCheckMark(bool f)
    {
        _checkMark.SetActive(f);
    }

    public void ShowMissMark(bool f)
    {
        _missMark.SetActive(f);
    }

    public void ChangeAlpha(float a)
    {
        Color color = _renderer.color;
        color.a = a;
        _renderer.color = color;
    }
}
