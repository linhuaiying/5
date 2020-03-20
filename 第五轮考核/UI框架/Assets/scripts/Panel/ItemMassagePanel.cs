using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemMassagePanel :BasePanel
{
    private CanvasGroup canvasGroup;
    private void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
    }
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        transform.DOScale(0, 0.5f).OnComplete(() => canvasGroup.alpha = 0);
        
}
    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}
