using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject NormalZombiePrefab;
    public GameObject GiantZombiePrefab;
    public GameObject RunnerZombiePrefab;
    public GameObject MinerZombiePrefab;
    public Transform zombiesStorage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnZombie(int type)
    {
        //Debug.Log("Create Zombie: " + type);
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
            case 3:
                zombie = Instantiate(MinerZombiePrefab, zombiesStorage.position, zombiesStorage.rotation);
                break;
        }

        zombie.transform.SetParent(zombiesStorage);
        //zombie.transform.localPosition = new Vector3(0, 0, 0);
        //zombie.transform.rotation = Quaternion.identity;
        zombie.tag = "Enemy";
        zombie.layer = LayerMask.NameToLayer("zombies");
    }

    public bool IsZombieEmpty()
    {
        Transform zombie = transform.GetChild(0);
        //if (zombie.childCount == 0) Debug.Log("can't find zombie");
        return zombie.childCount == 0;
    }
}
