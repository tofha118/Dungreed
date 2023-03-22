using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Image> dashImg;

    [SerializeField]
    private Image dashEnd;

    [SerializeField]
    private Sprite sizeX;

    private DashData dashData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UpdateDash()
    {
        int dashCount = dashData.maxDashCount;

        for (int i = 0; i < dashCount; i++)
        {
            dashImg[i].gameObject.SetActive(true);
        }

    }

}

public class DashData 
{
    public int maxDashCount;
    public int currDashCount;


}