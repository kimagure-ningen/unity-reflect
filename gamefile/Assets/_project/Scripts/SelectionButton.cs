using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectionButton : MonoBehaviour
{
    [SerializeField] MenuMaster menuMaster;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnMouseDown()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f, gameObject.transform.position.z);
        audioSource.Play();
    }

    private void OnMouseUp()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.3f, gameObject.transform.position.z);
    }

    private void OnMouseEnter()
    {
        gameObject.transform.DOScale(new Vector3(2.8f, 0.15f, 2.8f), 0.3f);
        audioSource.Play();
    }

    private void OnMouseExit()
    {
        gameObject.transform.DOScale(new Vector3(2.5f, 0.15f, 2.5f), 0.3f);
    }
}
