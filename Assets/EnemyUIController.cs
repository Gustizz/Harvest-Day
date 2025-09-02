using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIController : MonoBehaviour
{
    public RectTransform healthScale;
    public float slotSize;
    
    public void SetUpHealthBar(float maxHealth)
    {
        slotSize = 1 / (float) maxHealth;
        healthScale.localScale = new Vector3(0.98f,1,1);
    }

    public void UpdateHealthBar(float health)
    {
        if(health < 0)
        {
            health = 0;
        }

        healthScale.localScale = new Vector3(slotSize * health, 1, 1);
    }
}
