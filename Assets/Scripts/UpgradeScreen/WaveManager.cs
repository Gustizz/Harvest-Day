using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    public GameObject readyButton;
    public LeanTweenType readyButtonEaseType;
    public LeanTweenType EnterEaseTypeCards, ExitEaseTypeCards;

    public Spawner spawnerSc;
    public bool ready = false;

    public GameObject[] cards;
    public GameObject levelUpText;

    public HealthScript healthScirpt;
    public int regenAmount;

    public SoundManager soundManager;

    public GunController gunController;

    // Start is called before the first frame update
    void Start()
    {
        //startButtonEnter();
        EnterUpgradeCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void startButtonEnter()
    //{
    //    LeanTween.scale(readyButton, new Vector3(2, 2, 2), 1f).setEase(readyButtonEaseType);
    //}

    //public void startButtonExit()
    //{
        //LeanTween.scale(readyButton, new Vector3(0, 0, 0), 1f).setEase(readyButtonEaseType);
    //}

    public void EnterUpgradeCards()
    {
        gunController.canShoot = false;

        healthScirpt.RegenHouse(regenAmount);
        

        LeanTween.moveY(levelUpText, 8.2f, 1f).setDelay(0.25f).setEase(EnterEaseTypeCards);

        for (int i = 0; i < cards.Length; i++)
        {
            LeanTween.moveY(cards[i], -1f, 1f).setDelay(0.25f).setEase(EnterEaseTypeCards);
        }
        StartCoroutine(SoundEffectDelay());
        
    }

    public void ExitUpgradeCards()
    {
        LeanTween.moveY(levelUpText, 15f, 1f).setDelay(0.25f).setEase(ExitEaseTypeCards);

        for (int i = 0; i < cards.Length; i++)
        {
            LeanTween.moveY(cards[i], -22f, 1f).setDelay(0.25f).setEase(ExitEaseTypeCards);
        }
        StartCoroutine(SoundEffectDelay());
    }

    IEnumerator SoundEffectDelay()
    {
        yield return new WaitForSeconds(0.4f);
        soundManager.PlaySound(soundManager.cardsEntering, 0.01f, 1f);
    }
}
