using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    // projectile and diamond prefabs
    public GameObject projPrefab;
    public GameObject diamondPrefab;

    public bool canSpawn = true;
    public List<Transform> SpawnPositions = new List<Transform>();
    public float timeBetweenSpawnsProj;
    public float timeBetweenSpawnsDiam;
    private List<GameObject> projList = new List<GameObject>();
    private List<GameObject> diamList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnRoutine2());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnProjectile()
    {
        Vector3 randomPosition = SpawnPositions[Random.Range(0, SpawnPositions.Count)].position;
        GameObject proj = Instantiate(projPrefab, randomPosition, projPrefab.transform.rotation);
        projList.Add(proj);
        print(randomPosition);
        print(SpawnPositions.Count);
        
        proj.GetComponent<MoveProjectile>().SetSpawner(this);
        
    }
    private void SpawnDiamond()
    {
        Vector3 randomPosition = SpawnPositions[Random.Range(0, SpawnPositions.Count)].position;
        GameObject diam = Instantiate(diamondPrefab, randomPosition, diamondPrefab.transform.rotation);
        diamList.Add(diam);
        print(randomPosition);
        print(SpawnPositions.Count);
        
        diam.GetComponent<MoveDiamond>().SetSpawner(this);

    }
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(timeBetweenSpawnsProj);
        }
    }

    private IEnumerator SpawnRoutine2()
    {
        while (canSpawn)
        {
            SpawnDiamond();
            yield return new WaitForSeconds(timeBetweenSpawnsDiam);
        }
    }

    public void RemoveProjFromList(GameObject proj)
    {
        projList.Remove(proj);
    }

    public void DestroyAllProjectiles()
    {
        foreach (GameObject proj in projList)
        {
            Destroy(proj);
        }

        projList.Clear();
    }
}
