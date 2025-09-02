using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public GameObject leanTweenGO;

    public WaveManager waveManager;

    public LeanTweenType waveTextEaseType;
    public GameObject waveText;
    private int waveCount;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        button.interactable = false;
        waveCount++;

       // waveManager.startButtonExit();
        waveManager.ExitUpgradeCards();

        waveText.GetComponent<Text>().text = "Wave: " + waveCount.ToString();
        waveText.GetComponent<Text>().color = new Color(0.1f,0.1f,0.1f,1);
        LeanTween.scale(waveText, new Vector3(1f, 1f, 1f), 0.5f).setDelay(0.5f).setOnComplete(minimiseText);
        


    }

    void SpawnEnemies()
    {
        waveManager.spawnerSc.ready = true;
        waveManager.spawnerSc.StartEnemies();
    }

    void minimiseText()
    {
        
        LeanTween.scale(waveText, new Vector3(0, 0, 0), 1.2f).setDelay(0.25f).setOnComplete(SpawnEnemies);
        LeanTween.alphaText(waveText.GetComponent<RectTransform>(), 0, 1.5f);

    }


}
