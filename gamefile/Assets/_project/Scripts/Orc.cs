using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Orc : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private BattleField battleField;
    private BoxCollider orcCollider;
    private float enemyHealth = 100f;
    private Rigidbody orcRigidbody;
    private bool isDying = false;
    private bool isDead = false;

    private void Start()
    {
        orcRigidbody = GetComponent<Rigidbody>();
        orcCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (orcRigidbody.velocity.magnitude < 1f)
        {
            anim.SetTrigger("IsStopping");
        } else if (orcRigidbody.velocity.magnitude == 0f)
        {
            anim.SetTrigger("Stop");
        }
        
        if (enemyHealth <= 0)
        {
            if (!isDead)
            {
                Die();
            }
        }

        if (enemyHealth <= 25f)
        {
            if (!isDying)
            {
                Dizzy();
            }
        }
    }

    public void RunForward()
    {
        if (isDead)
        {
            return;
        }
        transform.DOLocalMoveX(155f, 4f)
            .SetEase(Ease.InOutCubic)
            .OnPlay(() =>
                {
                if (isDead)
                {
                    return;
                }
                anim.SetBool("IsMoving", true);
            })
            .OnComplete(() =>
            {
                if (isDead)
                {
                    return;
                }
                Debug.Log("Called");
                anim.SetBool("IsMoving", false);
                StartCoroutine(Attack());
            });
    }
    
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            yield break;
        }
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            yield break;
        }
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            yield break;
        }
        
        Debug.Log("Attack");
        transform.Rotate(0f, 180f, 0f);
        RunForward();
    }

    public void Damage()
    {
        if (enemyHealth >= 0f)
        {
            enemyHealth--;
        }
    }

    private void Dizzy()
    {
        isDying = true;
        transform.DOScale(0.75f, 1f).OnComplete(() => anim.SetTrigger("Dizzy"));
    }

    private void Die()
    {
        battleField.OrcDead();
        isDead = true;
        orcCollider.enabled = false;
        transform.DOKill();
        anim.SetTrigger("Die");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f, ForceMode.Impulse);
            other.gameObject.GetComponent<Player>().Damage();
        }
    }
}
