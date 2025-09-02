using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public UpgradeScript upgradeSC;
    public GameObject particleHit;

    public float exipiryTime = 4;
    void Start()
    {
        
        damage = GameObject.FindGameObjectWithTag("TurretTop").GetComponent<GunController>().damage;
        exipiryTime = GameObject.FindGameObjectWithTag("Card Manager").GetComponent<CardManager>().bulletExpiryTime;

        StartCoroutine(BulletExpiry(exipiryTime));


        //upgradeSC = GameObject.FindGameObjectWithTag("ShopController").GetComponent<UpgradeScript>();
        //damage = upgradeSC.damageModifier;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Hit!");
       if(collision.gameObject.tag == "Bullet")
        {
            return;
        }

        else if(collision.gameObject.tag == "Carrot")
        {
            collision.gameObject.GetComponent<AIWalkTowardsMiddle>().Damaged(damage, true);
        }
        else if (collision.gameObject.tag == "Pumkin")
        {
            collision.gameObject.GetComponent<PumkinScript>().Damaged(damage, true);
        }
       else if (collision.gameObject.tag == "Tomatoe")
        {
            collision.gameObject.GetComponent<TomatoScript>().Damaged(damage, true);
        }



        Instantiate(particleHit, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }


    IEnumerator BulletExpiry(float _expiryTime)
    {
        
        Debug.Log(_expiryTime);
        yield return new WaitForSeconds(_expiryTime);
        Instantiate(particleHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
