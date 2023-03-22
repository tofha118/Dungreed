using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonMapUIBody : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    

    public List<Map_Room_Tile> roomtiles = new List<Map_Room_Tile>();
    public GameObject[] maplist;

    [Header("�ν����� �ʱ�ȭ �ʿ�")]
    public Map_Room_Tile roomtile;

    public Vector2 downpos;

    public bool SettingDone = false;

    public DungeonMapUI parent = null;

    public Vector2Int nowPlayerPosIndex;

    public int size;

    private void Awake()
    {
        //roomtiles = new Map_Room_Tile[20];
        // GameObject obj = Instantiate(roomtile);
        parent = GetComponentInParent<DungeonMapUI>();

    }

    public void ClickedTeleportHere(int x,int y)
    {
        //���� �÷��̾ �ִ� ���� ã�������� �ش� ������ ������ �����ش�.
        for(int i=0;i<roomtiles.Count;i++)
        {
            if(roomtiles[i].NowPlayerEnter)
            {
                if (!roomtiles[i].IsTeleporter || !roomtiles[i].IsCleared)
                {
                    return;
                }
                nowPlayerPosIndex = roomtiles[i].roomindex;
                maplist[nowPlayerPosIndex.x + (nowPlayerPosIndex.y * size)].GetComponent<BaseStage>().NowPlayerEnter = false;
                break;
            }
        }
        
        maplist[x + (y * size)].GetComponent<BaseStage>().Teleporter.TeleportHere();
        UpdateRoomTiles();
    }

    public void SetRoomTiles(GameObject[] roominfo, int Xsize)
    {
        if(roomtiles.Count>0)
        {
            for(int i=0;i<roomtiles.Count;i++)
            {
                Destroy(roomtiles[i].gameObject);
                
            }
            roomtiles.Clear();
        }
       
        maplist = roominfo;
        size = Xsize;
        Vector2 startpos = new Vector2(-254, 300);
        int x, y;
        for (int i = 0; i < roominfo.Length; i++)
        {
            if(roominfo[i]!=null)
            {
                x = i % Xsize;
                y = (i / Xsize) * -1;
                Map_Room_Tile temp = GameObject.Instantiate(roomtile);
                temp.parent = this;
                temp.transform.parent = this.transform;
                temp.TileSetting(roominfo[i], i % Xsize, i / Xsize);
                temp.transform.localPosition = startpos + new Vector2(x, y) * 130;
                roomtiles.Add(temp);
            }
        }
    }

    public void UpdateRoomTiles()
    {
        for(int i=0;i<roomtiles.Count;i++)
        {
            roomtiles[i].DataUpdate();
        }
    }

    //Ȱ��ȭ�ɶ����� ������ �ٽ� �����ؼ� �����ش�.
    private void OnEnable()
    {
        UpdateRoomTiles();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("up");

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("down");
        downpos = eventData.position;
    }
    


    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("drag");
        Vector3 dir = eventData.position - downpos;
        this.transform.position = this.transform.position + dir;
        downpos = eventData.position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Click");

        //downpos
        //this.transform
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exit");
    }


    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if(!SettingDone)
        {
            for (int i = 0; i < roomtiles.Count; i++)
            {
                
                if (!roomtiles[i].AllSettingDone)
                {
                    break;
                }
                else
                {
                    count++;
                }

                
            }
            if(count>=roomtiles.Count)
            {
                SettingDone = true;
                parent.gameObject.SetActive(false);
            }
        }
        
    }
}
