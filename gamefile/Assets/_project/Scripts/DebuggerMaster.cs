using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerMaster : MonoBehaviour
{
    [SerializeField] private GameObject debuggerUI;
    [SerializeField] private GameObject player;
    private bool isConsoleOpen = false;

    private void Start()
    {
        debuggerUI.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (debuggerUI.activeSelf)
            {
                debuggerUI.SetActive(false);
            }
            else
            {
                debuggerUI.SetActive(true);
            }
        }

        if (isConsoleOpen)
        {
            debuggerUI.SetActive(true);
        }
        else
        {
            debuggerUI.SetActive(false);
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

    public void OnTeleport4ButtonDown()
    {
        player.gameObject.transform.position = new Vector3(107f, 32f, 0);
    }
    
    public void OnTeleport5ButtonDown()
    {
        Debug.Log("Teleporting to (138, 42)");
        player.gameObject.transform.position = new Vector3(138f, 43f, 0);
    }

    public void OnDebugButtonDown()
    {
        isConsoleOpen = !isConsoleOpen;
    }
}
