using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Bullet : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rigidbody;
    public bool bulitonof = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ω√¿€!");
        bulitonof = false;
        rigidbody = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      
    }

    // Update is called once per frame
    void Update()
    {
       if(bulitonof==true)
        {
            StartCoroutine("exit1");         
        }
    }
    IEnumerator exit1()
    {
        yield return new WaitForSeconds(0.5f);
        bulitonof = false;
        GameManager.Resource.Destroy(this.gameObject);
    }
    public void Bulletof()
    {
        bulitonof = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage;
        float critical = player.GetComponent<Player>().Critical;
        bool criticalon = false;
        float tmp = Random.Range(0, 10);
        damage = player.Damage;
        if (critical >= tmp)
        {
            criticalon = true;
            damage = player.Damage * 1.1f;
        }
        if (collision.tag=="Boss")
        {
            collision.GetComponent<Boss>().Damaged(damage, criticalon);
            bulitonof = true;
        }
        if (collision.tag == "Arsha")
        {
            collision.GetComponent<Arsha>().Damaged(damage, criticalon);
             bulitonof = true;
        }
        if (collision.tag == "SkelDog")
        {
            collision.GetComponent<SkelDog>().Damaged(damage, criticalon);
            bulitonof = true;
         }
        if (collision.tag == "AbyssGuardian")
        { 
            collision.GetComponent<AbyssGuardian>().Damaged(damage, criticalon);
            bulitonof = true;
        }

        if (collision.tag == "wall")
        {
            bulitonof = true;
        }
   }
}
