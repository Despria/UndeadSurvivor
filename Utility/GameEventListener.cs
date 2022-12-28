using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameEvent에 반응할 오브젝트의 스크립트가 상속하는 GameEventListener.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event To Subscribe")]
    public GameEvent Event;

    [Tooltip("UnityEvent Subscribe to GameEvent")]
    public UnityEvent Response;

    protected void OnEnable()
    { 
        Event.RegisterListener(this);
    }

    protected void OnDisable()
    { 
        Event.UnregisterListener(this); 
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
