using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class TypeWriter : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [FormerlySerializedAs("fullText")] [SerializeField] private string fullText1;
    [SerializeField] private string fullText2;
    private TextMeshPro tutorialText;
    private string currentText = "";
    private bool isNearby = false;

    private void Start()
    {
        tutorialText = gameObject.GetComponent<TextMeshPro>();
        StartCoroutine(ShowText1());
    }

    private IEnumerator ShowText1()
    {
        for (int i = 0; i < fullText1.Length; i++)
        {
            if (!isNearby)
            {
                break;
            }
            Debug.Log(currentText);
            currentText = fullText1.Substring(0, i);
            tutorialText.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1f);
        tutorialText.text = "";
        StartCoroutine(ShowText2());
    }

    private IEnumerator ShowText2()
    {
        for (int i = 0; i < fullText2.Length; i++)
        {
            if (!isNearby)
            {
                break;
            }
            Debug.Log(currentText);
            currentText = fullText2.Substring(0, i);
            tutorialText.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1f);
        tutorialText.text = "";
        StartCoroutine(ShowText1());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialText.text = "";
            isNearby = false;
        }
    }
}
