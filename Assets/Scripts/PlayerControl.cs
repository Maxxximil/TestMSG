using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{ 
    public static PlayerControl Instance;

    [SerializeField] private SellButton sellButton;

    public bool canClick = true;
    public int questCount = 0;

    private List<SimpleProduct> _choosenProducts;
    private int count = 0;
    private int _correctProductsCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateChoosenProducts()
    {
        _choosenProducts = new List<SimpleProduct>();
    }

    public void AddChoosenProduct(SimpleProduct simpleProduct)
    {
        _choosenProducts.Add(simpleProduct);
        count++;
        Check();
    }

    public void RemoveChoosenProduct(SimpleProduct simpleProduct)
    {
        _choosenProducts.Remove(simpleProduct);
        count--;
        Check();
    }

    public void Check()
    {
        if(count >= questCount)
        {
            canClick = false;
            sellButton.SetActive(true);
        }
        else
        {
            canClick = true;
            sellButton.SetActive(false);
        }
    }

    public void SellProducts()
    {
        SimpleProduct[] prod = GameController.instance.GetQuestProducts();
        for (int i = 0; i < _choosenProducts.Count; i++)
        {
            foreach (SimpleProduct product in prod)
            {
                if (_choosenProducts[i].product.productId == product.product.productId)
                {
                    _correctProductsCount++;
                }
            }
        }
        GameController.instance.ShowShopScreen();
        if (_correctProductsCount == prod.Length)
        {
            Debug.Log("Max Products");
        }
        else
        {
            Debug.Log("Right products: " +  _correctProductsCount);
        }
    }
}
