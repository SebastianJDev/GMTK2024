using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
    private (string name, string id) UserInfo;
    [SerializeField] private TMP_InputField InputName;
    [SerializeField] private UnityEvent OnRegisterNewUser;

    private void Awake()
    {
        PlayFabSettings.TitleId = "3DA83";
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("id") && PlayerPrefs.HasKey("UserName"))
        {
            UserInfo.id = PlayerPrefs.GetString("id");
            UserInfo.name = PlayerPrefs.GetString("UserName");
            Debug.Log("ID y Nombre recuperados: " + UserInfo.id + ", " + UserInfo.name);
            if (!string.IsNullOrEmpty(UserInfo.id) && !string.IsNullOrEmpty(UserInfo.name))
            {
                RegisterOrLoginUser();
            }
            else
            {
                OnRegisterNewUser.Invoke();
            }
        }
        else
        {
            OnRegisterNewUser.Invoke();
        }
    }

    public void GuardarNombreUsuario()
    {
        UserInfo.name = InputName.text;
        PlayerPrefs.SetString("UserName", UserInfo.name);
        UserInfo.id = GenerateCustomId(); // Genera un ID único adecuado
        PlayerPrefs.SetString("id", UserInfo.id);
        RegisterNewUser();
    }

    private void RegisterOrLoginUser()
    {
        Debug.Log("Intentando iniciar sesión con ID: " + UserInfo.id);
        string username = UserInfo.id.Substring(0, UserInfo.id.Length / 2);
        var request = new LoginWithPlayFabRequest
        {
            TitleId = PlayFabSettings.TitleId,
            Username = username,
            Password = username
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Inicio de sesión exitoso: " + result.PlayFabId);
        if (string.IsNullOrEmpty(UserInfo.name))
        {
            UpdateDisplayName(); // Actualiza el nombre de usuario si no está establecido
        }
        CheckIfUserExists(result.PlayFabId);
    }

    private void OnLoginError(PlayFabError error)
    {
        Debug.LogError("Error al iniciar sesión: " + error.GenerateErrorReport());
        if (error.Error == PlayFabErrorCode.AccountNotFound || error.Error == PlayFabErrorCode.InvalidParams)
        {
            Debug.LogError("Cuenta no encontrada o parámetros inválidos. Intentando registrar...");
            OnRegisterNewUser.Invoke();
        }
        else
        {
            Debug.LogError("Error desconocido: " + error.GenerateErrorReport());
        }
    }

    private void RegisterNewUser()
    {
        string username = UserInfo.id.Substring(0, UserInfo.id.Length / 2);
        Debug.Log("Registrando nuevo usuario con ID: " + UserInfo.id);

        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Password = username,
            DisplayName = UserInfo.name,
            RequireBothUsernameAndEmail = false,
            TitleId = PlayFabSettings.TitleId
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterErrors);
    }

    private void OnRegisterErrors(PlayFabError error)
    {
        Debug.LogError("Error al registrar: " + error.GenerateErrorReport());
        switch (error.Error)
        {
            case PlayFabErrorCode.UsernameNotAvailable:
                Debug.LogError("Este nombre de usuario ya existe");
                break;
            case PlayFabErrorCode.InvalidParams:
                Debug.LogError("Parámetros inválidos");
                break;
            default:
                Debug.LogError("Error desconocido: " + error.GenerateErrorReport());
                break;
        }
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registro exitoso: " + result.PlayFabId);
        UpdateDisplayName(); // Actualiza el nombre de usuario después del registro
        SceneManager.LoadScene(1);
    }

    private void UpdateDisplayName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = UserInfo.name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateDisplayNameSuccess, OnUpdateDisplayNameError);
    }

    private void OnUpdateDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Nombre de visualización actualizado exitosamente.");
    }

    private void OnUpdateDisplayNameError(PlayFabError error)
    {
        Debug.LogError("Error al actualizar el nombre de visualización: " + error.GenerateErrorReport());
    }

    private void CheckIfUserExists(string playFabId)
    {
        var request = new GetUserDataRequest
        {
            PlayFabId = playFabId
        };

        PlayFabClientAPI.GetUserData(request, OnUserExists, OnUserExistError);
    }

    private void OnUserExistError(PlayFabError error)
    {
        Debug.LogError("Error al verificar usuario existente: " + error.GenerateErrorReport());
    }

    private void OnUserExists(GetUserDataResult result)
    {
        Debug.Log("Usuario existe en PlayFab con ID: " + UserInfo.id);
        SceneManager.LoadScene(1);
    }

    private string GenerateCustomId()
    {
        string guid = System.Guid.NewGuid().ToString("N"); // Elimina los guiones
        return guid.Length <= 100 ? guid : guid.Substring(0, 100); // Asegura que el ID tenga una longitud adecuada
    }
}
