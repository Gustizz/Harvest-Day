using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PumkinScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float walkSpeed = 1;
    private float baseWalkSpeed;
    public float health = 5;

    public Animator animator;

    private bool isAlive = true;
    public bool needWalk = true;


    public Transform firePoint;
    public int bulletForce;
    public GameObject bulletPrefab;

    public Spawner spawnerSC;
    public SpriteRenderer sp;
    public CardManager cardManager;

    private int freezeState = 0;
    private bool canRunFunction = true;

    public Light2D outerGlow;

    public EnemyUIController _enemyUIController;
    public Transform enemyCanvas;

    public EnemyFreezeManager enemyFreezeManager;

    public SoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        _enemyUIController.SetUpHealthBar(health);

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        spawnerSC = GameObject.FindGameObjectWithTag("SpawnController").GetComponent<Spawner>();
        rb = GetComponent<Rigidbody2D>();
        cardManager = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>();

        baseWalkSpeed = walkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //walkingDist = Vector2.Distance(transform.position, new Vector2(0, 0));


        if (isAlive && needWalk)
        {

            enemyCanvas.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            enemyCanvas.rotation = Quaternion.identity;

            Vector3 vectorToTarget = new Vector3(0, 0, 0) - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);

            rb.velocity = transform.right * walkSpeed;
        }

        else if (isAlive & !needWalk)
        {
            rb.velocity = transform.right * 0;
            animator.SetBool("isShooting", true);

        }


    }

    void Shoot()
    {
        Debug.Log("Shot");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.transform.right.normalized * bulletForce, ForceMode2D.Impulse);
    }

    public void Damaged(float damage, bool damageFromBullet)
    {
        if (damageFromBullet)
        {
            spawnerSC.shotsHit++;
        }



        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.enemyHit);
        soundManager.PlaySound(clip, 0.01f, 1f);

        float freezeMultiplier = cardManager.freezeMultiplier;
        health -= damage;
        KnockBack(Random.Range(5, 13) * 0.1f);

        if (freezeMultiplier > 0 && freezeState < 6 && damageFromBullet)
        {
            StartCoroutine(DissolveFreeze(freezeMultiplier));
        }

        if (health <= 0)
        {
            isAlive = false;

            rb.velocity = transform.right * 0;
            animator.SetBool("isAlive", false);

            if (canRunFunction)
            {
                spawnerSC.enemiesLeft--;
                spawnerSC.amountOfEnemiesKilled++;
                canRunFunction = false;
            }
            
            spawnerSC.CheckIfRoundEnd();

            StartCoroutine(DissolveBody());
        }

        _enemyUIController.UpdateHealthBar(health);

    }

    IEnumerator DissolveBody()
    {

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);

        for (float i = 1; i > -0.2; i -= 0.1f)
        {
            //sp.color = new Color(0,0,0,0) ;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, i);
            outerGlow.intensity -= 0.15f;

            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PumkinZone")
        {
            needWalk = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PumkinZone")
        {
            needWalk = true;
            animator.SetBool("isShooting", false);
        }
    }



    public void KnockBack(float force)
    {
        Debug.Log("RUN");
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
        //sp.color = new Color(sp.color.r - freezeState / 10, sp.color.g - freezeState / 10, sp.color.b);

        yield return new WaitForSeconds(Random.Range(3, 5));

        freezeState--;
        enemyFreezeManager.UpdateState(freezeState);
        walkSpeed = baseWalkSpeed - ((freezeState / 7) * _freezeMultiplier);
        //sp.color = new Color(sp.color.r + freezeState / 10, sp.color.g + freezeState / 10, sp.color.b);
    }



}
