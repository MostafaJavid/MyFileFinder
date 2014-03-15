// -----------------------------------------------------------------------
// <copyright file="FileFinderCondition.cs" company="">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FileFinderCondition
    {
        DateTime Now;
        public FileFinderCondition()
        {
            this.Now = DateTime.Now;
        }
        public bool IsValid(string file)
        {
            //return file.ToLower().Contains("netbeans");
            if (CheckExtenstion(file))
            {
                var CTime = File.GetCreationTime(file);
                if (this.Now.Subtract(CTime).TotalDays >= 0 && this.Now.Subtract(CTime).TotalDays <= 7)
                    return true;
            }
            return false;
        }

        private bool CheckExtenstion(string file)
        {
            //return !(file.ToLower().EndsWith(".mp3"));
            return IsVideoFile(file);
        }

        private static bool IsVideoFile(string file)
        {
            return (file.ToLower().EndsWith(".flv") ||
                    file.ToLower().EndsWith(".mp4") ||
                    file.ToLower().EndsWith(".wmv") ||
                    file.ToLower().EndsWith(".3gp") ||
                    file.ToLower().EndsWith(".vob") ||
                    file.ToLower().EndsWith(".ts")  ||
                    file.ToLower().EndsWith(".avi") ||
                    file.ToLower().EndsWith(".mov") ||
                    file.ToLower().EndsWith(".mpg") ||
                    file.ToLower().EndsWith(".mpeg") ||
                    file.ToLower().EndsWith(".rmvb") ||
                    file.ToLower().EndsWith(".rm") ||
                    file.ToLower().EndsWith(".asf") ||
                    file.ToLower().EndsWith(".mkv") 
                    );
        }
    }
}
