using System;

namespace GameInterfaces
{
    public interface IGame
    {
        int Id { get; set; }
        string Name { get; set; }
        int ProdYear { get; set; }
        string Description { get; set; }
    }
}
