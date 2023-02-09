using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasManager CanvasManager;

    void Start(){
        CanvasManager = this.gameObject.GetComponent<CanvasManager>();
        
    }
}
