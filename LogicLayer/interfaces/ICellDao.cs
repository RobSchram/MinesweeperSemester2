using LogicLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface ICellDao
    {
        CellDto GetCell(int horizontal, int vertical);
    }
}
