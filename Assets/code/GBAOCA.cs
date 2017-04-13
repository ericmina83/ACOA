using UnityEngine;
using System.Collections.Generic;

namespace GBAOC {
    public class GBAOCA : MonoBehaviour {
        public List<List<GBSpot>> map;
        private List<GBAnt> ants;

        public GBSpot spotPrefab;
        public GBAnt antPrefab;
        public GameObject spotsParrent;
        //public GameObject antsParrent;

        public float alpha;
        public float beta;
        public float p {
            get;
            private set;
        }
        public float Q {
            get;
            private set;
        }
        public readonly int countOfAnts;
        public readonly int mapSize;

        public GBAOCA():this(20,20) {

        }

        public GBAOCA(int mapSize,int countOfAnts) {
            this.mapSize = mapSize;
            this.countOfAnts = countOfAnts;
            int startX = 0;
            int startY = 0;
            int stopX = Random.Range(1, mapSize);
            int stopY = Random.Range(1, mapSize);
            ants = new List<GBAnt>();

            //creation map
            map = new List<List<GBSpot>>();
            for (int i = 0; i < mapSize; i++) {
                map.Add(new List<GBSpot>());
                for (int j = 0; j < mapSize; j++) {
                    GBSpot spot = GameObject.Instantiate<GBSpot>(spotPrefab);
                    spot.transform.SetParent(spotsParrent.transform);
                    spot.transform.position = new Vector3(i, j, 3);

                    map[i].Add(spot);
                }
            }

            map[0][0].spotType = SPOT_TYPE.START;
            map[stopX][stopY].spotType = SPOT_TYPE.STOP;

            ////creation ant
            //for (int i = 0; i < countOfAnts; i++) {
            //    GBAnt ant = GameObject.Instantiate<GBAnt>(antPrefab);
            //    ant.gameObject.transform.SetParent(antsParrent.transform);
            //    ant.currentSpot = ant.prevSpot = map[startX][startY];
            //    ants.Add(ant);
            //}
        }
    }
}