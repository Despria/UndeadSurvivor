using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameEvent를 구독중인 GameEventListener들을 등록하고 이벤트 발생시 발생 여부를 전파하는 스크립터블 오브젝트.<br/>
/// 1. 이벤트 발생을 알릴 오브젝트에 GameEvent 필드 선언 후, GameEvent 스크립터블 오브젝트 에셋 생성 후 연결.<br/>
/// 2. 해당 이벤트를 구독할 오브젝트들에 GameEventListener를 상속받는 컴포넌트 추가 후, 1에서 생성한 것과 같은 스크립터블 오브젝트 연결.<br/>
///    GameEventListener를 상속받는 컴포넌트들은 UnityEvent.Events 네임스페이스를 선언.<br/>
///    UnityEvent에 구독자를 등록하면, GameEvent가 Raise될때 UnityEvent의 구독자들이 실행됨.
/// </summary>

[CreateAssetMenu(fileName = "Event", menuName = "Scriptable Objects/Event", order = 0)]
public class GameEvent : ScriptableObject
{
    /// ※ GameEventListener 리스트에 SerializeField를 적용할 수 없으므로 주의.<br/>
    ///   Scriptable Object는 Scene에 관계 없이 정보를 저장하기 때문에 <br/>
    ///   Scene에 종속적인 SerializeField의 정보를 제대로 읽을 수 없음.<br/>
    List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener); 
    }
}