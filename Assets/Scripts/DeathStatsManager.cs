using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStatsManager : MonoBehaviour
{
    public static DeathStatsManager Instance;

    public int waveCount = 0;
    public int enemiesKilled = 0;
    public float shotsFired = 0;
    public float shotsHit = 0;

    public Spawner spawnerSc;
    public CardManager cardManagerSc;
    public GunController turretTop;

    public bool ready;

    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        waveCount = 0;
        enemiesKilled = 0;

        //Instance = this;
        //DontDestroyOnLoad(gameObject);


    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FetchStats()
    {
        waveCount = spawnerSc.waveCount;
        enemiesKilled = spawnerSc.amountOfEnemiesKilled;
        shotsFired = (int)turretTop.amountShotsFired;
        shotsHit = (int)spawnerSc.shotsHit;
    }
}
