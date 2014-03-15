// -----------------------------------------------------------------------
// <copyright file="FileFinder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FileFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Diagnostics;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileFinder : FileFinderBase,IFileFinder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FileFinderCondition condition;
        public FolderCondition folderCondition;
        bool CheckInnerDirectories = true;

        public FileFinder(bool checkInner)
        {
            this.condition = new FileFinderCondition();
            this.folderCondition = new FolderCondition();
            this.CheckInnerDirectories = checkInner;
        }

        public void Find(string path)
        {
            DoCheckDirectory(path);
        }

        protected override void DoCheckDirectory(string path)
        {
            try
            {
                if (MultiThreadedFileFinder.Cancel)
                {
                    return;
                }
                OnFolderChanged(path);
                CheckFiles(path);
                if (CheckInnerDirectories)
                {
                    var dirs = Directory.GetDirectories(path);
                    foreach (var dir in dirs)
                    {
                        if (folderCondition.IsValid(dir))
                        {
                            //lstResult.Items.Add(dir,result);
                            CheckFiles(dir);
                            DoCheckDirectory(dir);
                        }
                        else
                        {
                            int a = 10;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                log.Error("************checking directory failed.************ path=" + path, exception);
                
            }
        }

        private void CheckFiles(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var f in files)
                {
                    if (condition.IsValid(f))
                    {
                        OnFileFound(f);
                    }
                }

            }
            catch (Exception exception)
            {
                log.Error("************checking file failed.************path=" + path, exception);
            }
        }


    }
}
