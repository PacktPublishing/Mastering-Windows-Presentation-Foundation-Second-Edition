using System;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Enables implementing classes to use the animated features of animatable user controls.
    /// </summary>
    public class Animatable
    {
        private AdditionStatus additionStatus = AdditionStatus.ReadyToAnimate;
        private RemovalStatus removalStatus = RemovalStatus.None;
        private TransitionStatus transitionStatus = TransitionStatus.None;
        private IAnimatable owner;

        /// <summary>
        /// Initializes a new Animatable object with the specified owner input parameter.
        /// </summary>
        /// <param name="owner">The IAnimatable object to be animatable.</param>
        public Animatable(IAnimatable owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Initializes a new empty Animatable object.
        /// </summary>
        /// <remarks>This constructor should not be used directly in code. It is only present for instantiation using reflection.</remarks>
        public Animatable() { }

        /// <summary>
        /// This event is raised when the RemovalStatus property is changed.
        /// </summary>
        public event EventHandler<EventArgs> OnRemovalStatusChanged;

        /// <summary>
        /// This event is raised when the TransitionStatus property is changed.
        /// </summary>
        public event EventHandler<EventArgs> OnTransitionStatusChanged;

        /// <summary>
        /// The IAnimatable object to be animatable.
        /// </summary>
        public IAnimatable Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        /// <summary>
        /// Gets or sets a value that describes the current status of the object's addition animation.
        /// </summary>
        public AdditionStatus AdditionStatus
        {
            get { return additionStatus; }
            set { additionStatus = value; }
        }

        /// <summary>
        /// Gets or sets a value that describes the current status of the object's transition animation.
        /// </summary>
        public TransitionStatus TransitionStatus
        {
            get { return transitionStatus; }
            set
            {
                transitionStatus = value;
                OnTransitionStatusChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Gets or sets a value that describes the current status of the object's removal animation.
        /// </summary>
        public RemovalStatus RemovalStatus
        {
            get { return removalStatus; }
            set
            {
                removalStatus = value;
                OnRemovalStatusChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}