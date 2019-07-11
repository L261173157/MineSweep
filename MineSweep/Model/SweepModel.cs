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
        private int[,] sweep;

        public int[,] Sweep { get => sweep; set { sweep = value; RaisePropertyChanged(() => Sweep); } }
    }
}
