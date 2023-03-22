using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Map_Room_Tile : MonoBehaviour
{
    public enum Direction { Up, Right, Down, Left, D_Max };
    public enum MapElements { Wormb, Start, End, Restaurant, Shop, Chest, ElementMax };

    public GameObject[] MapIconElements;

    public DungeonMapUIBody parent = null;

    public Transform pos1;
    public Transform pos2;

    public GameObject Icon1;
    public GameObject Icon2;

    public Sprite SeletedSprite;
    public Sprite NonSeletSprite;

    public List<GameObject> IconList = new List<GameObject>();

    [SerializeField]
    BaseStage baseinfo;
    [SerializeField]
    Image body;//
    [SerializeField]
    Image[] line;//
    [SerializeField]
    int roomnum;
    [SerializeField]
    bool isSearched;
    [SerializeField]
    bool isTeleporter;
    GameObject teleporterIcon;
    [SerializeField]
    bool nowPlayerEnter;
    [SerializeField]
    public Vector2Int roomindex;
    [SerializeField]
    bool iscleared;


    MapManager.ROOMTYPE roomtype;

    //public enum ROOMTYPE { Start, Restaurant, Shop, End, Boss, NOMAL, MAX };

    private bool isactive;

    //맵 타일들 띄우기
    //아직 활성화 되지 않은 맵들 안보이게 하기 이정도?
    public bool AllSettingDone = false;

    public MapManager.ROOMTYPE Roomtype
    {
        get
        {
            return roomtype;
        }
        set
        {
            roomtype = value;
            if(Icon1==null)
            {
                switch (roomtype)
                {
                    case MapManager.ROOMTYPE.Start:
                        Icon1 = GameObject.Instantiate(MapIconElements[(int)MapElements.Start]);
                        break;

                    case MapManager.ROOMTYPE.End:
                        Icon1 = GameObject.Instantiate(MapIconElements[(int)MapElements.End]);
                        break;

                    case MapManager.ROOMTYPE.Restaurant:
                        Icon1 = GameObject.Instantiate(MapIconElements[(int)MapElements.Restaurant]);
                        break;

                    case MapManager.ROOMTYPE.Shop:
                        Icon1 = GameObject.Instantiate(MapIconElements[(int)MapElements.Shop]);
                        break;
                }
            }
            if(Icon1!=null)
            {
                Icon1.transform.parent = this.transform;
                Icon1.transform.localPosition = pos1.localPosition;
            }
        }
    }

    public bool IsCleared
    {
        get
        {
            return iscleared;
        }

        set
        {
            iscleared = value;
        }
    }

    public bool IsActive
    {
        get
        {
            return isactive;
        }
        set
        {
            isactive = value;
            if(isactive)
            {
                if (Icon1 != null)
                {
                    Icon1.SetActive(true);

                }
                if(IsTeleporter)
                {
                    Icon2.SetActive(true);
                }
                
                
                this.GetComponent<Image>().enabled = true;
                for (int i = 0; i < (int)Direction.D_Max; i++)
                {
                    line[i].GetComponent<Image>().enabled = true;
                }

            }
            else
            {
                this.GetComponent<Image>().enabled = false;
                for(int i=0;i<(int)Direction.D_Max;i++)
                {
                    line[i].GetComponent<Image>().enabled = false;
                }
                if(IsTeleporter)
                {
                    Icon2.SetActive(false);
                }
                if(Icon1!=null)
                {
                    Icon1.SetActive(false);

                }
            }
        }
    }

    public bool IsSearched
    {
        get
        {
            return isSearched;
        }
        set
        {
            isSearched = value;
            if(isSearched)
            {
                //this.gameObject.SetActive(true);
                IsActive = true;
            }
            else 
            {
                //this.gameObject.SetActive(false);
                IsActive = false;
            }
        }
    }

    public bool IsTeleporter
    {
        get
        {
            return isTeleporter;
        }
        set
        {
            isTeleporter = value;
            if(isTeleporter)
            {
                if(Icon2==null)
                {
                    Icon2 = GameObject.Instantiate(MapIconElements[(int)MapElements.Wormb]);

                    //Icon2.GetComponent<Button>().onClick.AddListener(TeleporteHere);

                    Icon2.GetComponent<UIClickableIcon>().AddClickAction(TeleporteHere);

                    Icon2.transform.parent = this.transform;
                    
                    Icon2.transform.localPosition = pos2.localPosition;
                    
                }
            }
        }
    }

    public bool NowPlayerEnter
    {
        get
        {
            return nowPlayerEnter;
        }

        set
        {
            nowPlayerEnter = value;
            if(nowPlayerEnter)
            {
                StartCoroutine(SelectedAction());
            }
            else
            {
                StopCoroutine(SelectedAction());
                GetComponent<Image>().sprite = NonSeletSprite;
            }
        }
    }


    MapManager.ROOMTYPE type;//시작, 음식점, 상점, 끝, 보스, 일반

    private void Awake()
    {
        //parent = GetComponentInParent<DungeonMapUIBody>();
        Init();

    }



    IEnumerator SelectedAction()
    {
        List<Sprite> list = new List<Sprite>();
        list.Add(SeletedSprite);
        list.Add(NonSeletSprite);
        int index = 0;
        while(true)
        {
            if(!NowPlayerEnter)
            {
                yield break;
            }
            GetComponent<Image>().sprite = list[index];
            index = (index + 1) % list.Count;
            yield return new WaitForSeconds(1f);
        }
        //this.GetComponent<Image>().
        
    }

    public void TeleporteHere()
    {
        //Debug.Log($"{roomindex.x},{roomindex.y} 번방 텔포 눌림");
        parent.ClickedTeleportHere(roomindex.x, roomindex.y);
    }

    public void Init()
    {
        //pos1 = this.transform.Find("Pos1");
        //pos2 = this.transform.Find("Pos2");
        body = GetComponent<Image>();

        line = new Image[(int)Direction.D_Max];
        Image[] temp = GetComponentsInChildren<Image>();
        

        for (int j = 0; j < temp.Length; j++)
        {
            for (Direction i = Direction.Up; i < Direction.D_Max; i++)
            {
                if (temp[(int)j].name == i.ToString() + "Line")
                {
                    line[(int)i] = temp[(int)j];
                }
            }
        }
        roomnum = -1;
        IsSearched = false;
        IsTeleporter = false;
        NowPlayerEnter = false;
        baseinfo = null;

        type = MapManager.ROOMTYPE.Start;
    }
    public void TileSetting(GameObject RoomObj, int x, int y)//클리어는 안했지만 현재 플레이어가 방에 들어가 있을때의 경우
    {
        baseinfo = RoomObj.GetComponent<BaseStage>();
        roomnum = baseinfo.StageLinkedData.Num;
        IsTeleporter = baseinfo.IsTeleporter;
        NowPlayerEnter = baseinfo.NowPlayerEnter;
        roomindex = new Vector2Int(x, y);
        Roomtype = baseinfo.type;
        SetLine(baseinfo.StageLinkedData);
        IsCleared = baseinfo.RoomIsClear;//텔포할때 사용


        IsSearched = baseinfo.IsSearched;//이 값이 false면 비활성화 또한 isactive

        if(!AllSettingDone)
        {
            AllSettingDone = true;
        }
        
    }


    public void DataUpdate()
    {
        IsSearched = baseinfo.IsSearched;
        IsCleared = baseinfo.RoomIsClear;
        

        IsTeleporter = baseinfo.IsTeleporter;

        NowPlayerEnter = baseinfo.NowPlayerEnter;
    }





    public void SetLine(LinkedData link)
    {
        if (link.UpMap == null) line[(int)Direction.Up].gameObject.SetActive(false);
        if (link.DownMap == null) line[(int)Direction.Down].gameObject.SetActive(false);
        if (link.RightMap == null) line[(int)Direction.Right].gameObject.SetActive(false);
        if (link.LeftMap == null) line[(int)Direction.Left].gameObject.SetActive(false);
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
