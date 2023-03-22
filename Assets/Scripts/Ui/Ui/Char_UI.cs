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
        txtCoin.text = player.Gold.ToString();  // ���� �ؽ�Ʈ

        txtLv.text = player.Level.ToString();  // ���� �ؽ�Ʈ

        txtHgr.text = string.Format("{0} / {1}", player.Hungrycurr.ToString(), player.HungryMax.ToString());  // ����� �ؽ�Ʈ

        hpbar.fillAmount = (float)player.Hp / (float)player.MaxHP;  // ü�¹�
        txtHp.text = string.Format("{0} / {1}", player.Hp.ToString(), player.MaxHP.ToString());  // ü�� �ؽ�Ʈ

        //// �뽬 ����
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
        //// �뽬 ������

        if (Input.GetKeyDown(KeyCode.F5))
            Result_Open();
    }

    public void Result_Open()
    {
        result.SetActive(true);
    }
}
