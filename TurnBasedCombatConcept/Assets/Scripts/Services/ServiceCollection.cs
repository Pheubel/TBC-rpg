using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

/// <summary> Signatures that this class can be used in the GameServiceCollection.</summary>
public interface IService { }

/// <summary> A container which uses a singleton patern for service classes which can be added and removed on the fly.</summary>
public static class GameServiceCollection
{
    /// <summary> The collection of available services.</summary>
    private static ConcurrentDictionary<Type, IService> _services = new ConcurrentDictionary<Type, IService>();

    /// <summary> Looks if the given service is stored inside of the service collection.</summary>
    /// <typeparam name="T"> Service name.</typeparam>
    public static bool ServiceIsAvailable<T>() where T : IService => _services.ContainsKey(typeof(T));


    public static void AddService<T>(T instance) where T: IService
    {
        if (!_services.TryAdd(typeof(T), instance))
            throw new Exception($" instance of service {nameof(T)} already exists inside of the service collection.");
    }

    /// <summary> Tries to add an instance of a service to the service collection.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    /// <param name="instance"> Service instance</param>
    /// <returns> true if succes, false if key already exists.</returns>
    public static bool TryAddService<T>(T instance) where T : IService => _services.TryAdd(typeof(T), instance);

    /// <summary> Adds or updates the service in the service collection to the new service instance.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    /// <param name="instance">The instance of the service which will be stored.</param>
    public static void SetService<T>(T instance) where T : IService
    {
        _services.AddOrUpdate(typeof(T),instance, (k,v) => instance);
    }

    /// <summary> Gets the stored instance of the given service from the service collection.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    /// <returns> The stored instance of the given service.</returns>
    /// <remarks> If no instance of the given service is found, the function will throw an exception.</remarks>
    public static T GetService<T>() where T : IService
    {
        IService value;
        if (_services.TryGetValue(typeof(T), out value))
        {
            return (T)value;
        }

        throw new Exception($"No instance found of service: {nameof(T)}");
    }

    /// <summary> Tries to get the stored instance of the given service from the service collection.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    /// <param name="service"> the resulting service, default value of the service if not.</param>
    /// <returns> true if found, false if not.</returns>
    public static bool TryGetService<T>(out T service) where T : IService
    {
        IService value;
        if (_services.TryGetValue(typeof(T), out value))
        {
            service = (T)value;
            return true; ;
        }

        service = default(T);
        return false;
    }

    /// <summary> Removes the given service from the service collecion.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    /// <remarks> If no service is found, an exception will be thrown.</remarks>
    public static void RemoveService<T>() where T : IService
    {
        IService removedService;
        if(! _services.TryRemove(typeof(T), out removedService))
            throw new Exception($"No instance found of service: {nameof(T)}");
    }

    /// <summary> Attempts to remove the given service from the service collecion, returns true if succesful.</summary>
    /// <typeparam name="T"> Service name</typeparam>
    public static bool TryRemoveService<T>() where T : IService
    {
        IService removedService;
        return _services.TryRemove(typeof(T), out removedService);
    }
}
