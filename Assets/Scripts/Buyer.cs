using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

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

    public void Move(Transform position)
    {
        _target = position;
        _startMove = true;
        _animator.SetBool("IsMove", _startMove);
    }


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
                BuyerController.instance.FinishTarget(this.gameObject.transform);
                //ActivateQuest();
            }
        }
    }

    public void ActivateQuest()
    {
        GameController.instance.CreateQuest();
        StartCoroutine(ShowQuest());
        
    }

    IEnumerator ShowQuest()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        yield return new WaitForSeconds(5f);
        GameController.instance.ShowShopScreen();
        _union.SetActive(false);
        MusicController.instance.PlayBubbleDisappeared();
    }

    public void ShowNiceEmodji()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        _nice.SetActive(true);
    }

    public void ShowAngryEmodji()
    {
        _union.SetActive(true);
        MusicController.instance.PlayBubbleAppeared();
        _angry.SetActive(true);
    }

    public void HideAll()
    {
        _union.SetActive(false);
        MusicController.instance.PlayBubbleDisappeared();
        _angry.SetActive(false);
        _nice.SetActive(false);
    }
}
