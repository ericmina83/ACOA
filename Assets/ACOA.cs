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
	float p = 0.5f;
	public float Q = 1.0f;
	bool running = false;
    readonly int countOfAnts = 20;

	public static int mapSize = 20;

	// Use this for initialization
	void Start () {
		ants = new List<Ant> ();
        
		int startX = 0; 
		int startY = 0;

		int stopX = Random.Range (1, mapSize);
		int stopY = Random.Range (1, mapSize);

		while (startX == stopX && startY == stopY) {
			stopX = Random.Range (1, mapSize);
         	stopY = Random.Range (1, mapSize);
		}

		map = new List<List<Spot>> ();
		for (int i = 0; i < mapSize; i++) {
			map.Add(new List<Spot> ());
			for (int j = 0; j < mapSize; j++) {
				Spot spot = GameObject.Instantiate<Spot>(spotPrefab);
                spot.transform.SetParent(spotsParrent.transform);
                spot.transform.position = new Vector3(i, j, 3);

                map[i].Add(spot);
			}
		}

		map [0] [0].spotType = Spot.SPOT_TYPE.START;
		map [stopX] [stopY].spotType = Spot.SPOT_TYPE.STOP;

		for (int i = 0; i < countOfAnts; i++) {
			Ant ant = GameObject.Instantiate<Ant> (antPrefab);
            ant.gameObject.transform.SetParent(antsParrent.transform);
			ant.currentSpot = ant.prevSpot = map [startX][startY];
			ants.Add (ant);
		}

		instance = this; 
	}
	
	// Update is called once per frame
	void Update () {
		if(running == true)
		{
			
			foreach (Ant ant in ants) {
				ant.walk();
			}
			for (int i = 0; i < mapSize; i++) {
				for (int j = 0; j < mapSize; j++) {
					map [i] [j].pheromone = p * map [i] [j].pheromone;
				}
			}
			foreach (Ant ant in ants) {
				if (ant.getFood)
					ant.currentSpot.pheromone += (Q / (ant.pathCount + 1.0f));
			}
		}
	}

	public void btnRunning(){
		running = !running;
	}

}
