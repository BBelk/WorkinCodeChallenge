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
}
