using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class BuyerController : MonoBehaviour
{
    public static BuyerController instance;

    [SerializeField] private Transform _buyPosition;
    [SerializeField] private Transform _exitPosition;
    [SerializeField] private GameObject _buyerObject;
    [SerializeField] private GameObject firstProduct;
    [SerializeField] private GameObject secondProduct;
    [SerializeField] private GameObject thirdProduct;

    public Buyer buyer;

    private List<SimpleProduct> _questProducts;

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
        Move(_buyPosition);
    }

    public void Exit()
    {
        Vector2 vec = new Vector2(-buyer.gameObject.transform.localScale.x, buyer.gameObject.transform.localScale.y);
        buyer.gameObject.transform.localScale = vec;
        Move(_exitPosition);
    }

    private void Move(Transform m)
    {
       buyer.Move(m);
    }

    public void FinishTarget(Transform p)
    {
        if(Vector2.Distance(p.position, _buyPosition.position) < 0.1f)
        {
            buyer.ActivateQuest();
        }
        if(Vector2.Distance(p.position, _exitPosition.position) < 0.1f)
        {
            _buyerObject.SetActive(false);
            Vector2 vec = new Vector2(-buyer.gameObject.transform.localScale.x, buyer.gameObject.transform.localScale.y);
            buyer.gameObject.transform.localScale = vec;
            GameController.instance.ResetGame();
        }
    }

    public void Reaction(bool t)
    {
        foreach(var qp in _questProducts)
        {
            qp.gameObject.SetActive(false);
        }
        if (t)
        {
            buyer.ShowNiceEmodji();
        }
        else
        {
            buyer.ShowAngryEmodji();
        }
    }

    public void SetQuest(SimpleProduct[] products)
    {
        _questProducts = new List<SimpleProduct>();
        switch (products.Length)
        {
            case 1:
                var fp = Instantiate(products[0],firstProduct.transform);
                _questProducts.Add(fp);
                break;
            case 2:
                var ffp = Instantiate(products[0], firstProduct.transform);
                var sp = Instantiate(products[1], secondProduct.transform);
                _questProducts.Add(ffp);
                _questProducts.Add(sp);

                break;
            case 3:
                var fffp = Instantiate(products[0], firstProduct.transform);
                var ssp = Instantiate(products[1], secondProduct.transform);
                var tp = Instantiate(products[2], thirdProduct.transform);
                _questProducts.Add(fffp);
                _questProducts.Add(ssp);
                _questProducts.Add(tp);
                break;
        }
        foreach (var product in _questProducts)
        {
            var col = product.GetComponent<BoxCollider2D>();
            col.enabled = false;
        }
    }

    public void ResetBuyer()
    {
        buyer.HideAll();
        for (int i = _questProducts.Count - 1; i >= 0; i--)
        {
            Destroy(_questProducts[i].gameObject);
            _questProducts.RemoveAt(i);

        }
        StartCoroutine(Pause());
       
        
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        Enter();
    }
}
