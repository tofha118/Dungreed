using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    string m_ID;

    Dictionary<string, string[]> talkData;

    [SerializeField]
    private BaseNPC m_npc;

    [SerializeField]
    private Text nick;
    [SerializeField]
    private Text dialog;
    [SerializeField]
    private Text action;

    int talkIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<string, string[]>();
        GenerateData();

        offDialogBox();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            offDialogBox();
        }
    }

    public void offDialogBox()
    {
        this.gameObject.SetActive(false);
    }

    public void findNpc(BaseNPC npc)
    {
        this.m_npc = npc;

        setting();
    }

    void setting()
    {
        nick.text = m_npc.NPCNick;
        action.text = m_npc.NPCAction;
        if (GetTalk(m_npc.NPCName) != null)
        {
            dialog.text = GetTalk(m_npc.NPCName);
            talkIndex++;
            setting();
        }
    }

    void GenerateData()
    {
        //��� ���� 
        talkData.Add("NPC_BlackSmith", new string[] { "�׸� ���� ������ �ƴ����������� ������ �� �ɼ�.", "���� ���� �� �װ� ���̶��. ������ �ٽ� ���Գ�" });  // ���尣
        talkData.Add("NPC_Builder", new string[] { "��, �ڳױ�." });  // �Ǽ�
        talkData.Add("NPC_Commander", new string[] { "�ڳ� �Ա�! �Ʒ��� �ٰ�?" });  // �Ʒ�
        talkData.Add("NPC_Merchant", new string[] { "�ȳ��ϼ���! ���� ���� ���� �־��!" });   // ����
        talkData.Add("NPC_PistolMan", new string[] { "" });  // �ѱ�
        talkData.Add("NPC_Temple", new string[] { "����� ���� ������, �и� �и� �丮���Բ� ���� �� ���� �ſ���." });  // ���
        talkData.Add("NPC_Horerica", new string[] { "������ � �丮�� ��÷� ���̳���?" });  // ������
    }

    public string GetTalk(string id)
    {
        if (talkIndex == talkData[id].Length)
        {
            talkIndex = 0;
            return null;
        }
        else
            return talkData[id][talkIndex];
    }
}
