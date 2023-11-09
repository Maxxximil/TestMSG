using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//—крипт отвечает за логику одного продукта
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

    //ѕри нажатии на продукт добавл€ем или удал€ем его из списка игрока и измен€ем внешний вид продукта
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

    public void ShowCheckMark(bool f)//ѕоказываем/скрываем галочку
    {
        _checkMark.SetActive(f);
    }

    public void ShowMissMark(bool f)//ѕоказываем/скрываем крестик
    {
        _missMark.SetActive(f);
    }

    public void ChangeAlpha(float a)//ћен€ем прозрачность
    {
        Color color = _renderer.color;
        color.a = a;
        _renderer.color = color;
    }
}
