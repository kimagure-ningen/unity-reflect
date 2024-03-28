using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera waterCam;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waterCam.Priority = 15;
        }
    }
}
