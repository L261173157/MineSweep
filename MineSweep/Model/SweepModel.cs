using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MineSweep.Model
{
    public class SweepModel: ObservableObject
    {
        #region 属性
        private int[,] sweep;  //地雷数据

        public int[,] Sweep { get => sweep; set { sweep = value; RaisePropertyChanged(() => Sweep); } }

        private int[,] sweepView; //页面显示数据

        public int[,] SweepView { get => sweepView; set { sweepView = value; RaisePropertyChanged(() => SweepView); } }

       

        private List<int> sweepView1D; //页面显示数据的一维输出，用于wpf显示
        public List<int> SweepView1D { get => sweepView1D; set { sweepView1D = value; RaisePropertyChanged(() => SweepView1D); } }

        private int sweepNumer;  //地雷数据

        public int SweepNumer { get => sweepNumer; set { sweepNumer = value; RaisePropertyChanged(() => SweepNumer); } }
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
