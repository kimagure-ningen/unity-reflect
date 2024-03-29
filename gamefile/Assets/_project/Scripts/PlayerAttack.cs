using UnityEngine;
using UnityEngine.EventSystems;
using Utility;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField]
    private Transform cameraTransform;
    private Vector3 originalCameraTransform;
    
    private Vector3 touchPos;
    private Vector3 releasePos;

    private float attackDirX;
    private float attackDirY;

    private bool isAttacking;

    [SerializeField]
    private Light light;
    
    [SerializeField] private LineRenderer lineRenderer;
    private float lineLength = 0f;

    [SerializeField] private Animator anim;

    [SerializeField] private GameObject particle;

    private Player player;

    private bool isStickPressed = false;

    private Vector3 attackingPos;
    private bool isTouching = false;
    
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalCameraTransform = cameraTransform.position;
        
        isAttacking = false;

        player = GetComponent<Player>();
    }

    private void Update()
    {
        // 光線の描画
        var poses = PhysicsUtil.RefrectionLinePoses(new Vector3(transform.position.x + 0.25f, transform.position.y + 1f, transform.position.z), new Vector3(-attackDirX, attackDirY, 0).normalized,  lineLength, 1).ToArray();
        lineRenderer.positionCount = poses.Length;
        lineRenderer.SetPositions(poses);
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        
        // PC DEBUG用
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //     // Debug.Log("touchPos: " + touchPos);
        // }
        //
        // if (Input.GetMouseButtonUp(0))
        // {
        //     releasePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //     // Debug.Log("releasePos: " + releasePos);
        //     if (!isAttacking)
        //     {
        //         Attack();
        //         anim.SetTrigger("IsAttacking");
        //     }
        //     isAttacking = true;
        //     
        // }
        
        if (player.isStickMoving)
        {
            if (Input.touchCount == 2)
            {
                Touch touch = Input.GetTouch(1);
                GetSwipe(touch);
            } 
        }
        else
        {
            if (Input.touchCount == 1)
            {
                if (isStickPressed)
                {
                    return;
                }
                
                Touch touch = Input.GetTouch(0);
                GetSwipe(touch);
            }
        }
    }

    private void GetSwipe(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            touchPos = new Vector3(touch.position.x, touch.position.y, 0);
            isTouching = true;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            if (!isTouching)
            {
                return;
            }
            releasePos = new Vector3(touch.position.x, touch.position.y, 0);
            
            if (!isAttacking)
            {
                Attack();
                isAttacking = true;
                isTouching = false;
                anim.SetTrigger("IsAttacking");
            }
        }
    }
    
    private void Attack()
    {
        attackingPos = new Vector3(transform.position.x + 0.25f, transform.position.y + 1.0f, transform.position.z);
        attackDirX = releasePos.x - touchPos.x;
        attackDirY = releasePos.y - touchPos.y;
        
        audioSource.Play();

        DOTween.To(() => lineLength, x => lineLength = x, 30f, 0.7f).OnComplete(() =>
        {
            DOTween.To(() => light.intensity, x => light.intensity = x, 15, 0.3f).OnComplete(() =>
            {
                lineLength = 0f;
                cameraTransform.localPosition = originalCameraTransform + Random.insideUnitSphere * 0.02f;
                DOTween.To(() => light.intensity, x => light.intensity = x, 2, 0.1f).OnComplete(() =>
                {
                    isAttacking = false;
                });
            });
        });
    }

    public void StickPressed()
    {
        isStickPressed = true;
    }

    public void StickReleased()
    {
        isStickPressed = false;
    }
}
