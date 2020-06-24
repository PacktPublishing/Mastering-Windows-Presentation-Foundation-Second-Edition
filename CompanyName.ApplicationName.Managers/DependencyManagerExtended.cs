using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Responsible for maintaining the dependencies between interfaces and concrete implementations of those interfaces in the application.
    /// </summary>
    public class DependencyManagerExtended
    {
        private static DependencyManagerExtended instance;
        private static DependencyRegistrations registeredDependencies = new DependencyRegistrations();

        private DependencyManagerExtended() { }

        /// <summary>
        /// Gets the single available instance of the application DependencyManagerExtended object.
        /// </summary>
        public static DependencyManagerExtended Instance
        {
            get { return instance ?? (instance = new DependencyManagerExtended()); }
        }

        /// <summary>
        /// Gets the number of registered dependencies contained in the DependencyManagerExtended object.
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
            if (!typeof(S).IsInterface) throw new ArgumentException("The generic type parameter S on the generic Register method must be an interface.", "S");
            if (!registeredDependencies.Contains<S>()) registeredDependencies.Add<S>(new ConcreteImplementation(typeof(T), null));
            else
            {
                registeredDependencies.Remove<T>();
                registeredDependencies.Add<S>(new ConcreteImplementation(typeof(T), null));
            }
        }

        /// <summary>
        /// Registers the interface type represented by the generic S parameter with the concrete implementation of that interface represented by the generic T parameter, along with parameters to use in the construction of the object.
        /// </summary>
        /// <typeparam name="S">The interface type to register.</typeparam>
        /// <typeparam name="T">The concrete implementation of the interface type represented by the generic S parameter.</typeparam>
        /// <param name="parameters">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If parameters is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
        /// <exception cref="ArgumentException">Throws an ArgumentException if the generic type parameter S is not an interface.</exception>
        public void Register<S, T>(params object[] parameters) where S : class where T : class 
        {
            if (!typeof(S).IsInterface) throw new ArgumentException("The generic type parameter S on the generic Register method must be an interface.", "S");
            if (!registeredDependencies.Contains<S>()) registeredDependencies.Add<S>(new ConcreteImplementation(typeof(T), parameters));
            else
            {
                registeredDependencies.Remove<S>();
                registeredDependencies.Add<S>(new ConcreteImplementation(typeof(T), parameters));
            }
        }

        /// <summary>
        /// Registers all interface types found in the assembly of the Type specified by the generic type parameter T and matches all found concrete implementations of those interfaces.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to register all interfaces and concrete implementations of those interfaces from.</typeparam>
        public void RegisterAllInterfacesInAssemblyOf<T>() where T : class
        {
            Assembly assembly = typeof(T).Assembly;
            IEnumerable<Type> interfaces = assembly.GetTypes().Where(p => p.IsInterface);
            foreach (Type interfaceType in interfaces)
            {
                IEnumerable<Type> implementingTypes = assembly.GetTypes().Where(p => interfaceType.IsAssignableFrom(p) && !p.IsInterface);
                ConcreteImplementation[] concreteImplementations = implementingTypes.Select(t => new ConcreteImplementation(t, null)).ToArray();
                    if (concreteImplementations != null && concreteImplementations.Any())
                    registeredDependencies.Add(interfaceType, concreteImplementations);
            }
        }

        /// <summary>
        /// Registers all types found in the assembly of the Type specified by the generic type parameter T where matches of implemented interfaces are found.
        /// </summary>
        /// <typeparam name="T">A type from the assembly to register all types with found implemented interfaces from.</typeparam>
        public void RegisterAllConcreteImplementationsInAssemblyOf<T>() where T : class
        {
            Assembly assembly = typeof(T).Assembly;
            IEnumerable<Type> concreteImplementations = assembly.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Count() > 0);
            foreach (Type concreteImplementationType in concreteImplementations)
            {
                IEnumerable<Type> interfaceTypes = concreteImplementationType.GetInterfaces();
                foreach (Type interfaceType in interfaceTypes)
                {
                    ConcreteImplementation concreteImplementation = new ConcreteImplementation(concreteImplementationType, null);
                    if (concreteImplementations != null && concreteImplementations.Any())
                        registeredDependencies.Add(interfaceType, concreteImplementation);
                }
            }
        }

        /// <summary>
        /// Returns the concrete implementation of the interface type represented by the generic type parameter T.
        /// </summary>
        /// <typeparam name="T">The type of object to instantiate and return.</typeparam>
        /// <returns>The type of object represented by the generic type parameter T.</returns>
        public T Resolve<T>() where T : class
        {
            DependencyRegistration registration = registeredDependencies.GetRegistration<T>();
            if (registration == null) return null;
            ConcreteImplementation concreteImplementation = registration.ConcreteImplementations.First();
            if (concreteImplementation.ConstructorParameters == null) return Activator.CreateInstance(concreteImplementation.Type) as T;
            else return Activator.CreateInstance(concreteImplementation.Type, concreteImplementation.ConstructorParameters) as T;
        }
    }
}