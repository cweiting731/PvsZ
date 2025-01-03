using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject NormalZombiePrefab;
    public GameObject GiantZombiePrefab;
    public GameObject RunnerZombiePrefab;
    public Transform zombiesStorage;
    //public float OriginalSpawnTime;
    //public int NormalRate;
    //public int GiantRate;
    //public int RunnerRate;
    //public Type type;
    //private float Timer;
    //private int cardinalNum;
    private LinkedList<GameObject> Zombies;
    //public enum Type
    //{
    //    Normal,
    //    Giant,
    //    Runner,
    //}

    void Start()
    {
        //Timer = 0f;
        //GiantRate += NormalRate;
        //RunnerRate += GiantRate;
        //cardinalNum = RunnerRate;
        Zombies = new LinkedList<GameObject>();

        //int zombiesLayer = LayerMask.NameToLayer("zombies");
        //Physics.IgnoreLayerCollision(zombiesLayer, zombiesLayer, true);
        //Debug.Log("Ignore zombie layer collision");
    }

    // Update is called once per frame
    void Update()
    {
        //Timer += Time.deltaTime;
        //if (Timer > OriginalSpawnTime)
        //{
        //    Timer = 0f;
        //    int randomTypeNum = Random.Range(0, cardinalNum);
        //    if (randomTypeNum < NormalRate)
        //    {
        //        type = Type.Normal;
        //    }
        //    else if (randomTypeNum < GiantRate)
        //    {
        //        type = Type.Giant;
        //    }
        //    else if (randomTypeNum < RunnerRate)
        //    {
        //        type = Type.Runner;
        //    }
        //    SpawnZombie(type);
        //}
    }

    public void SpawnZombie(int type)
    {
        Debug.Log("Create Zombie: " + type);
        GameObject zombie = null;
        switch(type)
        {
            case 0:
                zombie = Instantiate(NormalZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
            case 1:
                zombie = Instantiate(GiantZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
            case 2:
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
