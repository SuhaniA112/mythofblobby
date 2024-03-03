using UnityEngine;

public class Coin : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ScoreManager.Instance.UpdateScore();
            Destroy(gameObject);
        }
    }
}
