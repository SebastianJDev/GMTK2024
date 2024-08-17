using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    public GameObject Silueta;
    public float[] Escalas;
    public Sprite[] Siluetas;
    public GameObject targetObject;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public float animationDuration = 0.5f;
    public float animationDurationFinal = 0.5f;
    public float waitTime = 2f;
    public float hideDuration = 0.1f;

    private void Start()
    {
        targetObject.transform.position = position1.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(targetObject.transform.DOMove(position2.position, animationDuration))
                .AppendInterval(waitTime)
                .Append(targetObject.transform.DOMove(position3.position, animationDurationFinal)) 
                .AppendCallback(Asignar) 
                .AppendCallback(() => StartCoroutine(HideAndMoveBack()))
                .SetLoops(-1, LoopType.Restart);
    }
    private IEnumerator HideAndMoveBack()
    {
        targetObject.SetActive(false);
        yield return new WaitForSeconds(hideDuration);
        targetObject.transform.position = position1.position;
        targetObject.SetActive(true);
    }
    public void Asignar()
    {
        float randomNumber = Random.Range(0, Escalas.Length);
        int randomNumberSprite = Random.Range(0,Siluetas.Length);
        if (Escalas.Length > 0)
        {
            Silueta.transform.localScale = new Vector2(Escalas[Mathf.FloorToInt(randomNumber)], Escalas[Mathf.FloorToInt(randomNumber)]);
            Silueta.GetComponent<Image>().sprite = Siluetas[randomNumberSprite];
        }

    }
}
