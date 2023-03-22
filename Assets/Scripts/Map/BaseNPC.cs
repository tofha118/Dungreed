using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseNPC : MonoBehaviour
{
    public string NPCName;

    public string NPCAction;
    public string NPCNick;

    public PlayerInteraction interaction;

    public bool NowQuest;

    public GameObject EnterPopUp;

    public GameObject QuestPopUp;

    public GameObject Body;

    [SerializeField]
    private GameObject DialogBox;
    [SerializeField]
    private GameObject Btn;
    private Button m_Btn_Action;

    public void InitSetting()
    {
        interaction = GetComponent<PlayerInteraction>();
        Body = this.transform.Find("Body").gameObject;
        EnterPopUp = transform.Find("EnterPopUp").gameObject;
        EnterPopUp.SetActive(false);
        //interaction.AddEnterAction(ShowPopUp);
        //interaction.AddOutAction(ClosePopUp);
        interaction.AddKeydownAction(ShowKeyDownPopUp);
        if (DialogBox == null)
        {
            DialogBox = GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(0).gameObject;
            Btn = DialogBox.transform.GetChild(2).transform.GetChild(1).gameObject;
        }
    }


    //���� ����Ʈ�� ������ ����Ʈ�˾��� ���� ������ ������ ���� f �����ۿ� �˾��� ����.
    
    public void ShowPopUp()
    {
        if (EnterPopUp != null)
            EnterPopUp.SetActive(true);
    }

    public void ClosePopUp()
    {
        if (EnterPopUp != null)
            if (EnterPopUp.activeSelf)
                EnterPopUp.SetActive(false);

    }

    public void ShowQuestPopUp()
    {
        if (QuestPopUp != null)
            QuestPopUp.SetActive(true);
    }

    public void ShowKeyDownPopUp()
    {
        Debug.Log($"{NPCName}����");

        DialogBox.SetActive(true);
        DialogBox.GetComponent<Dialog>().findNpc(this);

        m_Btn_Action.onClick.RemoveAllListeners();
        btnAction();
    }

    public void btnAction()
    {
        string name = NPCName;
        m_Btn_Action.onClick.AddListener(delegate () { ActionManager.instance.actionOn(name); });
    }

    private void Start()
    {
        InitSetting();

        m_Btn_Action = Btn.GetComponent<Button>();
    }

    //// Start is called before the first frame update
    //virtual void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
