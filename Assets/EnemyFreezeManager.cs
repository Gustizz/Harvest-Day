using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezeManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Material freezeMat;
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = freezeMat;
    }

    public void UpdateState(int freezeLevel)
    {
        switch (freezeLevel)
        {
            case 0:
                _spriteRenderer.material.SetInt("Frozen",0);
                break;
            
            case 1:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",1f);
                break;
            
            case 2:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",1.5f);
                break;
            
            case 3:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",2f);
                break;
            
            case 4:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",2.5f);
                break;
            
            case 5:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",3f);
                break;
            
            case 6:
                _spriteRenderer.material.SetInt("Frozen",1);
                _spriteRenderer.material.SetFloat("BlueNess",3.5f);
                break;
        }
    }
}
