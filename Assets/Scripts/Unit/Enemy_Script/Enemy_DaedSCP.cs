using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DaedSCP : MonoBehaviour
{
    
    IEnumerator Dead_Courtine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Resource.Destroy(this.gameObject);
        
        
    }
    public void Init(Transform pos)
    {
        this.transform.position = pos.position;
        StartCoroutine(Dead_Courtine());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
