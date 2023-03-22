using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enm_Hp_Bar_Manager: Singleton<Enm_Hp_Bar_Manager>
{
    [SerializeField] GameObject m_hpbarPrefab = null;

    List<Unit> m_enemies = new List<Unit>();  // 적
    List<Transform> m_objectList = new List<Transform>();  // 오브젝트 위치
    List<GameObject> m_hpBarList = new List<GameObject>();  // 체력 바
    List<Image> m_hpList = new List<Image>();  // 체력

    Camera m_cam = null;



    // Start is called before the first frame update
    public void UpadateList()
    {
        InitList();

        m_cam = Camera.main;

        List<GameObject> t_objectList = new List<GameObject>();
        foreach(Transform go in SpawnManager.instance.NowSpawndList)
        {
            t_objectList.Add(go.gameObject);
        }

        for (int i = 0; i < t_objectList.Count; i++)
        {
            m_objectList.Add(t_objectList[i].transform);
            GameObject t_hpbar = Instantiate(m_hpbarPrefab, t_objectList[i].transform.position, Quaternion.identity, transform);
            m_hpBarList.Add(t_hpbar);
            m_hpList.Add(t_hpbar.transform.GetChild(0).GetComponent<Image>());
            m_enemies.Add(t_objectList[i].GetComponent<Unit>());
        }
    }

    void InitList()
    {
        m_enemies.Clear();
        m_hpBarList.Clear();
        m_hpList.Clear();
        m_objectList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_enemies.Count; i++)
        {
            if (m_enemies[i].Hp <= 0f)
            {
                m_hpBarList[i].SetActive(false);
            }
            m_hpList[i].fillAmount = m_enemies[i].Hp / m_enemies[i].MaxHP;
        }

        for (int i = 0; i < m_objectList.Count; i++)
        {
            m_hpBarList[i].transform.position = m_cam.WorldToScreenPoint(m_objectList[i].position + new Vector3(0, 1f, 0));
        }
    }
}
