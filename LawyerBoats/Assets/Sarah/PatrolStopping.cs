using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStopping : MonoBehaviour
{
    float StoppingTimer;
    public float StopFor;
    public int AmountOfStops;
    Patrol PatrolComp;
    int PatrolNodes;
    List<GameObject> StoppingNodes;
    int StopEvery = 0;
    int CurrentStop = 0;

    // Start is called before the first frame update
    void Start()
    {
        StoppingNodes = new List<GameObject>();
        PatrolComp = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PatrolNodes == 0)
        {
            PatrolNodes = PatrolComp.PatrolLocation.Count;

             StopEvery = Mathf.CeilToInt(PatrolNodes / (AmountOfStops + 1));

            for(int i = 0; i < PatrolComp.PatrolLocation.Count; i += StopEvery)
            {
                StoppingNodes.Add(PatrolComp.PatrolLocation[i]);
            }
        }

        if(PatrolComp.PatrolTo == StoppingNodes[CurrentStop])
        {
            GetComponent<Enemy>().shouldMove = false;
            StoppingTimer = Time.deltaTime;

            if(StoppingTimer >= StopFor)
            {
                StoppingTimer = 0;
                CurrentStop++;
                GetComponent<Enemy>().shouldMove = true;
            }
        }


    }
}
