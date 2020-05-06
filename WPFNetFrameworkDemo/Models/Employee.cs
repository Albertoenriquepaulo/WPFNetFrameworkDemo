using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel; //Notifica cuando hay cambios en un property, cuando se invoka (INotifyPropertyChanged)
namespace WPFNetFrameworkDemo.Models
{
    public class Employee : BaseObject, INotifyPropertyChanged
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

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); } //Cada vez que cambie el valor de Id, tambien cambiará id, y se notificara, por eso aprovechamos el seter para llamar a la función 
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged("Age"); }
        }


    }
}
