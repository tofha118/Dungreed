using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogbox;

    [SerializeField]

    private void Awake()
    {
        BaseNPC[] npcs = FindObjectsOfType<BaseNPC>();
        PlayerInteraction[] interactions = new PlayerInteraction[npcs.Length];

        for (int i = 0; i < npcs.Length; i++) 
        {
            interactions[i] = npcs[i].interaction;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
