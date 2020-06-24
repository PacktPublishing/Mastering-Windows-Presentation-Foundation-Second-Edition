using CompanyName.ApplicationName.Managers.Interfaces;
using System.IO;
using System.IO.IsolatedStorage;

namespace CompanyName.ApplicationName.Managers
{
    /// <summary>
    /// Provides access to safe methods to store, retrieve and delete files from isolated storage stores on the users' computers.
    /// </summary>
    public class HardDriveManager : IHardDriveManager
    {
        private IsolatedStorageFile GetIsolatedStorageFile()
        {
            return IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain, null, null);
        }

        /// <summary>
        /// Saves a string input to a text file in isolated storage.
        /// </summary>
        /// <param name="filePath">The relative path of the file within isolated storage.</param>
        /// <param name="fileContents">The string contents of the text file to store in isolated storage.</param>
        public void SaveTextFile(string filePath, string fileContents)
        {
            try
            {
                IsolatedStorageFile isolatedStorageFile = GetIsolatedStorageFile();
                using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(filePath, FileMode.OpenOrCreate, isolatedStorageFile))
                {
                    using (StreamWriter streamWriter = new StreamWriter(isolatedStorageFileStream))
                    {
                        streamWriter.Write(fileContents);
                    }
                }
            }
            catch { /*Log error*/ }
        }

        /// <summary>
        /// Reads a text file from isolated storage and returns a string representing its contents.
        /// </summary>
        /// <param name="filePath">The relative path of the file within isolated storage.</param>
        public string ReadTextFile(string filePath)
        {
            string fileContents = string.Empty;
            try
            {
                IsolatedStorageFile isolatedStorageFile = GetIsolatedStorageFile();
                if (isolatedStorageFile.FileExists(filePath))
                {
                    using (IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream(filePath, FileMode.Open, isolatedStorageFile))
                    {
                        using (StreamReader streamReader = new StreamReader(isolatedStorageFileStream))
                        {
                            fileContents = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch { /*Log error*/ }
            return fileContents;
        }

        /// <summary>
        /// Permanently deletes a text file from isolated storage.
        /// </summary>
        /// <param name="filePath">The relative path of the file within isolated storage.</param>
        public void DeleteFile(string filePath)
        {
            try
            {
                IsolatedStorageFile isolatedStorageFile = GetIsolatedStorageFile();
                isolatedStorageFile.DeleteFile(filePath);
            }
            catch { /*Log error*/ }
        }

        /// <summary>
        /// Creates a folder in isolated storage.
        /// </summary>
        /// <param name="folderName">The relative path of the folder within isolated storage.</param>
        public void CreateFolder(string folderName)
        {
            try
            {
                IsolatedStorageFile isolatedStorageFile = GetIsolatedStorageFile();
                isolatedStorageFile.CreateDirectory(folderName);
            }
            catch { /*Log error*/ }
        }

        /// <summary>
        /// Permanently deletes a folder from isolated storage.
        /// </summary>
        /// <param name="folderName">The relative path of the folder within isolated storage.</param>
        public void DeleteFolder(string folderName)
        {
            try
            {
                IsolatedStorageFile isolatedStorageFile = GetIsolatedStorageFile();
                isolatedStorageFile.DeleteDirectory(folderName);
            }
            catch { /*Log error*/ }
        }
    }
}