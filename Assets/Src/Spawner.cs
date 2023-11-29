using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;

    public Transform transf;

    private float spawnDelay = 5.0f;
    // Start is called before the first frame update

    void Awake()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        MeshRenderer render = GetComponent<MeshRenderer>();
        filter.sharedMesh = null;
        render.sharedMaterial = null;
    }

    void Start()
    {
        transf = transform;
        Spawn();
    }

    public void OnSpawn()
    {
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        Spawn();
    }

    private void Spawn()
    {
        var gameObject = Instantiate(prefab, transf.position, transf.rotation);

        if (gameObject.TryGetComponent(out ISpawnControled spawnControled))
        {
            spawnControled.Spawn += OnSpawn;
        }
    }

    void OnValidate()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        MeshRenderer render = GetComponent<MeshRenderer>();
        filter.sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        render.sharedMaterial = prefab.GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = prefab.transform.localScale;
    }
}
