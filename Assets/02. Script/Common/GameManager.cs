using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ConfirmPanelController;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private GameObject signInPanel;
    [SerializeField] private GameObject signUpPanel;
    [SerializeField] private GameObject signInObj;
    [SerializeField] private GameObject signOutObj;

    [SerializeField] private TextMeshProUGUI showId;
    
    private Constants.GameType _gameType;
    
    private Canvas _canvas;
    
    // Game Logic
    private GameLogic _gameLogic;
    
    private GameUIController _gameUIController;

    internal static void OpenConfirmPanel(string v)
    {
        throw new NotImplementedException();
    }

    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    
    public void OpenConfirmPanel(string message, 
        ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    public void OpenSignInPanel()
    {
        if (_canvas != null)
        {
            var signInPanelObject = Instantiate(signInPanel, _canvas.transform);
            signInPanelObject.GetComponent<SignInPanelController>().Show();
        }
    }

    public void OpenSignUpPanel()
    {
        if (_canvas != null)
        {
            var signUpPanelObject = Instantiate(signUpPanel, _canvas.transform);
            signUpPanelObject.GetComponent<SignUpPanelController>().Show();
        }
    }


    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }

            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }

    public void LogInStatus()
    {
        signInObj.SetActive(!signInObj.activeSelf);
        signOutObj.SetActive(!signInObj.activeSelf);
    }
}
