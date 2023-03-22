using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsha_Attack : MonoBehaviour
{
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().Damaged(5);
            Debug.Log("적군 공격");
        }
    }

    IEnumerator RemoveCoroutine()
    {

        yield return new WaitForSeconds(2f);
        GameManager.Destroy(this.gameObject);
    }

    void Start()
    {
        StartCoroutine(RemoveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
