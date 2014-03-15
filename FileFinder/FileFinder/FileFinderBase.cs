// -----------------------------------------------------------------------
// <copyright file="FileFinderBase.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FileFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class FileFinderBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<FolderChangedEventArgs> FolderChanged;
        public event EventHandler<FileFoundEventArgs> FileFound;

        protected void OnFolderChanged(string path)
        {
            if (FolderChanged != null)
            {
                FolderChanged(null, new FolderChangedEventArgs(path));
            }
        }

        protected void OnFileFound(string path)
        {
            if (FileFound != null)
            {
                FileFound(null, new FileFoundEventArgs(path));
            }
        }

        protected abstract void DoCheckDirectory(string path);

    }
}
