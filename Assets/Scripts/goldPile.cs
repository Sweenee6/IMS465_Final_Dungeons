using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldPile : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject[] goldPieces;
    [SerializeField] private int goldMin;
    private int goldNum;

    private UIManager UI = null;
    private objectPooler objPooler;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        objPooler = objectPooler.Instance;

        
    }
    public void OnObjectSpawn()
    {
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
            int pIndex = other.GetComponent<PlayerController>().playerNum;
            UI.UpdateScore(goldNum, pIndex);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
