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


    //맵 ui를 띄워서 유저가 클릭하도록 해주고 클릭된 인덱스값을 가지고 mapmanager의 teleport함수를 실행시켜 주면 해당 함수에서 해당 맵의 텔레포터를 찾아서 teleporthere함수를 실행 시켜 준다.
    //텔레포트 시작 애니메이션 끝나면 실행
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


    

    //애니메이션 끝나고 실행 이건 
    //캐릭터가 일단 넘어오고 화면에서는 안보이게 한다.
    //그러고 애니메이션을 실행시키고 애니메이션이 끝나면 플레이어가 움직이도록
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

    //비활성화 되어 있을때 사용해서 활성화 시켜준다.
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


    //활성화 되어있는 것을 활성화 시켜준다.
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
