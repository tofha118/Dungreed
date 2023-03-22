using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public enum MonsterType { Nomal, Fly, MiddleBoss, Boss, Max };

    public List<GameObject> FlyEnemy;
    public List<GameObject> NomalEnemy;
    public List<GameObject> ModdleBoss;

    public List<Transform> PosSpawn;

    public List<Transform> NowSpawndList;

    public void Spawn(MonsterType Num , Transform Pos)
    {
        GameObject copyObj = null;
        int rnd;
        switch (Num)
        {
            case MonsterType.Nomal:
                rnd = Random.Range(0, NomalEnemy.Count);
                copyObj = GameManager.Resource.Instantiate($"Enemy_prefabs/{NomalEnemy[rnd].name}");
                copyObj.GetComponent<SkelDog>().Init();
                break;

            case MonsterType.MiddleBoss:
                rnd = Random.Range(0, ModdleBoss.Count);
                copyObj = GameManager.Resource.Instantiate($"Enemy_prefabs/{ModdleBoss[rnd].name}");
                copyObj.GetComponent<AbyssGuardian>().Init();
                break;

            case MonsterType.Fly:
                rnd = Random.Range(0, FlyEnemy.Count);
                copyObj = GameManager.Resource.Instantiate($"Enemy_prefabs/{FlyEnemy[rnd].name}");
                copyObj.GetComponent<Arsha>().Init();
                break;
          
        }
        copyObj.transform.position = Pos.position;
        NowSpawndList.Add(copyObj.transform);
    }

    public void DeadMonster(GameObject monster)
    {
        NowSpawndList.Remove(monster.transform);
    }


    void Start()
    {

        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[1]);

    }

    
    void Update()
    {
        
    }
}
