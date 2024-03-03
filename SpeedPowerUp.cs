using UnityEngine;

public class SpeedPowerUp : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Player>().BoostSpeed();
            Destroy(gameObject);
        }
    }
}
