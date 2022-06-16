using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tile_prefabs;
    public float z_spawn = 0;
    public float tile_length = 30;
    public int num_tile = 5;

    private List<GameObject> active_tile = new List<GameObject>();
    
    public Transform player;
    void Start()
    {
        for(int i=0; i<num_tile; i++)
        {
            if(i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0,tile_prefabs.Length));
        }
    }

    void Update()
    {
        if(player.position.z - 35 > z_spawn - (num_tile * tile_length))
        {
            SpawnTile(Random.Range(0, num_tile));
            DeleteTile();
        }
    }
    
    public void SpawnTile(int tile_ind)
    {
        GameObject go = Instantiate(tile_prefabs[tile_ind], transform.forward * z_spawn , transform.rotation);
        active_tile.Add(go);
        z_spawn += tile_length;
    }
    private void DeleteTile()
    {
        Destroy(active_tile[0]);
        active_tile.RemoveAt(0);
    }
}
