using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public string sceneName;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) Debug.LogError("ERROR: NO GAME MANAGER exists in scene.");
            return instance;
        }
    }

    private void Awake() => instance = this;

    // Load Scene Manager
    public void LoadScene() => SceneManager.LoadScene(sceneName);
    
}
