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

       

        private List<int> sweepView1D;
        public List<int> SweepView1D { get => sweepView1D; set { sweepView1D = value; RaisePropertyChanged(() => SweepView1D); } }

        #endregion
    }
}
