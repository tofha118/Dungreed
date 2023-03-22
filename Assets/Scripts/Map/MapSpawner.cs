using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapSpawner : Singleton<MapSpawner>
{
    public enum DIRECTION { NODATA = -1, UP, RIGHT, DOWN, LEFT, MAX };

    public Dictionary<DIRECTION, Vector2Int> dirIndex = new Dictionary<DIRECTION, Vector2Int>()
    {
        { DIRECTION.UP,new Vector2Int(0,1)},
        { DIRECTION.RIGHT,new Vector2Int(1,0)},
        { DIRECTION.DOWN,new Vector2Int(0,-1)},
        { DIRECTION.LEFT,new Vector2Int(-1,0)},
    };

    [Serializable]
    public class SpawnOption
    {
        public int MaxNum;
        public int MinNum;
        public int MaxRestaurant;
        public int MaxShop;


        //public List<GameObject> MapPrefabs;

        [Range(0.00f, 1.00f), Tooltip("각 방의 텔레포터 스폰 확률")]
        public float teleporterPercent;
        [Range(0.00f, 1.00f), Tooltip("각 방의 브론즈박스 스폰 확률")]
        public float BronzeChestPercent;
        [Range(0.00f, 1.00f), Tooltip("각 방의 실버박스 스폰 확률")]
        public float SilverChestPercent;
        [Range(0.00f, 1.00f), Tooltip("각 방의 골드박스 스폰 확률")]
        public float GoldChestPercent;

        public int MaxListSize = 5;
    }

    [Serializable]
    public class CurrentValue
    {
        public int NowCount;
        public int RestaurantCount;
        public int ShopCount;
        [SerializeField]
        public StageData[] Maplist;
        //public GameObject[] MapPrefabsList;
        public List<GameObject> CurrentRoomObjs;
        public GameObject[] MapObjList;
    }

    [SerializeField]
    public SpawnOption option = new SpawnOption();
    [SerializeField]
    public CurrentValue current = new CurrentValue();

    public MapManager mapmanager;




    public void InitSetting()
    {
        
        current.Maplist = new StageData[option.MaxListSize * option.MaxListSize];
        //current.MapPrefabsList = new GameObject[option.MaxListSize * option.MaxListSize];
        current.MapObjList = new GameObject[option.MaxListSize * option.MaxListSize];
        current.CurrentRoomObjs = new List<GameObject>();
        for (int i = 0; i < option.MaxListSize * option.MaxListSize - 1; i++)
        {
            current.Maplist[i] = null;
            //current.MapPrefabsList[i] = null;
            current.MapObjList[i] = null;
        }
    }

    //맵매니저에 정보를 보내서 해당하는 맵을 받아온다.
    public void StageLoad(StageData[] arr, int size)
    {

    }


    //방들중 특수방들이 아닌 일반방들을 찾아서 그중에 하나를 보스방으로 정한다.
    public void SetBossRoom(MapManager.STAGE stage)
    {
        int size = option.MaxListSize;
        List<int> list = new List<int>();
        for (int i = 0; i < size * size; i++)
        {
            if(current.MapObjList[i]!=null)
            {
                BaseStage stagescript = current.MapObjList[i].GetComponent<BaseStage>();
                if(stagescript.type== MapManager.ROOMTYPE.NOMAL)
                {
                    list.Add(i);
                }
            }
        }
        Debug.Log("boss1");
        int rnd = UnityEngine.Random.Range(0, list.Count);
        int index = list[rnd];
        Debug.Log("boss2");
        //LinkedData linkdata = current.MapObjList[index].GetComponent<BaseStage>().StageLinkedData;

        GameObject.Destroy(current.MapObjList[index]);
        Debug.Log("boss3");
        //current.MapObjList[index] = mapmanager.StageLoad(MapManager.ROOMTYPE.Boss);

        GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Boss);
        Debug.Log("boss4");
        current.MapObjList[index] = GameObject.Instantiate(obj);
        Debug.Log("boss5");
        current.MapObjList[index].GetComponent<BaseStage>().StageLinkedData = SetLinkingData(index);
        Debug.Log("boss6");
    }


    int co = 0;
    //시작위치, 레스토랑, 상점, 제단, 보스방 등의 방을 만들어 준다.
    public void StageSetting()
    {
        bool[] dir = new bool[(int)Door.DoorType.DoorMax];
        //direction.Initialize();
        bool flag = false;
        int size = option.MaxListSize;
        for (int i = 0; i < size * size; i++)
        {
            int count = 0;
            flag = false;

            for (int b = 0; b < dir.Length; b++)
            {
                dir[b] = false;
            }

            if (current.Maplist[i]!=null)
            {
                if (current.Maplist[i].Num == 0)
                {
                    /**/
                    GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Start);
                    current.MapObjList[i] = GameObject.Instantiate(obj);
                    /**/

                    Debug.Log($"{current.Maplist[i].Num}번째방 시작방");
                    //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                }
                //else if (current.Maplist[i].Num == option.MaxNum - 1)//마지막 방은 끝방으로 한다.
                else if (current.Maplist[i].Num == current.NowCount - 1)//마지막 방은 끝방으로 한다.
                {
                    /**/
                    GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.End);
                    current.MapObjList[i] = GameObject.Instantiate(obj);
                    /**/

                    Debug.Log($"{current.Maplist[i].Num}번째방 끝방");
                    //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                }
                else//0번방과 마지막 방을 빼고는 연결된 방의 개수에 따라 방의 클래스를 정해준다.
                {
                    if (current.Maplist[i].RightMap != null)
                    {
                        dir[(int)Door.DoorType.Right] = true;
                        count++;
                    }
                    if (current.Maplist[i].LeftMap != null)
                    {
                        dir[(int)Door.DoorType.Left] = true;
                        count++;
                    }
                    if (current.Maplist[i].UpMap != null)
                    {
                        dir[(int)Door.DoorType.Up] = true;
                        count++;
                    }
                    if (current.Maplist[i].DownMap != null)
                    {
                        dir[(int)Door.DoorType.Down] = true;
                        count++;
                    }
                    //주변에 존재하는 방의 개수를 가지고 방의 크기를 결정한다.

                    //100% 큰방
                    if (count <= 2)//길이 하나 또는 두개인 방은 무조건 작은방 이면서 상점과, 음식점이 없으면 상점과 음식점이 된다.
                    {
                        int rnd = UnityEngine.Random.Range(0, 100);
                        if (current.RestaurantCount < option.MaxRestaurant)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Restaurant);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            current.RestaurantCount++;
                            Debug.Log($"{current.Maplist[i].Num}번째방 레스토랑");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                        }
                        else if (current.ShopCount < option.MaxShop)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Shop);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            current.ShopCount++;
                            Debug.Log($"{current.Maplist[i].Num}번째방 상점");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                        }
                        else
                        {

                            if (rnd < 70)
                            {
                                /**/
                                GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.SMALL);
                                current.MapObjList[i] = GameObject.Instantiate(obj);
                                /**/

                                

                                Debug.Log($"{current.Maplist[i].Num}번째방 작은방");
                                //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                            }
                            else
                            {
                                /**/
                                GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.MEDIUM);
                                current.MapObjList[i] = GameObject.Instantiate(obj);
                                /**/

                                Debug.Log($"{current.Maplist[i].Num}번째방 중간방");
                                //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                            }

                        }
                    }
                    else if (count <= 3)//70%중간방,30%큰방
                    {
                        int rnd = UnityEngine.Random.Range(0, 100);
                        if (rnd < 70)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.MEDIUM);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/



                            Debug.Log($"{current.Maplist[i].Num}번째방 중간방");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                        }
                        else
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.LARGE);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            Debug.Log($"{current.Maplist[i].Num}번째방 큰방");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                        }
                    }
                    else if (count <= 4)//길이 두개있는방도 레스토랑과 상점방이 없으면 번호가 빠른 방이 레스토랑과 상점방으로 뽑힌다.
                    {
                        /**/
                        GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.LARGE);
                        current.MapObjList[i] = GameObject.Instantiate(obj);
                        /**/

                        Debug.Log($"{current.Maplist[i].Num}번째방 큰방");
                        // current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//방들의 링크정보를 각각에게 들려준다.
                    }
                }
            }

            if (current.MapObjList[i] != null)
            {
                for (int a = 0; a < (int)Door.DoorType.DoorMax; a++)
                {
                    //문이 존재 해야 하는데 생성된 맵이 해당위치에 문이 없는 방이면 다시뽑게 한다.
                    if (dir[a])
                    {
                        if (current.MapObjList[i].GetComponent<BaseStage>().door[a] == null)
                        {
                            flag = true;
                            Debug.Log($"{current.MapObjList[i].name}번재방 문 오류");
                            GameObject.Destroy(current.MapObjList[i]);
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                i--;
                continue;
            }

            //프리팹 리스트에 값이 들어갔으면 주변에 있는 방들을 검사해서 링크데이터를 넣어준다.
            if (current.MapObjList[i] != null)
            {
                LinkedData data = SetLinkingData(i);
                current.MapObjList[i].GetComponent<BaseStage>().StageLinkedData = data;
                //obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));
                Debug.Log($"{i}번방 링크세팅");
            }

        }//for

    }//function

    public LinkedData SetLinkingData(int index)
    {
        LinkedData linkeddata = new LinkedData();
        int yval = option.MaxListSize;
        int x = index % yval;
        int y = index / yval;
        linkeddata.Num = current.Maplist[index].Num;

        //linkeddata에서 어느방향이 연결되어 있는 지 확인하고
        //prefabs에서 해당방향에 방이 현재 만들어져 있는지 확인하고
        //서로 연결한다.

        if (current.Maplist[index].LeftMap!=null)
        {
            if(current.MapObjList[(x - 1) + (y * yval)] != null)
            {
                linkeddata.LeftMap = current.MapObjList[(x - 1) + (y * yval)];
                if (current.MapObjList[(x - 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[(x - 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData.RightMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if(current.Maplist[index].RightMap!=null)
        {
            if (current.MapObjList[(x + 1) + (y * yval)] != null)
            {
                linkeddata.RightMap = current.MapObjList[(x + 1) + (y * yval)];

                if (current.MapObjList[(x + 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[(x + 1) + (y * yval)].GetComponent<BaseStage>().StageLinkedData.LeftMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if(current.Maplist[index].UpMap!=null)
        {
            if(current.MapObjList[x + ((y - 1) * yval)] !=null)
            {
                linkeddata.UpMap = current.MapObjList[x + ((y - 1) * yval)];

                if (current.MapObjList[x + ((y - 1) * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[x + ((y - 1) * yval)].GetComponent<BaseStage>().StageLinkedData.DownMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
        if(current.Maplist[index].DownMap!=null)
        {
            if(current.MapObjList[x + ((y + 1) * yval)] !=null)
            {
                linkeddata.DownMap = current.MapObjList[x + ((y + 1) * yval)];

                if (current.MapObjList[x + ((y + 1) * yval)].GetComponent<BaseStage>().StageLinkedData != null)
                {
                    current.MapObjList[x + ((y + 1) * yval)].GetComponent<BaseStage>().StageLinkedData.UpMap = current.MapObjList[(x) + (y * yval)];
                }
            }
        }
              
        return linkeddata;
    }


    //스포너에서 맵을 생성을 해서 맵 배열을 만들어 주고 해당 배열을 맵 매니저에 넘겨준다.
    //public int MapSpawn(int x, int y, StageData Parent)
    public Vector2Int MapSpawn(int x, int y, StageData Parent)
    {
        if (current.NowCount >= option.MaxNum)
        {
            //Debug.Log($"전부 끝났을때 리턴");
            //return option.MaxNum;
            return new Vector2Int(x, y);
        }

        int RandNum;
        int yval = option.MaxListSize;
        //들어오면 해당 위치에 방을 만들고 
        //음식점, 상점, 방 크기, 상자 스폰 등을 결정한다.
        //보스전이 있어야 하면 보스방도 스폰
        if (current.Maplist[x + (y * yval)] == null)
        {

            current.Maplist[x + (y * yval)] = new StageData();
            Debug.Log($"[{x},{y}] {x + (y * yval)}생성");
            current.Maplist[x + (y * yval)].InitSttting(current.NowCount, x, y);
            current.NowCount++;

            if(Parent!=null)
            {
                if (Parent.indexX == x)
                {
                    if(Parent.indexY>y)//아래쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].DownMap = Parent;
                        Parent.UpMap = current.Maplist[x + (y * yval)];
                    }
                    else//위쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].UpMap = Parent;
                        Parent.DownMap = current.Maplist[x + (y * yval)];

                        
                    }
                }
                else if (Parent.indexY == y)
                {
                    if(Parent.indexX>x)//오른쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].RightMap = Parent;
                        Parent.LeftMap = current.Maplist[x + (y * yval)];
                    }
                    else//왼쪽과 연결
                    {
                        current.Maplist[x + (y * yval)].LeftMap = Parent;
                        Parent.RightMap = current.Maplist[x + (y * yval)];
                    }
                }
            }

        }
        else
        {
            //return option.MaxNum;
            return new Vector2Int(x, y);
        }

        //천번째 방은 항상 오른쪽으로간다.
        if (current.NowCount == 1)
        {
            MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)]);
            //return option.MaxNum;
            return new Vector2Int(x + 1, y);
        }

        //왼쪽
        RandNum = UnityEngine.Random.Range(0, 100);
        //Debug.Log($"랜덤{RandNum}");
        if (RandNum <= 70)
        {
            if (x - 1 >= 1 && current.Maplist[(x - 1) + (y * yval)] == null)
            {
                MapSpawn(x - 1, y, current.Maplist[(x) + (y * yval)]);
            }
        }

        //오른쪽
        RandNum = UnityEngine.Random.Range(0, 100);

        if (RandNum <= 70)
        {
            if (x + 1 <= option.MaxListSize - 1 && current.Maplist[(x + 1) + (y * yval)] == null)
            {
                MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)]);
            }
        }

        //위쪽
        RandNum = UnityEngine.Random.Range(0, 100);
        if (RandNum <= 70)
        {
            if (y - 1 >= 0 && current.Maplist[x + ((y - 1) * yval)] == null)
            {
                MapSpawn(x, y - 1, current.Maplist[(x) + (y * yval)]);
            }
        }

        //아래쪽
        RandNum = UnityEngine.Random.Range(0, 100);
        if (RandNum <= 70)
        {
            if (y + 1 <= option.MaxListSize - 1 && current.Maplist[x + ((y + 1) * yval)] == null)
            {
                MapSpawn(x, y + 1, current.Maplist[(x) + (y * yval)]);
            }
        }
        //Debug.Log($"만들곳이 없을때 리턴");
        //return current.NowCount;
        return new Vector2Int(x, y);
    }


    public void CreateStageData(int x, int y)
    {
        int yval = option.MaxListSize;
        if (current.Maplist[x + (y * yval)] == null)
        {

        }
    }



    //맵스폰하고 스폰된 맵을 맵 매니저한테 넘겨준다.
    public void SpawnStart(int NowFloor)
    {
        current.NowCount = 0;
        //층수를 증가시키고맵을 만든다.
        //
        Vector2Int lastindex = MapSpawn(0, 2, null);

        //방이 덜만들어졌을때는 강제적으로 최소 개수만큼 만들어 준다.
        if(current.NowCount<option.MinNum)
        {
            
        }

        StageSetting();
        //Debug.Log("1");
        if (mapmanager.NowFloor % 2 == 0)
        {
            //SetBossRoom(MapManager.Instance.NowStage);
            SetBossRoom(mapmanager.NowStage);
        }
        //Debug.Log("2");

        //if (MapManager.Instance.NowFloor % 2 == 0)


        // Debug.Log("2");
        //MapManager.Instance.StageSetting(current.MapObjList, option.MaxListSize);


        //Debug.Log("3");
        ShowMaps();

        mapmanager.StageSetting(current.MapObjList, option.MaxListSize);
        //current.NowCount

        //Debug.Log("4");
        //맵이 만들어 지면 전체맵도 만든다
        //Debug.Log("맵 넣우준다111111111111111111111111111");
        DungeonMapUI.Instance.Body.SetRoomTiles(current.MapObjList, option.MaxListSize);

        Debug.Log($"만들어진 방의 개수{current.NowCount}");
    }

    public void ShowMaps()
    {
        int interval = 70;
        int yval = option.MaxListSize;

        for (int y = 0; y < option.MaxListSize; y++)
        {
            for (int x = 0; x < option.MaxListSize; x++)
            {
                if(current.MapObjList[x + (y * yval)]!=null)
                {
                    //GameObject obj = GameObject.Instantiate(current.MapPrefabsList[x + (y * yval)]);
                    GameObject obj = current.MapObjList[x + (y * yval)];
                    obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));
                    //Debug.Log("1");

                    //LinkedData data = SetLinkingData(x + (y * yval));
                    //obj.GetComponent<BaseStage>().StageLinkedData = data;
                    
                    
                    //Debug.Log("2");
                    int num= obj.GetComponent<BaseStage>().StageLinkedData.Num;
                    //Debug.Log("3");
                    //obj.name = current.MapPrefabsList[x + (y * yval)].name /*+ $"_{num}"*/;
                    obj.name = $"Room_{num}";
                    //Debug.Log("4");

                    obj.GetComponent<BaseStage>().StageNum = num;

                    //obj.GetComponent<BaseStage>().Initsetting();
                }
            }
        }
        
    }


    private void Awake()
    {
        InitSetting();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnStart(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
