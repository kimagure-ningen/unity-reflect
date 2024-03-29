using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TransitionsPlus;

public class MenuMaster : MonoBehaviour
{
    private bool isTitleScreen = true;
    
    [SerializeField] private CinemachineVirtualCamera LevelSelectCamera;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject levelSelectUI;
    [SerializeField] private GameObject disclaimerUI;
    [SerializeField] private GameObject stage1Transition;
    [SerializeField] private GameObject stage2Transition;

    private void Awake()
    {
        titleUI.SetActive(true);
        levelSelectUI.SetActive(false);
        disclaimerUI.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.75f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTitleScreen || Input.touchCount > 0 && isTitleScreen)
        {
            if (isTitleScreen)
            {
                titleUI.SetActive(false);
                levelSelectUI.SetActive(true);
                disclaimerUI.SetActive(false);
                LevelSelectCamera.Priority = 15;
            }
        }
    }

    public void Stage1ButtonPressed()
    {
        Debug.Log("Stage 1 Button Pressed!");
        stage1Transition.SetActive(true);
    }
    
    public void Stage2ButtonPressed()
    {
        Debug.Log("Stage 2 Button Pressed!");
        stage2Transition.SetActive(true);
    }
}
