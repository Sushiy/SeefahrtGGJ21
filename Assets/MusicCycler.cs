﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCycler : MonoBehaviour
{

    private AudioSource audSource;
    private int clipAmount;
    private int prevClipIndex;
    public AudioClip[] soundClips;
    public float minWaitTime = 2;

    private void Awake() 
    {
        audSource = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        clipAmount = soundClips.Length - 1;

    }

    void Start()
    {
        StartCoroutine(playAudioSnippets());
    }

    IEnumerator playAudioSnippets()
    {
        yield return new WaitForSeconds(2);
        while(true)
        {

            var clipIndex = Random.Range(0, clipAmount);
            // if(prevClipIndex == clipIndex)
            // {clipIndex = soundClips.Length - 1;}

            audSource.clip = soundClips[clipIndex];
            audSource.Play();

            while(audSource.isPlaying)
            {
                yield return null;
            }

            var secondsToWait = Random.Range(minWaitTime,minWaitTime+3);
            yield return new WaitForSeconds(secondsToWait); 
        }


    }
}
