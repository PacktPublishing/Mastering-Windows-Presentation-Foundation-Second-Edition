using System;
using System.Collections.Generic;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Provides a way to register interfaces and link them with specific concrete implementations and to later resolve them from their linked interface types.
    /// </summary>
    public class DependencyManager
    {
        private static DependencyManager instance;
        private static Dictionary<Type, Type> registeredDependencies = new Dictionary<Type, Type>();

        private DependencyManager() { }

        /// <summary>
        /// Gets the single available instance of the application DependencyManager object.
        /// </summary>
        public static DependencyManager Instance
        {
            get { return instance ?? (instance = new DependencyManager()); }
        }

        /// <summary>
        /// Gets the number of registered dependencies contained in the DependencyManager object.
        /// </summary>
        public int Count
        {
            get { return registeredDependencies.Count; }
        }

        /// <summary>
        /// Clears the registered dependencies from the DependencyManager object.
        /// </summary>
        public void ClearRegistrations()
        {
            registeredDependencies.Clear();
        }

        /// <summary>
        /// Registers the interface type represented by the generic S parameter with the concrete implementation of that interface represented by the generic T parameter.
        /// </summary>
        /// <typeparam name="S">The interface type to register.</typeparam>
        /// <typeparam name="T">The concrete implementation of the interface type represented by the generic S parameter.</typeparam>
        /// <exception cref="ArgumentException">Throws an ArgumentException if the generic type parameter S is not an interface.</exception>
        public void Register<S, T>() where S : class where T : class
        {
            if (!typeof(S).IsInterface) throw new ArgumentException("The S generic type parameter of the Register method must be an interface.", "S");
            if (!typeof(S).IsAssignableFrom(typeof(T))) throw new ArgumentException("The T generic type parameter must be a class that implements the interface specified by the S generic type parameter", "T");
            if (!registeredDependencies.ContainsKey(typeof(S))) registeredDependencies.Add(typeof(S), typeof(T));
        }

        /// <summary>
        /// Returns the concrete implementation of the interface type represented the generic type parameter.
        /// </summary>
        /// <typeparam name="T">The concrete implementation of the interface type represented the generic type parameter.</typeparam>
        /// <returns>The concrete implementation of the interface type represented the generic type parameter.</returns>
        public T Resolve<T>() where T : class
        {
            Type type = registeredDependencies[typeof(T)];
            return Activator.CreateInstance(type) as T;
        }

        /// <summary>
        /// Returns the concrete implementation of the interface type represented the generic type parameter and passes the parameters specified by the parameters input parameter to its constructor.
        /// </summary>
        /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <typeparam name="T">The concrete implementation of the interface type represented the generic type parameter.</typeparam>
        /// <returns>The concrete implementation of the interface type represented the input parameter and generic type parameter.</returns>
        public T Resolve<T>(params object[] args) where T : class
        {
            Type type = registeredDependencies[typeof(T)];
            if (args == null || args.Length == 0) return Activator.CreateInstance(type) as T;
            else return Activator.CreateInstance(type, args) as T;
        }
    }
}