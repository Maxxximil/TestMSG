using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private GameObject _buyer;


    private void Start()
    {
        Enter();
    }

    private void Enter()
    {
        _buyer.SetActive(true);
        Move();
    }

    private void Exit()
    {
        _buyer.SetActive(false);
    }

    private void Move()
    {
        Buyer.buyer.Move(_position);
    }
}
