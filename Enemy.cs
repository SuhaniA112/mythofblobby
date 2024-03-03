using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {


    [SerializeField] private Transform speedBoostPrefab;
    [SerializeField] private Transform coinPrefab;

    private int health;


    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
            int randomNumber = Random.Range(0, 20);
            if (randomNumber < 2) {
                Instantiate(speedBoostPrefab, transform.position, Quaternion.identity);
            }  
            if (randomNumber != 19) {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            EnemyManager.Instance.IncreaseEnemyKillCount();
        }
    }

    public void SetHealth(int health) {
        this.health = health;
    }
}
