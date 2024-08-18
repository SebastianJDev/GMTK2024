using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NameInputHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button confirmButton;
    [SerializeField] private PlayFabLogin playFabLoginManager;

    void Start()
    {
        // A�adir listener al bot�n
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    void OnConfirmButtonClicked()
    {
        // Obtener el nombre del campo de entrada
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // Llamar al m�todo SetPlayerName en PlayFabLoginManager
            //playFabLoginManager.SetPlayerName(playerName);
        }
        else
        {
            Debug.LogWarning("El campo de nombre est� vac�o");
        }
    }
}
