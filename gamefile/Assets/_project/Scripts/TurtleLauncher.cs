using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TurtleLauncher : MonoBehaviour
{
    [SerializeField] private GameObject turtlePrefab;
    [SerializeField] private float launchForce = 700;
    [SerializeField] private float launchRate = 3f;
    private float nextLaunchTime;

    private void Update()
    {
        if (Time.time > nextLaunchTime)
        {
            nextLaunchTime = Time.time + launchRate;
            LaunchTurtle();
        }
    }

    private void LaunchTurtle()
    {
        GameObject turtle = Instantiate(turtlePrefab, gameObject.transform.position, quaternion.identity);
        turtle.GetComponent<Rigidbody>().AddForce(Vector3.up * launchForce);
    }
}
