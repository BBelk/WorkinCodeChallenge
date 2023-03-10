using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
  public GameManager GameManager;

  public List<GameObject> allStarObjects;
  public GameObject starFieldObject;
  public List<GameObject> allMeteors;
  public int meteorIndex;
  public GameObject meteorHolder;

  public GameObject playerHolder;

  public List<Vector2> mapEdges;

  void Start()
  {
    GenerateStarField();
    GenerateMeteors();
  }
  public void GenerateStarField()
  {
    var currentSize = allStarObjects[0].transform.localEulerAngles.x;
    for (int x = 0; x < 100; x++)
    {
      var oldStar = allStarObjects[UnityEngine.Random.Range(0, 3)];
      var newStar = Instantiate(oldStar, starFieldObject.transform);
      var newScaler = UnityEngine.Random.Range(-0.1f, 0.1f) + currentSize;
      newStar.transform.localEulerAngles = new Vector3(newScaler, newScaler, newScaler);
      allStarObjects.Add(newStar);
    }
    ScatterStars();
  }

  public void ScatterStars()
  {
    foreach (GameObject newStar in allStarObjects)
    {
      newStar.transform.localPosition = new Vector3(UnityEngine.Random.Range(-22f, -71f), allStarObjects[0].transform.localPosition.y, UnityEngine.Random.Range(-132f, -81f));
    }
  }

  public void GenerateMeteors()
  {
    for (int x = 0; x < 30; x++)
    {
      var newMeteor = Instantiate(allMeteors[0], meteorHolder.transform);
      allMeteors.Add(newMeteor);
      newMeteor.transform.position = new Vector3(-1111f, -1111f, -1111f);
    }
  }

  public void SpawnMeteor()
  {
    var newMeteor = allMeteors[meteorIndex];
    var height = Random.Range(mapEdges[1].x, mapEdges[1].y);
    var newAngle = Random.Range(30f, 330f);
    if (newAngle < 210f && newAngle > 150f)
    {
      newAngle += 45f * Random.value < 0.5f ? 1 : -1;
    }
    newMeteor.GetComponent<MeteorScript>().StartMeteor(true, new Vector3(mapEdges[0].x + 0.1f, -0.3f, height), newAngle);
    IncrementMeteorIndex();
  }
  public void IncrementMeteorIndex()
  {
    meteorIndex += 1;
    if (meteorIndex >= allMeteors.Count) { meteorIndex = 0; }
  }

  public int meteorCount = 0;

  public void StartGame()
  {
    meteorCount = 20;
    for (int x = 0; x < 5; x++)
    {
      SpawnMeteor();
    }
  }

  public void BigMeteorDestroyed(Vector3 oldPosition, float oldAngle)
  {
    var newAngleAdd = -30f;
    for (int x = 0; x < 3; x++)
    {
      var newMeteor1 = allMeteors[meteorIndex];
      newMeteor1.GetComponent<MeteorScript>().StartMeteor(false, oldPosition, oldAngle + newAngleAdd);
      IncrementMeteorIndex();
      newAngleAdd += 30f;
    }
  }

  public void SubtractMeteor()
  {
    meteorCount -= 1;
    if (meteorCount == 0)
    {
      GameManager.PlayerController.StartBossTransition();
    }
  }

  public void PauseMeteors()
  {
    foreach (GameObject newObj in allMeteors)
    {
      newObj.GetComponent<MeteorScript>().PauseMeteor();
    }
  }
  public void Resume()
  {
    foreach (GameObject newObj in allMeteors)
    {
      newObj.GetComponent<MeteorScript>().Resume();
    }
  }

  public void ResetAll()
  {
    ScatterStars();
    foreach (GameObject newObj in allMeteors)
    {
      newObj.GetComponent<MeteorScript>().ResetMeteor();
      newObj.transform.position = new Vector3(-1111f, -1111f, -1111f);
    }
  }

}
