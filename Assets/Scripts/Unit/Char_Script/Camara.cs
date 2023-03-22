using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Camara camara;
    public GameObject obj_player;
    public Vector3 Target;
    public float cameraHalfWidth;
    public float cameraHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        obj_player =  GameObject.Find("Player");
        obj_player.GetComponent<Player>();
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
       
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = 5f;
        if (obj_player.GetComponent<Player>().minBound.x + cameraHalfWidth > obj_player.GetComponent<Player>().maxBound.x - cameraHalfWidth)
        {
            Vector3 desiredPosition = new Vector3(
             ((obj_player.GetComponent<Player>().maxBound.x + obj_player.GetComponent<Player>().minBound.x) / 2),   // X
                Mathf.Clamp(obj_player.transform.position.y - 2, obj_player.GetComponent<Player>().minBound.y + cameraHalfHeight-1, obj_player.GetComponent<Player>().maxBound.y - cameraHalfHeight+1), -10); // Y           );  
            camara.transform.position = Vector3.Lerp(camara.transform.position, desiredPosition, Time.deltaTime * moveSpeed);
            // camara.transform.position = new Vector3((obj_player.GetComponent<Player>().maxBound.x + obj_player.GetComponent<Player>().minBound.x) /2, (obj_player.GetComponent<Player>().maxBound.y + obj_player.GetComponent<Player>().minBound.y)/2 , this.transform.position.z);
        }
        else
        {
            Vector3 desiredPosition = new Vector3(
              Mathf.Clamp(obj_player.transform.position.x + 2, obj_player.GetComponent<Player>().minBound.x + cameraHalfWidth - 1, obj_player.GetComponent<Player>().maxBound.x - cameraHalfWidth + 1),   // X
              Mathf.Clamp(obj_player.transform.position.y - 2, obj_player.GetComponent<Player>().minBound.y + cameraHalfHeight - 1, obj_player.GetComponent<Player>().maxBound.y - cameraHalfHeight + 1), -10); // Y           );                                                                                                  // Z
            camara.transform.position = Vector3.Lerp(camara.transform.position, desiredPosition, Time.deltaTime * moveSpeed);
        }

        //Vector3 a = camara.transform.position;
        //a = obj_player.transform.position;
        //a.z = -10;
        //a.y = obj_player.transform.position.y;
        //camara.transform.position = a; //200,100  
        //if (obj_player.GetComponent<Player>().minBound.x + cameraHalfWidth > obj_player.GetComponent<Player>().maxBound.x - cameraHalfWidth)
        //{
        //    Vector3 desiredPosition = new Vector3(
        //    Mathf.Clamp(obj_player.transform.position.x + 2,  obj_player.GetComponent<Player>().maxBound.x - cameraHalfWidth-1, obj_player.GetComponent<Player>().minBound.x + cameraHalfWidth+1),   // X
        //    Mathf.Clamp(obj_player.transform.position.y - 2, obj_player.GetComponent<Player>().minBound.y + cameraHalfHeight-1, obj_player.GetComponent<Player>().maxBound.y - cameraHalfHeight+1), -10); // Y           );                                                                                                  // Z
        //    camara.transform.position = Vector3.Lerp(camara.transform.position, desiredPosition, Time.deltaTime * moveSpeed);

        //}

        //Target.Set(obj_player.transform.position.x, obj_player.transform.position.y, this.transform.position.z);
        //camara.transform.position = Vector3.Lerp(this.transform.position, Target, moveSpeed * Time.deltaTime);
        //// -10 , -18
        //float clampedX = Mathf.Clamp(this.transform.position.x, obj_player.GetComponent<Player>().minBound.x + cameraHalfWidth, obj_player.GetComponent<Player>().maxBound.x - cameraHalfWidth);
        //float clampedY = Mathf.Clamp(this.transform.position.y, obj_player.GetComponent<Player>().minBound.y + cameraHalfHeight, obj_player.GetComponent<Player>().maxBound.y - cameraHalfHeight);

        //camara.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
    }
}
