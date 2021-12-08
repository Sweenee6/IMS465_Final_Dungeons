using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldPile : MonoBehaviour
{
    [SerializeField] private GameObject[] goldPieces;
    [SerializeField] private int goldMin;
    private int goldNum;

    private UIManager UI = null;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        goldNum = Random.Range(goldMin, goldPieces.Length);

        // show only the amount of gold as in the gold number
        for (int i = 0; i < goldNum; i++)
        {
            goldPieces[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UI.UpdateScore(goldNum);
            Destroy(this.gameObject);
        }
    }
}
