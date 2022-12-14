using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace USBCopyer
{
    public partial class diskUUID : Form
    {

        public diskUUID()
        {
            InitializeComponent();
            Icon = Program.ico;
            version.Text += Application.ProductVersion;
            getDisk();
        }

        private void diskUUID_Load(object sender, EventArgs e)
        {
            
        }

        void getDisk()
        {
            string[] logicalDiskList = Directory.GetLogicalDrives();
            diskList.Items.Clear();
            foreach (string logicalDisk in logicalDiskList)
            {
                diskList.Items.Add(logicalDisk.Substring(0,2));
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            getDisk();
        }

        /*
        DriveType
            Unknown (0)
            No Root Directory (1)
            Removable Disk (2)
            Local Disk (3)
            Network Drive (4)
            Compact Disc (5)
            RAM Disk (6)
        */
        private void diskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            thisDiskName.Text = "";
            thisDiskSer.Text = "";
            thisDiskFileSystem.Text = "";
            thisDiskDescription.Text = "";
            try
            {
                string disk = diskList.SelectedItem.ToString();
                ManagementObject diskinfo = new ManagementObject("win32_logicaldisk.deviceid=\"" + disk + "\"");
                thisDiskID.Text = disk;
                thisDiskID2.Text = diskinfo.Properties["Name"].Value.ToString();
                thisDiskName.Text = diskinfo.Properties["VolumeName"].Value.ToString();
                thisDiskFileSystem.Text = diskinfo.Properties["FileSystem"].Value.ToString();
                thisDiskDescription.Text = "[" + diskinfo.Properties["DriveType"].Value.ToString() + "] " + diskinfo.Properties["Description"].Value.ToString();
                thisDiskSer.Text = diskinfo.Properties["VolumeSerialNumber"].Value.ToString();
            }
            catch (Exception) { }
        }
    }
}
