using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] availableCardUpgrades;
    public bool generate = true;

    public WaveManager waveManager;

    public GunController gunController;

    public float freezeMultiplier = 0;

    public Light2D outerLight;
    public int minKnockBack = 5;

    public float bulletExpiryTime = 0.45f;
    private int[] upgradeNumberStore = new int[3];

    public ShopManager shopManager;

    public HealthScript healthScirpt;
    public int takeAwayHealthModifier;

    public GameObject laser;

    public SoundManager soundManger;

    public Camera cam;

    void Start()
    {
        ResetStats();
        GenerateCards(waveManager.cards);
    }

    // Update is called once per frame
    void Update()
    {
        if (generate)
        {
            //GenerateCards(waveManager.cards);
            //generate = false;
        }
    }

    public void GenerateCards(GameObject[] cardsArray)
    {
        for (int i = 0; i < 3; i++)
        {
            upgradeNumberStore[i] = Random.Range(0, availableCardUpgrades.Length);
        }

        while(upgradeNumberStore[0] == upgradeNumberStore[1] || upgradeNumberStore[0] == upgradeNumberStore[2])
        {
            upgradeNumberStore[0] = Random.Range(0, availableCardUpgrades.Length);
        }

        while(upgradeNumberStore[1] == upgradeNumberStore[2])
        {
            upgradeNumberStore[1] = Random.Range(0, availableCardUpgrades.Length);
        }


        


        for (int i = 0; i < 3; i++)
        {
            Card firstCard = cardsArray[i].GetComponent<Card>();
            //int randInt = Random.Range(0, availableCardUpgrades.Length);
            Card chosenUpgrade = availableCardUpgrades[upgradeNumberStore[i]].GetComponent<Card>();
            firstCard.UpdateCard(chosenUpgrade.description, chosenUpgrade.preview, chosenUpgrade.id);
        }


    }

    public void ApplyUpgrade(int _id)
    {
        CardsInteractable(false);
        soundManger.PlaySound(soundManger.uiSounds[0], 0.1f, 1f);

        switch (_id)
        {
            case 0:
                CardUpgrade0();
                break;

            case 1:
                CardUpgrade1();
                break;
            case 2:
                CardUpgrade2();
                break;
            case 3:
                CardUpgrade3();
                break;
            case 4:
                CardUpgrade4();
                break;
            case 5:
                CardUpgrade5();
                break;
            case 6:
                CardUpgrade6();
                break;
        }

        waveManager.ExitUpgradeCards();
        shopManager.ShopEnter();
        shopManager.continueButton.interactable = true;

        /*
        waveManager.ExitUpgradeCards();
        waveManager.spawnerSc.ready = true;
        waveManager.spawnerSc.StartEnemies();
        */
    }




    void CardUpgrade0()
    {
        gunController.bulletForce -= 10;
        gunController.bulletPrefab.transform.localScale += new Vector3(1, 1, 1);
        Debug.Log("Upgrade 0");
    }

    void CardUpgrade1()
    {
        freezeMultiplier += 0.4f;
        gunController.speed -= 1.5f;
        if(gunController.speed <= 0)
        {
            gunController.speed = 0.5f;
        }
        Debug.Log("Upgrade 1");
    }

    void CardUpgrade2()
    {
        gunController.shootCooldownTime -= 0.05f;
        gunController.spreadModifier += 2;
        gunController.ModifySpread(gunController.spreadModifier);

    }

    void CardUpgrade3()
    {
        //cam.orthographicSize += 2;

        outerLight.intensity += 0.15f;
        minKnockBack -= 1;
    }

    void CardUpgrade4()
    {
        gunController.spreadModifier += 3; 
        gunController.numofProjectiles += 2;
        gunController.shootCooldownTime += 0.05f;

        minKnockBack = 2;
        gunController.damage /= 2;
        bulletExpiryTime = 0.4f;
        
    }

    void CardUpgrade5()
    {
        gunController.spreadModifier -= 3;
        gunController.damage += 2;
        gunController.shootCooldownTime += 0.1f;
        gunController.bulletForce += 10;
        laser.SetActive(true);

        bulletExpiryTime += 1;
        outerLight.intensity += 0.05f;
    }

    void CardUpgrade6()
    {
        waveManager.regenAmount += 15;

        takeAwayHealthModifier += 10;
        healthScirpt.UpdateMaxHP(takeAwayHealthModifier);
    }

    void ResetStats()
    {
        gunController.bulletPrefab.transform.localScale = new Vector3(1, 4, 1);
        freezeMultiplier = 0;

    }

    public void CardsInteractable(bool isInteractable)
    {
        if (isInteractable)
        {
            for (int i = 0; i < 3; i++)
            {
                waveManager.cards[i].GetComponent<Button>().interactable = true;
            }
        }

        else
        {
            for (int i = 0; i < 3; i++)
            {
                waveManager.cards[i].GetComponent<Button>().interactable = false;
            }
        }


    }
}
