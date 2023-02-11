using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameManager GameManager;
  public GameObject PlayerObject;
  public GameObject PlayerObject2;
  public Rigidbody PlayerR;

  public float moveSpeed;

  public GameObject testObject;
  public bool canControl;

  // Update is called once per frame
  void Update(){
    float distance;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (plane.Raycast(ray, out distance)){
      worldPosition = ray.GetPoint(distance);

      worldPosition = new Vector3(worldPosition.x, PlayerObject2.transform.position.y, worldPosition.z);
      testObject.transform.position = worldPosition;
      Vector3 dir = worldPosition - PlayerObject2.transform.position;
      dir.y = 0;
      PlayerObject2.transform.rotation = Quaternion.LookRotation(dir);
    }
  }

  public Vector3 worldPosition;
  Plane plane = new Plane(Vector3.up, 0);


  Vector3 moveDirectionX, moveDirectionZ;
  void FixedUpdate()
  {
    if(canControl){
    moveDirectionX = PlayerObject.transform.forward * Input.GetAxis("Vertical");
    moveDirectionZ = PlayerObject.transform.right * Input.GetAxis("Horizontal");

    PlayerR.AddForce((moveDirectionX + moveDirectionZ).normalized * moveSpeed * 10f, ForceMode.Force);

    var playerPos = PlayerObject.transform.localPosition; 
        // var playerZ = playerHolder.transform.localPosition.z; 
        if(playerPos.x < GameManager.MapController.mapEdges[0].x){PlayerObject.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].y, playerPos.y, playerPos.z);}    
        if(playerPos.x > GameManager.MapController.mapEdges[0].y){PlayerObject.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].x, playerPos.y, playerPos.z);} 

        if(playerPos.z < GameManager.MapController.mapEdges[1].x){PlayerObject.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].y);}     
        if(playerPos.z > GameManager.MapController.mapEdges[1].y){PlayerObject.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].x);}  
    }


  }
}
