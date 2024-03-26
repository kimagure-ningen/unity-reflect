using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject joystickUI;
    public void Damage()
    {
        Debug.Log("Attacked by Enemy!");
        anim.SetTrigger("Die");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);
        gameOverUI.SetActive(true);
        joystickUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBounds"))
        {
            Damage();
        }
    }
}
