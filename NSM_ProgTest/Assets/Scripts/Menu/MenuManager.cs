using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    protected Button buttonToChangeScene;
    [SerializeField]
    protected Button buttonToExitGame;
    [SerializeField]
    protected readonly int sceneIndex;
    void Awake()
    {
        buttonToChangeScene.onClick.AddListener(NextScene);
        buttonToExitGame.onClick.AddListener(ExitGame);        
    }
    void NextScene() => SceneManager.LoadSceneAsync(sceneIndex + 1, LoadSceneMode.Single);
    void ExitGame() => Application.Quit();
}




