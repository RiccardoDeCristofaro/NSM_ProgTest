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
    protected Button buttonToHelp;
    [SerializeField]
    protected readonly int sceneIndex;
    [SerializeField]
    protected private GameObject HelpInfo;
    void Awake()
    {
        buttonToChangeScene.onClick.AddListener(NextScene);
        buttonToExitGame.onClick.AddListener(ExitGame);
        buttonToHelp.onClick.AddListener(HelpGame);
        
    }

    private void HelpGame() => HelpInfo.gameObject.SetActive(true);

    void NextScene() => SceneManager.LoadSceneAsync(sceneIndex + 1, LoadSceneMode.Single);
    void ExitGame() => Application.Quit();
}




