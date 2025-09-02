using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectScript : MonoBehaviour
{

    public float expiryTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWhenDone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyWhenDone()
    {
        yield return new WaitForSeconds(expiryTime);
        Destroy(gameObject);
    }
}
