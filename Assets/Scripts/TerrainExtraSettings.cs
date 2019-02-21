using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainExtraSettings : MonoBehaviour {
    public float detailDrawDistance;
    public int heightmapLOD;
    Terrain terrain;
	// Use this for initialization
	void Start () {
        terrain = GetComponent<Terrain>();
        terrain.detailObjectDistance = detailDrawDistance;
        terrain.heightmapMaximumLOD = heightmapLOD;

	}

}
