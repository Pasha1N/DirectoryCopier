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
    public class MainViewModel: EventINotifyPropertyChanged
    {
        private ICommand commandGetPathFrom;
        private ICommand commandGetPathWhere;
        private string pathFrom;
        private string pathWhere;

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

        public string PathWhere
        {
            get => pathWhere;
            set => SetProperty(ref pathWhere, value);
        }

        private void ReadFile()
        {
         
            
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
            PathWhere = folderBrowserDialog.SelectedPath;
        }

        private void WriteFile()
        {


        }


    }
}