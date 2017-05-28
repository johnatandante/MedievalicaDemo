using System;
using System.Collections.Generic;
using System.Text;

namespace Medievalica.Game.Interfaces
{
    public interface ISessionClient
    {

        string Name { get; }

        string TokenId { get;  }

    }
}
