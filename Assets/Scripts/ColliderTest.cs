using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{

    HealthScript hpScript;

    public GameObject cam;
    public int spikeDamage = 0;

    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        hpScript = cam.GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Carrot")
        {
            collision.gameObject.GetComponent<AIWalkTowardsMiddle>().pushBack();
            collision.gameObject.GetComponent<AIWalkTowardsMiddle>().Damaged(spikeDamage, false);
            hpScript.health -= 5;

            AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.fenceHit);
            soundManager.PlaySound(clip, 0.025f, 1f);
        }

        if(collision.gameObject.tag == "PumkinSeed")
        {
            hpScript.health -= 1;

            AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.fenceHit);
            soundManager.PlaySound(clip, 0.025f, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        hpScript.health -= damage;

        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.fenceHit);
        soundManager.PlaySound(clip, 0.025f, 1f);
    }
}
