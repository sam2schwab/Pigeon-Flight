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
    [SerializeField] int plumesToCollect;
    [SerializeField] int maxHp = 5;

    float currentTimer;
    Text TimerText;
    int collectedPlumes = 0;
    int currentHp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        plumesToCollect = Mathf.Min(plumeSpawner.GetComponent<PlumesSpawner>().plumesToSpawn, plumesToCollect);
        CatWinsGo.SetActive(false);
        PigeonsWinGo.SetActive(false);
        currentTimer = gameTimer;
        TimerText = TimerGo.GetComponent<Text>();
        UpdatePlumeText(0);
        UpdateHpText(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedPlumes < plumesToCollect)
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
    }
    public void UpdatePlumeText(int delta)
    {
        collectedPlumes += delta;
        PlumeCountGo.GetComponent<Text>().text = collectedPlumes.ToString() + "/" + plumesToCollect.ToString();
        if (currentTimer > 0 && collectedPlumes >= plumesToCollect && currentHp > 0)
        {
            PigeonsWinGo.SetActive(true);
        }
    }
    public void UpdateHpText(int delta)
    {
        currentHp -= delta;
        HpCountGo.GetComponent<Text>().text = currentHp.ToString() + "/" + maxHp.ToString();
        if (currentHp <= 0 && collectedPlumes < plumesToCollect)
        {
            CatWinsGo.SetActive(true);
        }
    }
}
