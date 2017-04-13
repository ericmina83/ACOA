using UnityEngine;

namespace GBAOC {
    public enum SPOT_TYPE { SPOT, START, STOP, WALL }

    public class GBSpot : MonoBehaviour {      
        public float pheromone = default(float);
        public SPOT_TYPE spotType = SPOT_TYPE.SPOT;

        void Start() {
            this.GetComponent<MeshRenderer>().material.color = Color.black;
            if (spotType == SPOT_TYPE.START)
                this.GetComponent<MeshRenderer>().material.color = Color.blue;
            else if (spotType == SPOT_TYPE.STOP)
                this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        void Update() {
            if (spotType == SPOT_TYPE.SPOT)
                this.GetComponent<MeshRenderer>().material.color = new Color(pheromone / 100, pheromone / 100, pheromone / 100);
        }
    }
}