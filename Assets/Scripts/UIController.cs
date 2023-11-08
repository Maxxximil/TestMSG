using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private TMP_Text _money;

    public int startMoney = 500;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", startMoney);
        }
        _money.text ="$ " + PlayerPrefs.GetInt("Money").ToString();
    }

    public void AddMoney(int money)
    {
        money += PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", money);
        _money.text= "$ " + money.ToString();
    }
}
