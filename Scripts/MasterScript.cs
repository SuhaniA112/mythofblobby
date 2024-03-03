using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{
    public static MasterScript Instance { get; private set; }
    public Vector3 inputDirection = new Vector3(0, -1, 0);

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection = new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection = new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection = new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection = new Vector3(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputDirection = new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputDirection = new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputDirection = new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputDirection = new Vector3(1, 0, 0);
        }
    }
}
