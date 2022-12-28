using UnityEngine;

/// <summary>
/// Singleton으로 사용할 수 있는 Scriptable Object Asset을 생성할 수 있음.<br/>
/// 해당 클래스를 상속받는 클래스에 CreateAssetMenu Attribute를 사용하여 Asset을 생성.<br/>
/// 해당 Asset은 Resources 폴더 내부에 존재해야 함.<br/>
/// 생성한 Asset을 통해 게임 내 어디서든 Singleton에 접근하는 것처럼 Asset 내부의 정보에 접근 가능.<br/>
/// https://www.youtube.com/watch?v=6kWUGEQiMUI
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");
                if (assets == null || assets.Length < 1)
                {
                    throw new System.Exception("Could not find any Scriptable Object Instances in the Resources.");
                }
                else if (assets.Length > 1) 
                {
                    Debug.LogWarning("Multiple Instances of Singleton Scriptable Object found in the Resources.");
                }

                instance = assets[0];
            }
            return instance;
        }
    }
}
