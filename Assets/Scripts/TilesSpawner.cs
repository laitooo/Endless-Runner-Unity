using UnityEngine;
using System.Collections.Generic;

public class TilesSpawner : MonoBehaviour {
    public Transform playerTransform;
    public Transform tilesContainerTransform;
    public GameObject firstTile;
    public GameObject[] tilesPrefabs;

    private List<GameObject> activeList;
    private float spawnZ = 500f;
    private float tileLength = 1000f;
    private float safeZone = 1250f;
    private int numTilesOnScreen = 3;
    private int lastPrefabIndex = 0;

    void Start() {
        activeList = new List<GameObject>();
        spawnFirstTile();
        for (int i=1; i<=numTilesOnScreen; i++) {
            spawnRandomTile();
        }
    }

    void Update() {
        if (playerTransform.position.z - safeZone> (spawnZ - (numTilesOnScreen * tileLength))) {
            spawnRandomTile();
            deleteTile();
        }
    }

    private void spawnRandomTile() {
        lastPrefabIndex = randomIndex();
        spawnTile(tilesPrefabs[lastPrefabIndex]);
    }

    private void spawnFirstTile() {
        spawnTile(firstTile);
    }

    private void spawnTile(GameObject tile) {
        GameObject gameObject = (GameObject) Instantiate(tile);
        gameObject.transform.SetParent(tilesContainerTransform);
        gameObject.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeList.Add(gameObject);
    }

    private void deleteTile() {
        Destroy(activeList[0]);
        activeList.RemoveAt(0);
    }

    private int randomIndex() {
        if (tilesPrefabs.Length <= 1) {
            return 0;
        }

        int randomIndex = 0;
        do {
            randomIndex = Random.Range(0, tilesPrefabs.Length);
        } while (randomIndex == lastPrefabIndex);

        return randomIndex;
    }
}
