using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTest : MonoBehaviour
{
    public Enemy ToDestory;
    public float DestoryIn = 10;
    public int RepeatTime = 0;
    internal int repeated = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (repeated < RepeatTime)
        {
            //print("Timer");
            timer += Time.deltaTime;

            if (DestoryIn <= timer)
            {
                ToDestory.isAlive = false;
            }
        }
    }
}
