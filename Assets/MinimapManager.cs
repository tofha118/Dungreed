using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : Singleton<MinimapManager>
{
    //현재 플레이어가 위치한 맵의 정보를 받아와서 미니맵을 만들어 준다.
    //현재 씬의 캔버스를 찾아서 캔버스에 붙여줄 미니맵 객체를 만들어서 붙여주고
    
    //현재 맵 정보를 받아와서 미니맵 객체에게 넣어준다.
    //이후 미니맵은 주어진 정보를 따라서 미니맵을 생성해서 화면에 보여준다.

    public Minimap minimap;
    public MinimapTile minimaptile;

    public Transform Origin_BottomLeft;
    public Transform Origin_TopRight;





    public int[] MinimapInfo;




    // Start is called before the first frame update
    void Start()
    {
        
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
