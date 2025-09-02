using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricitySounds : MonoBehaviour
{
    // Start is called before the first frame update


    public SoundManager soundManager;
    public float volume;


    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(StaticSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StaticSound()
    {
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        soundManager.PlaySound(soundManager.electricityFence, volume, 1f);
        StartCoroutine(StaticSound());
    }
}
