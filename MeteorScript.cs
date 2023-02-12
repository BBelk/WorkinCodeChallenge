using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public GameManager GameManager;
    public MapController MapController;

    public Rigidbody myRigid;
    public GameObject showMeteorObject;
    public ParticleSystem meteorExplosionPS;
    public SphereCollider mySC;

    public bool isBig;

    void Start(){
        // StartMeteor(false, new Vector3(-10f, -0.7f, -30f), 1f);
    }

    public void StartMeteor(bool doBig, Vector3 startPosition, float newAngle){
        var torqueSpeed = 100f;
        if(doBig){this.transform.localScale = new Vector3(1f, 1f, 1f);}
        if(!doBig){this.transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);torqueSpeed *= 5f;}
        this.transform.position = startPosition;
        showMeteorObject.SetActive(true);
        mySC.enabled = true;
        showMeteorObject.GetComponent<Rigidbody>().AddTorque(Random.onUnitSphere * 100f);
        this.transform.localEulerAngles = new Vector3(0f, newAngle, 0f);
         myRigid.AddForce(transform.forward * UnityEngine.Random.Range(700f, 1000f));
    }

    public void ResetMeteor(){
        showMeteorObject.SetActive(false);
        meteorExplosionPS.Stop();
        // this.gameObject.SetActive(false);
        mySC.enabled = false;
    }

    public void ExplodeMeteor(){
        meteorExplosionPS.Play();
        showMeteorObject.SetActive(false);
        mySC.enabled = false;
        myRigid.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            MapController.GameManager.PlayerHit();
        }
        if(other.gameObject.tag == "Bullet"){
            other.gameObject.GetComponent<BulletScript>().HitObject();
            ExplodeMeteor();
        }
    }

    void FixedUpdate(){
        var playerPos = this.transform.localPosition;
      if (playerPos.x < GameManager.MapController.mapEdges[0].x) { this.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].y, playerPos.y, playerPos.z); }
      if (playerPos.x > GameManager.MapController.mapEdges[0].y) { this.transform.localPosition = new Vector3(GameManager.MapController.mapEdges[0].x, playerPos.y, playerPos.z); }

      if (playerPos.z < GameManager.MapController.mapEdges[1].x) { this.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].y); }
      if (playerPos.z > GameManager.MapController.mapEdges[1].y) { this.transform.localPosition = new Vector3(playerPos.x, playerPos.y, GameManager.MapController.mapEdges[1].x); }
    }
}
