using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Name explains everything...
/// </summary>
public class UIPulseAnimation : MonoBehaviour
{

    public Vector3 scaleValue = new Vector3(1.1f, 1.05f, 1f); //Scale value to adjust pulse magnitude.
    public float duration = 0.5f; 

    RectTransform _rect;
    private void Awake()
    {
        _rect = gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        transform.DOScale(scaleValue, duration).SetLoops(-1, LoopType.Yoyo);  //pulse handled by DOTween plugin.
    }

    /// <summary>
    /// We are returning our UI rect to its initial state.
    /// </summary>
    private void OnDisable()
    {
        transform.DOKill();
        _rect.localScale = new Vector3(1f, 1f, 1f);
    }
}
