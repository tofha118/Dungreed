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
        //대사 생성 
        talkData.Add("NPC_BlackSmith", new string[] { "그리 좋은 물건은 아니지만··· 보탬은 될 걸세.", "오늘 만든 건 그게 끝이라네. 다음에 다시 오게나" });  // 대장간
        talkData.Add("NPC_Builder", new string[] { "오, 자네군." });  // 건설
        talkData.Add("NPC_Commander", new string[] { "자네 왔군! 훈련할 텐가?" });  // 훈련
        talkData.Add("NPC_Merchant", new string[] { "안녕하세요! 좋은 물건 많이 있어요!" });   // 상점
        talkData.Add("NPC_PistolMan", new string[] { "" });  // 총기
        talkData.Add("NPC_Temple", new string[] { "당신의 강한 마음은, 분명 분명 페리누님께 닿을 수 있을 거예요." });  // 사원
        talkData.Add("NPC_Horerica", new string[] { "오늘은 어떤 요리를 드시러 오셨나요?" });  // 음식점
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
