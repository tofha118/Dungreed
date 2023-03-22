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

        [Range(0.00f, 1.00f), Tooltip("�� ���� �ڷ����� ���� Ȯ��")]
        public float teleporterPercent;
        [Range(0.00f, 1.00f), Tooltip("�� ���� �����ڽ� ���� Ȯ��")]
        public float BronzeChestPercent;
        [Range(0.00f, 1.00f), Tooltip("�� ���� �ǹ��ڽ� ���� Ȯ��")]
        public float SilverChestPercent;
        [Range(0.00f, 1.00f), Tooltip("�� ���� ���ڽ� ���� Ȯ��")]
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

    //�ʸŴ����� ������ ������ �ش��ϴ� ���� �޾ƿ´�.
    public void StageLoad(StageData[] arr, int size)
    {

    }


    //����� Ư������� �ƴ� �Ϲݹ���� ã�Ƽ� ���߿� �ϳ��� ���������� ���Ѵ�.
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
    //������ġ, �������, ����, ����, ������ ���� ���� ����� �ش�.
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

                    Debug.Log($"{current.Maplist[i].Num}��°�� ���۹�");
                    //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                }
                //else if (current.Maplist[i].Num == option.MaxNum - 1)//������ ���� �������� �Ѵ�.
                else if (current.Maplist[i].Num == current.NowCount - 1)//������ ���� �������� �Ѵ�.
                {
                    /**/
                    GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.End);
                    current.MapObjList[i] = GameObject.Instantiate(obj);
                    /**/

                    Debug.Log($"{current.Maplist[i].Num}��°�� ����");
                    //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                }
                else//0����� ������ ���� ����� ����� ���� ������ ���� ���� Ŭ������ �����ش�.
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
                    //�ֺ��� �����ϴ� ���� ������ ������ ���� ũ�⸦ �����Ѵ�.

                    //100% ū��
                    if (count <= 2)//���� �ϳ� �Ǵ� �ΰ��� ���� ������ ������ �̸鼭 ������, �������� ������ ������ �������� �ȴ�.
                    {
                        int rnd = UnityEngine.Random.Range(0, 100);
                        if (current.RestaurantCount < option.MaxRestaurant)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Restaurant);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            current.RestaurantCount++;
                            Debug.Log($"{current.Maplist[i].Num}��°�� �������");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                        }
                        else if (current.ShopCount < option.MaxShop)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.Shop);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            current.ShopCount++;
                            Debug.Log($"{current.Maplist[i].Num}��°�� ����");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                        }
                        else
                        {

                            if (rnd < 70)
                            {
                                /**/
                                GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.SMALL);
                                current.MapObjList[i] = GameObject.Instantiate(obj);
                                /**/

                                

                                Debug.Log($"{current.Maplist[i].Num}��°�� ������");
                                //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                            }
                            else
                            {
                                /**/
                                GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.MEDIUM);
                                current.MapObjList[i] = GameObject.Instantiate(obj);
                                /**/

                                Debug.Log($"{current.Maplist[i].Num}��°�� �߰���");
                                //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                            }

                        }
                    }
                    else if (count <= 3)//70%�߰���,30%ū��
                    {
                        int rnd = UnityEngine.Random.Range(0, 100);
                        if (rnd < 70)
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.MEDIUM);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/



                            Debug.Log($"{current.Maplist[i].Num}��°�� �߰���");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                        }
                        else
                        {
                            /**/
                            GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.LARGE);
                            current.MapObjList[i] = GameObject.Instantiate(obj);
                            /**/

                            Debug.Log($"{current.Maplist[i].Num}��°�� ū��");
                            //current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                        }
                    }
                    else if (count <= 4)//���� �ΰ��ִ¹浵 ��������� �������� ������ ��ȣ�� ���� ���� ��������� ���������� ������.
                    {
                        /**/
                        GameObject obj = mapmanager.StageLoad(MapManager.ROOMTYPE.NOMAL, MapManager.ROOMCLASS.LARGE);
                        current.MapObjList[i] = GameObject.Instantiate(obj);
                        /**/

                        Debug.Log($"{current.Maplist[i].Num}��°�� ū��");
                        // current.MapPrefabsList[i].GetComponent<BaseStage>().StageLinkedData = current.Maplist[i];//����� ��ũ������ �������� ����ش�.
                    }
                }
            }

            if (current.MapObjList[i] != null)
            {
                for (int a = 0; a < (int)Door.DoorType.DoorMax; a++)
                {
                    //���� ���� �ؾ� �ϴµ� ������ ���� �ش���ġ�� ���� ���� ���̸� �ٽṵ̂� �Ѵ�.
                    if (dir[a])
                    {
                        if (current.MapObjList[i].GetComponent<BaseStage>().door[a] == null)
                        {
                            flag = true;
                            Debug.Log($"{current.MapObjList[i].name}����� �� ����");
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

            //������ ����Ʈ�� ���� ������ �ֺ��� �ִ� ����� �˻��ؼ� ��ũ�����͸� �־��ش�.
            if (current.MapObjList[i] != null)
            {
                LinkedData data = SetLinkingData(i);
                current.MapObjList[i].GetComponent<BaseStage>().StageLinkedData = data;
                //obj.transform.position = new Vector3(transform.position.x + (x * interval), transform.position.y + ((y * interval) * -1));
                Debug.Log($"{i}���� ��ũ����");
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

        //linkeddata���� ��������� ����Ǿ� �ִ� �� Ȯ���ϰ�
        //prefabs���� �ش���⿡ ���� ���� ������� �ִ��� Ȯ���ϰ�
        //���� �����Ѵ�.

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


    //�����ʿ��� ���� ������ �ؼ� �� �迭�� ����� �ְ� �ش� �迭�� �� �Ŵ����� �Ѱ��ش�.
    //public int MapSpawn(int x, int y, StageData Parent)
    public Vector2Int MapSpawn(int x, int y, StageData Parent)
    {
        if (current.NowCount >= option.MaxNum)
        {
            //Debug.Log($"���� �������� ����");
            //return option.MaxNum;
            return new Vector2Int(x, y);
        }

        int RandNum;
        int yval = option.MaxListSize;
        //������ �ش� ��ġ�� ���� ����� 
        //������, ����, �� ũ��, ���� ���� ���� �����Ѵ�.
        //�������� �־�� �ϸ� �����浵 ����
        if (current.Maplist[x + (y * yval)] == null)
        {

            current.Maplist[x + (y * yval)] = new StageData();
            Debug.Log($"[{x},{y}] {x + (y * yval)}����");
            current.Maplist[x + (y * yval)].InitSttting(current.NowCount, x, y);
            current.NowCount++;

            if(Parent!=null)
            {
                if (Parent.indexX == x)
                {
                    if(Parent.indexY>y)//�Ʒ��ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].DownMap = Parent;
                        Parent.UpMap = current.Maplist[x + (y * yval)];
                    }
                    else//���ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].UpMap = Parent;
                        Parent.DownMap = current.Maplist[x + (y * yval)];

                        
                    }
                }
                else if (Parent.indexY == y)
                {
                    if(Parent.indexX>x)//�����ʰ� ����
                    {
                        current.Maplist[x + (y * yval)].RightMap = Parent;
                        Parent.LeftMap = current.Maplist[x + (y * yval)];
                    }
                    else//���ʰ� ����
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

        //õ��° ���� �׻� ���������ΰ���.
        if (current.NowCount == 1)
        {
            MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)]);
            //return option.MaxNum;
            return new Vector2Int(x + 1, y);
        }

        //����
        RandNum = UnityEngine.Random.Range(0, 100);
        //Debug.Log($"����{RandNum}");
        if (RandNum <= 70)
        {
            if (x - 1 >= 1 && current.Maplist[(x - 1) + (y * yval)] == null)
            {
                MapSpawn(x - 1, y, current.Maplist[(x) + (y * yval)]);
            }
        }

        //������
        RandNum = UnityEngine.Random.Range(0, 100);

        if (RandNum <= 70)
        {
            if (x + 1 <= option.MaxListSize - 1 && current.Maplist[(x + 1) + (y * yval)] == null)
            {
                MapSpawn(x + 1, y, current.Maplist[(x) + (y * yval)]);
            }
        }

        //����
        RandNum = UnityEngine.Random.Range(0, 100);
        if (RandNum <= 70)
        {
            if (y - 1 >= 0 && current.Maplist[x + ((y - 1) * yval)] == null)
            {
                MapSpawn(x, y - 1, current.Maplist[(x) + (y * yval)]);
            }
        }

        //�Ʒ���
        RandNum = UnityEngine.Random.Range(0, 100);
        if (RandNum <= 70)
        {
            if (y + 1 <= option.MaxListSize - 1 && current.Maplist[x + ((y + 1) * yval)] == null)
            {
                MapSpawn(x, y + 1, current.Maplist[(x) + (y * yval)]);
            }
        }
        //Debug.Log($"������� ������ ����");
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



    //�ʽ����ϰ� ������ ���� �� �Ŵ������� �Ѱ��ش�.
    public void SpawnStart(int NowFloor)
    {
        current.NowCount = 0;
        //������ ������Ű����� �����.
        //
        Vector2Int lastindex = MapSpawn(0, 2, null);

        //���� ��������������� ���������� �ּ� ������ŭ ����� �ش�.
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
        //���� ����� ���� ��ü�ʵ� �����
        //Debug.Log("�� �ֿ��ش�111111111111111111111111111");
        DungeonMapUI.Instance.Body.SetRoomTiles(current.MapObjList, option.MaxListSize);

        Debug.Log($"������� ���� ����{current.NowCount}");
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
