using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour
{
    public float spawnInterval = 5f;
    public int NormalRate;
    public int GiantRate;
    public int RunnerRate;
    public int MinerRate;
    public ZombieSpawner spawner_zero;
    public ZombieSpawner spawner_one;
    public ZombieSpawner spawner_two;
    public ZombieSpawner spawner_three;
    public ZombieSpawner spawner_four;
    public float waveDuration;
    public float waveInterval;
    public CenterController centerController;
    ///////////////////////////////////////
    private int level;
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
        MinerRate += RunnerRate;
        cardinalNum = MinerRate;
        int zombiesLayer = LayerMask.NameToLayer("zombies");
        Physics.IgnoreLayerCollision(zombiesLayer, zombiesLayer, true);
        level = centerController.level;
        Debug.Log("Ignore zombie layer collision");
    }

    // Update is called once per frame
    void Update()
    {
        if (waveTimes < 3) // 3�i������N���ͦ��L�ͤF
        {
            if (Timer >= (waveInterval - 5f))
            {
                // ��ܤ�r�b���e UI "�@�j�i�L�ͥ��b����"
            }
            if (Timer >= waveInterval && waveTimes < 3)
            {
                // UI "�@�j�i�L�ͥ��b����" �M��
                isWave = true;
                StartCoroutine(waveSpawn());
                Timer = 0f;
                waveTimes++;
                Debug.Log("Wave �}�l" + waveTimes);
            }
            if (spawnTimer >= spawnInterval && !isWave)
            {
                SpawnZombie();
                spawnTimer = 0f;
            }
            Timer += Time.deltaTime;
            if (!isWave) spawnTimer += Time.deltaTime;
        }
        else
        {
            // �����S�L�ͤF �q���@�� level
            //Debug.Log("wait zombie clear" + spawner_zero.IsZombieEmpty() + spawner_one.IsZombieEmpty() + spawner_two.IsZombieEmpty() + spawner_three.IsZombieEmpty() + spawner_four.IsZombieEmpty());
            if (spawner_zero.IsZombieEmpty() && spawner_one.IsZombieEmpty() && spawner_two.IsZombieEmpty() && spawner_three.IsZombieEmpty() && spawner_four.IsZombieEmpty())
            {
                centerController.NextLeveling();
                level = centerController.level;
                waveTimes = 0;
                Timer = 0f;
                spawnTimer = 0f;
                Debug.Log("Level" + level);
            }
        }
    }

    private void SpawnZombie()
    {
        Shuffle(rows);
        
        int zombieNum = (level < 5) ? level: 5; // zombie�W����5��

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
            else if (randomTypeNum < MinerRate)
                type = 3;

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
            yield return new WaitForSeconds(1f); // ����1��
            elapsedTime += 1f;
        }

        Debug.Log("wave����");
        isWave = false;
    }
}
