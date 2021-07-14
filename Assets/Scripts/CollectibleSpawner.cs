using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public Collectible collectibleTemplete; //if we want more then one type of collectible, we will put them here to initialize.
    private Queue<Collectible> collectiblePool = new Queue<Collectible>(); //collectible pool, taken collectibles will not deleted for future use.
    private List<Collectible> activeCollectibles = new List<Collectible>(); //we will track our active collectibles from here. (max count in the scene will be 5 etc.)

    public Rect spawnArea;
    public int initialCollectibleCount = 5;
    public int maxCollectibleCount = 5;
    public float timeToSpawnNewCollectible = 3f; //in Seconds -> TODO: Randomize this too...
    public int maxSpawnCountPerTime = 1;  // TODO: Randomize this too...
    public float minDistanceToPlayer = 1f; //Min distance to player to spawn..
    private float timer = 0;

    public void ApplyInitialSpawn()
    {

        List<Collectible> collectibleHolder = new List<Collectible>();
        collectibleHolder.AddRange(activeCollectibles);

        foreach (var activeCollectible in collectibleHolder)
        {
            activeCollectible.Die();
        }

        SpawnCollectible(initialCollectibleCount);

    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > timeToSpawnNewCollectible && activeCollectibles.Count < maxCollectibleCount)
        {
            SpawnCollectible(maxSpawnCountPerTime);
            timer = 0;
        }

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(spawnArea.xMin, 0.1f, spawnArea.yMin), new Vector3(spawnArea.xMin, 0.1f, spawnArea.yMax));
        Gizmos.DrawLine(new Vector3(spawnArea.xMin, 0.1f, spawnArea.yMin), new Vector3(spawnArea.xMax, 0.1f, spawnArea.yMin));
        
        Gizmos.DrawLine(new Vector3(spawnArea.xMax, 0.1f, spawnArea.yMax), new Vector3(spawnArea.xMin, 0.1f, spawnArea.yMax));
        Gizmos.DrawLine(new Vector3(spawnArea.xMax, 0.1f, spawnArea.yMax), new Vector3(spawnArea.xMax, 0.1f, spawnArea.yMin));

    }

    public void AddToPool(Collectible collectible)
    {
        collectiblePool.Enqueue(collectible);
        activeCollectibles.Remove(collectible);
    }

    private Collectible GetCollectible()
    {
        if (collectiblePool.Count > 0)
        {
            return collectiblePool.Dequeue();
        }
        Collectible collectible = Instantiate(collectibleTemplete, transform);
        collectible.spawner = this;
        return collectible;
    }

    public void SpawnCollectible(int count)
    {
        //TODO: check player pos. to not spawn near him.
        for (int i = 0; i < count; i++)
        {
            Collectible collectible = GetCollectible();
            activeCollectibles.Add(collectible);

            Vector3 spawnPoint = new Vector3(Random.Range(spawnArea.xMin, spawnArea.xMax), 0, Random.Range(spawnArea.yMin, spawnArea.yMax));

            int maxIteration = 999;
            int currentIteration = 0;
            while(Vector3.Distance(spawnPoint,LevelManager.Instance.player.transform.position) < minDistanceToPlayer)
            {
                spawnPoint = new Vector3(Random.Range(spawnArea.xMin, spawnArea.xMax), 0, Random.Range(spawnArea.yMin, spawnArea.yMax));
                currentIteration++;

                if (currentIteration > maxIteration) break;
            }

            collectible.Spawn(spawnPoint);
        }       
    }

}
