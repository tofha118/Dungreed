using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DungeonEnter : MonoBehaviour
{
    public LayerMask playerlayer;

    public GameObject DungeonEat;

    public GameObject playerobj = null;

    // Start is called before the first frame update
    void Start()
    {
        DungeonEat.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("플레이어 들어옴");
            DungeonEat.SetActive(true);
            Vector3 temp = new Vector3(collision.transform.position.x, DungeonEat.transform.position.y);
            DungeonEat.transform.position = temp;
            playerobj = collision.gameObject;
            Save_Inventory_Info();
            SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[4]);
            Invoke("EnterDungeon", 1.4f);
        }
    }

    public void EnterDungeon()
    {
        if (playerobj != null)
        {
            Debug.Log("던전으로");
            DontDestroyOnLoad(playerobj);
            SceneManager.LoadScene("Main_Dungeon_Scene");
        }

    }

    void Save()
    {
        Player player = playerobj.GetComponent<Player>();

        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            if (Inventory.Instance.slots[i].item != null)
            {
                player.slots[i] = Inventory.Instance.slots[i].item.item.itemCode;
            }
        }
        for (int i = 0; i < Inventory.Instance.equipment_1Slot.Length; i++)
        {
            if (Inventory.Instance.equipment_1Slot[i].item != null)
            {
                player.equip_Slots_1[i] = Inventory.Instance.equipment_1Slot[i].item.item.itemCode;
            }
        }
        for (int i = 0; i < Inventory.Instance.equipment_2Slot.Length; i++)
        {
            if (Inventory.Instance.equipment_2Slot[i].item != null)
            {
                player.equip_Slots_2[i] = Inventory.Instance.equipment_2Slot[i].item.item.itemCode;
            }
        }
        for (int i = 0; i < Inventory.Instance.Acc_Slot.Length; i++)
        {
            if (Inventory.Instance.Acc_Slot[i].item != null)
            {
                player.acc_Slots[i] = Inventory.Instance.Acc_Slot[i].item.item.itemCode;
            }
        }
    }

    void Save_Inventory_Info()
    {
        Debug.Log("저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장저장");
        Save();

        Player player = playerobj.GetComponent<Player>();

        string slotsArr = ""; // 문자열 생성
        string equipSlots1Arr = ""; // 문자열 생성
        string equipSlots2Arr = ""; // 문자열 생성
        string accSlotsArr = ""; // 문자열 생성

        for (int i = 0; i < player.slots.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            slotsArr = slotsArr + player.slots[i];
            if (i < player.slots.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                slotsArr = slotsArr + ",";
            }
        }

        for (int i = 0; i < player.equip_Slots_1.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            equipSlots1Arr = equipSlots1Arr + player.equip_Slots_1[i];
            if (i < player.equip_Slots_1.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                equipSlots1Arr = equipSlots1Arr + ",";
            }
        }

        for (int i = 0; i < player.equip_Slots_2.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            equipSlots2Arr = equipSlots2Arr + player.equip_Slots_2[i];
            if (i < player.equip_Slots_2.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                equipSlots2Arr = equipSlots2Arr + ",";
            }
        }

        for (int i = 0; i < player.acc_Slots.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            accSlotsArr = accSlotsArr + player.acc_Slots[i];
            if (i < player.acc_Slots.Length - 1) // 최대 길이의 -1까지만 ,를 저장
            {
                accSlotsArr = accSlotsArr + ",";
            }
        }

        PlayerPrefs.SetString("slotsList", slotsArr);
        PlayerPrefs.SetString("equipSlots1List", equipSlots1Arr);
        PlayerPrefs.SetString("equipSlots2List", equipSlots2Arr);
        PlayerPrefs.SetString("accSlotsList", accSlotsArr);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
