using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBoxBoard.Equipment
{
    public class ShowingData : INotifyPropertyChanged
    {
        //实现了一个后台数据变化，前台自动变化的类
        public String simpleData__;

        public event PropertyChangedEventHandler PropertyChanged;

        public String SimpleData
        {
            get { return simpleData__; }
            set
            {
                simpleData__ = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, 
                        new PropertyChangedEventArgs("SimpleData"));
                }
            }
        }

        public ShowingData(String simpleData_)
        {
            SimpleData = simpleData_;
        }
    }
}
