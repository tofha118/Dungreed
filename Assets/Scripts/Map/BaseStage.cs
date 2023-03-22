using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class BaseStage : MonoBehaviour
{
    //public enum Direction { Left, Right, Up, Down,DIREC };

    //public enum MapType { NOMAL, RESTAURANT, SHOP, MAPTYPEMAX};
    public enum TileElement { BackGround, Wall, Moveable,Door, ElementMax };

    public enum Chests { Stash, Rare, Yellow, Gold, Boss,ChestMax };

    public GameObject[] ChestPrefabs;

    public Tilemap[] tiles;

    public Door[] door;

    public MonsterSpawnPoint[] spawnpoint;

    public List<GameObject> ChestList;

    [SerializeField]
    public LinkedData StageLinkedData;

    //��ũ�嵥���Ϳ� ���� ���� 2�� �̻��̸� ���� Ȯ���� �߰�ũ��, 3���̻��̸� ����Ȯ���� ū��, 4�� �̻��̸� ������ ū�� ��� �� ó���� �ؼ� ���������� ���� �������ش�.
    //public enum STAGECLASS { SMALL, MEDIUM, LARGE };

    public MapManager.ROOMTYPE type;

    public MapManager.ROOMCLASS sizeclass;


    public Transform StartPos;
    public Vector3Int StartIndex;
    public Transform EndPos;
    public Vector3Int EndInedex;
    //public Rect RoomArea;

    public GameObject playerobj;


    public MapManager.STAGE NowStage;

    public int NowFloor = 0;

    //
    public int StageNum = -1;

    //���� ������ �ƴ��� �濡 ������ ���͵��� ��� ��� ���� ���� Ǯ����.
    public bool RoomLocked;


    public Vector2 RoomSize;
    public Transform bottomleft;
    public Transform topright;
    public Rect RoomActiveArea;
    public Rect RoomArea;
    public Vector3 CenterPos = new Vector3(-3000f, -3000f);


    public int MaxX;
    public int MaxY;

    public int[] Roominfo;

    public bool RoomIsClear;

    public bool NowSpawned;
    [SerializeField]
    private bool isteleporter;
    [SerializeField]
    private bool nowplayerenter = false;
    [SerializeField]
    private bool issearched = false;

    public bool IsSearched
    {
        get
        {
            return issearched;
        }
        set
        {
            issearched = value;
        }
    }


    public bool NowPlayerEnter
    {
        get
        {
            return nowplayerenter;
        }
        set
        {
            nowplayerenter = value;
            if(nowplayerenter)
            {
                if(IsSearched==false)
                {
                    IsSearched = true;
                }
                Minimap.Instance.MinimapInfoSetting(Roominfo, MaxX, MaxY, bottomleft, topright, playerobj.transform);
            }
        }
    }

    public DungeonTeleporter Teleporter;


    public float UpdateSecond = 0.1f;
    private float mytime;


    public bool IsTeleporter
    {
        get
        {
            return isteleporter;
        }
        set
        {
            isteleporter = value;
            
            Teleporter.IsActive = isteleporter;
            //Teleporter.gameObject.SetActive(IsTeleporter);

        }
    }

    

    public void  LoadDoors()
    {
        door = new Door[(int)Door.DoorType.DoorMax];
        for (Door.DoorType i = Door.DoorType.Up; i < Door.DoorType.DoorMax; i++)
        {
            door[(int)i] = null;
        }

        Door[] temp = GetComponentsInChildren<Door>();
        foreach (var i in temp)
        {
            door[(int)i.type] = i;
            i.gameObject.SetActive(false);
        }
    }

    //�ڽ��� �Ʒ��� �ִ� ������ �޾ƿ´�.
    public void Initsetting()
    {
        //70%Ȯ���� �ڷ����� Ȱ��ȭ
        
        int rand = UnityEngine.Random.Range(0, 100);
        IsTeleporter = (rand <= 70);


        

        Vector2 temp1 = bottomleft.position + new Vector3(2, 1, 0);
        Vector2 temp2 = topright.position + new Vector3(-2, -1, 0);
        RoomActiveArea.x = temp1.x;
        RoomActiveArea.y = temp1.y;
        RoomActiveArea.width = temp2.x - temp1.x;
        RoomActiveArea.height = temp2.y - temp1.y;


        RoomArea.x = bottomleft.position.x;
        RoomArea.y = bottomleft.position.y;
        RoomArea.width = topright.position.x - bottomleft.position.x;
        RoomArea.height = topright.position.y - bottomleft.position.y;

        CenterPos = (topright.position - bottomleft.position) * 0.5f;
        

        //��ũ������ �ִµ� �ش� ��ġ�� ���� ������ 
        //�ش� ���� �ٸ� ���̶� ��ü �Ѵ�.
        //��ũ������ �ְ� �ش� ���⿡ ���� �����ϸ� ���� ����� �ش�.
        //���� Ȱ��ȭ �Ҷ� �������� ���� ����־� �ش�.
        if (StageLinkedData.RightMap!=null)
        {
            if(door[(int)Door.DoorType.Right]!=null)
            {
                door[(int)Door.DoorType.Right].gameObject.SetActive(true);
                door[(int)Door.DoorType.Right].CreateDoor(this.gameObject);
            }
        }
        if(StageLinkedData.LeftMap!=null)
        {
            if (door[(int)Door.DoorType.Left] != null)
            {
                door[(int)Door.DoorType.Left].gameObject.SetActive(true);
                door[(int)Door.DoorType.Left].CreateDoor(this.gameObject);
            }
        }
        if(StageLinkedData.UpMap!=null)
        {
            if (door[(int)Door.DoorType.Up] != null)
            {
                door[(int)Door.DoorType.Up].gameObject.SetActive(true);
                door[(int)Door.DoorType.Up].CreateDoor(this.gameObject);
            }
        }
        if(StageLinkedData.DownMap!=null)
        {
            if (door[(int)Door.DoorType.Down] != null)
            {
                door[(int)Door.DoorType.Down].gameObject.SetActive(true);
                door[(int)Door.DoorType.Down].CreateDoor(this.gameObject);
            }
        }

        if (type == MapManager.ROOMTYPE.Start)
        {
           
            StartPos = transform.Find($"{MapManager.Instance.NowStage}StartGate");
            
            if (playerobj != null)
            {
                playerobj.transform.position = StartPos.position;
                NowPlayerEnter = true;
            }
 
        }
        else if(type == MapManager.ROOMTYPE.End)
        {
            EndPos = transform.Find($"{MapManager.Instance.NowStage}EndGate");
           
        }

    }

    //������ ���鼭 
    public void LoadDoorInfo()
    {

    }

    public bool IsDoorExist(Door.DoorType type)
    {
        //bool flag = false;
        if (door[(int)type] == null)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    public void MonsterSetting()
    {

    }

    public void RoomAreaSetting()
    {

    }

    public void LoadMapInfo()
    {
        //Ray ray;
        RaycastHit2D[] hit;
        
        Vector3Int bottomleftindex = tiles[(int)TileElement.Wall].WorldToCell(bottomleft.position);
        //Debug.Log($"{name}���� �ε� ���� bottomleft = {bottomleft.position.x},{bottomleft.position.y},{bottomleft.position.z}");
        Vector3Int toprightindex = tiles[(int)TileElement.Wall].WorldToCell(topright.position);
        //Debug.Log($"{name}���� �ε� ���� topright = {topright.position.x},{topright.position.y},{topright.position.z}");
        int maxX = toprightindex.x - bottomleftindex.x + 1;
        MaxX = maxX;
        int maxY = toprightindex.y - bottomleftindex.y + 1;
        MaxY = maxY;
        Roominfo = new int[maxX * maxY];

        Vector3 temp = tiles[(int)TileElement.Wall].GetCellCenterWorld(bottomleftindex);

        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                temp.x = bottomleft.position.x + x;
                temp.y = bottomleft.position.y + y + 0.3f;
              

                hit = Physics2D.RaycastAll(temp, new Vector2(1, 1), 0f);



                Roominfo[x + (y * maxX)] = 0;
                foreach(var a in hit)
                {
                    if (a)
                    {
                        if (a.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        {
                            Roominfo[x + (y * maxX)] = (int)TileElement.Wall;

                          
                        }
                        else if (a.transform.gameObject.layer == LayerMask.NameToLayer("Moveable"))
                        {
                            Roominfo[x + (y * maxX)] = (int)TileElement.Moveable;
                            break;
                            
                        }
                        //if (a.transform.gameObject.layer == LayerMask.NameToLayer("Door"))
                        //{
                        //    Roominfo[x + (y * maxX)] = /*(int)TileElement.Door10;

                        //    GameObject obj = GameObject.Find("TestCircle");
                        //    GameObject copy = GameObject.Instantiate(obj);
                        //    copy.transform.position = a.point;
                        //    copy.name = "Door";
                        //    break;
                        //}

                    }
                }
               

            }
        }


    }

    
    public void CheckRoomState()
    {
        if(NowPlayerEnter)
        {
            if (mytime <= Time.time)
            {
                mytime = Time.time + UpdateSecond;
                //Debug.Log("����");

                //���� ���͵��� �������� �ʾҰų� ���� Ŭ������� �ʾ����� ĳ������ ������ �˻��Ѵ�.
                if (NowSpawned == false && RoomIsClear == false)
                {
                    //Debug.Log("����");
                    //�÷��̾ �� ���� ������ ������ �ش� ���� �ݰ� �ش���� ���͵��� ��ȯ�Ѵ�. ���� �̹� ���͵��� �� �׾��ų� ���� Ŭ���� �Ǿ� �ְų� ���Ͱ� ���� ���̸� �׳� �״�� �д�. 
                    if (RoomActiveArea.Contains(playerobj.transform.position))
                    //if (IsEnterPlayArea()) 
                    {
                        //Debug.Log("�÷��̿���������");
                        RoomStart();

                    }
                }

                //������ ������������ ���Ͱ� �� �׾������� �˻��Ѵ�.
                if (NowSpawned == true)
                {
                    if(this.type!=MapManager.ROOMTYPE.Boss)
                    {
                        if (SpawnManager.Instance.NowSpawndList.Count <= 0)
                        {
                            //������ ���� ���� ����
                            RoomClear();
                        }
                    }
                    else
                    {
                        if(BossManager.Instance.cur_Boss.isDie)
                        {
                            RoomClear();
                        }
                    }

                }


            }
        }
    }

    public void RoomStart()
    {
        
        if (spawnpoint.Length != 0 && RoomIsClear == false)
        {
            NowSpawned = true;
            //Debug.Log("�÷��̿���������");
            for (int i = 0; i < door.Length; i++)
            {
                if (door[i] != null)
                {
                    //Ȱ��ȭ �Ǿ��ִ� ������ �ݾ��ְ� ���͵��� �����Ѵ�.
                    if (door[i].gameObject.activeSelf)
                    {
                        door[i].NowDoorLocked = true;
                    }
                }
                
            }

            if (IsTeleporter)
            {
                //IsTeleporter = false;
                Teleporter.IsActive = false;
            }
            if (spawnpoint[0].type != SpawnManager.MonsterType.Boss)
            {
                for (int j = 0; j < spawnpoint.Length; j++)
                {
                    SpawnManager.Instance.Spawn(spawnpoint[j].type, spawnpoint[j].transform);
                    //MonsterCount++;
                }
            }
            else
            {
                BossManager.Instance.Boss_Start = true;
            }
            Enm_Hp_Bar_Manager.Instance.UpadateList();
            Minimap.Instance.CreateMonsterTile(SpawnManager.Instance.NowSpawndList);
        }
        if (spawnpoint.Length <= 0)
        {
            RoomIsClear = true;
        }

    }

    //�ٽ� �ڷ���Ʈ�� ������ Ȱ��ȭ �����ְ� ���� Ŭ������ ���� Ȯ���� ���ڸ� �������ش�.
    public void RoomClear()
    {
        if(!RoomIsClear)
        {
            RoomIsClear = true;
            for (int i = 0; i < door.Length; i++)
            {
                if (door[i] != null)
                {
                    //Ȱ��ȭ �Ǿ��ִ� ������ �ٽ� �����ش�.
                    if (door[i].gameObject.activeSelf)
                    {
                        door[i].NowDoorLocked = false;
                    }
                }
            }
            if (IsTeleporter)
            {
                Teleporter.IsActive = true;
            }
            ChestSpawn();
        }
        
    }


    public void ChestSpawn()
    {
        int per; //�ش� �濡 ������ Ȯ��
        int rnd;
        int Crnd;
        int[] CPer = new int[(int)Chests.ChestMax];
        for(int i=0;i<CPer.Length;i++)
        {
            CPer[i] = 0;
        }

        if(type!=MapManager.ROOMTYPE.Boss)
        {
            if (sizeclass == MapManager.ROOMCLASS.SMALL)
            {
                per = 20;
                CPer[(int)Chests.Stash] = 85;
                CPer[(int)Chests.Rare] = 15;
                CPer[(int)Chests.Yellow] = 5;
            }
            else if (sizeclass == MapManager.ROOMCLASS.LARGE)
            {
                per = 60;
                CPer[(int)Chests.Stash] = 70;
                CPer[(int)Chests.Rare] = 20;
                CPer[(int)Chests.Yellow] = 10;
                CPer[(int)Chests.Gold] = 15;
            }
            else
            {
                per = 100;
                CPer[(int)Chests.Stash] = 40;
                CPer[(int)Chests.Rare] = 35;
                CPer[(int)Chests.Yellow] = 25;
                CPer[(int)Chests.Gold] = 35;
            }


            rnd = UnityEngine.Random.Range(0, 100);

            if (rnd <= per)
            {
                Crnd = UnityEngine.Random.Range(0, 100);
                for (int i = (int)Chests.Yellow; i >= (int)Chests.Stash; i--)
                {
                    if(Crnd<=CPer[i])
                    {
                        GameObject obj = GameObject.Instantiate(ChestPrefabs[i]);
                        obj.transform.position = spawnpoint[spawnpoint.Length - 1].transform.position;
                        ChestList.Add(obj);
                        break;
                    }
                }
                if(Crnd<=CPer[(int)Chests.Gold])
                {
                    GameObject obj = GameObject.Instantiate(ChestPrefabs[(int)Chests.Gold]);
                    obj.transform.position = spawnpoint[spawnpoint.Length - 1].transform.position + new Vector3(1, 0, 0);
                    ChestList.Add(obj);
                }

            }
        }
        else
        {
            GameObject obj = GameObject.Instantiate(ChestPrefabs[(int)Chests.Boss]);
            obj.transform.position = spawnpoint[spawnpoint.Length - 1].transform.position;
            ChestList.Add(obj);
        }
    }

    public bool IsEnterPlayArea()
    {
        bool flag = false;
        Vector3 pos = playerobj.transform.position;
        if (pos.x >= RoomActiveArea.xMin && pos.x <= RoomActiveArea.xMax) 
        {
            if (pos.y <= RoomActiveArea.yMin && pos.y >= RoomActiveArea.yMax) 
            {
                flag = true;
            }
        }
        return flag;
    }




    private void Start()
    {
        //Initsetting();
        

    }

    private void Awake()
    {
        playerobj = GameObject.Find("Player");
        Teleporter = GetComponentInChildren<DungeonTeleporter>();
        bottomleft = transform.Find("BottomLeft");
        topright = transform.Find("TopRight");
        tiles = new Tilemap[(int)TileElement.ElementMax];
        //ChestPrefabs = (GameObject[])Resources.LoadAll("Prefabs/Ui_Prefabs/Inventory/Tresure");
        Tilemap[] temp = GetComponentsInChildren<Tilemap>();
        foreach (var a in temp)
        {
            if (a.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                tiles[(int)TileElement.Wall] = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("BackGround"))
            {
                tiles[(int)TileElement.BackGround] = a;
            }
            else if (a.gameObject.layer == LayerMask.NameToLayer("Moveable"))
            {
                tiles[(int)TileElement.Moveable] = a;
            }
        }
        spawnpoint = GetComponentsInChildren<MonsterSpawnPoint>();
        LoadMapInfo();
        LoadDoors();
    }

    int count = 0;
    private void Update()
    {
        CheckRoomState();
        if(ChestList.Count>0)
        {
            for(int i=0;i<ChestList.Count;i++)
            {
                if (ChestList[i].GetComponent<GoldTresure>().Open)
                {
                    count++;  
                }
                else
                {
                    
                    break;
                }
            }
            if(count>=ChestList.Count)
            {
                for(int i=0;i<ChestList.Count;i++)
                {
                    Destroy(ChestList[i].gameObject, 5f);
                }
                ChestList.Clear();
            }
        }
    }
}
