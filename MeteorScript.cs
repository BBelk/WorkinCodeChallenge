using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public MapController MapController;

    public Rigidbody myRigid;
    public GameObject showMeteorObject;
    public ParticleSystem meteorExplosionPS;

    public bool isBig;

    public void ResetMeteor(){
        showMeteorObject.SetActive(false);
        meteorExplosionPS.Stop();
        this.gameObject.SetActive(false);
    }

    public void ExplodeMeter(){
        meteorExplosionPS.Play();
        showMeteorObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            MapController.GameManager.PlayerHit();
        }
        if(other.gameObject.tag == "Bullet"){
            other.gameObject.GetComponent<BulletScript>().HitObject();
            ExplodeMeter();
        }
    }
}
