using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject joystickUI;
    private bool isEnd = false;
    public bool isStickMoving = false;

    private void Update()
    {
        if (isEnd)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    public void Damage()
    {
        Debug.Log("Attacked by Enemy!");
        anim.SetTrigger("Die");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3f);
        gameOverUI.SetActive(true);
        joystickUI.SetActive(false);
        isEnd = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathBounds"))
        {
            Damage();
        }
    }
}
