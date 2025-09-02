using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenTest : MonoBehaviour
{

    public Vector2 destination;
    public LeanTweenType easeType, exitEaseType;

    public GameObject readyButton;
    public LeanTweenType readyButtonEaseType;

    public Spawner spawnerSc;


    public bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        //ShopEnter();
        startButtonEnter();
    }

    // Update is called once per frame
    void Update()
    {
        //if (ready)
        //{
           // startButtonEnter();
            //ready = false;
        //}
    }

    public void AnimateText()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f);
    }

    public void ShopEnter()
    {
        LeanTween.moveX(gameObject, 15.5f, 1f).setDelay(0.25f).setEase(easeType);
    }

    public void startButtonEnter()
    {
        LeanTween.scale(readyButton, new Vector3(2,2,2), 1f).setEase(readyButtonEaseType);
    }

    public void startButtonExit()
    {
        LeanTween.scale(readyButton, new Vector3(0, 0, 0), 1f).setEase(readyButtonEaseType);
    }

    public void ShopExit()
    {
        LeanTween.moveX(gameObject, 27f, 1f).setDelay(0.25f).setEase(exitEaseType);
    }
}
