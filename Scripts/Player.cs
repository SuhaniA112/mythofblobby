using System;
using UnityEngine;

public class Player : MonoBehaviour {


    public static Player Instance { get; private set; }


    public event EventHandler OnPlayerSwordAttack;


    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer swordSpriteRenderer;
    [SerializeField] private Transform hitParticleTransform;

    private Vector2 faceDirection;
    private float moveSpeed = 5;
    private float cooldownTimer;
    private float speedBoostTimer;

    private float health = 5;

        
    private void Awake() {
        Instance = Instance == null ? this : throw new Exception($"There is more than one Instance of {Instance}!");
    }

    private void Start() {
        GameInput.Instance.OnPlayerSwordAttack += GameInput_OnPlayerSwordAttack;

        faceDirection = Vector2.right;
    }

    private void GameInput_OnPlayerSwordAttack(object sender, EventArgs e) {
        if (cooldownTimer > 0) {
            return;
        }

        float cooldownMax = 0.34f;
        cooldownTimer = cooldownMax;

        float attackRange = 1.75f;
        Vector2 hitboxSize = Vector2.one * 1.3f;
        RaycastHit2D[] attackRaycastArray = Physics2D.BoxCastAll(transform.position, hitboxSize, 0f, GetAttackDirection(), attackRange, enemyLayerMask);
        foreach (RaycastHit2D raycastHit in attackRaycastArray) {
            IDamageable damageable = raycastHit.collider.GetComponent<IDamageable>();
            if (damageable != null) {
                int swordDamage = 1;
                damageable.TakeDamage(swordDamage);
                Instantiate(hitParticleTransform, raycastHit.collider.transform.position, Quaternion.identity);
            }
        }

        OnPlayerSwordAttack?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        HandleMove();

        if (cooldownTimer > 0) {
            cooldownTimer -= Time.deltaTime;
        }

        if (speedBoostTimer < 0) {
            moveSpeed = 5;
        } else {
            speedBoostTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            Application.Quit();
        }
    }

    private void HandleMove() {
        Vector2 moveDirection = GameInput.Instance.GetMoveVectorNormalized();
        faceDirection = moveDirection != Vector2.zero ? moveDirection : faceDirection;

        if (faceDirection.y > 0) {
            playerSpriteRenderer.sprite = backSprite;
            transform.localScale = Vector2.one;
        } else if (faceDirection.y < 0) {
            playerSpriteRenderer.sprite = frontSprite;
            transform.localScale = new Vector2(-1, 1);
        } else {
            playerSpriteRenderer.sprite = rightSprite;
            if (faceDirection.x > 0) {
                transform.localScale = Vector2.one;
            }
            if (faceDirection.x < 0) {
                transform.localScale = new Vector2(-1, 1);
            }
        }

        //float obstacleCheckDistance = .03f;
        //Vector2 playerSize = new Vector2(0.8f, 0.8f);

        //RaycastHit2D obstacleCheckRayCastX = Physics2D.BoxCast(transform.position, playerSize, 0f, Vector2.right * moveDirection.x, obstacleCheckDistance, obstacleLayerMask);
        //if (obstacleCheckRayCastX.collider != null) {
        //    moveDirection.x = 0;
        //}

        //RaycastHit2D obstacleCheckRayCastY = Physics2D.BoxCast(transform.position, playerSize, 0f, Vector2.up * moveDirection.y, obstacleCheckDistance, obstacleLayerMask);
        //if (obstacleCheckRayCastY.collider != null) {
        //    moveDirection.y = 0;
        //}

        transform.position = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;
    }

    public Vector2 GetAttackDirection() {
        Vector2 attackDirection;
        if (faceDirection.y != 0) {
            attackDirection = faceDirection.y > 0 ? Vector2.up : Vector2.down;
        } else {
            attackDirection = faceDirection.x > 0 ? Vector2.right : Vector2.left;
        }
        return attackDirection;
    }

    public void BoostSpeed() {
        moveSpeed += 2;
        speedBoostTimer = 10;
    }
}
