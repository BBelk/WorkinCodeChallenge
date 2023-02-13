using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Rigidbody myRigid;
    public GameObject bulletObject;
    public SphereCollider bulletSC;
    public ParticleSystem bulletExplodePS;

    public Coroutine bulletAutoReset;
    public Vector3 storedVelocity;
    public bool isBoss;

    public void PauseBullet(){
        storedVelocity = myRigid.velocity;
        myRigid.velocity = Vector3.zero;
    }

    public void Resume(){
        myRigid.velocity = storedVelocity;
    }
    public void HitObject(){
        bulletExplodePS.Play();
        bulletObject.SetActive(false);
        bulletSC.enabled = false;
        myRigid.velocity = Vector3.zero;
        ResetBullet();
    }

    public void ResetBullet(){
        if(bulletAutoReset != null){StopCoroutine(bulletAutoReset);bulletAutoReset = null;}
        this.transform.localPosition = new Vector3(1000f, 1000f, 1000f);
        bulletObject.SetActive(false);
        bulletSC.enabled = false;
        myRigid.velocity = Vector3.zero;
        if(isBoss){bulletExplodePS.Stop();}
    }

    public void FireBullet(float bulletSpeed, float newAngle){
        if(bulletAutoReset != null){StopCoroutine(bulletAutoReset);bulletAutoReset = null;}
        this.transform.localEulerAngles = new Vector3(0f, newAngle, 0f);
        bulletObject.SetActive(true);
        bulletSC.enabled = true;
        myRigid.AddForce(transform.forward * bulletSpeed);
        
        if(isBoss){bulletExplodePS.Play();}
        bulletAutoReset = StartCoroutine("CountdownReset", 5.0f);
    }

    public IEnumerator CountdownReset(){
        yield return new WaitForSeconds(5.0f);
        ResetBullet();
    }


}
