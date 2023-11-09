using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Скрипт управления игрой
public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField] private GameObject _shopScreen;

    public List<SimpleProduct> allProducts;
    public Transform startPositionShopScreen;
    public Transform endPositionShopScreen;
    public float speed = 1f;

    private SimpleProduct[] _questProducts;
    private bool _showShopScreen = false;
    private Transform _target;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _target = startPositionShopScreen;
        _questProducts = new SimpleProduct[1];
    }

    public void CreateQuest()//Создаем квест на покупку
    {
        int productCount = Random.Range(1, 4);
        PlayerControl.Instance.questCount = productCount;//Передаем игроку сколько должно быть продуктов
        _questProducts = new SimpleProduct[productCount];
        for(int i =  0; i < productCount; i++)
        {
            do
            {
                _questProducts[i] = SetQuestProduct();
            } while (Check(_questProducts[i]));
        }
        BuyerController.instance.SetQuest(_questProducts);//Передаем покупателю список продуктов
    }

    public SimpleProduct[] GetQuestProducts()//Получаем список продуктов
    {
        return _questProducts;
    }

    private bool Check(SimpleProduct product)//Проверяем чтобы продукты не повторялись
    {
        int count = 0;
        foreach(var i in _questProducts)
        {
            if(i == product) count++;
        }
        if(count>1) return true;
        else return false;
    }

    private SimpleProduct SetQuestProduct()//Выбираем рандомный продукт со всего списка
    {
        int randomProduct = Random.Range(1, allProducts.Count);
        return allProducts[randomProduct];
    }

    

    public void ShowShopScreen()//Выводим/скрываем экран магазина
    {
        if (_target == endPositionShopScreen)
        {
            _target = startPositionShopScreen;
        }
        else
        {
            _target = endPositionShopScreen;
        }
        _showShopScreen = true;
    }

    private void Update()//Движение магазина
    {
        if (_showShopScreen)
        {
            Vector2 direction = _target.position - _shopScreen.transform.position;
            _shopScreen.transform.Translate(direction.normalized * speed * Time.deltaTime);

            if (Vector2.Distance(_shopScreen.transform.position, _target.position) < 0.1f)
            {
                _showShopScreen = false;
            }
        }
    }

    public void ResetGame()//Сброс игры
    {
        PlayerControl.Instance.ResetPlayer();//Сброс игрока
        BuyerController.instance.ResetBuyer();//Сброс покупателя
    }
}
