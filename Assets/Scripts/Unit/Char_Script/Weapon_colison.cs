using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_colison : MonoBehaviour
{
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage;
        float critical = player.GetComponent<Player>().Critical;
        bool criticalon=false;
        float tmp = Random.Range(0, 10);
        damage = player.Damage;
        if (critical>=tmp)
        {
            criticalon = true;
            damage = player.Damage * 1.1f;
        }
        if(collision.tag=="Boss")
        {
            collision.GetComponent<Boss>().Damaged(damage, criticalon);
        }      
        if (collision.tag == "Arsha")
            collision.GetComponent<Arsha>().Damaged(damage, criticalon);
        if (collision.tag == "SkelDog")
            collision.GetComponent<SkelDog>().Damaged(damage, criticalon);
        if (collision.tag == "AbyssGuardian")
            collision.GetComponent<AbyssGuardian>().Damaged(damage, criticalon);
       }
    }



