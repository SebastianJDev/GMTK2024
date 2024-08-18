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
        // Añadir listener al botón
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
    }

    void OnConfirmButtonClicked()
    {
        // Obtener el nombre del campo de entrada
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // Llamar al método SetPlayerName en PlayFabLoginManager
            //playFabLoginManager.SetPlayerName(playerName);
        }
        else
        {
            Debug.LogWarning("El campo de nombre está vacío");
        }
    }
}
