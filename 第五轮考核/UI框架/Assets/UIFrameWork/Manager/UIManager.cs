using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UIManager
{
    //单例模式的核心
    //1.定义一个静态的对象，在外部访问，在内部构造
    //2.构造方法私有化
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();

            }
            return _instance;
        }
    }
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if(canvasTransform==null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;//存储所有面板prefab的路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//保存所有实例化面板的游戏物体身上的BasePanel组件
    private Stack<BasePanel> panelStack;
    private UIManager() //单例模式
    {
        ParseUIPanelTypeJson();//解析所有面板信息
    }
    //将页面入栈，即显示在屏幕上
    public void PushPanel(UIPanelType panelType)
    {  if (panelStack == null)
         panelStack = new Stack<BasePanel>();
        //判断栈是否为空，即是否有页面
        if(panelStack.Count>0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    //将页面出栈，即不显示
    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();
        if (panelStack.Count <= 0) return;
        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }
   private BasePanel GetPanel(UIPanelType panelType)
    {
        if(panelDict==null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }
        BasePanel panel=panelDict.TryGet(panelType);//使用扩展方法
        if (panel == null)
        { //如果找不到，那么就找这个面板的prefab的路径，然后根据prefab去实例化面板
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(panelType, instPanel.transform.GetComponent<BasePanel>());
            return instPanel.transform.GetComponent<BasePanel>();
        }
        else return panel;
    }
    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList=new List<UIPanelInfo>();
    }
     
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
       TextAsset ta= Resources.Load<TextAsset>("UIPanelType");
      UIPanelTypeJson JsonObject=  JsonUtility.FromJson<UIPanelTypeJson>(ta.text);
      foreach(UIPanelInfo info in JsonObject.infoList)
        {
            panelPathDict.Add(info.panelType, info.path);
        }
    }
    public void Test()
    {
        string path;
        Debug.Log(panelPathDict.TryGetValue(UIPanelType.Knapsack, out path));
    }
}
