using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;


//Скрипт для управления покупателем
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

    //Активируем покупателя
    private void Enter()
    {
        _buyerObject.SetActive(true);
        Move(_buyPosition);//передаем место куда надо передвигаться
    }

    //Разворачиваем покупателя и передаем координаты к выходу
    public void Exit()
    {
        Vector2 vec = new Vector2(-buyer.gameObject.transform.localScale.x, buyer.gameObject.transform.localScale.y);
        buyer.gameObject.transform.localScale = vec;
        Move(_exitPosition);
    }

    //Вызываем у покупателя передвижение в переданные координаты
    private void Move(Transform m)
    {
       buyer.Move(m);
    }

    //Проверяем в какую именно точку мы дошли
    public void FinishTarget(Transform p)
    {
        if(Vector2.Distance(p.position, _buyPosition.position) < 0.1f)
        {
            buyer.ActivateQuest();//Если в точку покупки то активируем квест
        }
        if(Vector2.Distance(p.position, _exitPosition.position) < 0.1f)//Если в точку выхода, то деактивируем покупателя и разворачиваем его
        {
            _buyerObject.SetActive(false);
            Vector2 vec = new Vector2(-buyer.gameObject.transform.localScale.x, buyer.gameObject.transform.localScale.y);
            buyer.gameObject.transform.localScale = vec;
            GameController.instance.ResetGame();//Отчищаем и перезапускаем игру
        }
    }
    //Реакция на покупку
    public void Reaction(bool t)
    {
        foreach(var qp in _questProducts)
        {
            qp.gameObject.SetActive(false);//Деактивируем продукты в облачке
        }
        if (t)//показываем эмоцию в зависимости от кол-ва продуктов
        {
            buyer.ShowNiceEmodji();
        }
        else
        {
            buyer.ShowAngryEmodji();
        }
    }

    public void SetQuest(SimpleProduct[] products)//Получаем список продуктов и создаем их в облачке
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

    public void ResetBuyer()//Сбрасываем покупателся
    {
        buyer.HideAll();
        for (int i = _questProducts.Count - 1; i >= 0; i--)//Удаляем продукты из облачка
        {
            Destroy(_questProducts[i].gameObject);
            _questProducts.RemoveAt(i);

        }
        StartCoroutine(Pause());//Запускаем паузу в секунду и заново активируем покупателя
       
        
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        Enter();//Новый круг покупки
    }
}
