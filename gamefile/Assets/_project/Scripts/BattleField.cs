using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class BattleField : MonoBehaviour
{
    [SerializeField] private GameObject movingWall1;
    [SerializeField] private GameObject movingWall2;
    [SerializeField] private CinemachineVirtualCamera battleFieldCam;
    [SerializeField] private CinemachineVirtualCamera orcCam;
    [SerializeField] private GameObject joystickUI;
    [SerializeField] private Animator orcAnim;
    [SerializeField] private GameObject orc;
    // private CameraShake orcCameShake;

    private void Start()
    {
        // orcCameShake = orcCam.gameObject.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Entered Battle Field!");
            battleFieldCam.Priority = 15;
            other.gameObject.GetComponent<PlayerController>().jumpPower = 10f;
            StartCoroutine(LockDown());
        }
    }

    private IEnumerator LockDown()
    {
        yield return new WaitForSeconds(2f);
        movingWall1.transform.DOScaleY(40f, 2f)
            .OnComplete(() =>
            {
                orcCam.Priority = 20;
                joystickUI.SetActive(false);
            });
        yield return new WaitForSeconds(4f);
        orcAnim.SetTrigger("Taunt");
        // orcCameShake.InduceStress(1f);
        yield return new WaitForSeconds(2f);
        orcAnim.SetTrigger("Attack2");
        yield return new WaitForSeconds(3f);
        orcCam.Priority = 5;
        joystickUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        orc.GetComponent<Orc>().RunForward();
    }
}
