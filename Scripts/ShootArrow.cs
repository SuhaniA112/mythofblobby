using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 2.0f;
    private float despawnTime = 5.0f;

    private GameObject player;
    private Vector3 arrowDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.gameObject;
        arrowDirection = MasterScript.Instance.inputDirection;

        transform.position = player.transform.position + arrowDirection;
        if (arrowDirection.x == -1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90.0f);
        }
        else if (arrowDirection.y == -1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        }
        else if (arrowDirection.x == 1.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90.0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += arrowDirection * arrowSpeed * Time.deltaTime;
        despawnTime -= Time.deltaTime;
        if (despawnTime <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        print(damageable);
        if (damageable != null || collision.collider.gameObject.layer == 7)
        {
            int arrowDamage = 1;
            damageable.TakeDamage(arrowDamage);
            //Instantiate(hitParticleTransform, raycastHit.collider.transform.position, Quaternion.identity);
        }
        GameObject.Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        print(damageable);
        if (damageable != null || collision.gameObject.layer == 7)
        {
            int arrowDamage = 1;
            damageable.TakeDamage(arrowDamage);
            //Instantiate(hitParticleTransform, raycastHit.collider.transform.position, Quaternion.identity);
        }
        GameObject.Destroy(gameObject);
    }
}
