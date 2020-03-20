using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnapsackPanel : BasePanel
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
        Vector3 temp = transform.localPosition;
        temp.x = 600;
        transform.localPosition = temp;
        transform.DOLocalMoveX(0, 0.5f);
    }
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        transform.DOLocalMoveX(600, 0.5f).OnComplete(()=> canvasGroup.alpha = 0);
    }
    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
    public void OnItemButtonClick()
    {
        UIManager.Instance.PushPanel(UIPanelType.ItemMessage);
    }
}
