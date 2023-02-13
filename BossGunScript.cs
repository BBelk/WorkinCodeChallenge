using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunScript : MonoBehaviour
{
  public BossScript BossScript;
  public int leftRightFront;
  public GameObject playerObject;


  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Bullet")
    {
      other.gameObject.GetComponent<BulletScript>().HitObject();
      var isBoss = other.gameObject.GetComponent<BulletScript>().isBoss;
      if (isBoss) { return; }
      BossScript.Hit(leftRightFront);
    }
  }

  void FixedUpdate()
  {
    Vector3 dir = playerObject.transform.position - this.transform.position;
    dir.y = 0;
    this.transform.rotation = Quaternion.LookRotation(dir);
  }
}
