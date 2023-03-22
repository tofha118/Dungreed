using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    [SerializeField]
    private Image[] ScreenBox; // 보스 등장시 위아래 검은색 박스
    [SerializeField]
    private Text[] BossName_Text; // 보스 이름, 별칭 텍스트
    [SerializeField]
    private Image End_Screen; // 보스 사망시 흰색 화면
    [SerializeField]
    public Image[] Boss_HP_Image; // 체력 바 관련 이미지들, 보스 시작시 켜줌.
    [SerializeField]
    private Image Boss_HP_Bar; // 보스 체력바

    public Boss cur_Boss;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //Boss_Start = true;
    }

    bool first_Check = false;
    public bool Boss_Start = false;
    public bool boss_Scene_Start = false;
    void Screen_Effect()
    {
        if (!Boss_Start)
            return;

        if (!first_Check)
        {
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.boss_Audioclip[0]);
            first_Check = true;
            boss_Scene_Start = true;
        }


        foreach (Image img in ScreenBox)
        {
            Color alpha = img.color;
            alpha.a += Time.deltaTime * 1f;
            img.color = alpha;

            if (img.color.a >= 1)
            {
                Boss_Start = false;
            }
        }

        if (!Boss_Start)
        {
            cur_Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
            SoundManager.Instance.effectSource.PlayOneShot(SoundManager.Instance.boss_Audioclip[1]);
            StartCoroutine(Boss_Name_Coroutine());
            Debug.Log("실행");

            foreach (Image img in Boss_HP_Image)
            {
                img.gameObject.SetActive(true);
            }
        }
    }

    int name_Count = 0;
    IEnumerator Boss_Name_Coroutine()
    {
        if (name_Count < 2)
        {
            while (true)
            {
                Color alpha = BossName_Text[name_Count].color;
                alpha.a += Time.deltaTime * 1f;
                BossName_Text[name_Count].color = alpha;

                if (BossName_Text[name_Count].color.a >= 1)
                {
                    name_Count++;
                    break;
                }
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.25f);

        if (name_Count < 2)
        {
            StartCoroutine(Boss_Name_Coroutine());
        }
        else
        {
            yield return new WaitForSeconds(1f);

            bool endflag = false;
            while (true)
            {
                yield return null;

                for (int i = 0; i < 2; i++)
                {
                    Color alphaA = ScreenBox[i].color;
                    Color alphaB = BossName_Text[i].color;

                    alphaA.a -= Time.deltaTime * 1f;
                    alphaB.a -= Time.deltaTime * 1f;

                    ScreenBox[i].color = alphaA;
                    BossName_Text[i].color = alphaB;

                    if (alphaA.a <= 0)
                    {
                        endflag = true;
                    }
                }

                if (endflag)
                {
                    StartCoroutine(cur_Boss.Pattern_Coroutine());
                    break;
                }
            }
        }
    }

    public IEnumerator End_Screen_Coroutine()
    {
        Color al = End_Screen.color;
        al.a = 1;
        End_Screen.color = al;

        while (true)
        {
            Color alpha = End_Screen.color;
            alpha.a -= Time.deltaTime * 0.5f;
            End_Screen.color = alpha;

            if (End_Screen.color.a <= 0)
            {
                break;
            }
            yield return null;
        }

    }

    void Boss_HP_Update()
    {
        if (boss_Scene_Start)
            Boss_HP_Bar.fillAmount = cur_Boss.Hp / cur_Boss.MaxHP;
    }

    void Update()
    {
        Screen_Effect();
        Boss_HP_Update();
    }
}
