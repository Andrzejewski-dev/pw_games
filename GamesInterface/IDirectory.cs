using System;
using System.Collections.Generic;

namespace GameInterfaces
{
    public interface ICatalog
    {
        List<IGame> GetAllGames();
    }
}
