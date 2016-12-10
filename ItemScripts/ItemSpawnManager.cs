using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawnManager : NetworkBehaviour
{

    //var voor spawnen items
    public Transform[] Spawnpoints;
    public GameObject[] Items;
    public int MinTimeBetweenSpawns;
    public int MaxTimeBetweenSpawns;

    private Transform CurrentSpawnpoint;
    private GameObject CurrentItem;
    private GameObject SpawnableObject;

    private bool IsGenerating = true;

    [HideInInspector]
    static public List<string> AvailableSpawnpoints = new List<string>();

    [HideInInspector]
    static public GameObject RandomSpawnpointObject;

    void Start()
    {
        SyncAvailableSpawnpoints();
        if (IsGenerating == true)
            CmdRandomGenerator();
    }
    void Update()
    {
        Check();
    }
    void SyncAvailableSpawnpoints()
    {
        for (int i = 0; i < Spawnpoints.Length; i++)
        {
            AvailableSpawnpoints.Add(Spawnpoints[i].name);
        }
    }
    void Check()
    {
        if (AvailableSpawnpoints.Count == 1)
        {
            IsGenerating = false;
        }
        else
        {
            IsGenerating = true;
        }

    }
    [Command]
    void CmdRandomGenerator()
    {
        if (IsGenerating == true)
        {
            RandomSpawnpointObject = GameObject.FindGameObjectWithTag(AvailableSpawnpoints[Random.Range(1, AvailableSpawnpoints.Count)]);

            CurrentSpawnpoint = RandomSpawnpointObject.transform;
        }
        CurrentItem = Items[Random.Range(1, Items.Length)];

        StartCoroutine(SpawnItems());

    }
    [Client]
    IEnumerator SpawnItems()
    {
        yield return new WaitForSeconds(Random.Range(MinTimeBetweenSpawns, MaxTimeBetweenSpawns));

        Spawn();

        if (IsGenerating == true)
            CmdRandomGenerator();
    }
    void Spawn()
    {
        SpawnableObject = (GameObject)Instantiate(CurrentItem, CurrentSpawnpoint.transform.position, CurrentItem.transform.rotation);
        NetworkServer.Spawn(SpawnableObject);
    }

}
