using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinaqEdit
{
    public class BaseModel : INotifyPropertyChanged
    {
        #region Constructors

        #endregion

        #region Commands

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void RaiseStaticPropertyChanged(string propName)
        {
            EventHandler<PropertyChangedEventArgs> handler = StaticPropertyChanged;
            if (handler != null)
                handler(null, new PropertyChangedEventArgs(propName));

        } 
    }
}
