using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class LifeManager : MonoBehaviour
{
    public Image[] lifeImages; // Las imágenes de las vidas (X)
    public float shakeDuration = 0.5f; // Duración de la animación de shake
    public float shakeStrength = 10f; // Fuerza de la animación de shake
    private int lives = 3; // Cantidad de vidas iniciales
    [SerializeField]private Color fall;
    private void Start()
    {
        // Inicializar las imágenes de las vidas a su color original
        foreach (var image in lifeImages)
        {
            image.color = Color.black;
        }
    }

    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;

            // Actualizar la imagen de vida correspondiente
            var image = lifeImages[lives];
            image.color = fall; // Cambiar el color a rojo
            image.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false, true).OnComplete(() =>
            {
                // Si se quedan sin vidas, reiniciar el nivel
                if (lives == 0)
                {
                    RestartLevel();
                }
            });


        }
    }

    private void RestartLevel()
    {
        // Reiniciar el nivel (puedes ajustar esto según cómo manejes los niveles en tu juego)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
