using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateDamageNumber : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] TextMeshProUGUI damageTxt;

    void Awake()
    {
        damageTxt.text = damage.ToString();
    }

    void Update()
    {
        
    }

    public void SetDamageNum(int dmg)
    {
        damage = dmg;
        damageTxt.text = dmg.ToString();
    }

    public int GetDamageNum()
    {
        return damage;
    }
}
