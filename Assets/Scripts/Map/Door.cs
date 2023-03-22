using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//base
public class Door : MonoBehaviour
{
    public enum DoorType { Up, Down, Right, Left, DoorMax };

    //public GameObject[] DoorPrefabs;

    public Tilemap WallTilemap;
    public Tilemap BackTilemap;
    public Tilemap MoveableTilemap;


    public Tilemap MainWallTilemap;
    public Tilemap MainBackTilemap;
    public Tilemap MainMoveableTilemap;

    public Tile tile;

    public Transform StartPos;
    public Transform EndPos;

    public Vector3Int DoorStartIndex;
    public Vector3Int DoorEndIndex;

    public Vector3Int MainStartIndex;
    public Vector3Int MainEndIndex;


    public Vector2Int size;

    public GameObject NextRoom;

    public BoxCollider2D coll;
    public LayerMask PlayerMask;
    public DoorType type;

    public BoxCollider2D collision;
    public GameObject ParentStage;
    public Transform TeleportPos;

    public bool nowdoorlocked;

    //public GameObject SealStone;
    public Animator SealStoneAni;
    public AnimationClip SealStoneOpenAni;
    public AnimationClip SealStoneCloseAni;
    public ParticleSystem Doorparticle;
    [Header("==================Test==================")]
    public Transform MapInfoPos;
    public Vector3Int StartIndex;
    public Vector3Int ParentStartIndex;
    public bool NowDoorLocked
    {
        get
        {
            return nowdoorlocked;
        }
        set
        {
            nowdoorlocked = value;
            if(Doorparticle!=null&&SealStoneAni!=null)
            {
                if (nowdoorlocked)
                {
                    Doorparticle.gameObject.SetActive(false);
                    //SealStoneAni.SetTrigger("SealClose");
                    StartCoroutine(SealStoneActive());
                }
                else
                {
                    //SealStoneAni.SetTrigger("SealOpen");
                    StartCoroutine(SealStoneInactive());
                    //SealStoneAni.gameObject.SetActive(false);
                    //Invoke("SealStoneActive", 1f);
                    Doorparticle.gameObject.SetActive(true);
                }
            }
        }
    }



    IEnumerator SealStoneActive()
    {
        //SealStoneAni.gameObject.SetActive(true);
        if (SealStoneAni.gameObject.activeSelf == false)
        {
            SealStoneAni.gameObject.SetActive(true);
        }
        if (SealStoneAni.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            SealStoneAni.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        SealStoneAni.SetTrigger("SealClose");
        yield return new WaitForSeconds(0);
    }

    IEnumerator SealStoneInactive()
    {
        //SealStoneAni.gameObject.SetActive(false);
        SealStoneAni.SetTrigger("SealOpen");
        yield return new WaitForSeconds(SealStoneOpenAni.length);
        SealStoneAni.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SealStoneAni.gameObject.SetActive(false);
    }


    //문이 생성될 타일맵의 인덱스번호를 넘겨주면 해당 인덱스부터 문을 생성한다.
    public void CreateDoor(GameObject tilemaps)
    {
        ParentStage = tilemaps;
        

        Tilemap[] temp = tilemaps.GetComponentsInChildren<Tilemap>();
        foreach (var a in temp)
        {
            if (a.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                MainWallTilemap = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("BackGround"))
            {
                MainBackTilemap = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("Moveable"))
            {
                MainMoveableTilemap = a;
            }
        }

        if(this.type==DoorType.Up)
        {
            NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.UpMap;
            //string name = tilemaps.GetComponent<BaseStage>().StageLinkedData.UpMap.gameObject.name;
            //NextRoom = GameObject.Find(name);
        }
        else if(this.type == DoorType.Down)
        {
            NextRoom= tilemaps.GetComponent<BaseStage>().StageLinkedData.DownMap;
            //NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.DownMap;
            //string name = tilemaps.GetComponent<BaseStage>().StageLinkedData.DownMap.gameObject.name;
            //NextRoom = GameObject.Find(name);
        }
        else if (this.type == DoorType.Right)
        {
            NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.RightMap;
            //NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.RightMap;
            //string name = tilemaps.GetComponent<BaseStage>().StageLinkedData.RightMap.gameObject.name;
            //NextRoom = GameObject.Find(name);
        }
        else if (this.type == DoorType.Left)
        {
            NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.LeftMap;
            //NextRoom = tilemaps.GetComponent<BaseStage>().StageLinkedData.LeftMap;
            //string name = tilemaps.GetComponent<BaseStage>().StageLinkedData.LeftMap.gameObject.name;
            //NextRoom = GameObject.Find(name);
        }

        GetTileInfo(type);

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector3Int index = new Vector3Int(x, y * -1, 0);
                index = MainStartIndex + index;

                //TileBase tile = WallTilemap.GetTile(index);
                MainWallTilemap.SetTile(index, null);

                //tile = BackTilemap.GetTile(index);
                MainBackTilemap.SetTile(index, null);

                if(MoveableTilemap!=null)
                {
                    //tile = MoveableTilemap.GetTile(index);
                    MainMoveableTilemap.SetTile(index, null);
                }
            }
        }

        BaseStage stagedata = tilemaps.GetComponent<BaseStage>();
        SetStageDoorInfo();
        //if (type == DoorType.DOWN) NextRoom = stagedata.StageLinkedData.DownMap;
        //if (type == DoorType.DOWN)
        // if (type == DoorType.DOWN)
        //if (type == DoorType.DOWN)
    }

    //문이 만ㄷ르어 지면 해당 문이 위치하는 맵의 정보에 문에대한 값을 넣어준다.
    public void SetStageDoorInfo()
    {
        int x = 0;
        int y = 0;
        RaycastHit2D[] hit;
        int size = ParentStage.GetComponent<BaseStage>().MaxX;
        StartIndex = MainWallTilemap.WorldToCell(MapInfoPos.position);
        
        Vector3 temp = MainWallTilemap.GetCellCenterWorld(StartIndex);
        if (this.type == DoorType.Right || this.type == DoorType.Left)
        {
            for (y = 0; y < 4; y++)
            {
                temp.x = MapInfoPos.position.x + x;
                temp.y = MapInfoPos.position.y + y + 0.3f;
                hit = Physics2D.RaycastAll(temp, new Vector2(1, 1), 0f);

                Vector3Int index = StartIndex;
                index.y = index.y + y;
                //stagedata.Roominfo[x + (y * maxX)] = 0;
                foreach (var a in hit)
                {
                    if (a)
                    {
                        if (a.transform.gameObject.layer == LayerMask.NameToLayer("Door"))
                        {
                            ParentStage.GetComponent<BaseStage>().Roominfo[index.x + (index.y * size)] = (int)BaseStage.TileElement.Door;
                            //ParentStage.Roominfo[] = /*(int)TileElement.Door*/10;

                            //GameObject obj = GameObject.Find("TestCircle");
                            //GameObject copy = GameObject.Instantiate(obj);
                            //copy.transform.position = a.point;
                            //copy.name = "Door";
                            break;
                        }

                    }
                }

            }
        }
        else if (this.type == DoorType.Up || this.type == DoorType.Down)
        {
            for (x = 0; x < 4; x++)
            {
                temp.x = MapInfoPos.position.x + x;
                temp.y = MapInfoPos.position.y + y + 0.3f;
                hit = Physics2D.RaycastAll(temp, new Vector2(1, 1), 0f);

                Vector3Int index = StartIndex;
                index.x = index.x + x;
                //stagedata.Roominfo[x + (y * maxX)] = 0;
                

                foreach (var a in hit)
                {
                    if (a)
                    {
                        if (a.transform.gameObject.layer == LayerMask.NameToLayer("Door")|| a.transform.gameObject.layer == LayerMask.NameToLayer("Moveable"))
                        {
                            ParentStage.GetComponent<BaseStage>().Roominfo[index.x + (index.y * size)] = (int)BaseStage.TileElement.Door;
                            //ParentStage.Roominfo[] = /*(int)TileElement.Door*/10;

                            //GameObject obj = GameObject.Find("TestCircle");
                            //GameObject copy = GameObject.Instantiate(obj);
                            //copy.transform.position = a.point;
                            //copy.name = "Door";
                            break;
                        }

                    }
                }
            }
        }
    }


    //문 타입에 따른 정보들을 받아서 준다. 가로세로 크기, 정보가 있는 셀의 시작 인덱스 번호 등등
    public void GetTileInfo(DoorType type)
    {
        //Tilemap[] temp = DoorPrefabs[(int)type].GetComponentsInChildren<Tilemap>();
        Tilemap[] temp = GetComponentsInChildren<Tilemap>();//자기 자신의 타일맵 정보를 받아온다.
        foreach (var a in temp)
        {
            if (a.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                WallTilemap = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("BackGround"))
            {
                BackTilemap = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("Moveable"))
            {
                MoveableTilemap = a;
            }
        }

        StartPos = transform.Find("StartPos");
        EndPos= transform.Find("EndPos");

        DoorStartIndex = WallTilemap.WorldToCell(StartPos.position);
        DoorEndIndex= WallTilemap.WorldToCell(EndPos.position);
        
        MainStartIndex=MainWallTilemap.WorldToCell(StartPos.position);
        MainEndIndex = MainWallTilemap.WorldToCell(EndPos.position);

        size.x = DoorEndIndex.x - DoorStartIndex.x + 1;
        size.y = DoorStartIndex.y - DoorEndIndex.y + 1;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("트리거 들어옴");
        if(!NowDoorLocked)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[6]);
                Debug.Log("NameToLayer 들어옴");
                GoNextMap(collision.gameObject);
            }
        }
        
        //if(collision.gameObject.layer == PlayerMask)
        //{
        //    Debug.Log("GoNextMap 들어옴");
            
        //}
    }


    public void GoNextMap(GameObject Player)
    {
        Door pos = null;
        GameObject temp = null;
        if (this.type==DoorType.Up)
        {
            //string name = ParentStage.GetComponent<BaseStage>().StageLinkedData.UpMap.gameObject.name;
            temp = ParentStage.GetComponent<BaseStage>().StageLinkedData.UpMap;
            //string name = temp.gameObject.name;

            //NextRoom = GameObject.Find(name);
            pos = NextRoom.GetComponent<BaseStage>().door[(int)DoorType.Down];
        }
        else if (this.type == DoorType.Down)
        {
            //string name = ParentStage.GetComponent<BaseStage>().StageLinkedData.DownMap.gameObject.name;

            temp = ParentStage.GetComponent<BaseStage>().StageLinkedData.DownMap;
            //string name = temp.gameObject.name;

            //NextRoom = GameObject.Find(name);
            pos = NextRoom.GetComponent<BaseStage>().door[(int)DoorType.Up];
        }
        else if (this.type == DoorType.Right)
        {
            //string name = ParentStage.GetComponent<BaseStage>().StageLinkedData.RightMap.gameObject.name;

            temp = ParentStage.GetComponent<BaseStage>().StageLinkedData.RightMap;
            //string name = temp.gameObject.name;

            //NextRoom = GameObject.Find(name);
            pos = NextRoom.GetComponent<BaseStage>().door[(int)DoorType.Left];
        }
        else if (this.type == DoorType.Left)
        {
            //string name = ParentStage.GetComponent<BaseStage>().StageLinkedData.LeftMap.gameObject.name;

            temp = ParentStage.GetComponent<BaseStage>().StageLinkedData.LeftMap;
            //string name = temp.name;

            //NextRoom = GameObject.Find(name);
            pos = NextRoom.GetComponent<BaseStage>().door[(int)DoorType.Right];
        }
        string name = temp.gameObject.name;

        NextRoom = GameObject.Find(name);

        ParentStage.GetComponent<BaseStage>().NowPlayerEnter = false;

        NextRoom.GetComponent<BaseStage>().NowPlayerEnter = true;

        Player.transform.position = pos.TeleportPos.position;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        //DoorPrefabs = new GameObject[(int)(DoorType.Left + 1)];
        //for(DoorType i=DoorType.Up;i<=DoorType.Left;i++)
        //{
        //    DoorPrefabs[(int)i] = Resources.Load<GameObject>($"Prefabs/DoorPrefabs/{i.ToString()}Door");
        //}
        WallTilemap = null;
        BackTilemap = null;
        MoveableTilemap = null;


        MainWallTilemap = null;
        MainBackTilemap = null;
        MainMoveableTilemap = null;
        
        SealStoneAni = GetComponentInChildren<Animator>();
        Doorparticle = GetComponentInChildren<ParticleSystem>();

        NowDoorLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
