using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DirectoryCopier.Command;
using DirectoryCopier.Utilities;
using System.Windows.Forms;

namespace DirectoryCopier.ViewModel
{
    public class MainViewModel : EventINotifyPropertyChanged
    {
        private ICommand commandGetPathFrom;
        private ICommand commandGetPathWhere;
        private string pathFrom;
        private string pathTo;

        public MainViewModel()
        {
            commandGetPathFrom = new DelegateCommand(GetPathFrom);
            commandGetPathWhere = new DelegateCommand(GetPathWhere);
        }

        public ICommand CommandGetPathFrom => commandGetPathFrom;

        public ICommand CommandGetPathWhere => commandGetPathWhere;

        public string PathFrom
        {
            get => pathFrom;
            set => SetProperty(ref pathFrom, value);
        }

        public string PathTo
        {
            get => pathTo;
            set => SetProperty(ref pathTo, value);
        }


        private void GetPathFrom()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            PathFrom = folderBrowserDialog.SelectedPath;
        }

        private void GetPathWhere()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            PathTo = folderBrowserDialog.SelectedPath;
            CopyDirectry(PathTo);
        }

        private void CopyDirectry(string pathToCopy)
        {
            string path = (string)pathToCopy;
            DirectoryInfo directoryInfo = null;
            DirectoryInfo[] directoryInfos = null;
            FileInfo[] fileInfos = null;

            if (path != null)
            {
                try
                {
                    directoryInfo = new DirectoryInfo(pathFrom);
                    directoryInfos = directoryInfo.GetDirectories();
                    fileInfos = directoryInfo.GetFiles();
                    Directory.CreateDirectory(PathFrom);
                }
                catch (UnauthorizedAccessException)
                {
                }

                if (fileInfos.Length > 0)
                {
                    foreach (FileInfo file in fileInfos)
                    {                      
                        CopyFiles(file, pathTo);
                    }

                }


                //if (directoryInfos != null)
                //{
                //    foreach (DirectoryInfo directory in directoryInfos)
                //    {
                //        try
                //        {
                //            FileInfo[] currentDirectoryFiles = directory.GetFiles( );

                //            foreach (FileInfo item in currentDirectoryFiles)
                //            {

                //            }
                //        }
                //        catch (UnauthorizedAccessException)
                //        {
                //        }

                //        //GetDirectories(directory.FullName);
                //    }
                //}

            }
        }

        public void CopyFiles(FileInfo fromFile, string pathTo)
        {
            using (FileStream binaryReader = new FileStream(fromFile.FullName, FileMode.Open))
            {
                byte[] bytes = new byte[4];

                while(binaryReader.CanRead)
                {
                    IAsyncResult result = binaryReader.BeginRead(bytes, 0, 4, null, null);
                    BinaryWriterOfFiles(pathTo, bytes);
                    binaryReader.EndRead(result);
                }
            }
        }

       
        private void BinaryWriterOfFiles(string pathTo, byte[] bytes)
        {
            using (FileStream binaryWriter = new FileStream(pathTo, FileMode.OpenOrCreate))
            {
              IAsyncResult result= binaryWriter.BeginWrite(bytes, 0, bytes.Length, null, null);
                binaryWriter.EndWrite(result);
            }
        }
    }
}