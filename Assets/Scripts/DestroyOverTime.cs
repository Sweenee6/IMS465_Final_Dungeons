using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour, IPooledObject
{
    [SerializeField] private float timeToDestroy;
    private float timer;
    private objectPooler objPooler;

    void Start()
    {
        objPooler = objectPooler.Instance;
        timer = timeToDestroy;
    }

    public void OnObjectSpawn()
    {
        timer = timeToDestroy;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            //Destroy(gameObject);
            //requeue and hide

             gameObject.SetActive(false);
            //objPooler.poolDictionary["spell"].Enqueue(gameObject);
        }
    }
    
}
