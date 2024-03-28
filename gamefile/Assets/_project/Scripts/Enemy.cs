using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float enemySpeed = 1.25f;
    private bool isChasing = false;
    [SerializeField] private bool isStatic = false;

    private void Update()
    {
        if (isStatic)
        {
            return;
        }
        
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

    public void OnPlayerFound()
    {
        isChasing = true;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBounds"))
        {
            Destroy(gameObject);
            Debug.Log("Destroyed Enemy!");
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        if (isStatic)
        {
            yield return new WaitForSeconds(0.5f);
            player.GetComponent<Player>().Damage();
            yield break;
        }
        isChasing = false;
        anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Player>().Damage();
        anim.SetBool("IsAttacking", false);
    }
}
