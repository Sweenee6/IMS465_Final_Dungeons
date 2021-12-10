using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class gameManager : MonoBehaviour
{
    private int pNum = 0;
    public Transform[] playerTransfroms;
    [SerializeField] private Transform[] startPos;
    [SerializeField] private Color[] playerColor;

    private UIManager UI;

    #region Singleton
    public static gameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(GameObject player)
    {
        PlayerController pControl = player.GetComponent<PlayerController>();
        pControl.playerNum = pNum;
        pControl.startPosition = startPos[pNum];
        player.GetComponent<Renderer>().material.color = playerColor[pNum];

        //show player UI elements
        UI.showPlayerUI(pNum);

        playerTransfroms[pNum] = player.transform; //add player to array
        pNum += 1; // next player has higher index
    }
}
