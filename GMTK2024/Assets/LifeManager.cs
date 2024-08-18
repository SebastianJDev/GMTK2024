using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public GameObject PanelLose;
    public Image[] lifeImages; // Las imágenes de las vidas (X)
    public float shakeDuration = 0.5f; // Duración de la animación de shake
    public float shakeStrength = 10f; // Fuerza de la animación de shake
    private int lives = 3; // Cantidad de vidas iniciales
    public GameObject BarraNegra1;
    public GameObject BarraNegra2;
    public Transform posicionFinal1;
    public Transform posicionFinal2;
    [SerializeField]private Color fall;
    [SerializeField] private Button RetryButton;
    public float duration = 1f; // Duración de la animación
    public Vector3 targetScale = Vector3.one; // Escala final de la imagen
    private void Start()
    {
        RetryButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        });
        PanelLose.SetActive(false);
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
            if (lives != 0)
            {
                AudioManager.instance.Play("Fallo");
            }
            // Actualizar la imagen de vida correspondiente
            var image = lifeImages[lives];
            image.color = fall; // Cambiar el color a rojo
            image.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false, true).OnComplete(() =>
            {
                // Si se quedan sin vidas, reiniciar el nivel
                if (lives == 0)
                {
                    AudioManager.instance.Play("FalloUltimo");
                    RestartLevel();
                }
            });


        }
    }

    private void RestartLevel()
    {
        PanelLose.SetActive(true);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(BarraNegra1.transform.DOMove(posicionFinal1.position, 1.0f)).Join(BarraNegra2.transform.DOMove(posicionFinal2.position, 1.0f))
            .AppendCallback(TimeScale);
    }
    public void TimeScale()
    {
        Time.timeScale = 0;
    }
}
