using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Hunted))]

public class HeartbeatOpenScript : MonoBehaviour
{
    //Collection
    public AudioClip systole;
    public AudioClip diastole;

    public Hunted huntedScript;

    //Frequency
    private float minBPM;
    private float maxBPM;
    private float BPM;
    public float minSpacingTime = 0.081f;
    public float maxSpacingTime = 0.162f;
    private float spacingTime;

    public float time;
    public float beatEveryXsec;

    [Range(0, 1)] public float minVolume;
    [Range(0, 1)] public float maxVolume;
    private float volume;
    private AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        huntedScript = GetComponent<Hunted>();
        minBPM = huntedScript.minBpm;
        maxBPM = huntedScript.maxBpm;

        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BPM = huntedScript.GetBPM();

        //Calculate spacingTime
        beatEveryXsec = 60 / BPM;
        spacingTime = maxSpacingTime - ((BPM - minBPM) / (maxBPM - minBPM)) * (maxSpacingTime - minSpacingTime);

        //Volume
        volume = ((BPM - minBPM) / (maxBPM - minBPM)) * (maxVolume - minVolume) + minVolume;
        speaker.volume = volume;

        //Beating
        time += Time.deltaTime;
        if (time >= beatEveryXsec)
        {
            StartCoroutine(Beat());
            time -= beatEveryXsec;
        }
    }

    private IEnumerator Beat()
    {
        speaker.PlayOneShot(systole);
        yield return new WaitForSeconds(spacingTime);
        speaker.PlayOneShot(diastole);
    }

}
