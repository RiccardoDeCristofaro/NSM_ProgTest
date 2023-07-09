using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] protected GameObject panelEndGame;
    [SerializeField] protected Button Restartbutton;
    [SerializeField] protected Button QuitEndButton;

    [SerializeField] protected GameObject panelPauseGame;
    [SerializeField] protected Button ResumeGame;
    [SerializeField] protected Button Settings;
    [SerializeField] protected Button ExitPauseButton;
    [SerializeField] protected Button QuitFromPause;
    [SerializeField] protected Button YouWonButton;
    [SerializeField] protected Button ExitwonGame;
 
    private Scene currentScene;
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();      
        Restartbutton.onClick.AddListener(RestartScene);
        QuitEndButton.onClick.AddListener(QuitGame);

        ResumeGame.onClick.AddListener(UnPause);
        ExitPauseButton.onClick.AddListener(QuitGame);    
        QuitFromPause.onClick.AddListener(UnPause);

        YouWonButton.onClick.AddListener(RestartScene);
        ExitwonGame.onClick.AddListener(QuitGame);
    }
    private void Update()
    {
        Pause();
    }
    void RestartScene() => SceneManager.LoadSceneAsync(currentScene.buildIndex, LoadSceneMode.Single);
    void QuitGame() => SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    void Pause()
    {
        if (currentScene.buildIndex >= 1)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0.0f;
                panelPauseGame.SetActive(true);
            }
    }
    void UnPause()
    {
        Time.timeScale = 1.0f;
        panelPauseGame.SetActive(false);
    }
}
