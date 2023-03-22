using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    //private static MapManager _instance = null;

    //public static MapManager Instance
    //{
    //    get
    //    {
                
    //        return _instance;
    //    }
    //}

    public enum STAGE { Stage1, Stage2, Stage3, Stage4 ,StageMax };
    public enum ROOMTYPE { Start, Restaurant, Shop, End, Boss, NOMAL, MAX };
    public enum ROOMCLASS { SMALL, MEDIUM, LARGE };

    //public enum 
    public StageData[] LinkedData = null;
    //public int arrsize;

    public GameObject[] StageArr = null;
    public int arrsize;

    [SerializeField]
    private STAGE nowstage = STAGE.Stage1;
    [SerializeField]
    private int nowfloor = 0;

    public GameObject[] SpecialRoom;
    public GameObject startRoom;
    public GameObject[] largeroom;
    public GameObject[] mediumroom;
    public GameObject[] smallroom;
    public GameObject EndRoom;


    public int NowFloor
    {
        get
        {
            return nowfloor;
        }
        set
        {
            nowfloor = value;
            if (nowfloor % 2 != 0 && nowfloor != 1)
            {
                NowStage = NowStage + 1;
            }
        }
    }


    public STAGE NowStage
    {
        get 
        {
            return nowstage;
        }
        set
        {
            nowstage = value;
            LoadStagesToPrefabs(value);
        }
    }    





    public void LoadStagesToPrefabs(STAGE stage)
    {
        largeroom = Resources.LoadAll<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Large");
        mediumroom = Resources.LoadAll<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Medium");
        smallroom = Resources.LoadAll<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Small");

        SpecialRoom[(int)ROOMTYPE.Start]= Resources.Load<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Start/Stage1_Start");
        SpecialRoom[(int)ROOMTYPE.Shop] = Resources.Load<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Shop/Stage1_Shop");
        SpecialRoom[(int)ROOMTYPE.Restaurant] = Resources.Load<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/Restaurant/Stage1_Restaurant");
        SpecialRoom[(int)ROOMTYPE.End] = Resources.Load<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/End/Stage1_End");
        SpecialRoom[(int)ROOMTYPE.Boss] = Resources.Load<GameObject>($"Prefabs/Map_Prefabs/MapPrefabs/{stage.ToString()}/{ROOMTYPE.Boss.ToString()}/Stage1_Boss");

    }



    public void StageSetting(GameObject[] rooms, int size)
    {
        StageArr = rooms;
        arrsize = size;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if(rooms[x + (y * size)]!=null)
                {
                    rooms[x + (y * size)]?.GetComponent<BaseStage>().Initsetting();
                    //rooms[x + (y * size)].transform.position = new Vector3(transform.position.x + (x * 70), transform.position.y + ((y * 70) * -1));
                }
                
            }
        }
    }

    public void StageSetting(List<GameObject> list)
    {

    }

    //스테이지 세팅이 끝나면 시작
    public void StageStart()
    {

    }

    public void SetBossRoom()
    {

    }

    public void TeleportRoom(int x,int y)
    {

    }

    public GameObject StageLoad(ROOMTYPE type)
    {
        return SpecialRoom[(int)type];
    }

    public GameObject StageLoad(ROOMTYPE type, ROOMCLASS roomclass)
    {
        //랜덤으로 뽑아서 하나를 넘겨준다.
        int count = 0;
        if (roomclass == ROOMCLASS.SMALL) count = smallroom.Length;
        else if (roomclass == ROOMCLASS.MEDIUM) count = mediumroom.Length;
        else if (roomclass == ROOMCLASS.LARGE) count = largeroom.Length;

        int rnd = Random.Range(0, count);

        if (roomclass == ROOMCLASS.SMALL) return smallroom[rnd];
        else if (roomclass == ROOMCLASS.MEDIUM) return mediumroom[rnd];
        else return largeroom[rnd];

    }


    //public void StageLoad(StageData[] arr, int size)
    //{
    //    for (int i = 0; i < size * size; i++)
    //    {
    //        int count = 0;
    //        if (arr[i] != null)
    //        {
    //            if (arr[i].RightMap != null) count++;
    //            if (arr[i].LeftMap != null) count++;
    //            if (arr[i].UpMap != null) count++;
    //            if (arr[i].DownMap != null) count++;
    //            //주변에 존재하는 방의 개수를 가지고 방의 크기를 결정한다.

    //            //100% 큰방
    //            if (count >= 4)
    //            {

    //            }
    //            else if (count >= 3)//70%중간방,30%큰방
    //            {

    //            }
    //            else if (count >= 2)//길이 두개있는방도 레스토랑과 상점방이 없으면 번호가 빠른 방이 레스토랑과 상점방으로 뽑힌다.
    //            {

    //            }
    //            else//길이 하나있는방은 레스토랑 또는 상점방이 된다.
    //            {

    //            }

    //        }

    //    }
    //}

    public void InitSetting()
    {
        SpecialRoom = new GameObject[(int)ROOMTYPE.MAX];
    }
    public void InitSetting(StageData[] arr, int size)
    {
        LinkedData = arr;
        arrsize = size;
        for(int i=0;i<size*size;i++)
        {
            if (arr[i] != null)
            {

            }
        }





    }

    //현재 만들어져 있는  스테이지를 모두 없애주고 새로운 방들을 만들어 준다.
    public void NextStage()
    {
        DestroyRooms(StageArr);
        NowFloor++;
        MapSpawner.Instance.SpawnStart(NowFloor);
    }

    //오브젝트 풀링적용 예정
    public void DestroyRooms(GameObject[] stage)
    {
        for (int i = 0; i < stage.Length; i++)
        {
            GameObject.Destroy(stage[i]);
        }
    }

    private void Awake()
    {
        InitSetting();
        NowStage = STAGE.Stage1;
        NowFloor = 1;
        //if(_instance==null)
        //{
        //    _instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        
        //LoadStagesToPrefabs(STAGE.Stage1);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
