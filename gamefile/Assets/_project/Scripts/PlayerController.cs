using System;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utility;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 2f;
    private float jumpPower = 7.5f;
    private float lineDrawSpeed = 60f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Light light;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject visual;
    [SerializeField] private FixedJoystick joystick;
    
    private float lineLength = 0f;

    RaycastHit hit;
    
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1);
    }
    private void Update()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsJumping", false);
        
        if (Input.GetKey(KeyCode.LeftArrow) || joystick.Horizontal < 0)
        {
            transform.Translate(playerSpeed * Time.deltaTime,0f,0f);
            anim.SetBool("IsRunning", true);
            visual.transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (Input.GetKey(KeyCode.RightArrow) || joystick.Horizontal > 0)
        {
            transform.Translate(-playerSpeed * Time.deltaTime,0f,0f);
            anim.SetBool("IsRunning", true);
            visual.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(isGrounded());
            if (isGrounded())
            {
                anim.SetBool("IsJumping", true);
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    IEnumerator LightExplosion()
    {
        for (int i = 0; i < 150; i++) //150
        {
            light.intensity = i / 10f;
            yield return new WaitForSeconds(0.003f);
        }

        for (int i = 150; i > 1; i--)
        {
            light.intensity = i / 10f;
            yield return new WaitForSeconds(0.0005f);
        }

        light.intensity = 1f;
        lineLength = 0f;
    }
}
