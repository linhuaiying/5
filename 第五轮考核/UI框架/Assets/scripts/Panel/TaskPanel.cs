using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TaskPanel : BasePanel
{
    private CanvasGroup canvasGroup;
    private void Start()
    {
       if(canvasGroup==null) canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);
    }
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 0.5f);
    }
    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}
