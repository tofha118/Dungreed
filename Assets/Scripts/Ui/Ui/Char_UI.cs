using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_UI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Text txtCoin;

    [SerializeField]
    private Image hpbar;

    [SerializeField]
    private Text txtHp;

    [SerializeField]
    private Text txtLv;

    [SerializeField]
    private Text txtHgr;

    [SerializeField]
    private GameObject dash1;
    [SerializeField]
    private GameObject dash2;

    [SerializeField]
    private GameObject result;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        txtCoin.text = player.Gold.ToString();  // 동전 텍스트

        txtLv.text = player.Level.ToString();  // 레벨 텍스트

        txtHgr.text = string.Format("{0} / {1}", player.Hungrycurr.ToString(), player.HungryMax.ToString());  // 허기짐 텍스트

        hpbar.fillAmount = (float)player.Hp / (float)player.MaxHP;  // 체력바
        txtHp.text = string.Format("{0} / {1}", player.Hp.ToString(), player.MaxHP.ToString());  // 체력 텍스트

        //// 대쉬 시작
        if (player.Dashcount == 2) 
        {
            dash1.SetActive(true);
            dash2.SetActive(true);
        }
        else if(player.Dashcount == 1)
        {
            dash1.SetActive(true);
            dash2.SetActive(false);
        }
        else if(player.Dashcount == 0)
        {
            dash1.SetActive(false);
            dash2.SetActive(false);
        }
        //// 대쉬 마무리

        if (Input.GetKeyDown(KeyCode.F5))
            Result_Open();
    }

    public void Result_Open()
    {
        result.SetActive(true);
    }
}
