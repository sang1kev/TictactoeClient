using TMPro;
using UnityEngine;

public class ConfirmPanelController : PannelController
{
    [SerializeField] private TMP_Text messageText;

    public void Show(string message)
    {
        messageText.text = message;
        base.Show();
    }

    public void OnClickConfirmButton()
    {
        GameManager.Instance.ChangeToMainScene();
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
