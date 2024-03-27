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
    private Player player;
    private bool isGrounded = false;
    private bool jumpCoolTime = true;

    private void Start()
    {
        player = GetComponent<Player>();
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
            player.isStickMoving = true;
        } else if (Input.GetKey(KeyCode.RightArrow) || joystick.Horizontal > 0)
        {
            transform.Translate(-playerSpeed * Time.deltaTime,0f,0f);
            anim.SetBool("IsRunning", true);
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
        

        // 完成したら消す
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
