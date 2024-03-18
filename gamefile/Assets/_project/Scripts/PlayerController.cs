using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Utility;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 3f;
    private float jumpPower = 7.5f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Light light;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject visual;
    [SerializeField] private FixedJoystick joystick;
    
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.2f);
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
        
        
        if (Input.GetKeyDown(KeyCode.Space) || joystick.Vertical > 0.85f)
        {
            print(isGrounded());
            if (isGrounded())
            {
                anim.SetBool("IsJumping", true);
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }

        // 完成したら消す
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
