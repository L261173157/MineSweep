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
            sweepModel = new SweepModel() { Sweep = new int[5, 5], SweepView = new int[5, 5], SweepView1D = new List<int>(),SweepNumer=5 };
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
        //界面点击位置参数
        private SweepViewParam sweepViewParam;
        public SweepViewParam SweepViewParam
        {
            get
            {
                return sweepViewParam;
            }
            set
            {
                sweepViewParam = value;
                RaisePropertyChanged(nameof(SweepViewParam));
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
        //重新生成
        private RelayCommand resetCmd;
        public RelayCommand ResetCmd
        {
            get
            {
                if (resetCmd == null)
                    return new RelayCommand(Reset);
                return resetCmd;
            }
            set
            {
                resetCmd = value;
            }
        }
        //点击命令
        private RelayCommand<SweepViewParam> viewClick;
        public RelayCommand<SweepViewParam> ViewClick
        {
            get
            {
                if (viewClick == null)
                    return new RelayCommand<SweepViewParam>(Click);
                return viewClick;
            }
            set
            {
                viewClick = value;
            }
        }
        #endregion
        #region 命令方法
        //用于界面测试的方法
        private void Test() 
        {
            sweepModel.SweepView1D = sweepModel.Sweep.Cast<int>().ToList();
        }
        //重新生成地雷及界面
        private void Reset()
        {
            Array.Clear(sweepModel.Sweep, 0, sweepModel.Sweep.Length);
            Random random = new Random();
            int i = 0;
            while(true)
            {
                int a = random.Next(0, 5);
                int b = random.Next(0, 5);
                if(sweepModel.Sweep[a,b]==0)
                {
                    sweepModel.Sweep[a, b] = 1;
                    i++;
                }
                if (i == sweepModel.SweepNumer)
                    break;
            }
        }
        //点击界面按钮事件方法
        private void Click(SweepViewParam param)
        {
            sweepViewParam = param;
        }

        #endregion
    }
}
