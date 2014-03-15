// -----------------------------------------------------------------------
// <copyright file="FolderChangedEventArgs.cs" company="">
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
    public class FolderChangedEventArgs : EventArgs
    {
        public string FolderName { get; set; }

        public FolderChangedEventArgs(string name)
        {
            this.FolderName = name;
        }
    }
}
