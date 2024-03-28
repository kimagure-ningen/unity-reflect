using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    private bool isTitleScreen = true;
    
    [SerializeField] private CinemachineVirtualCamera LevelSelectCamera;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject titleUI;
    [SerializeField] private GameObject levelSelectUI;

    private void Awake()
    {
        titleUI.SetActive(true);
        levelSelectUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTitleScreen || Input.touchCount > 0 && isTitleScreen)
        {
            if (isTitleScreen)
            {
                titleUI.SetActive(false);
                levelSelectUI.SetActive(true);
                LevelSelectCamera.Priority = 15;
            }
        }
    }

    public void Stage1ButtonPressed()
    {
        SceneManager.LoadScene("Game1");
    }
}
