using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject visual;
    [SerializeField] private float arrowCooldown = 0.75f;
    private Vector3 bowDirection;

    private float bowAnimationTime = 0.5f;
    private float originalBowAnimationTime;

    private float originalArrowCooldown;
    private bool rotateToggle = true;

    // Start is called before the first frame update
    void Start()
    {
        originalArrowCooldown = arrowCooldown;
        originalBowAnimationTime = bowAnimationTime;
        visual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateToggle)
        {
            rotateToggle = false;
            bowDirection = MasterScript.Instance.inputDirection;
            if (bowDirection.y == 1.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (bowDirection.x == -1.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90.0f);
            }
            else if (bowDirection.y == -1.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180.0f);
            }
            else if (bowDirection.x == 1.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90.0f);
            }
        }
        


        arrowCooldown -= Time.deltaTime;
        bowAnimationTime -= Time.deltaTime;
        if (arrowCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                visual.SetActive(true);
                bowAnimationTime = originalBowAnimationTime;

                Instantiate(arrow);

                arrowCooldown = originalArrowCooldown;
                rotateToggle = true;
            }
        }
        if (bowAnimationTime < 0)
        {
            visual.SetActive(false);
        }
    }
}
