using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//скрипт с состояниями покупателя
public class Buyer : MonoBehaviour
{
    [SerializeField] private GameObject _union;
    [SerializeField] private GameObject _nice;
    [SerializeField] private GameObject _angry;

    public float speed = 1f;



    private bool _startMove = false;
    private Transform _target;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //метод управления движения в которой передаём позицию куда надо двигаться
    public void Move(Transform position)
    {
        _target = position;
        _startMove = true;
        _animator.SetBool("IsMove", _startMove);
    }

    //само движение покупателя
    private void Update()
    {
        if (_startMove)
        {
            Vector2 direction =_target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _target.position) < 0.1f)
            {
                _startMove = false;
                _animator.SetBool("IsMove", _startMove);
                BuyerController.instance.FinishTarget(this.gameObject.transform); //передаём в контроллер что мы достигли конечной точки движения
            }
        }
    }

    public void ActivateQuest()
    {
        GameController.instance.CreateQuest();//передаём в контроллер чтобы создать квест
        StartCoroutine(ShowQuest());//запускаем коротину с показыванием квеста
        
    }

    //показываем квест и через 5 сек скрываем
    IEnumerator ShowQuest()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        yield return new WaitForSeconds(5f);
        GameController.instance.ShowShopScreen();//Выводим экран магазина
        _union.SetActive(false);
        MusicController.instance.PlayBubbleDisappeared();
    }

    //Выводим эмоцию полностью верной покупки
    public void ShowNiceEmodji()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        _nice.SetActive(true);
    }

    //Выводим эмоцию неверной покупки
    public void ShowAngryEmodji()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        _angry.SetActive(true);
    }
    //Деактивируем все облачка и эмоции
    public void HideAll()
    {
        _union.SetActive(false);
        MusicController.instance.PlayBubbleDisappeared();
        _angry.SetActive(false);
        _nice.SetActive(false);
    }
}
