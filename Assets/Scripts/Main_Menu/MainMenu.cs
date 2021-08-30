using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSingleGame()
    {
        EditorSceneManager.LoadScene(1); // Single-Player Game scene
    }

    public void LoadCoOpGame()
    {
        EditorSceneManager.LoadScene(2); // Single-Player Game scene
    }

}
