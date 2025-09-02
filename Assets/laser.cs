using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).

    Rigidbody2D rb2D;

    public LineRenderer _lineRenderer;
    public LayerMask hitMask;
    public GameObject Light;
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 40, hitMask);
        //_lineRenderer.SetPosition(1, hit.transform.position);
        
        
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, (transform.right * 40f) + transform.position);
        Light.transform.position = (transform.right * 40f) + transform.position;
            
        // If it hits something...
        if (hit.collider != null)
        {
           _lineRenderer.SetPosition(1, hit.transform.position);
           Light.transform.position = hit.transform.position;
        }
    }
}
