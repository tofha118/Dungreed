using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restaurant_Manager : Singleton<Restaurant_Manager>
{
    [SerializeField]
    private GameObject rstrt;

    public Player p;

    [SerializeField]
    private Text coin_txt;
    [SerializeField]
    private Text hungry_txt;
    [SerializeField]
    private Image hungry_img;

    private void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void rstrtOn()
    {
        this.gameObject.SetActive(true);
    }

    public void rstrtOff()
    {
        this.gameObject.SetActive(false);
    }

    void updateTxt()
    {
        coin_txt.text = p.Gold.ToString();
        hungry_txt.text = string.Format("{0}  /  {1}", p.Hungrycurr, p.HungryMax);
        hungry_img.fillAmount = (float)p.Hungrycurr / p.HungryMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            rstrtOff();
        }

        updateTxt();
    }
}
