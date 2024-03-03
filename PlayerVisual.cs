using UnityEngine;

public class PlayerVisual : MonoBehaviour {


    private const string ON_SWORD_ATTACK_UP = "OnSwordAttackUp";
    private const string ON_SWORD_ATTACK_DOWN = "OnSwordAttackDown";
    private const string ON_SWORD_ATTACK_RIGHT = "OnSwordAttackRight";


    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnPlayerSwordAttack += Player_OnPlayerSwordAttack;
    }

    private void Player_OnPlayerSwordAttack(object sender, System.EventArgs e) {
        Vector2 attackDirection = Player.Instance.GetAttackDirection();
        if (attackDirection.y == 0) {
            animator.SetTrigger(ON_SWORD_ATTACK_RIGHT);
        } else if (attackDirection.y > 0) {
            animator.SetTrigger(ON_SWORD_ATTACK_UP);
        } else if (attackDirection.y < 0) {
            animator.SetTrigger(ON_SWORD_ATTACK_DOWN);
        }
    }
}
