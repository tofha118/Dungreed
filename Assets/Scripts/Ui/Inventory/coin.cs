using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public CoinGold coinPrice = null;
    [SerializeReference]
    private Rigidbody2D tempRg;

    public LayerMask floorMask;
    public void Start()
    {
        tempRg = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D lay = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, floorMask);
        if(lay)
        {
            tempRg.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

}

