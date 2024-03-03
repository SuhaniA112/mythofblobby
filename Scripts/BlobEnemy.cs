using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlobEnemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2.0f;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private float detectionRange = 10.0f;
    [SerializeField] private float detectionTime = 2.0f;

    private GameObject player;
    private Vector2 direction;
    private float distance;

    private float originalDetectionTime;
    private bool isDetected = false;
    private bool viewBlocked = true;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.gameObject;

        originalDetectionTime = detectionTime;
        detectionTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetection();
    }
    private void CheckDetection()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, distance, obstacleLayerMask);
        if (ray.collider == null)
        {
            viewBlocked = false;
        }
        else
        {
            viewBlocked = true;
        }

        if (distance < detectionRange && !viewBlocked)
        {
            isDetected = true;
        }
        else
        {
            isDetected = false;
        }

        if (isDetected)
        {
            detectionTime = originalDetectionTime;
        }
        if (detectionTime > 0.0f)
        {
            detectionTime -= Time.deltaTime;
            MoveEnemy();
        }
    }
    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
}
