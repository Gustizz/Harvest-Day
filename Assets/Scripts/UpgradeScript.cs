using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{

    public Image[] DMGUpgradePoints;
    private int upgradeAmountDMG;
    public int damageModifier;

    public Image[] ROFUpgradePoints;
    private int upgradeAmountROF;
    public float rateOfFireModifier;

    public GunController gunControllerScript;

    public Text upgradePointText;
    public int upgradePoints = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpgradePointUpdateDMG();
        UpgradePointUpdateROF();
    }

    void UpgradePointUpdateDMG()
    {
        for (int i = 0; i < DMGUpgradePoints.Length; i++)
        {
            DMGUpgradePoints[i].enabled = !DisplayUpgrade(upgradeAmountDMG, i);
        }


    }

    void UpgradePointUpdateROF()
    {
        for (int i = 0; i < ROFUpgradePoints.Length; i++)
        {
            ROFUpgradePoints[i].enabled = !DisplayUpgrade(upgradeAmountROF, i);
        }


    }

    bool DisplayUpgrade(float _upgradePoints, int pointNumber)
    {
        return ((pointNumber) >= _upgradePoints);
    }

    public void AddDamageUpgrade()
    {

        if(upgradeAmountDMG <= 6 && upgradePoints > 0)
        {
            
            upgradeAmountDMG++;
            damageModifier++;
            upgradePoints--;
        }

        upgradePointText.text = upgradePoints.ToString();

    }

    public void AddRateOfFireUpgrade()
    {
        if (upgradeAmountDMG <= 6 && upgradePoints > 0)
        {
            
            upgradeAmountROF++;
            gunControllerScript.shootCooldownTime -= rateOfFireModifier;
            upgradePoints--;
        }

        upgradePointText.text = upgradePoints.ToString();

    }
}
