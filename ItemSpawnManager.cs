using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemSpawnManager : NetworkBehaviour {

    //var voor spawnen items
    public Transform[] Spawnpoints;
    public GameObject[] Items;
    public int MinTimeBetweenSpawns;
    public int MaxTimeBetweenSpawns;

    private Transform CurrentSpawnpoint;
    private GameObject CurrentItem;

    private bool IsGenerating = true;

    void Start()
    {
        if(IsGenerating == true)
        StartCoroutine(RandomGenerator());
    }

    IEnumerator RandomGenerator ()
    {
        CurrentSpawnpoint = Spawnpoints[Random.Range(1, Spawnpoints.Length)];
        CurrentItem = Items[Random.Range(1, Items.Length)];
        Debug.Log("CurrenSpawnpoint: " + CurrentSpawnpoint + ", CurrentItem: " + CurrentItem);

        yield return new WaitForSeconds(Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns));
        Debug.Log("Spawning Object");

        CmdSpawnItems();

        if(IsGenerating == true)
        StartCoroutine(RandomGenerator());
    }
    [Command]
    void CmdSpawnItems()
    {
        SpawnItems(CurrentItem, CurrentSpawnpoint);
    }
    [Client]
    void SpawnItems(GameObject _CurrentItem, Transform _CurrentSpawnpoint)
    {
        Instantiate(_CurrentItem, _CurrentSpawnpoint);
    }
}
