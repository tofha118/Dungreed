using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMapUI : Singleton<DungeonMapUI>
{
    //public Panel mainPanel;
    //public Image 

    public DungeonMapUIBody Body;


    private void Awake()
    {
        Body = GetComponentInChildren<DungeonMapUIBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
