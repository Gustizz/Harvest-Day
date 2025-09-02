using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public int nextSceneIndex;
    public Animator animator;

    public float transitionTime;

    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(TransitionEffectEnter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void TryAgain()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        
        StartCoroutine(LoadLevel());
        TransitionSoundEffects();
    }

    public IEnumerator LoadLevel()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(nextSceneIndex);

    }

    public void TransitionSoundEffects()
    {
        StartCoroutine(TransitionEffectExit());
    }

    IEnumerator TransitionEffectExit()
    {
        soundManager.PlaySound(soundManager.TransitionSwoosh, 0.05f, 0.5f);
        yield return new WaitForSeconds(1f);
        soundManager.PlaySound(soundManager.shopClose[0], 0.075f, 1f);
    }

    IEnumerator TransitionEffectEnter()
    {
        yield return new WaitForSeconds(0.66f);
        soundManager.PlaySound(soundManager.shopClose[0], 0.075f, 1f);
        yield return new WaitForSeconds(0.16f);
        soundManager.PlaySound(soundManager.TransitionSwoosh, 0.05f, 0.5f);
        
        
    }
}
