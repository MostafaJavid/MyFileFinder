// -----------------------------------------------------------------------
// <copyright file="MultiThreadedFileFinder.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace FileFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Threading;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MultiThreadedFileFinder : FileFinderBase, IFileFinder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        List<WorkerArgument> inputs;
        public static bool Cancel { get; set; }
        
        protected override void DoCheckDirectory(string path)
        {
            inputs = new List<WorkerArgument>();
            var dirs = Directory.GetDirectories(path);
            var index = 0;
            var length = 1;
            while (index < dirs.Length)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                var input = new WorkerArgument();
                inputs.Add(input);
                for (int i = 0; i < length; i++)
                {
                    index += 1;
                    if (index < dirs.Length)
                    {
                        input.Folders.Add(dirs[index]);
                    }
                }
                worker.RunWorkerAsync(input);
            }

            foreach (var input in inputs)
            {
                //Thread.Sleep(1000);
                //input.autoResetEvent.WaitOne(5000);
            }
            
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var input = e.Argument as WorkerArgument;
            if (input != null)
            {
                var ff = new FileFinder(true);
                ff.FileFound += ff_FileFound;
                ff.FolderChanged += ff_FolderChanged;
                foreach (var path in input.Folders)
                {
                    if (MultiThreadedFileFinder.Cancel)
                    {
                        break;
                    }
                    ff.Find(path);
                }
                input.autoResetEvent.Set();
            }
        }

        void ff_FolderChanged(object sender, FolderChangedEventArgs e)
        {
            OnFolderChanged(e.FolderName);
        }

        void ff_FileFound(object sender, FileFoundEventArgs e)
        {
            OnFileFound(e.Path);
        }

        public void Find(string path)
        {
            log.Info("-------------------------------------------------------------------------");
            log.Info("---------------------new file and folder finding started.----------------");
            log.Info("-------------------------------------------------------------------------");
            DoCheckDirectory(path);
        }

        class WorkerArgument
        {
            public AutoResetEvent autoResetEvent { get; set; }
            public List<string> Folders { get; set; }

            public WorkerArgument()
            {
                this.autoResetEvent = new AutoResetEvent(false);
                this.Folders = new List<string>();
            }
        }
    }
}
