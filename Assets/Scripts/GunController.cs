using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float speed = 5f;
    public int bulletForce;
    public GameObject crosshair;
    public float damage = 1;

    public GameObject bulletPrefab;
    public GameObject[] firePoints;

    public Animator animator;

    public float shootCooldownTime;
    private float nextFireTime;

    public float spreadModifier = 0;
    public int numofProjectiles = 1;

    public ParticleSystem bulletsFallingEffect;
    public ParticleSystem shootingEffect;

    public SoundManager soundManager;

    public bool canShoot;

    public float amountShotsFired;

    private void Start()
    {
        Cursor.visible = false;

        soundManager = SoundManager.Instance;

    }
    void Update()
    {



        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        crosshair.transform.position = direction;


        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButton(0))
            {


                animator.SetBool("Shooting", true);

                nextFireTime = Time.time + shootCooldownTime;

                if (canShoot)
                {
                    for (int i = 0; i < numofProjectiles; i++)
                    {
                        Shoot(transform.rotation);
                    }
                }


                
            }
            else
            {
                animator.SetBool("Shooting", false);
            }

            
        }
    }

    void Shoot(Quaternion rotation)
    {
        amountShotsFired++;

        AudioClip clip = soundManager.RandomSoundFromClipList(soundManager.playerShoot);
        soundManager.PlaySound(clip, 0.0035f, 1f);
       

        Instantiate(bulletsFallingEffect, transform.position, Quaternion.identity);
        Instantiate(shootingEffect, firePoints[5].transform.position, Quaternion.identity);

        GameObject firePoint = firePoints[Random.Range(0, firePoints.Length)];

        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, rotation);

        if(bulletForce < 45)
        {
            bulletForce = 45;
        }

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.transform.right.normalized * bulletForce, ForceMode2D.Impulse);

    }

    public void ModifySpread(float _spreadModifier)
    {
        // FirePoints: 0 = left most one, 1 = between middle and left edge, 2 = middle, 3 = middle, 4 = between edge and middle on the right, 5 = righnt most one


        firePoints[0].transform.rotation = Quaternion.Euler(firePoints[0].transform.eulerAngles.x, firePoints[0].transform.eulerAngles.y, firePoints[0].transform.eulerAngles.z - _spreadModifier);
        firePoints[1].transform.rotation = Quaternion.Euler(firePoints[1].transform.eulerAngles.x, firePoints[1].transform.eulerAngles.y, firePoints[1].transform.eulerAngles.z - _spreadModifier);

        firePoints[2].transform.rotation = Quaternion.Euler(firePoints[2].transform.eulerAngles.x, firePoints[2].transform.eulerAngles.y, firePoints[2].transform.eulerAngles.z);
        firePoints[3].transform.rotation = Quaternion.Euler(firePoints[3].transform.eulerAngles.x, firePoints[3].transform.eulerAngles.y, firePoints[3].transform.eulerAngles.z);

        firePoints[4].transform.rotation = Quaternion.Euler(firePoints[4].transform.eulerAngles.x, firePoints[4].transform.eulerAngles.y, firePoints[4].transform.eulerAngles.z + _spreadModifier);
        firePoints[5].transform.rotation = Quaternion.Euler(firePoints[5].transform.eulerAngles.x, firePoints[5].transform.eulerAngles.y, firePoints[5].transform.eulerAngles.z + _spreadModifier);

    }

}
