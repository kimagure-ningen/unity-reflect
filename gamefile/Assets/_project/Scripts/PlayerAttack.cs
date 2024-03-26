using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        originalCameraTransform = cameraTransform.position;
        
        isAttacking = false;
    }

    private void Update()
    {
        // 光線の描画
        var poses = PhysicsUtil.RefrectionLinePoses(new Vector3(transform.position.x + 0.25f, transform.position.y + 1.0f, transform.position.z), new Vector3(-attackDirX, attackDirY, 0).normalized,  lineLength, 1).ToArray();
        lineRenderer.positionCount = poses.Length;
        lineRenderer.SetPositions(poses);
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            // Debug.Log("touchPos: " + touchPos);
        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            releasePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            // Debug.Log("releasePos: " + releasePos);
            if (!isAttacking)
            {
                Attack();
                anim.SetTrigger("IsAttacking");
            }
            isAttacking = true;
            
        }
    }

    private void Attack()
    {
        attackDirX = releasePos.x - touchPos.x;
        attackDirY = releasePos.y - touchPos.y;

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
    
    
}
