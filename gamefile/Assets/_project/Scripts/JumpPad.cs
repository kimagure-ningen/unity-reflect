using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float jumpPower = 1200f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jump!");
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
        }
    }
}
