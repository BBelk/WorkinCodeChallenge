using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerScript : MonoBehaviour
{
  public GameManager GameManager;
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Bullet")
    {
      var isBoss = other.gameObject.GetComponent<BulletScript>().isBoss;
      if (isBoss)
      {
        other.gameObject.GetComponent<BulletScript>().HitObject();
        GameManager.PlayerHit();
      }
    }
  }
}
