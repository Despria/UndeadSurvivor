using UnityEngine;

public class DontDestroyOnLoadSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    private static object lockObject = new object();

    protected void Awake()
    {
        lock (lockObject)
        {
            if (Instance == null)
            {
                Instance = (T)FindObjectOfType(typeof(T));

                if (Instance == null)
                {
                    Instance = gameObject.GetComponent<T>();
                }
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
