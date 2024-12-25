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
        foreach (GameObject z in Zombies)
        {
            Physics.IgnoreCollision(z.transform.Find("Zombie").gameObject.GetComponent<Collider>(), 
                                  zombie.transform.Find("Zombie").gameObject.GetComponent<Collider>());
        }
        Zombies.AddLast(zombie);
    }
}
