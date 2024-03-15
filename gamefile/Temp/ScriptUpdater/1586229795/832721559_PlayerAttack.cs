using System;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody rb;
    
    private Vector3 touchPos;
    private Vector3 releasePos;

    private float attackDirX;
    private float attackDirY;

    private bool isAttacking;
    
    [SerializeField] private LineRenderer lineRenderer;
    private float lineLength = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAttacking = false;
    }

    private void Update()
    {
        // 光線の描画
        var poses = PhysicsUtil.RefrectionLinePoses(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(attackDirX, attackDirY, 0),  lineLength, 1).ToArray();
        lineRenderer.positionCount = poses.Length;
        lineRenderer.SetPositions(poses);
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) == true)
        {
            touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Debug.Log("touchPos: " + touchPos);
        }

        if (Input.GetMouseButtonUp(0) == true)
        {
            releasePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Debug.Log("releasePos: " + releasePos);
            if (!isAttacking)
            {
                AttackDirection();
            }
            isAttacking = true;
            
        }
    }

    private void AttackDirection()
    {
        attackDirX = releasePos.x - touchPos.x;
        attackDirY = releasePos.y - touchPos.y;

        LightExplosion();
    }
    
    private async UniTask LightExplosion()
    {
        Debug.Log("LightExplosion");
        while (lineLength < 30f)
        {
            lineLength += 1f;
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
        }

        float explosionTime = 0f;
        while (explosionTime < 15f)
        {
            GetComponent<Light>().intensity += 0.1f;
        }
        lineLength = 0f;
        isAttacking = false;
    }
}
