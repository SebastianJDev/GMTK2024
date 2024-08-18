using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    public LeaderBoardManager leaderboardManager;
    public float TimeActual;
    private int Puntos = 0;
    public GameObject Silueta;
    public GameObject siluetaShadow;
    public GameObject targetObject;
    public float[] Escalas;
    public Sprite[] Siluetas;
    public Sprite[] SiluetasShadow;
    public GameObject Plataforma;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public float animationDuration = 0.5f;
    public float animationDurationFinal = 0.5f;
    public float waitTime = 2f;
    public float hideDuration = 0.1f;
    public TextMeshProUGUI puntosText; // Referencia al TextMeshProUGUI para el puntaje
    public TextMeshProUGUI puntosTextFinal;
    public float punchScale = 0.2f; // Escala del punch
    public float punchDuration = 0.3f; // Duración del punch
    public float shakeDuration = 0.5f; // Duración del shake
    public float shakeStrength = 0.5f; // Fuerza del shake

    public LifeManager lifeManager; // Referencia al LifeManager

    private Color defaultColor; // Color por defecto del texto
    public Color failColor = Color.red; // Color para el fallo

    private void Start()
    {
        TimeActual = Time.timeScale;
        if (puntosText != null)
        {
            defaultColor = puntosText.color;
        }
        Plataforma.transform.position = position1.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(Plataforma.transform.DOMove(position2.position, animationDuration))
                .AppendInterval(waitTime)
                .AppendCallback(Comprobar)
                .Append(Plataforma.transform.DOMove(position3.position, animationDurationFinal))
                .AppendCallback(Asignar)
                .AppendCallback(() => StartCoroutine(HideAndMoveBack()))
                .SetLoops(-1, LoopType.Restart);
    }

    public void Comprobar()
    {
        var siluetaImage = siluetaShadow.GetComponent<Image>().sprite.name;
        var targetImage = targetObject.GetComponent<Image>().sprite.name;

        if (siluetaImage == targetImage && Silueta.transform.localScale.x == targetObject.transform.localScale.x)
        {
            Puntos++;
            UpdateScoreText();
            AnimateScoreTextPunch();
            if (Time.timeScale <= 2.2f)
            {
                TimeActual += 0.03f;
                Time.timeScale = TimeActual;
            }
            leaderboardManager.SendScoreToLeaderboard(Puntos);
        }
        else
        {
            AnimateScoreTextShake();
            lifeManager.LoseLife();
        }
    }

    private IEnumerator HideAndMoveBack()
    {
        Plataforma.SetActive(false);
        yield return new WaitForSeconds(hideDuration);
        Plataforma.transform.position = position1.position;
        Plataforma.SetActive(true);
    }

    public void Asignar()
    {
        float randomNumber = Random.Range(0, Escalas.Length);
        int randomNumberSprite = Random.Range(0, Siluetas.Length);
        if (Escalas.Length > 0)
        {
            Silueta.transform.localScale = new Vector2(Escalas[Mathf.FloorToInt(randomNumber)], Escalas[Mathf.FloorToInt(randomNumber)]);
            Silueta.GetComponent<Image>().sprite = SiluetasShadow[randomNumberSprite];
            siluetaShadow.GetComponent<Image>().sprite = Siluetas[randomNumberSprite];
        }
    }

    private void UpdateScoreText()
    {
        if (puntosText != null)
        {
            puntosText.text = Puntos.ToString();
            puntosTextFinal.text = "POINTS: " + Puntos.ToString();
        }
    }

    private void AnimateScoreTextPunch()
    {
        if (puntosText != null)
        {
            AudioManager.instance.Play("Correcto");
            puntosText.transform.DOPunchScale(new Vector3(punchScale, punchScale, 0), punchDuration, 10, 1)
                .OnKill(() => Debug.Log("Punch animation complete."));
        }
    }

    private void AnimateScoreTextShake()
    {
        if (puntosText != null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(puntosText.DOColor(failColor, shakeDuration / 2))
                    .Append(puntosText.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false, true))
                    .Append(puntosText.DOColor(defaultColor, shakeDuration / 2))
                    .OnKill(() => Debug.Log("Shake and color animation complete."));
        }
    }
}
