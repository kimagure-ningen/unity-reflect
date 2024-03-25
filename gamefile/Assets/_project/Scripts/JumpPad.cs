using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class JumpPad : MonoBehaviour
{
    private float jumpPower = 20f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(UnityEngine.Vector3.up*jumpPower);
        }
    }
}
