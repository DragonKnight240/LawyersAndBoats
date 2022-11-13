using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePaths : MonoBehaviour
{
    public Button P1Upgrade1;
    public Button P1Upgrade2;

    public Button P2Upgrade1;
    public Button P2Upgrade2;

    public Button path1;
    public Button path2;
    void UpgradePath1()
    {
        P1Upgrade1.enabled = !P1Upgrade1.enabled;
        path2.enabled = false;
    }

    void UpgradePath2()
    {
        P2Upgrade1.enabled = !P2Upgrade1.enabled;
        path1.enabled = false;
    }
    
    void Upgrade2Path1()
    {
        P1Upgrade2.enabled = !P1Upgrade2.enabled;
    }

    void Upgrade2Path2()
    {
        P2Upgrade2.enabled = !P2Upgrade2.enabled;
    }
}
