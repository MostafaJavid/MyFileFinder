// -----------------------------------------------------------------------
// <copyright file="FileFoundEventArgs.cs" company="">
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
    public class FileFoundEventArgs : EventArgs
    {
        public string Path { get; set; }

        public FileFoundEventArgs(string path)
        {
            this.Path = path;
        }
    }
}
