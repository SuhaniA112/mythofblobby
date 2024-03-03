using UnityEngine;

public class EnemyManager : MonoBehaviour {


    public static EnemyManager Instance { get; private set; }


    [SerializeField] private Transform EnemyPrefab;
    [SerializeField] private Transform EnemySpawns;


    private int enemyHealthBonus;
    private int enemyKillCount;
    private int meleeEnemyHealth = 3;
    private float spawnTimer;
    private float spawnTimerMax = 5;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        enemyHealthBonus = 0;
        enemyKillCount = 1;
        spawnTimer = spawnTimerMax;
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0) {
            spawnTimer = spawnTimerMax;
            Transform enemyTransform = Instantiate(EnemyPrefab, EnemySpawns.GetChild(Random.Range(0, EnemySpawns.childCount)).position, Quaternion.identity);
            enemyTransform.GetComponent<Enemy>().SetHealth(meleeEnemyHealth + enemyHealthBonus);
        }
    }

    public void IncreaseEnemyKillCount() {
        enemyKillCount++;
        enemyHealthBonus = enemyKillCount / 5;
    }
}
