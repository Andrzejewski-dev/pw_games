using GameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesLibrary
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProdYear { get; set; }
        public string Description { get; set; }

        public Game()
        {
        }

        public Game(int id, string name, int prodYear, string description)
        {
            Id = id;
            Name = name;
            ProdYear = prodYear;
            Description = description;
        }

        internal Game Create(int id, string name, int prodYear, string description)
        {
            Id = id;
            Name = name;
            ProdYear = prodYear;
            Description = description;
            return this;
        }

    }
}
