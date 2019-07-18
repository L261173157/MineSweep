using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace MineSweep.Model
{
    public class SweepModel: ObservableObject
    {
        #region 属性
        private int[,] sweep;
        /// <summary>
        /// 地雷数据
        /// </summary>
        public int[,] Sweep { get => sweep; set { sweep = value; RaisePropertyChanged(() => Sweep); } }

        private int[,] sweepView; 
        /// <summary>
        /// 页面显示数据
        /// </summary>
        public int[,] SweepView { get => sweepView; set { sweepView = value; RaisePropertyChanged(() => SweepView); } }

       

        private ObservableCollection<int?> sweepView1D; //页面显示数据的一维输出，用于wpf显示
        /// <summary>
        /// 页面显示数据（一维）
        /// </summary>
        public ObservableCollection<int?> SweepView1D { get => sweepView1D; set { sweepView1D = value; RaisePropertyChanged(() => SweepView1D); } }

        private int sweepNumer;  
        /// <summary>
        /// 地雷数量
        /// </summary>
        public int SweepNumer { get => sweepNumer; set { sweepNumer = value; RaisePropertyChanged(() => SweepNumer); } }

        private int sweepNumerRemain;
        /// <summary>
        /// 地雷未标记数量
        /// </summary>
        public int SweepNumerRemain { get => sweepNumerRemain; set { sweepNumerRemain = value; RaisePropertyChanged(() => SweepNumerRemain); } }

        private int numRemain;
        /// <summary>
        /// 剩余未开数量
        /// </summary>
        public int NumRemain { get => numRemain; set { numRemain = value; RaisePropertyChanged(() => NumRemain); } }

        private int isWin;
        private int isWinLast;
        /// <summary>
        /// 胜利(0未完成，1胜利，2失败)
        /// </summary>
        public int Iswin
        {
            get
            {
                return isWin;

            }
            set
            {
                
                isWin = value;
                RaisePropertyChanged(() => Iswin);
                if (value != isWinLast)
                {
                    ONiswinChanged?.Invoke();
                }
                isWinLast = value;

            }
        }

        #endregion
        #region 自定义事件
        public delegate void WinChangedHandler();
        /// <summary>
        /// isWin改变时触发事件
        /// </summary>
        public event WinChangedHandler ONiswinChanged;
        #endregion
    }
    public class SweepViewParam
    {
        private int locationX;  //按钮x位置
        private int locationY;  //按钮x位置

        public int LocationX { get => locationX; set => locationX = value; }
        public int LocationY { get => locationY; set => locationY = value; }
    }
}
