using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Represents a System.Type object and any constructor input parameters that may be required for its initialization.
    /// </summary>
    public struct ConcreteImplementation
    {
        /// <summary>
        /// Initializes a new ConcreteImplementation struct with the values specified by the input parameters.
        /// </summary>
        public ConcreteImplementation(Type type, params object[] constructorParameters)
        {
            Type = type;
            ConstructorParameters = constructorParameters;
        }

        /// <summary>
        /// Gets or sets the System.Type object that this struct relates to.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the object array containing the values for the constructor parameter(s) to initialise the System.Type object that this struct relates to.
        /// </summary>
        public object[] ConstructorParameters { get; set; }
    }
}