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

  public List<GameObject> allBullets;
  public int bulletIndex;
  public float bulletSpeed;
  public Transform bulletHolder;

  public Vector3 storedVelocity;
  public Vector3 startPosition = new Vector3(0f, 6f, 5f);

  void Start(){
    GenerateBullets();
  }

  public void GenerateBullets(){
        var oldBullet = allBullets[0];
        for(int x = 0; x < 50; x++){
            var newBullet = Instantiate(oldBullet, bulletHolder);
            newBullet.transform.localPosition = new Vector3(1000f, 1000f, 1000f);
            allBullets.Add(newBullet);
        }
  }

  public void PausePlayer(){
    storedVelocity = PlayerR.velocity;
    PlayerR.velocity = Vector3.zero;
    canControl = false;
    foreach(GameObject newObj in allBullets){
        newObj.GetComponent<BulletScript>().PauseBullet();
    }
  }
  
    public void Resume(){
        PlayerR.velocity = storedVelocity;
        canControl = true;
        foreach(GameObject newObj in allBullets){
            newObj.GetComponent<BulletScript>().Resume();
        }
    }

    public void ResetAll(){
        foreach(GameObject newObj in allBullets){
            newObj.GetComponent<BulletScript>().ResetBullet();
            PlayerObject.transform.position = startPosition;
            PlayerObject2.transform.localEulerAngles = Vector3.zero;
        }
    }
  

    public Transform bulletLaunchPoint;
  public void FireBullet(){
    allBullets[bulletIndex].transform.position = bulletLaunchPoint.position;
    allBullets[bulletIndex].GetComponent<BulletScript>().FireBullet(bulletSpeed, PlayerObject2.transform.localEulerAngles.y +360f);
    bulletIndex += 1;

    if(bulletIndex >= allBullets.Count){bulletIndex = 0;}
  }

void Update(){
    if (canControl){
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
      if(Input.GetMouseButtonDown(0)){
        FireBullet();
      }
    }
  }

  public Vector3 worldPosition;
  Plane plane = new Plane(Vector3.up, 0);
  Vector3 moveDirectionX, moveDirectionZ;
  void FixedUpdate(){
    if (canControl){
        //movement section
      moveDirectionX = PlayerObject.transform.forward * Input.GetAxis("Vertical");
      moveDirectionZ = PlayerObject.transform.right * Input.GetAxis("Horizontal");

      PlayerR.AddForce((moveDirectionX + moveDirectionZ).normalized * moveSpeed * 10f, ForceMode.Force);

      var playerPos = PlayerObject.transform.localPosition;
      if (playerPos.x < GameManager.MapController.mapEdges[0].x) { PlayerObject.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].y, playerPos.y, playerPos.z); }
      if (playerPos.x > GameManager.MapController.mapEdges[0].y) { PlayerObject.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].x, playerPos.y, playerPos.z); }

      if (playerPos.z < GameManager.MapController.mapEdges[1].x) { PlayerObject.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].y); }
      if (playerPos.z > GameManager.MapController.mapEdges[1].y) { PlayerObject.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].x); }

      //fire buttlet
    }
  }
  
}
