using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
    }
}
