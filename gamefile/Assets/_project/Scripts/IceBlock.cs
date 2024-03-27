using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceBlock : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Slipped on Ice!");
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * 2000f);
        }
    }
    
    public void Melt()
    {
        Debug.Log("Ice Melted!");
        transform.DOScale(new Vector3(0f, 0f, 0f), 1.5f)
            .OnComplete(() => Destroy(gameObject));
    }
}
