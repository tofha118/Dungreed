using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    //npc���� �ڽ��� �����Ǹ� �˾Ƽ� npc�Ŵ����� �ڽ��� �־ �Ŵ����� ������ �±��.
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
