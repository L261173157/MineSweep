using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MineSweep.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;


namespace MineSweep.ViewModel
{
   public class SweepViewModel: ViewModelBase
    {
        public SweepViewModel()
        {
            sweepModel = new SweepModel() { Sweep = new int[5, 5], SweepView = new int[5, 5], SweepView1D = new List<int>() };
        }
        #region 属性
        private SweepModel sweepModel;
        public SweepModel SweepModel
        {
            get
            {
                return sweepModel;
            }
            set
            {
                sweepModel = value;
                RaisePropertyChanged(nameof(SweepModel));
            }
        }
        #endregion

        #region 命令
        private RelayCommand testCmd;
        public RelayCommand TestCmd
        {
            get
            {
                if (testCmd == null)
                    return new RelayCommand(Test);
                return testCmd;
            }
            set
            {
                testCmd = value;
            }
        }
        #endregion
        #region 命令方法
        private void Test()
        {
            sweepModel.SweepView1D = sweepModel.SweepView.Cast<int>().ToList();
        }

        #endregion
    }
}
