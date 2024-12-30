using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject NormalZombiePrefab;
    public GameObject GiantZombiePrefab;
    public GameObject RunnerZombiePrefab;
    public Transform zombiesStorage;
    public float OriginalSpawnTime;
    public int NormalRate;
    public int GiantRate;
    public int RunnerRate;
    private float Timer;
    private int cardinalNum;
    private Type type;
    private LinkedList<GameObject> Zombies;

    private enum Type
    {
        Normal, 
        Giant, 
        Runner, 
    }
    void Start()
    {
        Timer = 0f;
        GiantRate += NormalRate;
        RunnerRate += GiantRate;
        cardinalNum = RunnerRate;
        Zombies = new LinkedList<GameObject>();

        int zombiesLayer = LayerMask.NameToLayer("zombies");
        Physics.IgnoreLayerCollision(zombiesLayer, zombiesLayer, true);
        Debug.Log("Ignore zombie layer collision");
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > OriginalSpawnTime)
        {
            Timer = 0f;
            int randomTypeNum = Random.Range(0, cardinalNum);
            if (randomTypeNum < NormalRate)
            {
                type = Type.Normal;
            }
            else if (randomTypeNum < GiantRate)
            {
                type = Type.Giant;
            }
            else if (randomTypeNum < RunnerRate)
            {
                type = Type.Runner;
            }
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        Debug.Log("Create Zombie: " + type);
        GameObject zombie = null;
        switch(type)
        {
            case Type.Normal:
                zombie = Instantiate(NormalZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
            case Type.Giant:
                zombie = Instantiate(GiantZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
            case Type.Runner:
                zombie = Instantiate(RunnerZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
        }

        zombie.transform.SetParent(zombiesStorage);
        //zombie.transform.localPosition = new Vector3(0, 0, 0);
        //zombie.transform.rotation = Quaternion.identity;
        zombie.tag = "Enemy";
        zombie.layer = LayerMask.NameToLayer("zombies");
        Zombies.AddLast(zombie);
    }

    public void RemoveZombieFromLinkedList(GameObject zombie) 
    {
        if (Zombies.Contains(zombie))
        {
            Zombies.Remove(zombie);
        }
        else
        {
            Debug.Log("There isn't have the target zombie");
        }
    }
}
