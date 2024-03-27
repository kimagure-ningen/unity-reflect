using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orc : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float enemyHealth = 100f;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude < 1f)
        {
            anim.SetTrigger("IsStopping");
        } else if (rigidbody.velocity.magnitude == 0f)
        {
            anim.SetTrigger("Stop");
        }
    }

    public void RunForward()
    {
        transform.DOLocalMoveX(155f, 4f)
            .SetEase(Ease.InOutCubic)
            .OnPlay(() =>
                {
                anim.SetBool("IsMoving", true);
            })
            .OnComplete(() =>
            {
                Debug.Log("Called");
                anim.SetBool("IsMoving", false);
                StartCoroutine(Attack());
            });
    }
    
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("Attack1");
    }

    public void Damage()
    {
        
    }
}
