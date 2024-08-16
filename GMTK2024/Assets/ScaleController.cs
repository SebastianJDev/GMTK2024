using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScaleController : MonoBehaviour
{
    public GameObject targetObject; // El objeto que se va a escalar
    public GameObject Silueta; // El objeto que se va a escalar
    public Button growButton; // Bot�n para crecer el objeto
    public Button shrinkButton; // Bot�n para reducir el objeto
    public float scaleFactor = 0.5f; // Factor de escalado
    public float maxScale = 1.5f; // Tama�o m�ximo permitido
    public float minScale = 0.5f; // Tama�o m�nimo permitido
    public float animationDuration = 0.5f; // Duraci�n de la animaci�n

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
