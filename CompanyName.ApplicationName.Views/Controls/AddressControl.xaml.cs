using System;
using System.Windows;
using System.Windows.Controls;
using CompanyName.ApplicationName.DataModels;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides the ability to edit addresses.
    /// </summary>
    public partial class AddressControl : UserControl
    {
        /// <summary>
        /// Initializes a new empty AddressControl object with default values.
        /// </summary>
        public AddressControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// THe event that is raised when the Address in the control is changed.
        /// </summary>
        public event EventHandler<AddressEventArgs> AddressChanged;

        /// <summary>
        /// Represents the Address object to edit in the View.
        /// </summary>
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register(nameof(Address), typeof(Address), typeof(AddressControl), new PropertyMetadata(new Address()));

        /// <summary>
        /// Gets or sets the Address object to edit in the View.
        /// </summary>
        public virtual Address Address
        {
            get { return (Address)GetValue(AddressProperty); }
            set
            {
                if (!Address.Equals(value))
                {
                    Address oldAddress = Address;
                    SetValue(AddressProperty, value);
                    OnAddressChanged(new AddressEventArgs(oldAddress, value));
                }
            }
        }

        /// <summary>
        /// Invokes the AddressChanged event. Derived class that override this method must call this base class implementation in order to raise the event.
        /// </summary>
        /// <param name="e">The AddressEventArgs object containing the new and old Address objects.</param>
        protected virtual void OnAddressChanged(AddressEventArgs e)
        {
            AddressChanged?.Invoke(this, e);
        }
    }

    /// <summary>
    /// A custom EventArgs implementation that contains the new and old Address objects.
    /// </summary>
    public class AddressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new AddressEventArgs object values from the input parameters.
        /// </summary>
        public AddressEventArgs(Address oldAddress, Address newAddress)
        {
            OldAddress = oldAddress;
            NewAddress = newAddress;
        }

        /// <summary>
        /// Gets the previous Address object that was changed.
        /// </summary>
        public Address OldAddress { get; }

        /// <summary>
        /// Gets the new Address object that was set.
        /// </summary>
        public Address NewAddress { get; }
    }
}