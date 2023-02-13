using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject BossObject;
    public GameObject bossFrontObject;
    // public GameObject bossLeftGun;
    // public GameObject bossRightGun;
    public List<GameObject> allOvershields;
    public GameObject bossLeftWing;
    public GameObject bossRightWing;
    public GameObject bossBlockBox;

    // public Transform bossFrontFirePoint;
    // public Transform bossLeftFirePoint;
    // public Transform bossRightFirePoint;
    public List<GameObject> gunObjectList;
    public List<Transform> firePoints;

    public List<ParticleSystem> bossFrontPS;
    public List<ParticleSystem> bossLeftWingPS;
    public List<ParticleSystem> bossRightWingPS;

    public List<int> damageList;

    public List<GameObject> allBullets;
    public int bulletIndex;
    public float bulletSpeed;

    void Start(){
        GenerateBullets();
        // FireRandom();
        StartCoroutine(FireRandom());
    }

    public void GenerateBullets(){
        var oldBullet = allBullets[0];
        for(int x = 0; x < 50; x++){
            var newBullet = Instantiate(oldBullet, GameManager.PlayerController.bulletHolder);
            newBullet.transform.localPosition = new Vector3(10000f, 10000f, 10000f);
            allBullets.Add(newBullet);
        }
  }

    public void StartBoss(){
        bossLeftWing.SetActive(true);
        bossRightWing.SetActive(true);
        bossFrontObject.SetActive(true);
        damageList[0] = 10;
        damageList[1] = 10;
        damageList[2] = 15;
    }

    public IEnumerator FireRandom(){
        while(bossLeftWing.activeSelf == true && bossRightWing.activeSelf == true){
            FireBullet(0);
            FireBullet(1);
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }
        while(bossLeftWing.activeSelf == true && bossRightWing.activeSelf == false){
            FireBullet(0);
            yield return new WaitForSeconds(Random.Range(0.5f, 0.75f));
        }
        while(bossLeftWing.activeSelf == false && bossRightWing.activeSelf == true){
            FireBullet(1);
            yield return new WaitForSeconds(Random.Range(0.5f, 0.75f));
        }

    }

    public void FireBullet(int toFire){
        allBullets[bulletIndex].transform.position = firePoints[toFire].position;
        if(toFire == 1){
            allBullets[bulletIndex].GetComponent<BulletScript>().FireBullet(bulletSpeed, gunObjectList[toFire].transform.localEulerAngles.y +90f);
        }
        if(toFire == 0){
            allBullets[bulletIndex].GetComponent<BulletScript>().FireBullet(bulletSpeed, gunObjectList[toFire].transform.localEulerAngles.y -90f);
        }
    bulletIndex += 1;

    if(bulletIndex >= allBullets.Count){bulletIndex = 0;}
    }

    public void Hit(int leftRightFront){
        StartCoroutine(OvershieldFlash(leftRightFront));
        TookDamage(leftRightFront);
    }

    public IEnumerator OvershieldFlash(int leftRightFront){
        allOvershields[leftRightFront].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        allOvershields[leftRightFront].SetActive(false);
    }

    public void TookDamage(int leftRightFront){
        damageList[leftRightFront] = damageList[leftRightFront] - 1;
        // Hit(leftRightFront);
        if(damageList[leftRightFront] <= 0){
            if(leftRightFront == 0){
                KillLeftWing();
            }
            if(leftRightFront == 1){
                KillRightWing();
            }
            if(leftRightFront == 2){
                KillFront();
            }
        }
    }

    public void KillLeftWing(){
        bossLeftWing.SetActive(false);
        bossLeftWingPS[0].Play();
        bossLeftWingPS[1].Play();
    }
    public void KillRightWing(){
        bossRightWing.SetActive(false);
        bossRightWingPS[0].Play();
        bossRightWingPS[1].Play();
    }

    public void KillFront(){
        bossFrontObject.SetActive(false);
        bossFrontPS[0].Play();
    }

    
}
