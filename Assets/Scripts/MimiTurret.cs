using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimiTurret : MonoBehaviour
{

    GameObject[] array;
    public int radius;

    public bool ready = false;
    public LayerMask enemyLayer;

    private float lowestDist;
    private GameObject closetObject;

    public Transform firePoint;
    public GameObject miniTurretBullet;

    public int bulletForce;
    private float nextFireTime;

    public float shootCooldownTime;

    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        lowestDist = radius;

        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();


        var euler = transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = euler;
    }

    // Update is called once per frame
    void Update()
    {

        GetEnemiesInRadius();

        if(closetObject != null)
        {
            Vector2 direction = closetObject.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 40 * Time.deltaTime);


            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + shootCooldownTime;
                Shoot(rotation);
            }

                

            
        }


    }

    void GetEnemiesInRadius()
    {
        /*
        foreach(GameObject go in array)
        {
            float distanceSqr = (transform.position - go.transform.position).sqrMagnitude;
            if(distanceSqr < radiusCollider)
            {
                Debug.Log(go.name);
            }
        }
        */

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        lowestDist = radius;

        foreach(Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<BoxCollider2D>().enabled)
            {
                float dist = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (dist < lowestDist)
                {
                    lowestDist = dist;
                    closetObject = hitCollider.gameObject;
                    Debug.Log(hitCollider.name);
                }
            }


        }

        //ready = false;
    }



    void Shoot(Quaternion rotation)
    {
        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.playerShoot);
        soundManager.PlaySound(clip, 0.0015f, 1f);

        GameObject bullet = Instantiate(miniTurretBullet, firePoint.transform.position, rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.transform.right.normalized * bulletForce, ForceMode2D.Impulse);

    }
}
