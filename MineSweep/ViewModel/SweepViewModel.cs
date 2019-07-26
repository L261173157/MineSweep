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
            sweepModel = new SweepModel() { Sweep = new int[5, 5], SweepView = new int[5, 5], SweepView1D = new ObservableCollection<int?>(), SweepNumer = 5 };
            Reset();
            sweepModel.ONiswinChanged += SweepModel_ONiswinChanged;
        }
        #region 通知前台

        private void SweepModel_ONiswinChanged()
        {
            if (sweepModel.Iswin == 2)
                Messenger.Default.Send<string>("你踩雷了", "ShowWin");
            if (sweepModel.Iswin == 1)
                Messenger.Default.Send<string>("你胜利了", "ShowWin");
        }

        #endregion
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
        //内部测试
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
        
        /// <summary>
        /// 游戏重启命令
        /// </summary>
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
        
        /// <summary>
        /// 界面点击命令
        /// </summary>
        private RelayCommand<string> viewClick;
        public RelayCommand<string> ViewClick
        {
            get
            {
                if (viewClick == null)
                    return new RelayCommand<string>(Click, CanClick);
                return viewClick;
            }
            set
            {
                viewClick = value;
            }
        }
        /// <summary>
        /// 界面右击命令
        /// </summary>
        private RelayCommand<string> viewRightClick;
        public RelayCommand<string> ViewRightClick
        {
            get
            {
                if (viewRightClick == null)
                    return new RelayCommand<string>(RightClick, CanClick);
                return viewRightClick;
            }
            set
            {
                viewRightClick = value;
            }
        }
        /// <summary>
        /// 界面左右双击命令
        /// </summary>
        private RelayCommand<string> viewDoubleClick;
        public RelayCommand<string> ViewDoubleClick
        {
            get
            {
                if (viewRightClick == null)
                    return new RelayCommand<string>(DoubleClick, CanClick);
                return viewRightClick;
            }
            set
            {
                viewRightClick = value;
            }
        }
        #endregion
        #region 命令方法
        /// <summary>
        /// 测试方法
        /// </summary>
        private void Test() 
        {
            
            sweepModel.SweepView1D.Clear();
            foreach (var item in sweepModel.SweepView)
            {
                sweepModel.SweepView1D.Add(item);
            }
        }

        /// <summary>
        /// 游戏重启方法
        /// </summary>
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
            Array.Clear(sweepModel.SweepView, 0, sweepModel.SweepView.Length);
            sweepModel.SweepView1D.Clear();
            foreach (var item in sweepModel.SweepView)
            {
                sweepModel.SweepView1D.Add(item);
            }

            for (int j = 0; j < sweepModel.SweepView1D.Count; j++)
            {
                if (sweepModel.SweepView1D[j] == 0)
                {
                    sweepModel.SweepView1D[j] = null;
                }
            }

            sweepModel.Iswin = 0;
            sweepModel.NumRemain = 25-sweepModel.SweepNumer;
            sweepModel.SweepNumerRemain = sweepModel.SweepNumer;
        }

        /// <summary>
        /// 界面点击命令
        /// </summary>
        /// <param name="param">按钮参数（y,x） </param>
        private void Click(string param)
        {
            string[] paramChar = param.Split(',');
            int[] paramInt = new int[2];
            paramInt[0] = Convert.ToInt32(paramChar[0]);
            paramInt[1] = Convert.ToInt32(paramChar[1]);


            if (sweepModel.SweepView[paramInt[0], paramInt[1]] == 0)
            {
                if (sweepModel.Sweep[paramInt[0], paramInt[1]] == 1)
                {
                    sweepModel.SweepView[paramInt[0], paramInt[1]] = 9;
                    sweepModel.Iswin = 2;
                }
                else
                {
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1] - 1) >= 0)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] - 1, paramInt[1] - 1];
                    }
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1]) >= 0)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] - 1, paramInt[1]];
                    }
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1] + 1) < 5)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] - 1, paramInt[1] + 1];
                    }
                    if ((paramInt[0]) >= 0 && (paramInt[1] - 1) >= 0)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0], paramInt[1] - 1];
                    }
                    if ((paramInt[0]) >= 0 && (paramInt[1] + 1) < 5)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0], paramInt[1] + 1];
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1] - 1) >= 0)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] + 1, paramInt[1] - 1];
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1]) >= 0)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] + 1, paramInt[1]];
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1] + 1) < 5)
                    {
                        sweepModel.SweepView[paramInt[0], paramInt[1]] += sweepModel.Sweep[paramInt[0] + 1, paramInt[1] + 1];
                    }
                    sweepModel.NumRemain--;
                    if(sweepModel.NumRemain==0)
                    {
                        sweepModel.Iswin = 1;
                    }
                }
                if (sweepModel.SweepView[paramInt[0], paramInt[1]] == 0)
                {
                    sweepModel.SweepView[paramInt[0], paramInt[1]] = 10;
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1] - 1) >= 0)
                    {
                        string paramExt = (paramInt[0] - 1).ToString() + "," + (paramInt[1] - 1).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1]) >= 0)
                    {
                        string paramExt = (paramInt[0] - 1).ToString() + "," + (paramInt[1] ).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0] - 1) >= 0 && (paramInt[1] + 1) < 5)
                    {
                        string paramExt = (paramInt[0] - 1).ToString() + "," + (paramInt[1] + 1).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0]) >= 0 && (paramInt[1] - 1) >= 0)
                    {
                        string paramExt = (paramInt[0] ).ToString() + "," + (paramInt[1] - 1).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0]) >= 0 && (paramInt[1] + 1) < 5)
                    {
                        string paramExt = (paramInt[0] ).ToString() + "," + (paramInt[1] + 1).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1] - 1) >= 0)
                    {
                        string paramExt = (paramInt[0] + 1).ToString() + "," + (paramInt[1] - 1).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1]) >= 0)
                    {
                        string paramExt = (paramInt[0] + 1).ToString() + "," + (paramInt[1] ).ToString();
                        Click(paramExt);
                    }
                    if ((paramInt[0]) + 1 < 5 && (paramInt[1] + 1) < 5)
                    {
                        string paramExt = (paramInt[0] + 1).ToString() + "," + (paramInt[1] + 1).ToString();
                        Click(paramExt);
                    }
                }
                    
            }

            sweepModel.SweepView1D[paramInt[0] * 5 + paramInt[1]] = sweepModel.SweepView[paramInt[0], paramInt[1]];

        }
        
        /// <summary>
        /// 是否可以点击
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanClick(string arg)
        {
            string[] param = arg.Split(',');
            int y= Convert.ToInt32(param[0]);
            int x = Convert.ToInt32(param[1]);
            if (sweepModel.Iswin == 0 && sweepModel.SweepView[y,x]==0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 界面右击方法
        /// </summary>
        /// <param name="param">按钮参数（y,x）</param>
        private void RightClick(string arg)
        {
            string[] param = arg.Split(',');
            int y = Convert.ToInt32(param[0]);
            int x = Convert.ToInt32(param[1]);
            if (sweepModel.SweepView1D[y * 5 + x] == null)
            {
                sweepModel.SweepView1D[y * 5 + x] = 11;
                sweepModel.SweepNumerRemain --;
            }
                
            else
            {
                sweepModel.SweepView1D[y * 5 + x] = null;
                sweepModel.SweepNumerRemain++;
            }
               

        }
        /// <summary>
        /// 界面左右双击方法
        /// </summary>
        /// <param name="param">按钮参数（y,x）</param>
        private void DoubleClick(string arg)
        {
            string[] param = arg.Split(',');
            int y = Convert.ToInt32(param[0]);
            int x = Convert.ToInt32(param[1]);
            if (sweepModel.SweepView1D[y * 5 + x] == null)
            {
                
            }
            else
            {
                
            }


        }


        #endregion
    }
}
