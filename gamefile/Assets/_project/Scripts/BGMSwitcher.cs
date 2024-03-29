using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource bgm1;
    [SerializeField] private AudioSource bgm2;
    [SerializeField] private AudioSource bgm3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bgm1.Stop();
            bgm2.Play();
        }
    }
    
    public void PlayBGM3()
    {
        bgm2.Stop();
        bgm3.Play();
    }
}
