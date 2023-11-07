using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{ 
    public static PlayerControl Instance;

    [SerializeField] private SellButton sellButton;
    [SerializeField] private Player player;
    [SerializeField] private GameObject firstProduct;
    [SerializeField] private GameObject secondProduct;
    [SerializeField] private GameObject thirdProduct;

    public bool canClick = true;
    public int questCount = 0;
    public Transform trans;

    private List<SimpleProduct> _choosenProducts;
    private List<SimpleProduct> _products;
    private int count = 0;
    private int _correctProductsCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateChoosenProducts()
    {
        _choosenProducts = new List<SimpleProduct>();
        _products = new List<SimpleProduct>();
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
                    _choosenProducts[i].product.isRight = true;
                    _correctProductsCount++;
                }
            }
        }
        GameController.instance.ShowShopScreen();
        ChoosenProducts(_choosenProducts.ToArray());
        StartCoroutine(FirstCheck());
        float j = 1.5f;
        foreach (SimpleProduct product in _products)
        {
            if (product.product.isRight)
            {

                StartCoroutine(ShowCheckMark(product, j));
            }
            else
            {
                StartCoroutine(ShowMissMark(product, j));
            }
            j += 0.5f;
        }
    }

    

    
    private IEnumerator FirstCheck()
    {
        yield return new WaitForSeconds(1);
        player.ShowUnion();
    }

    

    private IEnumerator ShowCheckMark(SimpleProduct product, float i)
    {
        yield return new WaitForSeconds(i);
        product.ShowCheckMark(true);
    }
    private IEnumerator ShowMissMark(SimpleProduct product, float i)
    {
        yield return new WaitForSeconds(i);
        product.ShowMissMark(true);
    }

   


    private void ChoosenProducts(SimpleProduct[] products)
    {
        foreach(SimpleProduct product in products)
        {
            product.ChangeAlpha(0.5f);
        }
        Vector3 vec = Vector3.zero;
        vec.z = -2;
        if (products.Length == 1)
        {
            SimpleProduct firstSimpleProduct = Instantiate(products[0], vec, Quaternion.identity);
            ResetSimpleProduct(firstSimpleProduct, firstProduct.transform);
            firstSimpleProduct.product.isRight = products[0].product.isRight; 
            _products.Add(firstSimpleProduct);
        } else if (products.Length == 2)
        {
            SimpleProduct firstSimpleProduct = Instantiate(products[0], vec, Quaternion.identity);
            ResetSimpleProduct(firstSimpleProduct, firstProduct.transform);
            SimpleProduct secondSimpleProduct = Instantiate(products[1], vec, Quaternion.identity);
            ResetSimpleProduct(secondSimpleProduct, secondProduct.transform);
            _products.Add(firstSimpleProduct);
            _products.Add(secondSimpleProduct);

        }
        else if (products.Length == 3)
        {
            SimpleProduct firstSimpleProduct = Instantiate(products[0], vec, Quaternion.identity);
            ResetSimpleProduct(firstSimpleProduct, firstProduct.transform);
            SimpleProduct secondSimpleProduct = Instantiate(products[1], vec, Quaternion.identity);
            ResetSimpleProduct(secondSimpleProduct, secondProduct.transform);
            SimpleProduct thirdSimpleProduct = Instantiate(products[2], vec, Quaternion.identity);
            ResetSimpleProduct(thirdSimpleProduct, thirdProduct.transform);
            _products.Add(firstSimpleProduct);
            _products.Add(secondSimpleProduct);
            _products.Add(thirdSimpleProduct);
        }
        
    }

    private void ResetSimpleProduct(SimpleProduct simpleProduct, Transform transform)
    {
        simpleProduct.transform.parent = transform.transform;
        simpleProduct.transform.position = transform.transform.position;
        simpleProduct.transform.localScale = transform.transform.localScale;
        simpleProduct._checkMark.SetActive(false);
    }
}
