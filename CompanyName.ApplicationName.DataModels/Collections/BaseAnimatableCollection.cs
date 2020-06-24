using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.DataModels.Interfaces;

namespace CompanyName.ApplicationName.DataModels.Collections
{
    /// <summary>
    /// Represents a data collection that enable the animation of its items, maintains a current item and provides a number of helpful utility methods.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public class BaseAnimatableCollection<T> : BaseCollection<T> where T : class, IAnimatable, INotifyPropertyChanged, new()
    {
        private bool isAnimatable = true;

        /// <summary>
        /// Initializes a new BaseAnimatableCollection object and adds the items from the collection specified by the input parameter.
        /// </summary>
        public BaseAnimatableCollection(IEnumerable<T> collection)
        {
            foreach (T item in collection) Add(item);
        }

        /// <summary>
        /// Initializes a new empty BaseAnimatableCollection object.
        /// </summary>
        public BaseAnimatableCollection() : base() { }

        /// <summary>
        /// Gets or sets a value that specifies whether the items in this collection will be animatable or not.
        /// </summary>
        public bool IsAnimatable 
        {
            get { return isAnimatable; }
            set { isAnimatable = value; }
        }

        /// <summary>
        /// Returns the number of elements actually contained in the BaseAnimatableCollection&lt;T&gt; taking items whose removal is still being animated into account.
        /// </summary>
        /// <returns>The number of elements actually contained in the BaseAnimatableCollection&lt;T&gt; taking items whose removal is still being animated into account.</returns>
        public new int Count => IsAnimatable ? this.Count(i => i.Animatable.RemovalStatus == RemovalStatus.None) : this.Count();

        /// <summary>
        /// Adds an object to the end of the collection.
        /// </summary>
        /// <param name="item">The object to be added to the end of the collection. The value can be null.</param>
        public new void Add(T item)
        {
            item.Animatable.OnRemovalStatusChanged += Item_OnRemovalStatusChanged;
            item.Animatable.AdditionStatus = AdditionStatus.ReadyToAnimate;
            base.Add(item);
        }

        /// <summary>
        /// Adds the collection of objects specified by the collection input parameter to the end of this collection.
        /// </summary>
        /// <param name="collection">The collection to be added to the end of this collection. The value can be null.</param>
        public new virtual void Add(IEnumerable<T> collection)
        {
            foreach (T item in collection) Add(item);
        }

        /// <summary>
        /// Adds the collection of objects specified by the items input parameter to the end of this collection.
        /// </summary>
        /// <param name="items">The collection of objects to be added to the end of this collection. The value can be null.</param>
        public new virtual void Add(params T[] items)
        {
            Add(items as IEnumerable<T>);
        }

        /// <summary>
        /// Inserts an element into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public new void Insert(int index, T item)
        {
            item.Animatable.OnRemovalStatusChanged += Item_OnRemovalStatusChanged;
            item.Animatable.AdditionStatus = AdditionStatus.ReadyToAnimate;
            base.Insert(index, item);
        }

        /// <summary>
        /// Removes all elements from the BaseAnimatableCollection&lt;T&gt;.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (T item in this) item.Animatable.OnRemovalStatusChanged -= Item_OnRemovalStatusChanged;
            base.ClearItems();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the collection after first applying an exit animation in conjunction with an AnimatedPanel element.
        /// </summary>
        /// <param name="item">The object to remove from the collection. The value can be null for reference types.</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns false if item was not found in the original collection.</returns>
        public new bool Remove(T item)
        {
            if (item != null) item.Animatable.RemovalStatus = RemovalStatus.ReadyToAnimate;
            return true;
        }

        /// <summary>
        /// The event handler that finalises collection operations such as Remove after the relating animations are complete.
        /// </summary>
        /// <param name="sender">The collection item that requires an operation to be completed.</param>
        /// <param name="e">The EventArgs object of the event handler.</param>
        public void Item_OnRemovalStatusChanged(object sender, EventArgs e)
        {
            Animatable animatable = (Animatable)sender;
            if (animatable.RemovalStatus == RemovalStatus.ReadyToRemove || (animatable.RemovalStatus == RemovalStatus.ReadyToAnimate && !IsAnimatable))
            {
                base.Remove(animatable.Owner as T);
                animatable.RemovalStatus = RemovalStatus.None;
            }
        }
    }
}