using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    //npc들이 자신이 생성되면 알아서 npc매니저에 자신을 넣어서 매니저에 관리를 맞긴다.
    public List<BaseNPC> npclist;


    public void AddToNpcList(BaseNPC obj)
    {
        npclist.Add(obj);
    }
    
    public void DeleteToNpcList(BaseNPC obj)
    {
        npclist.Remove(obj);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
