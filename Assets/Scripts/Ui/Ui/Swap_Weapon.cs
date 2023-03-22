using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swap_Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject one;

    [SerializeField]
    private GameObject two;

    public GameObject Player;
    bool up = false; // true일때 one이 위에


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame 
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (up && !Player.GetComponent<Player>().attack )
            {
                RiseOne();
                SwapPosition();
                up = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!up && !Player.GetComponent<Player>().attack)
            {
                RiseTwo();
                SwapPosition();
                up = true;
            }
        }
    }

    void SwapPosition()
    {
        Vector2 temppos = one.transform.position;

        one.transform.position = two.transform.position;
        two.transform.position = temppos;
    }

    void RiseOne()
    {
        one.transform.SetAsFirstSibling();
    }

    void RiseTwo()
    {
        two.transform.SetAsFirstSibling();
    }
}
