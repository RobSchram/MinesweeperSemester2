﻿using LogicLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.interfaces
{
    public interface IFieldDao
    {
        public FieldDto GetField();
        public void ClearField();
        public void StoreField(Cell[,] mineField);
    }
}
