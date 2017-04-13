using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour {

    public float pheromone = 0.0f;
    public enum SPOT_TYPE { SPOT, START, STOP, WALL };

    public SPOT_TYPE spotType = SPOT_TYPE.SPOT;

    // Use this for initialization
    void Start() {
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        if (spotType == SPOT_TYPE.START)
            this.GetComponent<MeshRenderer>().material.color = Color.blue;
        else if (spotType == SPOT_TYPE.STOP)
            this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        else if (spotType == SPOT_TYPE.WALL)
            this.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update() {
        //if (spotType == SPOT_TYPE.START)
        //	this.GetComponent<MeshRenderer> ().material.color = Color.blue;
        //else if(spotType == SPOT_TYPE.STOP)
        //	this.GetComponent<MeshRenderer> ().material.color = Color.yellow;
        if (spotType == SPOT_TYPE.SPOT)
            this.GetComponent<MeshRenderer>().material.color = new Color(pheromone / 100, pheromone / 100, pheromone / 100);
    }
}
