using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    const int MAXAB = 20;

    [SerializeField]
    Text w_count_txt;
    [SerializeField]
    Text w_amount_txt;
    int wAb;  // 분노 공격력 배율2 

    [SerializeField]
    Text s_count_txt;
    [SerializeField]
    Text s_amount_txt;
    int sAb;   // 신속 공격속도 배율0.5

    [SerializeField]
    Text p_count_txt;
    [SerializeField]
    Text p_amount_txt;
    int pAb;  // 인내 이동속도 배율0.5퍼 

    [SerializeField]
    Text a_count_txt;
    [SerializeField]
    Text a_amount_txt;
    int aAb;   // 신비 크리티컬 배율 0.5퍼  

    [SerializeField]
    Text g_count_txt;
    [SerializeField]
    Text g_amount_txt;
    int gAb;  // 탐욕 최대체력 배율 2

    [SerializeField]
    Text useable_txt;
    int useableAb;  // 사용 가능한 포인트

    int maxAb=30;  // 최대치

    [SerializeField]
    private GameObject P;

    Player p;
    
    // Start is called before the first frame update
    void Start()
    {        
        wAb = 0;
        sAb = 0;
        pAb = 0;
        aAb = 0;
        gAb = 0;

       abilityShopOff();
        p = P.GetComponent<Player>();
        maxAb = p.Level;

        abReset();
        updateText();
    }
    public void abilityShopOn()
    {
        this.gameObject.SetActive(true);
    }

    void abilityShopOff()
    {
        this.gameObject.SetActive(false);
    }

    private void abReset()
    {
        useableAb = maxAb;
        wAb = 0;
        sAb = 0;
        pAb = 0;
        aAb = 0;
        gAb = 0;
    }
    void updateAbText()
    {
        w_amount_txt.text = string.Format("+ {0}", wAb * 2);
        s_amount_txt.text = string.Format("+ {0}", (float)sAb * 0.5);
        p_amount_txt.text = string.Format("+ {0}", (float)pAb * 0.5);
        a_amount_txt.text = string.Format("+ {0}", (float)aAb * 0.5);
        g_amount_txt.text = string.Format("+ {0}", gAb * 2);
    }

    void updateText()
    {
        w_count_txt.text = wAb.ToString();
        s_count_txt.text = sAb.ToString();
        p_count_txt.text = pAb.ToString();
        a_count_txt.text = aAb.ToString();
        g_count_txt.text = gAb.ToString();

        updateAbText();

        useable_txt.text = string.Format("남은 포인트 : {0}", useableAb);
    }

    public void increseW()
    {
        if (useableAb > 0 && wAb < MAXAB)
        {
            useableAb--;
            wAb++;
        }
        updateText();
    }
    public void increseS()
    {
        if (useableAb > 0 && sAb < MAXAB)
        {
            useableAb--;
            sAb++;
        }
        updateText();
    }
    public void increseP()
    {
        if (useableAb > 0 && pAb < MAXAB)
        {
            useableAb--;
            pAb++;
        }
        updateText();
    }
    public void increseA()
    {
        if (useableAb > 0 && aAb < MAXAB)
        {
            useableAb--;
            aAb++;
        }
        updateText();
    }
    public void increseG()
    {
        if (useableAb > 0 && gAb < MAXAB)
        {
            useableAb--;
            gAb++;
        }
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            abilityShopOff();
            p.UpdateAbility(wAb, pAb, sAb, aAb, gAb);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            abReset();
            updateText();
        }
        
    }
}
