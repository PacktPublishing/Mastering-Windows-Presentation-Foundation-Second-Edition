using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CompanyName.ApplicationName.DataModels;
using CompanyName.ApplicationName.DataModels.Collections;
using CompanyName.ApplicationName.Extensions;

namespace CompanyName.ApplicationName.Views.Controls
{
    /// <summary>
    /// Provides a system and UI control to handle and display user feedback in the application.
    /// </summary>
    public partial class FeedbackControl : UserControl
    {
        private static List<DispatcherTimer> timers = new List<DispatcherTimer>();

        /// <summary>
        /// Initializes a new empty FeedbackControl object with default values.
        /// </summary>
        public FeedbackControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents the collection of Feedback objects in the control.
        /// </summary>
        public static readonly DependencyProperty FeedbackProperty = DependencyProperty.Register(nameof(Feedback), typeof(FeedbackCollection), typeof(FeedbackControl), new UIPropertyMetadata(new FeedbackCollection(), (d, e) => ((FeedbackCollection)e.NewValue).CollectionChanged += ((FeedbackControl)d).Feedback_CollectionChanged));

        /// <summary>
        /// Gets or sets the collection of Feedback objects from the control.
        /// </summary>
        public FeedbackCollection Feedback
        {
            get { return (FeedbackCollection)GetValue(FeedbackProperty); }
            set { SetValue(FeedbackProperty, value); }
        }

        /// <summary>
        /// Represents a value that specifies whether the control has any Feedback objects or not.
        /// </summary>
        public static readonly DependencyProperty HasFeedbackProperty = DependencyProperty.Register(nameof(HasFeedback), typeof(bool), typeof(FeedbackControl), new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value that specifies whether the control has any Feedback objects or not.
        /// </summary>
        public bool HasFeedback
        {
            get { return (bool)GetValue(HasFeedbackProperty); }
            set { SetValue(HasFeedbackProperty, value); }
        }

        private void Feedback_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((e.OldItems == null || e.OldItems.Count == 0) && e.NewItems != null && e.NewItems.Count > 0)
            {
                e.NewItems.OfType<Feedback>().Where(f => !f.IsPermanent).ForEach(f => InitializeTimer(f));
            }
            HasFeedback = Feedback.Any();
        }

        private void InitializeTimer(Feedback feedback)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = feedback.Duration;
            timer.Tick += Timer_Tick;
            timer.Tag = new Tuple<Feedback, DateTime>(feedback, DateTime.Now);
            timer.Start();
            timers.Add(timer);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            timers.Remove(timer);
            Feedback feedback = ((Tuple<Feedback, DateTime>)timer.Tag).Item1;
            Feedback.Remove(feedback);
        }

        private void DeleteButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Feedback feedback = (Feedback)deleteButton.DataContext;
            Feedback.Remove(feedback);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (DispatcherTimer timer in timers)
            {
                timer.Stop();
                Tuple<Feedback, DateTime> tag = (Tuple<Feedback, DateTime>)timer.Tag;
                tag.Item1.Duration = timer.Interval = tag.Item1.Duration.Subtract(DateTime.Now.Subtract(tag.Item2));
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (DispatcherTimer timer in timers)
            {
                Feedback feedback = ((Tuple<Feedback, DateTime>)timer.Tag).Item1;
                timer.Tag = new Tuple<Feedback, DateTime>(feedback, DateTime.Now);
                timer.Start();
            }
        }
    }
}