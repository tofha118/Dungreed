using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Bullet : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("z");
            collision.GetComponent<Player>().Damaged(Damage);
        }
    }
}
