using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    public static BuyerController instance;

    [SerializeField] private Transform _position;
    [SerializeField] private GameObject _buyerObject;
    [SerializeField] private GameObject firstProduct;
    [SerializeField] private GameObject secondProduct;
    [SerializeField] private GameObject thirdProduct;

    public Buyer buyer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Enter();
    }

    private void Enter()
    {
        _buyerObject.SetActive(true);
        Move();
    }

    private void Exit()
    {
        _buyerObject.SetActive(false);
    }

    private void Move()
    {
       buyer.Move(_position);
    }

    public void Reaction()
    {

    }

    public void SetQuest(SimpleProduct[] products)
    {
        switch (products.Length)
        {
            case 1:
                Instantiate(products[0],firstProduct.transform);
                break;
            case 2:
                Instantiate(products[0], firstProduct.transform);
                Instantiate(products[1], secondProduct.transform);
                break;
            case 3:
                Instantiate(products[0], firstProduct.transform);
                Instantiate(products[1], secondProduct.transform);
                Instantiate(products[2], thirdProduct.transform);
                break;
        }
    }
}
