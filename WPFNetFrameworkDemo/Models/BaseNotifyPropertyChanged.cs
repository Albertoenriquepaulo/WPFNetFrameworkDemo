using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFNetFrameworkDemo.Models
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged //Esta interface debe estar implementada en cualquier clase que suponga hará cambios en UI
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) //Significa https://www.youtube.com/watch?v=dFvMpDDo9Mc&list=PL0wefbX90CmYNrO67FtZNDlnSrmWkF4bJ&index=3
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
