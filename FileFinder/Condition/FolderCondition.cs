// -----------------------------------------------------------------------
// <copyright file="FolderCondition.cs" company="">
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
    public class FolderCondition
    {
        List<string> inValidFolders;
        public FolderCondition()
        {
            this.inValidFolders = new List<string>();
            inValidFolders.Add(".m2");
            inValidFolders.Add("debug");
            inValidFolders.Add(@"settlementreport");
            inValidFolders.Add("demo");
            inValidFolders.Add("xmlindex");
            inValidFolders.Add(".svn");
            inValidFolders.Add("visual studio 2010");
        }
        public bool IsValid(string path)
        {
            path = path.ToLower();
            var result = !inValidFolders.Where(d => path.EndsWith(d)).Any();
            if (result)
            {
                return !path.Contains("caspian.banking");
            }
            return result;
        }
    }
}
