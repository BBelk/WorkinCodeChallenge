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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
    float distance;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (plane.Raycast(ray, out distance))
    {
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
void FixedUpdate() {
    moveDirectionX = PlayerObject.transform.forward * Input.GetAxis("Vertical");
    moveDirectionZ = PlayerObject.transform.right * Input.GetAxis("Horizontal");
    // moveDirectionY = orientation.right * horizontalInput;

    PlayerR.AddForce((moveDirectionX + moveDirectionZ).normalized * moveSpeed * 10f, ForceMode.Force);
}
}
