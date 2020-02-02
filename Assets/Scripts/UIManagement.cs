using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public GameObject CatWinsGo;
    public GameObject PigeonsWinGo;
    public GameObject PlumeCountGo;
    public GameObject HpCountGo;
    public GameObject TimerGo;
    public GameObject plumeSpawner;
    [SerializeField] float gameTimer = 1000.0f;

    float currentTimer;
    Text TimerText;
    int plumesToCollect;
    int collectedPlumes = 0;
    // Start is called before the first frame update
    void Start()
    {
        plumesToCollect = plumeSpawner.GetComponent<PlumesSpawner>().plumesToSpawn;
        CatWinsGo.SetActive(false);
        PigeonsWinGo.SetActive(false);
        currentTimer = gameTimer;
        TimerText = TimerGo.GetComponent<Text>();
        UpdatePlumeText(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            TimerText.text = (currentTimer.ToString("0000") + "s");
        }
        else
        {
            CatWinsGo.SetActive(true);
        }
    }
    public void UpdatePlumeText(int delta)
    {
        collectedPlumes += delta;
        PlumeCountGo.GetComponent<Text>().text = collectedPlumes.ToString() + "/" + plumesToCollect.ToString();
    }
}
