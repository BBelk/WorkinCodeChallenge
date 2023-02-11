using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody myRigid;
    public GameObject bulletObject;
    public SphereCollider bulletSC;
    public ParticleSystem bulletExplodePS;
    public void HitObject(){
        bulletExplodePS.Play();
        bulletObject.SetActive(false);
        bulletSC.enabled = false;
        myRigid.velocity = Vector3.zero;
        Invoke("ResetBullet", 1.0f);
    }

    public void ResetBullet(){
        this.transform.localPosition = new Vector3(1000f, 1000f, 1000f);
    }

    public void FireBullet(float bulletSpeed, float newAngle){
        this.transform.localEulerAngles = new Vector3(0f, newAngle, 0f);
        bulletObject.SetActive(true);
        bulletSC.enabled = true;
        myRigid.AddForce(transform.forward * bulletSpeed);

    }
}
