using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    internal List<GameObject> PatrolLocation;
    internal GameObject PatrolLocationsObject;
    internal GameObject PatrolTo;
    int PatrolLocationsMax = -1;
    internal int CurrentPatrolLocation = 0;
    bool HasReachedEnd = false;
    Enemy enemy;
    public bool Beeline;
    GameObject Base;

    // Start is called before the first frame update
    void Start()
    {
        PatrolLocation = new List<GameObject>();

        enemy = GetComponent<Enemy>();
        PatrolLocationsObject = GameObject.FindGameObjectWithTag("PatrolLocation");

        foreach (Transform child in PatrolLocationsObject.transform)
        {
            if (child.CompareTag("PatrolLocation"))
            {
                PatrolLocation.Add(child.gameObject);
                PatrolLocationsMax++;
            }

        }

        if (Beeline)
        {
            Base = GameObject.FindGameObjectWithTag("Base");
        }

        if (PatrolLocation != null)
        {
            if(Beeline)
            {
                PatrolTo = Base;
            }
            else
            {
                if (PatrolTo == null)
                {
                    PatrolTo = PatrolLocation[CurrentPatrolLocation];
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
            print("No path nodes");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasReachedEnd)
        {
            if (!Beeline)
            {
                if (new Vector3(PatrolTo.transform.position.x, enemy.transform.position.y, PatrolTo.transform.position.z) == enemy.transform.position)
                {
                    CurrentPatrolLocation++;

                    if (CurrentPatrolLocation <= PatrolLocationsMax)
                    {
                        PatrolTo = PatrolLocation[CurrentPatrolLocation];

                        transform.LookAt(PatrolTo.transform);

                    }
                    else
                    {
                        HasReachedEnd = true;
                    }
                }
            }
        }
    }
}
