using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScript : MonoBehaviour
{
    public void GameScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
