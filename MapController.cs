using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameManager GameManager;
    
    public List<GameObject> allStarObjects;
    public GameObject starFieldObject;
    // Start is called before the first frame update

    public List<GameObject> allMeteors;
    public GameObject meteorHolder;

    public GameObject playerHolder;

    public List<Vector2> mapEdges;
    void Start()
    {
        GenerateStarField();
    }
    public void GenerateStarField(){
        var currentSize = allStarObjects[0].transform.localEulerAngles.x;
        for(int x = 0; x < 100; x++){
            var oldStar = allStarObjects[UnityEngine.Random.Range(0, 3)];
            var newStar = Instantiate(oldStar, starFieldObject.transform);
            var newScaler = UnityEngine.Random.Range(-0.1f, 0.1f) + currentSize;
            newStar.transform.localEulerAngles = new Vector3(newScaler, newScaler, newScaler);
            allStarObjects.Add(newStar);
        }
        ScatterStars();
    }

    public void ScatterStars(){
        foreach(GameObject newStar in allStarObjects){
            newStar.transform.localPosition = new Vector3(UnityEngine.Random.Range(-22f, -71f), allStarObjects[0].transform.localPosition.y, UnityEngine.Random.Range(-132f, -81f));
        }
    }

    public void GenerateMeteors(){
        for(int x = 0; x < 30; x++){
            var newMeteor = Instantiate(allMeteors[0], meteorHolder.transform);
            allMeteors.Add(newMeteor);
        }
    }

    public void SpawnMeteor(){

    }

}
