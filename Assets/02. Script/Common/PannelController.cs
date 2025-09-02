using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PannelController : MonoBehaviour
{
    [SerializeField] private RectTransform pannelRectTransform;

    private CanvasGroup backGroundCanvasGroup;

    private void Awake()
    {
        backGroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        backGroundCanvasGroup.alpha = 0;
        pannelRectTransform.localScale = Vector3.zero;

        backGroundCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        pannelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        backGroundCanvasGroup.alpha = 1;
        pannelRectTransform.localScale = Vector3.one;

        backGroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        pannelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack);
    }
}
