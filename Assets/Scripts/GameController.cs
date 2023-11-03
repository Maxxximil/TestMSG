using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<SimpleProduct> allProducts;

    private SimpleProduct[] _questProducts;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _questProducts = new SimpleProduct[1];
    }

    public void CreateQuest()
    {
        int productCount = Random.Range(1, 4);
        _questProducts = new SimpleProduct[productCount];
        for(int i =  0; i < productCount; i++)
        {
            do
            {
                _questProducts[i] = SetQuestProduct();
            } while (Check(_questProducts[i]));
        }
        BuyerController.instance.SetQuest(_questProducts);
    }

    private bool Check(SimpleProduct product)
    {
        int count = 0;
        foreach(var i in _questProducts)
        {
            if(i == product) count++;
        }
        if(count>1) return true;
        else return false;
    }

    private SimpleProduct SetQuestProduct()
    {
        int randomProduct = Random.Range(1, allProducts.Count);
        return allProducts[randomProduct];
    }
}
