using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject option_Panel;
    public GameObject bird;
    public bool stopSpawn = true;
    void Start()
    {
        StartCoroutine(SpawnBird());
        SoundManager.Instance.bgmSource.PlayOneShot(SoundManager.Instance.UI_Audio[7]);
    }

    IEnumerator SpawnBird()
    {
        while (stopSpawn)
        {
           yield return new WaitForSecondsRealtime(10f);
           Instantiate(bird);
            float randY = Random.Range(-5f, 5f);
            bird.transform.position = new Vector3(-10f, randY);
        }
    }
    public void optionPanelOn()
    {
        option_Panel.transform.gameObject.SetActive(true);
    }
    public void optionPanelOff()
    {
        option_Panel.transform.gameObject.SetActive(false);
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("Main_Scene");

        SoundManager.Instance.Volume_Save();
    }
    public void stopSpawning()
    {
        stopSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
