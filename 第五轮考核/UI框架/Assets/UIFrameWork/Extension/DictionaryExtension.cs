using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//字典扩展
public static class DictionaryExtension 
{ //尝试根据key来取得value，得到的话返回value，否则返回空
    //this Dictionary<Tkey, Tvalue> dic 这个字典表示我们要获取值的字典
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey,Tvalue> dic,Tkey key)
    {
        Tvalue value;
        dic.TryGetValue(key, out value);
        return value;
    }
}
