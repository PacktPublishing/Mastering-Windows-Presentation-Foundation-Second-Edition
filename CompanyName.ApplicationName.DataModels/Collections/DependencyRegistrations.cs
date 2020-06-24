using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.ApplicationName.DataModels.Collections
{
    /// <summary>
    /// Represents a collection of DependencyRegistration objects in the application.
    /// </summary>
    public class DependencyRegistrations : List<DependencyRegistration>
    {
        /// <summary>
        /// Determines whether the InterfaceType property value of any DependencyRegistration object in the collection contains the Type specified by the generic T type parameter.
        /// </summary>
        /// <typeparam name="T">The Type to search the InterfaceType property values of each DependencyRegistration object in the collection for.</typeparam>
        /// <returns>True if the InterfaceType property value of any DependencyRegistration object in the collection contains the Type specified by the generic T type parameter; otherwise, false.</returns>
        public bool Contains<T>()
        {
            return this.Select(d => d.InterfaceType).Contains(typeof(T));
        }

        /// <summary>
        /// Gets the first DependencyRegistration object from the collection that has an InterfaceType property value that matches the Type specified by the generic T type parameter, or null if none exist.
        /// </summary>
        /// <typeparam name="T">The Type to search the InterfaceType property values of each DependencyRegistration object in the collection for.</typeparam>
        /// <returns>The first DependencyRegistration object from the collection that has an InterfaceType property value that matches the Type specified by the generic T type parameter, or null if none exist.</returns>
        public DependencyRegistration GetRegistration<T>()
        {
            return this.FirstOrDefault(d => d.InterfaceType == typeof(T));
        }

        /// <summary>
        /// Gets the first DependencyRegistration object from the collection that has an InterfaceType property value that matches the Type specified by the type input parameter, or null if none exist.
        /// </summary>
        /// <param name="type">The Type to search the InterfaceType property values of each DependencyRegistration object in the collection for.</param>
        /// <returns>The first DependencyRegistration object from the collection that has an InterfaceType property value that matches the Type specified by the type input parameter, or null if none exist.</returns>
        public DependencyRegistration GetRegistration(Type type)
        {
            return this.FirstOrDefault(d => d.InterfaceType == type);
        }

        /// <summary>
        /// Adds a DependencyRegistration object with values from the input parameters to the end of the collection.
        /// </summary>
        /// <param name="interfaceType">The Sytem.Type object that represents the type of interface in the DependencyRegistration object.</param>
        /// <param name="concreteImplementations">The collection of ConcreteImplementation instances that represent the concrete implementations that implement the type of interface specified by the interfaceType input parameter.</param>
        public void Add(Type interfaceType, params ConcreteImplementation[] concreteImplementations)
        {
            base.Add(new DependencyRegistration(interfaceType, concreteImplementations));
        }

        /// <summary>
        /// Adds a DependencyRegistration object with values from the input parameters to the end of the collection.
        /// </summary>
        /// <typeparam name="T">The Type to specify as the interface type in the new DependencyRegistration object to add.</typeparam>
        /// <param name="concreteImplementation">The ConcreteImplementation instance that represents the concrete implementation that implements the type of interface specified by the generic T type parameter.</param>
        public void Add<T>(ConcreteImplementation concreteImplementation)
        {
            base.Add(new DependencyRegistration(typeof(T), new List<ConcreteImplementation>() { concreteImplementation }));
        }

        /// <summary>
        /// Removes the single occurrence from the collection that has an InterfaceType property value that matches the Type specified by the generic T type parameter, if one exists; if the type does not exist, an InvalidOperationException will be thrown.
        /// </summary>
        /// <typeparam name="T">The Type that represents the interface type in the DependencyRegistration object to remove.</typeparam>
        /// <exception cref="InvalidOperationException">The type does not exist in the collection.</exception>
        public void Remove<T>()
        {
            Remove(this.Single(d => d.InterfaceType == typeof(T)));
        }
    }
}