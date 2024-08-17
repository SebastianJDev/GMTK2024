using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ScaleController : MonoBehaviour
{
    public Color[] Colores;
    public Sprite[] Siluetas;
    public TextMeshProUGUI TextTamaño;
    public GameObject targetObject; 
    public Button growButton; 
    public Button shrinkButton; 
    public float scaleFactor = 0.5f;
    public float maxScale = 1.5f;
    public float minScale = 0.5f; 
    public float animationDuration = 0.5f; 
    public float punchScale = 0.2f;
    public float punchDuration = 0.3f; 

    private bool isAnimating = false; 
    private Vector3 growButtonInitialScale;
    private Vector3 shrinkButtonInitialScale;

    private void Start()
    {
        growButton.onClick.AddListener(() => {
            GrowObject();
            AnimateButton(growButton.gameObject, growButtonInitialScale);
        });

        shrinkButton.onClick.AddListener(() => {
            ShrinkObject();
            AnimateButton(shrinkButton.gameObject, shrinkButtonInitialScale);
        });

        growButtonInitialScale = growButton.transform.localScale;
        shrinkButtonInitialScale = shrinkButton.transform.localScale;

        ActualizarTexto(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShrinkObject();
            AnimateButton(shrinkButton.gameObject, shrinkButtonInitialScale);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            GrowObject();
            AnimateButton(growButton.gameObject, growButtonInitialScale);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Circulo
            AudioManager.instance.Play("Scroll");
            var Sprite = targetObject.GetComponent<Image>();
            Sprite.sprite = Siluetas[0];
            Sprite.color = Colores[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Estrella
            AudioManager.instance.Play("Scroll");
            var Sprite = targetObject.GetComponent<Image>();
            Sprite.sprite = Siluetas[1];
            Sprite.color = Colores[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Cuadrado
            AudioManager.instance.Play("Scroll");
            var Sprite = targetObject.GetComponent<Image>();
            Sprite.sprite = Siluetas[2];
            Sprite.color = Colores[2];
        }
    }

    private void ActualizarTexto()
    {
        TextTamaño.text = targetObject.transform.localScale.x.ToString("F2") + "X";
    }

    private void GrowObject()
    {
        if (isAnimating) return; 
        isAnimating = true; 

        Vector3 newScale = targetObject.transform.localScale + new Vector3(scaleFactor, scaleFactor, 0);
        if (newScale.x <= maxScale && newScale.y <= maxScale)
        {
            AudioManager.instance.Play("BlopMas");
            targetObject.transform.DOScale(newScale, animationDuration)
                .OnUpdate(ActualizarTexto)
                .OnComplete(() => {
                    isAnimating = false; 
                    ActualizarTexto();
                });
        }
        else
        {
            isAnimating = false; 
        }
    }

    private void ShrinkObject()
    {
        if (isAnimating) return; 
        isAnimating = true;

        Vector3 newScale = targetObject.transform.localScale - new Vector3(scaleFactor, scaleFactor, 0);
        if (newScale.x >= minScale && newScale.y >= minScale)
        {
            AudioManager.instance.Play("BlopMenos");
            targetObject.transform.DOScale(newScale, animationDuration)
                .OnUpdate(ActualizarTexto)
                .OnComplete(() => {
                    isAnimating = false; 
                    ActualizarTexto();
                });
        }
        else
        {
            isAnimating = false; 
        }
    }

    private void AnimateButton(GameObject button, Vector3 initialScale)
    {
        button.transform.DOKill(); 
        button.transform.localScale = initialScale; 
        button.transform.DOPunchScale(new Vector3(punchScale, punchScale, 0), punchDuration, 10, 1);
    }
}
