using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Dto
{
    public class CellDto
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }
        public int IsMine { get; set; }
        public int IsVisible { get; set; }
        public int AmountOfMinesAroundCell { get; set; }
        public int GameId {  get; set; }
    }
}
