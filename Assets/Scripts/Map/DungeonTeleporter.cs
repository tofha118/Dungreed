using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTeleporter : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    private bool isActive;

    public AnimationClip UpAni;
    public AnimationClip DownAni;
    public AnimationClip IdleAni;

    public BaseStage parentmap;
    public PlayerInteraction interaction;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
            if(isActive)
            {
                StartCoroutine(TeleporterActive());
            }
            else
            {
                StartCoroutine(TeleporterInactive());
            }

        }
    }


    public BaseStage[] rooms;


    //�� ui�� ����� ������ Ŭ���ϵ��� ���ְ� Ŭ���� �ε������� ������ mapmanager�� teleport�Լ��� ������� �ָ� �ش� �Լ����� �ش� ���� �ڷ����͸� ã�Ƽ� teleporthere�Լ��� ���� ���� �ش�.
    //�ڷ���Ʈ ���� �ִϸ��̼� ������ ����
    public void Teleport(int x, int y, GameObject player)
    {
        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[6]);
        BaseStage stage = MapManager.Instance.StageArr[x + (y * MapManager.Instance.arrsize)].GetComponent<BaseStage>();
        if(stage.IsTeleporter)
        {
            //stage.Teleporter.TeleportHere(/**/);
        }
    }


    //public void TeleporterActive()
    //{
    //    //animator.SetTrigger("TeleporterActive");
    //}


    

    //�ִϸ��̼� ������ ���� �̰� 
    //ĳ���Ͱ� �ϴ� �Ѿ���� ȭ�鿡���� �Ⱥ��̰� �Ѵ�.
    //�׷��� �ִϸ��̼��� �����Ű�� �ִϸ��̼��� ������ �÷��̾ �����̵���
    public void TeleportHere(/*GameObject player*/)
    {
        if(IsActive)
        {
            parentmap.NowPlayerEnter = true;
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = this.transform.position + new Vector3(0, 1,0);
            //Player.gameObject.SetActive(false);

            animator.Play("Entry");


        }
    }

    public void ActiveAniOver()
    {
        Player.gameObject.SetActive(true);
    }

    //��Ȱ��ȭ �Ǿ� ������ ����ؼ� Ȱ��ȭ �����ش�.
    IEnumerator TeleporterActive()
    {
        //IsActive = true;
        if(GetComponent<SpriteRenderer>().enabled==false)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<PlayerInteraction>().enabled = true;
        }
        animator.SetTrigger("TeleporterUp");
        yield return new WaitForSeconds(0);
    }


    //Ȱ��ȭ �Ǿ��ִ� ���� Ȱ��ȭ �����ش�.
    IEnumerator TeleporterInactive()
    {
        //IsActive = false;
        animator.SetTrigger("TeleporterDown");
        yield return new WaitForSeconds(DownAni.length);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerInteraction>().enabled = false;
    }




    public void ShowAllMapUI()
    {
        if (DungeonMapUI.Instance.gameObject.activeSelf == true)
        {
            DungeonMapUI.Instance.gameObject.SetActive(false);
        }
        else
        {
            DungeonMapUI.Instance.gameObject.SetActive(true);
        }
    }


    

    private void Awake()
    {
        interaction = GetComponent<PlayerInteraction>();
        animator = GetComponent<Animator>();
        parentmap = GetComponentInParent<BaseStage>();
        interaction.AddKeydownAction(ShowAllMapUI);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
