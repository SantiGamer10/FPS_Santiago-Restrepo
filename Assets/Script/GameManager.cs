using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waveTime = 10;
    [SerializeField] private int enemyPerWave = 10;
    [SerializeField] private List<Enemy> _enemyListPrefab = new List<Enemy>();
    [SerializeField] private HealthPoints generatorHP;
    [SerializeField] private Transform generatorPosition;
    [SerializeField] private List<EnemySpawnerController> spawnList = new List<EnemySpawnerController>();
    [SerializeField] private float timeBetweenSpawns = 1;
    [Header("GameOver data")]
    [SerializeField] private BoolData winData;
    [SerializeField] private string victorySceneName = "VictoryMenu";
    private List<Enemy> enemyList = new();
    private int enemiesDie = 0;

    private void OnEnable()
    {
        generatorHP.dead += HandleGeneratorDie;    
    }

    private void OnDisable()
    {
        generatorHP.dead -= HandleGeneratorDie;
    }

    private void Awake()
    {
        Validate();
    }

    private void Start()
    {
        StartCoroutine(WaveStart());
    }

    private IEnumerator WaveStart()
    {
        yield return new WaitForSeconds(waveTime);
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < enemyPerWave; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            int spawnIndex = RandomIndexSpawn();

            Enemy enemy = spawnList[spawnIndex].SpawnEnemy(generatorPosition);
            if (enemy.TryGetComponent<HealthPoints>(out HealthPoints hp))
            {
                hp.dead += HandleEnemiesDie;
            }
            enemyList.Add(enemy);
        }
    }

    private void HandleEnemiesDie()
    {
        enemiesDie++;
        
        if (enemiesDie >= enemyPerWave)
        {
            WinOrLoseLogic(true);
        }
    }

    private void HandleGeneratorDie()
    {
        WinOrLoseLogic(false);
    }

    private void WinOrLoseLogic(bool isWinning)
    {
        winData._boolData = isWinning;
        SceneManager.LoadScene(victorySceneName);
    }

    private int RandomIndexSpawn()
    {
        return Random.Range(0, spawnList.Count);
    }

    private void Validate()
    {
        if (!generatorPosition)
        {
            Debug.LogError($"{name}: Generator position is null\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!generatorHP)
        {
            Debug.LogError($"{name}: Generator HP is null\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (spawnList.Count == 0)
        {
            Debug.LogError($"{name}: Spawn is null\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
        if (!winData)
        {
            Debug.LogError($"{name}: WinData is null\nPlease check and assign one.\nDisabled component.");
            enabled = false;
            return;
        }
    }
}