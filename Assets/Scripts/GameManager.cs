using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    ObjectManager _objManager = new ObjectManager();
    ResourceManager _resourceManager = new ResourceManager();

    public static ObjectManager ObjManager { get { return Instance._objManager; } }
    public static ResourceManager Resource { get { return Instance._resourceManager; } }

    GameObject Canvas;

    DungeonMapUI dungeonmapui = null;

    static void Init()
    {
        Instance._objManager.Init();
    }

    public static void Clear()
    {
        //ObjManager.Clear();
    }

    private void Awake()
    {
        Instance = this;
        dungeonmapui = DungeonMapUI.Instance;
        Canvas = GameObject.FindGameObjectWithTag("MainCanvas");
    }

    public void On_Damage_Text(Transform pos, float damage, bool critical = false)
    {
        float rndX = Random.Range(0f, 0.5f);
        float rndY = Random.Range(0f, 0.5f);

        RectTransform damageText_Rect = Resource.Instantiate("Ui_Prefabs/DamageUI", Canvas.transform)
            .GetComponent<RectTransform>();

        Text damage_Text = damageText_Rect.transform.GetChild(0).GetComponent<Text>();

        Rigidbody2D rigid = damageText_Rect.gameObject.GetComponent<Rigidbody2D>();

        // 데미지 텍스트를 띄우기 위한 좌표를 스크린에서 받아옴.
        Vector3 damagePos = Camera.main.WorldToScreenPoint(new Vector3(pos.position.x + 0.8f + rndX,
            pos.position.y + 0.2f + rndY, 0));

        damageText_Rect.position = damagePos; // 좌표 지정

        damageText_Rect.gameObject.SetActive(true); // 활성화 시켜 보여줌.

        damage_Text.text = damage.ToString(); // 텍스트를 입은 데미지

        if (critical)
            damage_Text.color = new Color(255, 255, 0);
        else
            damage_Text.color = new Color(255, 255, 255);

        rigid.AddForce(Vector2.right * 50f, ForceMode2D.Impulse);
        rigid.AddForce(Vector2.up * 100f, ForceMode2D.Impulse);

        StartCoroutine(Damage_Text_Off_Time(damageText_Rect.gameObject));
    }

    public void On_Coin_Text(Transform pos,float coin)
    {
        RectTransform coinText_Rect = Resource.Instantiate("Ui_Prefabs/CoinUI", Canvas.transform)
            .GetComponent<RectTransform>();

        Text coin_Text = coinText_Rect.transform.GetChild(0).GetComponent<Text>();

        Rigidbody2D rigid = coinText_Rect.gameObject.GetComponent<Rigidbody2D>();

        // 데미지 텍스트를 띄우기 위한 좌표를 스크린에서 받아옴.
        Vector3 coinPos = Camera.main.WorldToScreenPoint(new Vector3(pos.position.x + 0.8f, pos.position.y + 0.2f, 0));
        coinText_Rect.position = coinPos; // 좌표 지정

        coinText_Rect.gameObject.SetActive(true); // 활성화 시켜 보여줌.

        coin_Text.text = coin.ToString() + "G"; // 코인

        rigid.AddForce(Vector2.right * 50f, ForceMode2D.Impulse);
        rigid.AddForce(Vector2.up * 100f, ForceMode2D.Impulse);

        StartCoroutine(Damage_Text_Off_Time(coinText_Rect.gameObject));
    }

    IEnumerator Damage_Text_Off_Time(GameObject obj) // 피격당할 시 나타나는 데미지 텍스트를 보여준 후 꺼주기 위한 코루틴.
    {
        yield return new WaitForSeconds(0.5f);

        obj.SetActive(false);
    }

    void Start()
    {
        Inventory.Instance.GetSword();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            PlayerPrefs.DeleteAll();
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(DungeonMapUI.Instance.gameObject.activeSelf==true)
            {
                DungeonMapUI.Instance.gameObject.SetActive(false);
            }
            else
            {
                DungeonMapUI.Instance.gameObject.SetActive(true);
            }
        }
    }
}
