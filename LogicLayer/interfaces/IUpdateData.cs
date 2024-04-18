﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IUpdateData
    {
        void UpdateCell(int isMine, int isVisible, int amountOfMinesAroundCell);
    }
}