﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WlToolsLib.Producer
{
    public interface IProducer<T>
    {
        void Produc(T t);
    }
}
