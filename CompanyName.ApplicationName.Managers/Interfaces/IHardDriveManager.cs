namespace CompanyName.ApplicationName.Managers.Interfaces
{
    public interface IHardDriveManager
    {
        void CreateFolder(string folderName);

        void DeleteFile(string filePath);

        void DeleteFolder(string folderName);

        string ReadTextFile(string filePath);

        void SaveTextFile(string filePath, string fileContents);
    }
}