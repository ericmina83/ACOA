using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {
    public float distance = 0;
    public bool getFood = false;
    public Spot currentSpot;
    public Spot prevSpot;
    List<Spot> possibleSpot = new List<Spot>();
    private float fixedPh = 10.0f;

    // Use this for initialization
    void Start() {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = currentSpot.transform.position + new Vector3(0, 0, -3);
    }

    List<float> wheel = new List<float>();

    public void walk() {
        wheel.Clear();
        possibleSpot.Clear();
        int x = (int)currentSpot.transform.position.x;
        int y = (int)currentSpot.transform.position.y;

        Vector3 prevPos = prevSpot.transform.position;

        float sum = 0;

        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                if (i == 0 && j == 0)
                    continue;
                int xx = x + i;
                int yy = y + j;
                //if (xx >= 0 && yy >= 0 && xx < ACOA.mapSize && yy < ACOA.mapSize && !(xx == (int)prevPos.x && yy == (int)prevPos.y) && !(i == 0 && j == 0)) { 
                //看周圍費洛蒙

                if (xx == (int)prevPos.x && yy == (int)prevPos.y)
                    continue;

                if (xx >= 0 && yy >= 0 && xx < ACOA.mapSize && yy < ACOA.mapSize) {
                    Spot spot = ACOA.instance.map[xx][yy];//下一個移動的地點
                    if (spot.spotType == Spot.SPOT_TYPE.WALL)
                        continue;

                    if ((i * (x - prevPos.x) + j * (y - prevPos.y)) >= 0.0f) 
                        {
                        if (getFood == false) {
                            if (spot.spotType == Spot.SPOT_TYPE.STOP) {

                                prevSpot = ACOA.instance.map[x][y];//
                                //prevSpot = spot;
                                currentSpot = spot;
                                getFood = true;
                                distance = 0.0f;
                                return;
                            }
                        }
                        else if (getFood == true)
                            if (spot.spotType == Spot.SPOT_TYPE.START) {
                                
                                prevSpot = ACOA.instance.map[x][y];
                                currentSpot = spot;
                                getFood = false;
                                distance = 0.0f;
                                return;
                            }

                        possibleSpot.Add(spot);
                        sum += ACOA.instance.map[xx][yy].pheromone + fixedPh;
                        wheel.Add(sum);
                    }
                }
            }
        }


        if (possibleSpot.Count > 0) {
            Spot chosenSpot = possibleSpot[Random.Range(0, possibleSpot.Count)];

            float rand = Random.Range(0.0f, sum);


            for (int i = 0; i < wheel.Count; i++) {
                if (rand < wheel[i]) {
                    chosenSpot = possibleSpot[i];
                    break;
                }
            }


            prevSpot = currentSpot;
            currentSpot = chosenSpot;
            distance += (currentSpot.transform.position - prevSpot.transform.position).magnitude;
            //if (getFood == true) {
            //    distance += (currentSpot.transform.position - prevSpot.transform.position).magnitude;
            //}
        }

        else {
            Spot temp = currentSpot;
            currentSpot = prevSpot;
            prevSpot = temp;
        }
    }
}
