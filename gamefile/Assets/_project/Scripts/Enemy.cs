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
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    public void Damage()
    {
        Debug.Log("Damaged");
        Destroy(gameObject);
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
            StartCoroutine(Attack(other.gameObject));
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        isChasing = false;
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().Damage();
        anim.SetBool("IsAttacking", false);
    }
}
