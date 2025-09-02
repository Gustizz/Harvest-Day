using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public Image[] healthPoints;
    public float health, maxHealth = 130;

    public DeathStatsManager deathStatsManager;

    public MenuScript menuScript;

    private bool died = false;

    public Image deathTint;

    void Start()
    {
        health = maxHealth * 10;
        deathTint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();   
    }

    bool DisplayHealthPoint(float _health, int pointNumber)
    {
        return ((pointNumber * 10) >= _health);
    }

    void HealthBarFiller()
    {
        for(int i = 0; i < healthPoints.Length; i++)
        {
            healthPoints[i].enabled = !DisplayHealthPoint(health, i);
        }

        CheckIfDead();
    }

    void CheckIfDead()
    {

        if(health <= 0 && !died)
        {
            died = true;

            deathStatsManager.FetchStats();
            StartCoroutine(DeathAnimation());
            
            
        }
    }

    public void UpdateMaxHP(int maxHealthModifier)
    {
        maxHealth -= maxHealthModifier;
    }

    public void RegenHouse(int regenAmount)
    {
        health += regenAmount;
    }

    IEnumerator DeathAnimation()
    {
        deathTint.enabled = true;

        for (float i = 0; i <= 0.35f; i+= 0.03f)
        {
            deathTint.color = new Color(deathTint.color.r, deathTint.color.g, deathTint.color.b, i);
            yield return new WaitForSeconds(0.20f);
        }

        yield return new WaitForSeconds(0.75f);

        menuScript.LoadNextLevel();
    }
}
