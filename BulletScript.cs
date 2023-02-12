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
    public void HitObject(){
        bulletExplodePS.Play();
        bulletObject.SetActive(false);
        bulletSC.enabled = false;
        myRigid.velocity = Vector3.zero;
        Invoke("ResetBullet", 1.0f);
    }

    public void ResetBullet(){
        if(bulletAutoReset != null){StopCoroutine(bulletAutoReset);bulletAutoReset = null;}
        this.transform.localPosition = new Vector3(1000f, 1000f, 1000f);
        bulletObject.SetActive(false);
        bulletSC.enabled = false;
        myRigid.velocity = Vector3.zero;
    }

    public void FireBullet(float bulletSpeed, float newAngle){
        if(bulletAutoReset != null){StopCoroutine(bulletAutoReset);bulletAutoReset = null;}
        this.transform.localEulerAngles = new Vector3(0f, newAngle, 0f);
        bulletObject.SetActive(true);
        bulletSC.enabled = true;
        myRigid.AddForce(transform.forward * bulletSpeed);

        bulletAutoReset = StartCoroutine("CountdownReset", 5.0f);
    }

    public IEnumerator CountdownReset(){
        yield return new WaitForSeconds(5.0f);
        ResetBullet();
    }


}
