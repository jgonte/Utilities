using System;
using System.IO;
using System.Text;

namespace Utilities
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Creates a temporary directory to work with the libraries
        /// </summary>
        /// <returns></returns>
        public static string CreateTempDirectory()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// Deletes all the files and other folders that are in the folder identified by its path
        /// </summary>
        /// <param name="path"></param>
        public static void ClearFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.IsReadOnly = false;
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName); // Recurse
                di.Delete();
            }
        }

        /// <summary>
        /// Deletes the folder identified by path
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFolder(string path) 
        {
            ClearFolder(path);
            Directory.Delete(path);
        }

        /// <summary>
        /// Returns the relative path of absolutePath with respect to the basePath
        /// </summary>
        /// <param name="directory">The base path which is also an absolute one</param>
        /// <param name="path">The absolute path to extract the relative one from</param>
        /// <returns></returns>
        public static string GetRelativePath(string directory, string path)
        {
            char separator = Path.DirectorySeparatorChar;
            string[] directoryParts = directory.Split(separator);
            string[] pathParts = path.Split(separator);

            if (!directoryParts[0].Equals(pathParts[0], System.StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException("Paths do not have a common root");
            }

            int directoryLength = directoryParts.Length;
            int pathLength = pathParts.Length;
            int length = directoryLength < pathLength ? directoryLength : pathLength;
            int commonLength = 0;

            // Compute the index of the last common root
            for (int i = 0; i < length; ++i)
            {
                if (directoryParts[i].Equals(pathParts[i], System.StringComparison.InvariantCultureIgnoreCase))
                {
                    ++commonLength;
                }
                else
                {
                    break;
                }
            }

            StringBuilder builder = new StringBuilder();

            // Add the go back path (..\)
            for (int i = commonLength; i < directoryLength; ++i)
            {
                builder.Append("..");
                builder.Append(separator);
            }

            // Add the folders
            for (int i = commonLength; i < pathLength; ++i)
            {
                builder.Append(pathParts[i]);
                builder.Append(separator);
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        /// <summary>
        /// Traverses a directory recursively, calling the method OnDirectory for each node
        /// </summary>
        /// <param name="path"></param>
        /// <param name="OnDirectory"></param>
        /// <param name="CanTraverse"></param>
        public static void Traverse(string path, Action<DirectoryInfo, string, int> OnDirectory,  Func<Exception, DirectoryInfo, string, int, bool> OnException, Func<DirectoryInfo, int, bool> CanTraverse = null)
        {
            int level = 1;
            string relativePath = string.Empty;

            Traverse(new DirectoryInfo(path), relativePath, level, OnDirectory, OnException, CanTraverse);
        }

        private static void Traverse(DirectoryInfo directoryInfo, string relativePath, int level, Action<DirectoryInfo, string, int> OnDirectory, Func<Exception, DirectoryInfo, string, int, bool> OnException, Func<DirectoryInfo, int, bool> CanTraverse = null)
        {
            try
            {
                OnDirectory(directoryInfo, relativePath, level);

                string previousRelativePath = relativePath;
                relativePath = Path.Combine(relativePath, directoryInfo.Name);

                ++level;

                foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
                {
                    bool canTraverse = true;

                    if (CanTraverse != null)
                    {
                        canTraverse = CanTraverse(subDirectoryInfo, level);
                    }

                    if (canTraverse)
                    {
                        Traverse(subDirectoryInfo, relativePath, level, OnDirectory, OnException, CanTraverse); // Recurse
                    }
                }

                --level;

                relativePath = previousRelativePath; // Restore the relative path
            }
            catch (Exception exception)
            {
                if (OnException != null)
                {
                    if (!OnException(exception, directoryInfo, relativePath, level))
                    {
                        return; // Do not continue if the error handler returns false
                    }
                }
                else
                {
                    throw; // No error handler, therefore re-throw
                }
            }
        }

        /// <summary>
        /// Copies the contents recursively from the source direcotry to the destination one
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyDirectory(string source, string destination, Action<string> OnFileCopied = null, Func<Exception, DirectoryInfo, string, int, bool> OnException = null)
        {
            DirectoryHelper.Traverse(source,
                (directoryInfo, relativePath, level) =>
                {
                    string absoluteDirectoryPath = Path.Combine(destination, relativePath, directoryInfo.Name);

                    CopyFiles(directoryInfo, absoluteDirectoryPath, OnFileCopied);
                },
            OnException); // Do not handle errors
        }

        public static void CopyFiles(DirectoryInfo directoryInfo, string destination, Action<string> OnFileCopied = null)
        {
            // Make sure the directory is clean or created
            if (Directory.Exists(destination)) // Clear all the subdirectories and files
            {
                DirectoryHelper.ClearFolder(destination);
            }
            else // Else create the directory
            {
                Directory.CreateDirectory(destination);
            }

            // Copy all the files only, do not worry about sub-directories since they will be traversed as well
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                string absoluteFilePath = Path.Combine(destination, fileInfo.Name);

                // Copy the file to the destination directory
                fileInfo.CopyTo(absoluteFilePath, true);

                if (OnFileCopied != null)
                {
                    OnFileCopied(absoluteFilePath);
                }
            }
        }
    }
}
