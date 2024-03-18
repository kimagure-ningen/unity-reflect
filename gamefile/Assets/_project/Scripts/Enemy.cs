using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private float enemySpeed = 1.25f;
    private bool isChasing = false;

    private void Update()
    {
        if (isChasing)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed);
            anim.SetBool("IsMoving", true);
        }
    }

    public void Damage()
    {
        Debug.Log("Damaged");
        transform.DOScale(new Vector3(transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z * 0.5f), 0.5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isChasing = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "SideWall")
        {
            transform.Rotate(0f, 180f, 0f);
        }
        
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Damage();
        }
    }
}
