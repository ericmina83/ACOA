using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour {

	public float pathCount = 0;
	public bool getFood = false;
	public Spot currentSpot;
	public Spot prevSpot;


	List<Spot> possibleSpot = new List<Spot>();


	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer> ().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = currentSpot.transform.position + new Vector3 (0, 0, -3); 
	}

	List<float> wheel = new List<float>();

	public void walk(){
		pathCount++;
		wheel.Clear ();
		possibleSpot.Clear ();
		int x = (int)currentSpot.transform.position.x;
		int y = (int)currentSpot.transform.position.y;

		Vector3 prevPos = prevSpot.transform.position;

		float sum = 0;

		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int xx = x + i;
				int yy = y + j;
				if (xx >= 0 && yy >= 0 && xx < ACOA.mapSize && yy < ACOA.mapSize && xx != (int)prevPos.x && yy != (int)prevPos.y && !(i == 0 && j == 0)) {
					Spot spot = ACOA.instance.map [xx] [yy];

					if (((xx - x) * (x - prevPos.x) + (yy - y) * (y - prevPos.y)) >= 0.0f) {

						if (getFood == false) {
							if (spot.spotType == Spot.SPOT_TYPE.STOP) {
								prevSpot = currentSpot;
								currentSpot = spot;
								getFood = true;
								pathCount = 0.0f;
								return;
							}
						} else {
							if (spot.spotType == Spot.SPOT_TYPE.START) {
								prevSpot = spot;
								currentSpot = spot;
								getFood = false;
								pathCount = 0.0f;
								return;
							}
						}

						possibleSpot.Add (spot);
						sum += ACOA.instance.map [xx] [yy].pheromone + 0.5f;
						wheel.Add (sum);
					}
				}
			}
		}


		if (possibleSpot.Count > 0) {
			Debug.Log (possibleSpot.Count);
			Spot chosenSpot = possibleSpot [Random.Range (0, possibleSpot.Count)];

			float rand = Random.Range (0.0f, sum);

			if (sum > 0) {
				for (int i = 0; i < wheel.Count; i++) {
					if (rand < wheel [i]) {
						chosenSpot = possibleSpot [i];
						break;
					}
				}
			}

			prevSpot = currentSpot;
			currentSpot = chosenSpot;

			if (getFood == true)
				pathCount += (currentSpot.transform.position - currentSpot.transform.position).magnitude;
			
		} else {
			Spot temp = currentSpot;
			currentSpot = prevSpot;
			prevSpot = temp;
		}
	}
}
