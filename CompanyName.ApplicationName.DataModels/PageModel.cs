using CompanyName.ApplicationName.DataModels.Enums;
using CompanyName.ApplicationName.Extensions;
using System;

namespace CompanyName.ApplicationName.DataModels
{
    /// <summary>
    /// Proivides properties that describe a page in the related Mastering Windows Presentaion Foundation demonstration application.
    /// </summary>
    public class PageModel : BaseDataModel
    {
        private Type type = null;
        private Page page = Page.BitRate;
        private Chapter chapter = Chapter.One;

        /// <summary>
        /// Initializes a new PageModel object with the values from the input parameters.
        /// </summary>
        public PageModel(Type type, Page page, Chapter chapter)
        {
            Type = type;
            Page = page;
            Chapter = chapter;
        }

        /// <summary>
        /// Gets or sets the type of the PageModel object.
        /// </summary>
        public Type Type
        {
            get { return type; }
            set { if (type != value) { type = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the Page enumeration member of the PageModel object.
        /// </summary>
        public Page Page
        {
            get { return page; }
            set { if (page != value) { page = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Gets or sets the Chapter enumeration member of the PageModel object.
        /// </summary>
        public Chapter Chapter
        {
            get { return chapter; }
            set { if (chapter != value) { chapter = value; NotifyPropertyChanged(); } }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{page.GetDescription()}: {type}";
        }
    }
}