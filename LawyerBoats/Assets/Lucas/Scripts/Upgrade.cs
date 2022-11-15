using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    public Button path1;
    public Button path2;
    void OnMouseDown()
    {
        path1.enabled = !path1.enabled;
        path2.enabled = !path2.enabled;
    }
}
