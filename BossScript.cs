using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject BossObject;
    public GameObject bossFrontObject;
    public GameObject bossLeftWing;
    public GameObject bossRightWing;
    public GameObject bossBlockBox;
    public List<GameObject> allOvershields;
    public List<GameObject> gunObjectList;
    public List<Transform> firePoints;

    public List<ParticleSystem> bossPS;

    public List<int> damageList;

    public List<GameObject> allBullets;
    public int bulletIndex;
    public float bulletSpeed;
    public Coroutine firingCo;

    void Start(){
        GenerateBullets();
    }

    public void GenerateBullets(){
        var oldBullet = allBullets[0];
        for(int x = 0; x < 50; x++){
            var newBullet = Instantiate(oldBullet, GameManager.PlayerController.bulletHolder);
            newBullet.transform.localPosition = new Vector3(10000f, 10000f, 10000f);
            allBullets.Add(newBullet);
        }
  }

  public void PauseBoss(){
    foreach(GameObject newObj in allBullets){
        newObj.GetComponent<BulletScript>().PauseBullet();
    }
    StopCoroutine(firingCo);
  }

  public void Resume(){
        foreach(GameObject newObj in allBullets){
            newObj.GetComponent<BulletScript>().Resume();
        }
        
        firingCo = StartCoroutine(FireRandom());
    }

    public void StartBoss(){
       firingCo = StartCoroutine(FireRandom());
    }

    public void ResetBoss(){
        bossLeftWing.SetActive(true);
        bossRightWing.SetActive(true);
        bossFrontObject.SetActive(true);
        bossBlockBox.SetActive(true);
        damageList[0] = 15;
        damageList[1] = 15;
    }

    public IEnumerator FireRandom(){
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        while(bossLeftWing.activeSelf == true && bossRightWing.activeSelf == true){
            FireBullet(0);
            FireBullet(1);
            yield return new WaitForSeconds(Random.Range(0.75f, 1f));
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
           if(damageList[0] <= 0 && damageList[1] <= 0){
            BossDefeated();
           }

    }
    
    public void BossDefeated(){
        KillFront();
        StopCoroutine(firingCo);
        bossBlockBox.SetActive(false);
        GameManager.GameOver(true);
    }

    public void KillLeftWing(){
        bossLeftWing.SetActive(false);
        bossPS[0].Play();
    }
    public void KillRightWing(){
        bossRightWing.SetActive(false);
        bossPS[1].Play();
    }

    public void KillFront(){
        bossFrontObject.SetActive(false);
        bossPS[2].Play();
    }

    
}
