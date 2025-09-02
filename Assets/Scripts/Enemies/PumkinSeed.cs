using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinSeed : MonoBehaviour
{
    public int damage = 1;
    HealthScript hpScript;
    public GameObject particleHit;

    // Start is called before the first frame update
    void Start()
    {
        hpScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Fence")
        {


            Instantiate(particleHit, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            //Instantiate(collision.gameObject.GetComponent<Bullet>().particleHit, transform.position, Quaternion.identity);
        }

    }

    IEnumerator BulletExpiry()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
