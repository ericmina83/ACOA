using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACOA : MonoBehaviour {

    public static ACOA instance;

    public List<List<Spot>> map;

    List<Ant> ants;

    public Spot spotPrefab;
    public Ant antPrefab;
    public GameObject spotsParrent;
    public GameObject antsParrent;

    //parameter
    public float alpha = 0;
    public float beta = 0;
    float p = 0.99f;//保留綠
    const float Q = 100;//
    bool running = false;
    readonly int countOfAnts = 50;

    public static int mapSize = 20;


    //public ACOA() {
    //    int startX = 0;
    //    int startY = 0;
    //    int stopX = Random.Range(1, mapSize);
    //    int stopY = Random.Range(1, mapSize);
    //    ants = new List<Ant>();

    //    //creation map
    //    map = new List<List<Spot>>();
    //    for (int i = 0; i < mapSize; i++) {
    //        map.Add(new List<Spot>());
    //        for (int j = 0; j < mapSize; j++) {
    //            Spot spot = GameObject.Instantiate<Spot>(spotPrefab);
    //            spot.transform.SetParent(spotsParrent.transform);
    //            spot.transform.position = new Vector3(i, j, 3);

    //            map[i].Add(spot);
    //        }
    //    }

    //    map[0][0].spotType = Spot.SPOT_TYPE.START;
    //    map[stopX][stopY].spotType = Spot.SPOT_TYPE.STOP;

    //    //creation ant
    //    for (int i = 0; i < countOfAnts; i++) {
    //        Ant ant = GameObject.Instantiate<Ant>(antPrefab);
    //        ant.gameObject.transform.SetParent(antsParrent.transform);
    //        ant.currentSpot = ant.prevSpot = map[startX][startY];
    //        ants.Add(ant);
    //    }
    //}

    // Use this for initialization
    void Start() {

        ants = new List<Ant>();


        int startX = 0;
        int startY = 0;

        int stopX = Random.Range(10, mapSize);
        //int stopX = Random.Range(1, 10);
        int stopY = Random.Range(10, mapSize);

        //int stopY = Random.Range(1, 10);

        //while (startX == stopX && startY == stopY) {
        //    stopX = Random.Range(1, mapSize);
        //    stopY = Random.Range(1, mapSize);
        //}

        //creation map
        map = new List<List<Spot>>();
        for (int i = 0; i < mapSize; i++) {
            map.Add(new List<Spot>());
            for (int j = 0; j < mapSize; j++) {
                Spot spot = GameObject.Instantiate<Spot>(spotPrefab);
                spot.transform.SetParent(spotsParrent.transform);
                spot.transform.position = new Vector3(i, j, 3);

                map[i].Add(spot);
            }
        }

        for (int i = 0; i < 100; i++) {
            map[Random.Range(1, mapSize)][Random.Range(1, mapSize)].spotType = Spot.SPOT_TYPE.WALL;
        }

        map[0][0].spotType = Spot.SPOT_TYPE.START;
        map[stopX][stopY].spotType = Spot.SPOT_TYPE.STOP;

        //creation ant
        for (int i = 0; i < countOfAnts; i++) {
            Ant ant = GameObject.Instantiate<Ant>(antPrefab);
            ant.gameObject.transform.SetParent(antsParrent.transform);
            ant.currentSpot = ant.prevSpot = map[startX][startY];
            ants.Add(ant);
        }

        instance = this;
    }

    // Update is called once per frame
    void Update() {
        if (running == true) {
            string antsDis = "";
            foreach (Ant ant in ants) {
                ant.walk();
                antsDis += ant.distance + "  ";

            }
            //Debug.Log(antsDis);
            string phe = "";
            for (int i = 0; i < mapSize; i++) {
                for (int j = 0; j < mapSize; j++) {
                    map[i][j].pheromone = p * map[i][j].pheromone;
                    phe += map[i][j].pheromone + "\t";
                }

                phe += "\n";
            }

            //Debug.Log(phe);

            //Debug.Log((Q));
            foreach (Ant ant in ants) {
                if (ant.getFood)
                    ant.currentSpot.pheromone += (Q / (ant.distance));
                ant.currentSpot.pheromone += (Q / (ant.distance));

            }
        }
    }

    public void btnRunning() {
        running = !running;
    }

}
