using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utility;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 3f;
    public float jumpPower = 7.5f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Light light;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject visual;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private ParticleSystem dust;
    private Player player;
    private bool isGrounded = false;
    private bool jumpCoolTime = true;
    public bool isGameEnd = false;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsJumping", false);
        
        if (isGameEnd)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) || joystick.Horizontal < 0)
        {
            transform.Translate(playerSpeed * Time.deltaTime,0f,0f);
            anim.SetBool("IsRunning", true);
            if (isGrounded)
            {
                dust.Play();
                
            }
            visual.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.isStickMoving = true;
        } else if (Input.GetKey(KeyCode.RightArrow) || joystick.Horizontal > 0)
        {
            transform.Translate(-playerSpeed * Time.deltaTime,0f,0f);
            anim.SetBool("IsRunning", true);
            if (isGrounded)
            {
                dust.Play();
            }
            visual.transform.rotation = Quaternion.Euler(0, -90, 0);
            player.isStickMoving = true;
        }

        if (joystick.Horizontal == 0f && joystick.Vertical == 0f)
        {
            player.isStickMoving = false;
        }

        if (joystick.Vertical != 0f)
        {
            player.isStickMoving = true;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space) || joystick.Vertical > 0.85f)
        {
            if (isGrounded)
            {
                if (jumpCoolTime)
                {
                    StartCoroutine(Jump());
                }
            }
        }

        IEnumerator Jump()
        {
            anim.SetBool("IsJumping", true);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
            jumpCoolTime = false;
            yield return new WaitForSeconds(0.2f);
            jumpCoolTime = true;
            isGrounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded");
            isGrounded = true;
        }
    }
}
