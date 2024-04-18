using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IStoreData
    {
        void StoreCell(int horizontal, int vertical, int isMine, int isVisible, int amountOfMinesAroundCell);
    }
}
