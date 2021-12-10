using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showVictory : MonoBehaviour
{
    [SerializeField] private Text winText;

    private void Awake()
    {
        winText = GameObject.Find("WinText").GetComponent<Text>();
    }

    //When Boss destroyed, show win text
    private void OnDestroy()
    {
        winText.enabled = true;
    }
}
