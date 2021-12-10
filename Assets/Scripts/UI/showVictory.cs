using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showVictory : MonoBehaviour
{
    private UIManager UI = null;

    private void start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    //When Boss destroyed, show win text
    private void OnDisable()
    {
        if (UI != null)
        {
            UI.showWinText();
        }
    }
}
