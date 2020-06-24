using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Provides a pairing between an interface type and a collection of concrete implementations of that interface type.
    /// </summary>
    public class DependencyRegistration
    {
        /// <summary>
        /// Initializes a new DependencyRegistration object with the values provided by the input parameters.
        /// </summary>
        public DependencyRegistration(Type interfaceType, IEnumerable<ConcreteImplementation> concreteImplementations)
        {
            if (!concreteImplementations.All(c => interfaceType.IsAssignableFrom(c.Type))) throw new ArgumentException("The System.Type object specified by the ConcreteImplementation.Type property must implement the interface type specified by the interfaceType input parameter.", nameof(interfaceType));
            ConcreteImplementations = concreteImplementations;
            InterfaceType = interfaceType;
        }

        /// <summary>
        /// Gets or sets the Sytem.Type object that represents the type of interface in the DependencyRegistration object.
        /// </summary>
        public Type InterfaceType { get; private set; }

        /// <summary>
        /// Gets or sets the collection of ConcreteImplementation instances that represent the concrete implementations that implement the type of interface specified by the InterfaceType property.
        /// </summary>
        public IEnumerable<ConcreteImplementation> ConcreteImplementations { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (ConcreteImplementations == null || ConcreteImplementations.Count() == 0) return string.Format("{0}: -", InterfaceType);
            else if (ConcreteImplementations.Count() > 1) return string.Format("{0}: {1}", InterfaceType, ConcreteImplementations.Select(c => c.Type).ToCommaSeparatedString());
            else return string.Format("{0}: {1}", InterfaceType, ConcreteImplementations.First().Type);
        }
    }
}