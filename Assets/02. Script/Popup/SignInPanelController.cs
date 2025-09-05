using TMPro;
using UnityEngine;

public struct SignInData
{
    public string username;
    public string password;
}

public struct SignInResult
{
    public int result;
}

public class SignInPanelController : PanelController
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField pwInputField;

    public void OnClickConfirmButton()
    {
        string id = idInputField.text;
        string pw = pwInputField.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            Shake();
            GameManager.Instance.OpenConfirmPanel("ID 또는 Password가 입력되지 않았습니다.", 
                () => 
                {

                });

            return;
        }

        var signInData = new SignInData();
        signInData.username = id;
        signInData.password = pw;

        StartCoroutine(NetworkManager.Instance.SignIn(signInData, 
            () => 
            {
                Hide();
            }, 
            (result) => 
            {
                Shake();
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("ID가 유효하지 않습니다.",
                        () =>
                        {
                            idInputField.text = "";
                            pwInputField.text = "";
                        });
                }
                else if (result == 1)
                {
                    GameManager.Instance.OpenConfirmPanel("Password가 유효하지 않습니다.",
                        () =>
                        {
                            pwInputField.text = "";
                        });
                }
            })); 
    }

    public void OnClickCloseButton()
    {
        Hide();
    }

    public void OnClickSignUpButton()
    {
        GameManager.Instance.OpenSignUpPanel();
    }
}
