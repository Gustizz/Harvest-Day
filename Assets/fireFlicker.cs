using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class fireFlicker : MonoBehaviour
{
    [SerializeField]
    private Light2D lightSettings;

    private float targetIntensity;
    private float currentIntensity;
    public float leapSpeed;

    private void Update()
    {
        if (Mathf.Abs(targetIntensity - currentIntensity) < 0.01f)
        {
            StartCoroutine(changeFlickerTarget(Random.Range(24,46) * 0.01f));
        }
        else
        {
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, leapSpeed * Time.deltaTime);
        }

        lightSettings.intensity = currentIntensity;
    }

    IEnumerator changeFlickerTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        targetIntensity = Random.Range(30, 70) * 0.01f;
    }
}
