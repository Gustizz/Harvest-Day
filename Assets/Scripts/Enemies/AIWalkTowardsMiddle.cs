using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalkTowardsMiddle : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sp;

    public float walkSpeed = 1;
    private float baseWalkSpeed;
    public float health = 5;

    public Animator animator;

    private bool isAlive = true;

    public float knockBackOnHouse = 1.5f;

    public Spawner spawnerSC;

    public EnemyUIController _enemyUIController;
    public Transform enemyCanvas;
    public CardManager cardManager;

    private int freezeState = 0;
    private bool canRunFunction = true;

    public EnemyFreezeManager enemyFreezeManager;

    public SoundManager soundManager;


    
    // Start is called before the first frame update
    void Start()
    {
        enemyFreezeManager.UpdateState(freezeState);

        _enemyUIController.SetUpHealthBar(health);

        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        spawnerSC = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<Spawner>();
        cardManager = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>();

        baseWalkSpeed = walkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = Vector2.Lerp(transform.position, new Vector2(0, 0), Ti);
        if (isAlive)
        {
           //SET THE UI ABOVE player
            enemyCanvas.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            enemyCanvas.rotation = Quaternion.identity;
            
            Vector3 vectorToTarget = new Vector3(0, 0, 0) - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

            rb.velocity = transform.right * walkSpeed;

        }
    }



    public void Damaged(float damageAmount, bool damageFromBullet)
    {
        if (damageFromBullet)
        {
            spawnerSC.shotsHit++;
        }



        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.enemyHit);
        soundManager.PlaySound(clip, 0.01f, 1f);
        soundManager.PlaySound(soundManager.carrotHit, 0.001f, 1f);

        float freezeMultiplier = cardManager.freezeMultiplier;
        health -= damageAmount;
        walkSpeed -= 0.075f;

        if(walkSpeed <= 0.3f)
        {
            walkSpeed = 0.5f;
        }
        KnockBack(Random.Range(cardManager.minKnockBack, cardManager.minKnockBack + 6) * 0.1f);

        if(freezeMultiplier > 0 && freezeState < 6 && damageFromBullet)
        {
            StartCoroutine(DissolveFreeze(freezeMultiplier));
        }

        _enemyUIController.UpdateHealthBar(health);
        
        if (health <= 0)
        {
            
            isAlive = false;
            animator.SetBool("isAlive", false);
            rb.velocity = transform.right * 0;

            if (canRunFunction)
            {
                spawnerSC.enemiesLeft--;
                spawnerSC.amountOfEnemiesKilled++;
                canRunFunction = false;
            }

            
            
            spawnerSC.CheckIfRoundEnd();

            StartCoroutine(DissolveBody());
            
            //Destroy(gameObject);
        }
        //Debug.Log(health);
    }

    IEnumerator DissolveBody()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);

        for (float i = 1; i > -0.2; i -= 0.1f)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, i);
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }

    public void pushBack()
    {
        transform.position += -transform.right * knockBackOnHouse;
    }
    
    public void KnockBack(float force)
    {
        transform.position += -transform.right * force;
    }


    IEnumerator DissolveFreeze(float _freezeMultiplier)
    {
        freezeState++;

        enemyFreezeManager.UpdateState(freezeState);

        walkSpeed = baseWalkSpeed - ((freezeState) * _freezeMultiplier);
        if (walkSpeed < 0)
        {
            walkSpeed = 0.1f;
        }
       // sp.color = new Color(sp.color.r - freezeState / 10, sp.color.g - freezeState / 10, sp.color.b);

        yield return new WaitForSeconds(Random.Range(3,5));

        freezeState--;
        enemyFreezeManager.UpdateState(freezeState);
        walkSpeed = baseWalkSpeed - ((freezeState / 7) * _freezeMultiplier);
        //sp.color = new Color(sp.color.r + freezeState / 10, sp.color.g + freezeState / 10, sp.color.b);
    }

}
