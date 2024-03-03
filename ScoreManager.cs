using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {


    public static ScoreManager Instance { get; private set; }


    [SerializeField] private TextMeshProUGUI scoreText;


    private int score;


    private void Awake() {
        Instance = this;
    }

    public void UpdateScore() {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
