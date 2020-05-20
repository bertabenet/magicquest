using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool canSpawn = true;
    public List<Transform> spawnPositions = new List<Transform>();

    /* Diamond vars */
    public GameObject diamPrefab;
    private List<GameObject> diamList = new List<GameObject>();
    public float timeBetweenDiamondSpawns;

    /* Projectile vars */
    public GameObject projPrefab;
    private List<GameObject> projList = new List<GameObject>();
    public float timeBetweenProjectileSpawns;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDiamondRoutine());
        StartCoroutine(SpawnProjectileRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnDiamond()
    {
        Vector3 randomPosition = spawnPositions[1].position;
        GameObject diam = Instantiate(diamPrefab, randomPosition, diamPrefab.transform.rotation);
        diamList.Add(diam);

        diam.GetComponent<Movement>().SetSpawner(this);
    }

    private void SpawnProjectile()
    {
        Vector3 randomPosition = spawnPositions[Random.Range(0, spawnPositions.Count)].position;
        GameObject proj = Instantiate(projPrefab, randomPosition, projPrefab.transform.rotation);
        projList.Add(proj);

        proj.GetComponent<Movement>().SetSpawner(this);

    }

    private IEnumerator SpawnDiamondRoutine()
    {
        while (canSpawn)
        {
            SpawnDiamond();
            yield return new WaitForSeconds(timeBetweenDiamondSpawns);
        }
    }

    private IEnumerator SpawnProjectileRoutine()
    {
        while (canSpawn)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(timeBetweenProjectileSpawns);
        }
    }

    public void RemoveProjFromList(GameObject proj)
    {
        projList.Remove(proj);
    }
    public void RemoveDiamFromList(GameObject diam)
    {
        diamList.Remove(diam);
    }


    public void DestroyAllObjects()
    {
        foreach (GameObject diam in diamList)
        {
            Destroy(diam);
        }

        diamList.Clear();

        foreach (GameObject proj in projList)
        {
            Destroy(proj);
        }

        projList.Clear();

    }
}
