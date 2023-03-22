using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : Singleton<BackGround>
{
    [SerializeField]
    private GameObject[] images;
    [SerializeField]
    private float[] speeds;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        foreach (GameObject go in images)
            go.transform.position = player.transform.position;
        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[0]);
    }

    private void Update()
    {
        Vector3 tempVec1 = new Vector3(images[0].transform.position.x, -1);
        Vector3 tempVec2 = new Vector3(images[1].transform.position.x, -10);
        Vector3 tempVec3 = new Vector3(images[2].transform.position.x, -13);
        Vector3 playerVec1 = new Vector3(player.transform.position.x, -1);
        Vector3 playerVec2 = new Vector3(player.transform.position.x, -10);
        Vector3 playerVec3 = new Vector3(player.transform.position.x, -13);

        images[0].transform.position = 
            Vector3.Lerp(tempVec1, playerVec1, speeds[0] * Time.deltaTime);
        images[1].transform.position =
            Vector3.Lerp(tempVec2, playerVec2, speeds[1] * Time.deltaTime);
        images[2].transform.position =
            Vector3.Lerp(tempVec3, playerVec3, speeds[2] * Time.deltaTime);
    }
}
