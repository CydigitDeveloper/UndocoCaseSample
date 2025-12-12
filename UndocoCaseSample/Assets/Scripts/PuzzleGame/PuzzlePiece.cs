using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Settings")]
    [SerializeField] private RectTransform correctSlot;
    [SerializeField] private float snapDistance = 50f;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector2 _startPosition;
    private bool _isLocked = false;
    private PuzzleGameManager _manager;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _manager = FindObjectOfType<PuzzleGameManager>();
    }

    private void Start()
    {
        _startPosition = _rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isLocked) return;

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.6f;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isLocked) return;
        
        _rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isLocked) return;

        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        
        if (Vector2.Distance(transform.position, correctSlot.position) <= snapDistance)
        {
            SnapToPlace();
        }
        else
        {
            StartCoroutine(ReturnToStart());
        }
    }

    private void SnapToPlace()
    {
        _isLocked = true;
        
        transform.position= correctSlot.position;
        
        _manager.PiecePlaced();

        this.enabled = false;
    }

    private IEnumerator ReturnToStart()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Vector2 currentPos = _rectTransform.anchoredPosition;

        while (elapsed < duration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(currentPos, _startPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = _startPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isLocked) transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isLocked) transform.localScale = Vector3.one;
    }
}