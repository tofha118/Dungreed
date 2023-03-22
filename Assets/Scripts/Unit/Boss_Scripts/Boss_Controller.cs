using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    public Boss boss;

    public bool die_Start = false;
    public bool damage_Start = false;

    void Start()
    {
        boss = GetComponent<Boss>();
    }

    void Update()
    {
        if (die_Start)
        {
            die_Start = !die_Start;
            boss.Die();
        }

        if (damage_Start)
        {
            damage_Start = !damage_Start;
            //boss.Damaged();
        }
    }
}
