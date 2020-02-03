using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : SingletonMonoBehaviour<UIManagement>
{
    public GameObject CatWinsGo;
    public GameObject PigeonsWinGo;
    public GameObject PlumeCountGo;
    public GameObject HpCountGo;
    public GameObject TimerGo;

    private Text _timerText;
    private Text _plumesText;
    private Text _hpText;

    private void Start()
    {
        CatWinsGo.SetActive(false);
        PigeonsWinGo.SetActive(false);
        _timerText = TimerGo.GetComponent<Text>();
        _plumesText = PlumeCountGo.GetComponent<Text>();
        _hpText = HpCountGo.GetComponent<Text>();
    }

    public void DisplayCatVictory()
    {
        CatWinsGo.SetActive(true);
    }
    
    public void DisplayPigeonsVictory()
    {
        PigeonsWinGo.SetActive(true);
    }

    public void UpdateTimerText(float timerValue)
    {
        _timerText.text = (timerValue.ToString("0000") + "s");
    }
    
    public void UpdatePlumeText(int plumes, int plumesMax)
    {
        _plumesText.text = plumes.ToString() + "/" + plumesMax.ToString();
    }
    
    public void UpdateHpText(int hp, int maxHp)
    {
        _hpText.text = hp.ToString() + "/" + maxHp.ToString();
    }
}
