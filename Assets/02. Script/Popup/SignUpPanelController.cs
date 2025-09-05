using TMPro;
using UnityEngine;

public struct SignUpData
{
    public string username;
    public string password;
    public string nickname;
}

public struct SignUpResult
{
    public int result;
}

public class SignUpPanelController : PanelController
{
    [SerializeField] private TMP_InputField idInputField;
    [SerializeField] private TMP_InputField pwInputField;
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnClickConfirmButton()
    {
        string id = idInputField.text;
        string pw = pwInputField.text;
        string nickname = nicknameInputField.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw) || string.IsNullOrEmpty(nickname))
        {
            Shake();
            GameManager.Instance.OpenConfirmPanel("ID, Password 또는 Nickname이 입력되지 않았습니다.",
                () =>
                {

                });

            return;
        }

        var signUpData = new SignUpData();
        signUpData.username = id;
        signUpData.password = pw;
        signUpData.nickname = nickname;

        StartCoroutine(NetworkManager.Instance.SignUp(signUpData,
            () =>
            {
                Hide();
            },
            (result) =>
            {
                Shake();
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("해당 ID가 이미 존재합니다.",
                        () =>
                        {
                            idInputField.text = "";
                            pwInputField.text = "";
                            nicknameInputField.text = "";
                        });
                }
                if (result == 1)
                {
                    GameManager.Instance.OpenConfirmPanel("해당 Nickname이 이미 존재합니다.",
                        () =>
                        {
                            nicknameInputField.text = "";
                        });
                }
            }));
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
