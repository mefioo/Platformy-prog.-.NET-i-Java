using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OxyPlot;

namespace Lab01
{
    public class ViewModel : ViewModelBase
    {
        private PlotModel _scatterModel;
        public  PlotModel ScatterModel
        {
            get { return _scatterModel; }
            set
            {
                if (value != _scatterModel)
                {
                    _scatterModel = value;
                    OnPropertyChanged();
                }
            }
        }


    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String propName = null)
        {
            // C#6.O
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}