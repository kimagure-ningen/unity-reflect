using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class FireBallLauncher : MonoBehaviour
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private float launchRate = 5f;
    private float nextLaunchTime;

    private bool isNearby = false;
    
    private void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            nextLaunchTime = Time.time + launchRate;
            LaunchFireBall();
        }
    }
    
    private void LaunchFireBall()
    {
        if (isNearby)
        {
            Instantiate(fireBall, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearby = false;
        }
    }
}
