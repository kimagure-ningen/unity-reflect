using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Stage1Complete : MonoBehaviour
{
    [SerializeField] private GameObject chestCover;
    [SerializeField] private CinemachineVirtualCamera completeCam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            completeCam.Priority = 15;
            chestCover.transform.DOLocalRotate(new Vector3(70f, 0f, 0f), 1.5f);
        }
    }
}
