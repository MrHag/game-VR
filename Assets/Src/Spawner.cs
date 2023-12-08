using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;

    private Transform transf;

    private Coroutine spawning;

    private float spawnDelay = 5.0f;
    // Start is called before the first frame update

    // void Awake()
    // {
    //     //MeshFilter filter = GetComponent<MeshFilter>();

    //     // filter.sharedMesh = null;
    //     // render.sharedMaterial = null;
    // }

    private int _objectsCount = 0;

    private int ObjectsCount
    {
        get
        {
            return _objectsCount;
        }
        set
        {
            if (value <= 0)
            {
                OnSpawnStart();
            }
            else if (_objectsCount <= 0)
            {
                OnSpawnStop();
            }

            _objectsCount = value;
        }
    }

    void Start()
    {
        MeshRenderer render = GetComponent<MeshRenderer>();
        render.enabled = false;

        transf = transform;
        Spawn();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out ISpawnControled spawnControled))
        {
            ObjectsCount += 1;
            spawnControled.ODestroy += OnObjectDestroy;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ISpawnControled spawnControled))
        {
            ObjectsCount -= 1;
            spawnControled.ODestroy -= OnObjectDestroy;
        }
    }

    private void OnObjectDestroy(GameObject sender)
    {
        ObjectsCount -= 1;
    }

    public void OnSpawnStart()
    {
        print("START SPAWNING");
        spawning = StartCoroutine(SpawnDelay());
    }

    public void OnSpawnStop()
    {
        print("STOP SPAWNING");
        if (spawning != null)
            StopCoroutine(spawning);
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
            spawnControled.Spawn += OnSpawnStart;
        }
    }

    void OnValidate()
    {

        if (!prefab.TryGetComponent(out ISpawnControled spawnControled))
        {
            Debug.LogError("Cant find script that implements ISpawnControled");
            return;
        }

        MeshFilter filter = GetComponent<MeshFilter>();
        MeshRenderer render = GetComponent<MeshRenderer>();
        filter.sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        render.sharedMaterial = prefab.GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = prefab.transform.localScale;
    }
}
