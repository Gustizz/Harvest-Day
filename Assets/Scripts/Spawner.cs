using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<GameObject> nextEnemiesList = new List<GameObject>();
    List<GameObject> currentEnemies = new List<GameObject>();

    public GameObject carrotEnemy;
    public GameObject pumkinEnemy;
    public GameObject tomatoeEnemy;
    public int startingEnemiesAmount;

    public bool ready = false;

    public WaveManager waveManager;

    public int enemiesLeft;

    public UpgradeScript upgradeScript;
    public CardManager cardManager;

    public GameObject waveText;
    public int waveCount = 0;

    public int amountOfEnemiesKilled = 0;
    public float shotsHit = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startingEnemiesAmount; i++)
        {
            nextEnemiesList.Add(carrotEnemy);
        }

        enemiesLeft = startingEnemiesAmount;


        amountOfEnemiesKilled = 0;
        waveCount = 0;
    }


    public void StartEnemies()
    {

        if (ready)
        {
            for (int i = 0; i < nextEnemiesList.Count; i++)
            {

                float tempX = 0;
                float tempY = 0;

                int sides = Random.Range(0, 4);

                //sides: 0 = bot, 1 = top, 2 = left, 3 = right
                if (sides == 0)
                {
                    tempX = Random.Range(-25.0f, 25.0f);
                    tempY = Random.Range(-13.0f, -18.0f);
                }

                if (sides == 1)
                {
                    tempX = Random.Range(-25.0f, 25.0f);
                    tempY = Random.Range(13.0f, 18.0f);
                }

                if (sides == 2)
                {
                    tempX = Random.Range(-22.0f, -27.0f);
                    tempY = Random.Range(-18.0f, 18.0f);
                }

                if (sides == 3)
                {
                    tempX = Random.Range(22.0f, 27.0f);
                    tempY = Random.Range(-18.0f, 18.0f);
                }




                Instantiate(nextEnemiesList[i], new Vector2(tempX, tempY), Quaternion.identity);
                currentEnemies.Add(nextEnemiesList[i]);
            }


            enemiesLeft = nextEnemiesList.Count;

            ready = false;
            nextEnemiesList.Add(carrotEnemy);
            nextEnemiesList.Add(pumkinEnemy);

            if ((waveCount % 2) == 1)
            {
                nextEnemiesList.Add(tomatoeEnemy);
                nextEnemiesList.Add(tomatoeEnemy);
            }

        }



    }

    public void CheckIfRoundEnd()
    {
        if(enemiesLeft == 0)
        {

            upgradeScript.upgradePoints++;

            upgradeScript.upgradePointText.text = upgradeScript.upgradePoints.ToString();
            //leanTweenTest.ShopEnter();
            //waveManager.startButtonEnter();

            cardManager.GenerateCards(waveManager.cards);
            cardManager.CardsInteractable(true);
            waveManager.EnterUpgradeCards();

            waveCount++;



        }
    }
}
