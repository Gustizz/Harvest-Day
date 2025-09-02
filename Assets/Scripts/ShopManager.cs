using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;

public class ShopManager : MonoBehaviour
{

    public LeanTweenType easeType, exitEaseType;
    public Spawner spawnerSc;

    public Image[] addTurretUpgradePoints;
    private int amountOfTurrets;

    public Image[] spikedFenceUpgradePoints;
    private int spikedFenceAmount;
    public GameObject[] spikedFenceProgression;
    public ColliderTest fence;

    public GameObject turretPrefab;
    public WaveManager waveManager;

    public Button continueButton;

    public TextMeshProUGUI upgradePointsText;
    public int upgradePoints;

    public ParticleSystem shopEnterParticles;

    public SoundManager soundManager;

    public GunController gunController;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpgradeVisuals(addTurretUpgradePoints, amountOfTurrets);
        UpgradeVisuals(spikedFenceUpgradePoints, spikedFenceAmount);
    }

    public void ContinueButtonPressed()
    {
        gunController.canShoot = true;

        continueButton.interactable = false;
        soundManager.PlaySound(soundManager.uiSounds[0], 0.1f, 1f);

        

        //StartCoroutine(ShopExitSoundEffectDelay());



        LeanTween.moveX(gameObject, 27f, 1f).setDelay(0.25f).setEase(exitEaseType).setOnComplete(StartWave);
        StartCoroutine(ShakeCamera());


    }

    void StartWave()
    {
        waveManager.spawnerSc.ready = true;
        waveManager.spawnerSc.StartEnemies();
    }

    public void ShopEnter()
    {
        StartCoroutine(ShakeCamera());

        upgradePoints++;
        LeanTween.moveX(gameObject, 15.5f, 0.75f).setDelay(1f).setEase(easeType);
        upgradePointsText.text = upgradePoints.ToString();

        


    }

    public void ShopExit()
    {
        

        LeanTween.moveX(gameObject, 27f, 1.5f).setDelay(0.25f).setEase(exitEaseType);
        Instantiate(shopEnterParticles, shopEnterParticles.transform.position, Quaternion.identity);

    }

    IEnumerator ShakeCamera()
    {
        yield return new WaitForSeconds(1.32f);
        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.shopClose);
        soundManager.PlaySound(clip, 0.1f, 1f);

        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.2f, 1f);

        Instantiate(shopEnterParticles, shopEnterParticles.transform.position, Quaternion.identity);

    }

    IEnumerator ShopExitSoundEffectDelay()
    {
        yield return new WaitForSeconds(1.32f);
        soundManager.PlaySound(soundManager.shopClose[0], 0.1f, 1f);
    }


    public void AddTurretUpgrade()
    {
        if(amountOfTurrets < 5 && upgradePoints > 0)
        {

            soundManager.PlaySound(soundManager.uiSounds[1], 0.05f, 1f);

            upgradePoints--;
            upgradePointsText.text = upgradePoints.ToString();
            amountOfTurrets++;

            AddTurret(amountOfTurrets);


        }
    }

    public void AddSpikedFenceUpgrade()
    {
        if(spikedFenceAmount < 4 && upgradePoints > 0)
        {

            soundManager.PlaySound(soundManager.uiSounds[1], 0.05f, 1f);

            upgradePoints--;
            upgradePointsText.text = upgradePoints.ToString();
            spikedFenceAmount++;

            AddSpikedFence(spikedFenceAmount);
        }
    }
    void UpgradeVisuals(Image[] upgradePointList, int amountOfUpgrade)
    {
        for (int i = 0; i < upgradePointList.Length; i++)
        {
            upgradePointList[i].enabled = !DisplayUpgrade(amountOfUpgrade, i);
        }


    }

    bool DisplayUpgrade(float _upgradePoints, int pointNumber)
    {
        upgradePointsText.text = upgradePoints.ToString();
        return ((pointNumber) >= _upgradePoints);
    }

    void AddTurret(int pos)
    {
        // 1 = bottom left, 2 = top left, 3 = top right, 4 = bottom right

        switch (pos)
        {
            case 1:
                Instantiate(turretPrefab, new Vector2(-3, -4.1f), Quaternion.identity);
                break;
            case 2:
                Instantiate(turretPrefab, new Vector2(-3, 1.5f), Quaternion.identity);
                break;
            case 3:
                Instantiate(turretPrefab, new Vector2(3, 1.5f), Quaternion.identity);
                break;
            case 4:
                Instantiate(turretPrefab, new Vector2(3, -4.1f), Quaternion.identity);
                break;
        }

    }

    void AddSpikedFence(int amount)
    {
        switch (amount)
        {
            case 1:
                Instantiate(spikedFenceProgression[0], new Vector2(0.667f, -0.31f), Quaternion.identity);
                break;
            case 2:
                Instantiate(spikedFenceProgression[1], new Vector2(0.667f, -0.31f), Quaternion.identity);
                break;
            case 3:
                Instantiate(spikedFenceProgression[2], new Vector2(0.667f, -0.31f), Quaternion.identity);
                break;

        }


        fence.spikeDamage++;

    }
}
