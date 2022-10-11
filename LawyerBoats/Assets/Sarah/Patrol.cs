using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    List<GameObject> PatrolLocation;
    internal GameObject PatrolTo;
    int PatrolLocationsMax = -1;
    int CurrentPatrolLocation = 0;
    bool HasReachedEnd = false;
    EnemyMovement enemy;

    // Start is called before the first frame update
    void Start()
    {
        PatrolLocation = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("PatrolLocation"))
            {
                PatrolLocation.Add(child.gameObject);
                PatrolLocationsMax++;
                //print("Add Child");
            }
            else if(child.CompareTag("Enemy"))
            {
                enemy = child.gameObject.GetComponent<EnemyMovement>();
                enemy.PatrolComp = this;
            }

        }

        if (PatrolLocation != null)
        {
            PatrolTo = PatrolLocation[CurrentPatrolLocation];
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
            if (PatrolTo.transform.position == enemy.transform.position)
            {
                CurrentPatrolLocation++;

                if (CurrentPatrolLocation <= PatrolLocationsMax)
                {
                    PatrolTo = PatrolLocation[CurrentPatrolLocation];
                }
                else
                {
                    HasReachedEnd = true;
                }
            }
        }
    }
}
