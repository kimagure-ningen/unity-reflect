using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerMaster : MonoBehaviour
{
    [SerializeField] private GameObject debuggerUI;
    [SerializeField] private GameObject player;

    private void Start()
    {
        debuggerUI.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (debuggerUI.active)
            {
                debuggerUI.SetActive(false);
            }
            else
            {
                debuggerUI.SetActive(true);
            }
        }
    }

    public void OnTeleport1ButtonDown()
    {
        player.gameObject.transform.position = new Vector3(0, 0, 0);
    }
    
    public void OnTeleport2ButtonDown()
    {
        player.gameObject.transform.position = new Vector3(25, 0, 0);
    }

    public void OnTeleport3ButtonDown()
    {
        player.gameObject.transform.position = new Vector3(78, 33, 0);
    }
}
