using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;

    private Constants.GameType _gameType;

    private Canvas canvas;

    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message)
    {
        if (canvas != null)
        {
            var confirmPanelObj = Instantiate(confirmPanel, canvas.transform);
            confirmPanelObj.GetComponent<ConfirmPanelController>();
        }
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        canvas = FindFirstObjectByType<Canvas>();
    }
}
