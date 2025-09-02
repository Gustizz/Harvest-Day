using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FetchDeathStats : MonoBehaviour
{

    public TextMeshProUGUI waveCountText;
    public TextMeshProUGUI amountOfEnemeisKilledText;
    public TextMeshProUGUI amountOfShotsFired;
    public TextMeshProUGUI amountOfShotsHit;
    public TextMeshProUGUI accuracyText;

    public DeathStatsManager deathStatManagerSc;

    // Start is called before the first frame update
    void Start()
    {
        deathStatManagerSc = GameObject.FindGameObjectWithTag("DeathStatManager").GetComponent<DeathStatsManager>();
        deathStatManagerSc.waveCount++;
        ApplyStats();

        Destroy(deathStatManagerSc.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyStats()
    {
        waveCountText.text = "Wave Count: " + deathStatManagerSc.waveCount.ToString();
        amountOfEnemeisKilledText.text = "Enemies Killed: " + deathStatManagerSc.enemiesKilled.ToString();
        amountOfShotsFired.text = "Amount Of Shots Fired: " + deathStatManagerSc.shotsFired.ToString();
        amountOfShotsHit.text = "Amount Of Shots Hit: " + deathStatManagerSc.shotsHit.ToString();

        if(Mathf.RoundToInt((deathStatManagerSc.shotsHit / deathStatManagerSc.shotsFired) * 100) <= 0)
        {
            accuracyText.text = 0 + "%";
        }
        else
        {
            accuracyText.text = (Mathf.RoundToInt((deathStatManagerSc.shotsHit / deathStatManagerSc.shotsFired) * 100)).ToString() + "%";
        }

        


        
    }

}
