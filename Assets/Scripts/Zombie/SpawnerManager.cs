using UnityEditor.ShortcutManagement;
using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour
{
    public int level = 5;
    public float spawnInterval = 5f;
    public int NormalRate;
    public int GiantRate;
    public int RunnerRate;
    public ZombieSpawner spawner_zero;
    public ZombieSpawner spawner_one;
    public ZombieSpawner spawner_two;
    public ZombieSpawner spawner_three;
    public ZombieSpawner spawner_four;
    public float waveDuration;
    ///////////////////////////////////////
    private float Timer = 0f;
    private float spawnTimer = 0f;
    private int[] rows = { 0, 1, 2, 3, 4 };
    private int cardinalNum;
    private bool isWave = false;
    private int waveTimes = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GiantRate += NormalRate;
        RunnerRate += GiantRate;
        cardinalNum = RunnerRate;
        int zombiesLayer = LayerMask.NameToLayer("zombies");
        Physics.IgnoreLayerCollision(zombiesLayer, zombiesLayer, true);
        Debug.Log("Ignore zombie layer collision");
    }

    // Update is called once per frame
    void Update()
    {
        if (waveTimes < 3) // 3波結束後就不生成殭屍了
        {
            if (Timer >= 60f && waveTimes < 3)
            {
                isWave = true;
                StartCoroutine(waveSpawn());
                Timer = 0f;
                waveTimes++;
                Debug.Log("Wave 開始" + waveTimes);
            }
            if (spawnTimer >= spawnInterval && !isWave)
            {
                SpawnZombie();
                spawnTimer = 0f;
            }
            Timer += Time.deltaTime;
            if (!isWave) spawnTimer += Time.deltaTime;
        }
    }

    private void SpawnZombie()
    {
        Shuffle(rows);
        
        int zombieNum = (level < 5) ? level: 5; // zombie上限為5隻

        for (int i = 0; i < zombieNum; i++)
        {
            int randomTypeNum = Random.Range(0, cardinalNum);
            int type = 0; // 0 normal // 1 giant // 2 runner
            if (randomTypeNum < NormalRate)
                type = 0;
            else if (randomTypeNum < GiantRate)
                type = 1;
            else if (randomTypeNum < RunnerRate)
                type = 2;

            int rowIndex = rows[i];
            switch(rowIndex)
            {
                case 0:
                    spawner_zero.SpawnZombie(type);
                    break;
                case 1:
                    spawner_one.SpawnZombie(type);
                    break;
                case 2:
                    spawner_two.SpawnZombie(type);
                    break;
                case 3:
                    spawner_three.SpawnZombie(type);
                    break;
                case 4:
                    spawner_four.SpawnZombie(type);
                    break;
            }
        }
    }

    private void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    IEnumerator waveSpawn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < waveDuration)
        {
            SpawnZombie();
            yield return new WaitForSeconds(1f); // 等待1秒
            elapsedTime += 1f;
        }

        Debug.Log("wave結束");
        isWave = false;
    }
}
