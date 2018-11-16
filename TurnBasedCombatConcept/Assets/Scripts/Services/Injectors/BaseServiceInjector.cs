using UnityEngine;

/// <summary> A service injector base containing methods to automate injection handling. To override the awake/destroy behavior, create Awake/OnDestroy in the child class, remember to call the injection.</summary>
/// <typeparam name="T"> Service name</typeparam>
public abstract class BaseServiceInjector<T> : MonoBehaviour where T : IService
{
    /// <summary> The current service that will be injected.</summary>
    public T Service => _service;
    [SerializeField] private T _service;

    /// <summary> </summary>
    public bool IsActive => _isActive;
    [SerializeField] private bool _isActive;



    protected virtual void Awake()
    {
        Debug.Log($"Warning: {nameof(T)} Injector ({GetInstanceID()}) is currently active in the scene, if the service needs to be injected via an injector then ignore this message.");

        InjectService();
    }

    protected virtual void OnDestroy()
    {
        if (_isActive)
            RemoveService();
    }

    protected void InjectService()
    {
        GameServiceCollection.AddService(_service);
        _isActive = true;
    }

    protected void InjectService(bool throwIfExists = false)
    {
        if (!GameServiceCollection.TryAddService(_service) && throwIfExists)
            throw new System.Exception($"Service \"{nameof(T)}\" has already been injected");

        _isActive = true;
    }

    protected void UpdateService(T newInstance)
    {
        _service = newInstance;
        GameServiceCollection.SetService(newInstance);

        _isActive = true;
    }

    protected void RemoveService()
    {
        GameServiceCollection.RemoveService<T>();

        _isActive = false;
    }

    protected void RemoveService(bool throwIfNotExists = false)
    {
        if (!GameServiceCollection.TryRemoveService<T>() && throwIfNotExists)
            throw new System.Exception($"Service \"{nameof(T)}\" is currently not injected");

        _isActive = false;
    }
}
