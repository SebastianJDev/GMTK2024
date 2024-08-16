using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScaleController : MonoBehaviour
{
    public GameObject targetObject; // El objeto que se va a escalar
    public GameObject Silueta; // El objeto que se va a escalar
    public Button growButton; // Botón para crecer el objeto
    public Button shrinkButton; // Botón para reducir el objeto
    public float scaleFactor = 0.5f; // Factor de escalado
    public float maxScale = 1.5f; // Tamaño máximo permitido
    public float minScale = 0.5f; // Tamaño mínimo permitido
    public float animationDuration = 0.5f; // Duración de la animación

    private void Start()
    {
        growButton.onClick.AddListener(GrowObject);
        shrinkButton.onClick.AddListener(ShrinkObject);
    }

    private void GrowObject()
    {
        Vector3 newScale = targetObject.transform.localScale + new Vector3(scaleFactor, scaleFactor, 0);
        if (newScale.x <= maxScale && newScale.y <= maxScale)
        {
            targetObject.transform.DOScale(newScale, animationDuration);
            Silueta.transform.DOScale(newScale, animationDuration);
        }
    }

    private void ShrinkObject()
    {
        Vector3 newScale = targetObject.transform.localScale - new Vector3(scaleFactor, scaleFactor, 0);
        if (newScale.x >= minScale && newScale.y >= minScale)
        {
            targetObject.transform.DOScale(newScale, animationDuration);
            Silueta.transform.DOScale(newScale, animationDuration);
        }
    }
}
