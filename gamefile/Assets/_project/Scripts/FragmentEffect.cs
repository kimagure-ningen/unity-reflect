using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentEffect : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    
    public void EmmitFragment(Vector3 spawnPos)
    {
        GameObject _particle = Instantiate(particle, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnParticle(_particle));
    }

    private IEnumerator SpawnParticle(GameObject item)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(item);
    }
}
