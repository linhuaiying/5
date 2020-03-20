using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : BasePanel
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
    }
    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
}
