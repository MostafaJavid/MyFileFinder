using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace FileFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Object lockObject = new object();
        private void btnGo_Click(object sender, EventArgs e)
        {
            string path = "v:\\EveryOne\\";
            var fileFinder = new MultiThreadedFileFinder();
            fileFinder.FolderChanged += fileFinder_FolderChanged;
            fileFinder.FileFound += fileFinder_FileFound;
            fileFinder.Find(path);
            lblPath.Text = "";
        }

        void fileFinder_FileFound(object sender, FileFoundEventArgs e)
        {
            lock (lockObject)
            {
                //lstResult.Items.Add(e.Path);
                foreach (var item in lstResult.Items)
                {
                    if (item.ToString().Equals(e.Path))
                        return;
                }
                if (this.lstResult.InvokeRequired)
                {
                    Invoke(
                        (Action)delegate
                        {
                            lstResult.Items.Add(e.Path);
                            Application.DoEvents();
                        });
                }
                else
                {
                    lstResult.Items.Add(e.Path);
                    Application.DoEvents();
                }

                Application.DoEvents();
            }
        }

        void fileFinder_FolderChanged(object sender, FolderChangedEventArgs e)
        {
            //lock (lockObject)
            //{
            //    if (lblPath.InvokeRequired)
            //    {
            //        lblPath.Invoke(new MethodInvoker(delegate
            //        {
            //            lblPath.Text = e.FolderName;
            //            Application.DoEvents();
            //        }));
            //    }

            //}


            if (this.lblPath.InvokeRequired)
            {
                Invoke(
                    (Action)delegate
                    {
                        lblPath.Text = e.FolderName;
                        Application.DoEvents();
                    });
            }
            else
            {
                lblPath.Text = e.FolderName;
                Application.DoEvents();
            }

            //SetControlPropertyValue(lblPath, "Text", e.FolderName);
            Application.DoEvents();
        }

        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        private void lstResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MultiThreadedFileFinder.Cancel = true;
        }

        private void brnExport_Click(object sender, EventArgs e)
        {
            Dictionary<string,string> files = new Dictionary<string,string>();
            foreach (var item in lstResult.Items)
            {
                if (!files.ContainsKey(item.ToString()))
                {
                    files.Add(item.ToString(), "");
                }
            }

            var file = File.CreateText("c:\\Caspian\\Export.txt");
            foreach (var item in files)
            {
                file.WriteLine(item);
            }
            file.Flush();
            file.Close();
            MessageBox.Show("OK");
        }


    }
}
