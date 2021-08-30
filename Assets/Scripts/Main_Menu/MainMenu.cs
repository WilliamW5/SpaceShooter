using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        EditorSceneManager.LoadScene(1); // Game scene
    }
}
