/// <summary>
/// ObjectPooler에 포함되는 오브젝트가 상속받는 인터페이스.
/// </summary>

public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectInsert();
}