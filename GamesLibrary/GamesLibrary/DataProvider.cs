using System;
using GameInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesLibrary
{
    public class DataProvider:ICatalog
    {
        private static List<IGame> games;
        public static List<IGame> GetGames() {
            DataProvider.games = new List<IGame>();
            DataProvider.games.Add(new Game(1, "Wiedźmin 3: Dziki Gon",2015, "Gra action RPG, stanowiąca trzecią część przygód Geralta z Rivii. Podobnie jak we wcześniejszych odsłonach cyklu, Wiedźmin 3: Dziki Gon bazuje na motywach twórczości literackiej Andrzeja Sapkowskiego, jednak nie jest bezpośrednią adaptacją żadnej z jego książek."));
            DataProvider.games.Add(new Game(2, "Cyberpunk 2077", 2020, "Osadzona w otwartym świecie w klimacie science fiction gra RPG oparta na papierowym systemie fabularnym Cyberpunk. Produkcja została opracowana przez studio CD Projekt RED, które wsławiło się kultową serią o Wiedźminie."));
            DataProvider.games.Add(new Game(3, "Dragon Ball Z: Kakarot", 2020, "RPG akcji adaptujące kultową japońską serię animowaną Dragon Ball Z. Gracz wciela się w wojownika imieniem Goku, eksploruje świat i wypełnia zadania, a przede wszystkim – regularnie toczy widowiskowe starcia z adwersarzami."));
            DataProvider.games.Add(new Game(4, "Rain of Reflections", 2019, "Miks przygodówki i taktycznej strategii/RPG, który został osadzony w dystopijnym cyberpunkowym świecie i opowiada historię trzech różnych postaci (w epizodach). Rain of Reflections oferuje nietypowy system radzenia sobie z konfliktami. Liczą się w nim motywacje bohaterów, a przemoc nie jest jedynym rozwiązaniem."));
            DataProvider.games.Add(new Game(5, "Grand Theft Auto V", 2015, "Piąta, pełnoprawna odsłona niezwykle popularnej serii gier akcji, nad której rozwojem pieczę sprawuje studio Rockstar North we współpracy z koncernem Take Two Interactive. "));
            DataProvider.games.Add(new Game(6, "Farming Simulator 19", 2018, "Farming Simulator 19 to kolejna część popularnej serii symulatorów rolnika. Tym razem twórcy oparli swoją grę na ulepszonym silniku graficznym, a także zawarli w niej złożony z trzech dużych lokacji, rozbudowany otwarty świat."));
            DataProvider.games.Add(new Game(7, "The Elder Scrolls V: Skyrim", 2011, "Zrealizowana z dużym rozmachem gra action-RPG, będąca piątą częścią popularnego cyklu The Elder Scrolls. Produkcja studia Bethesda Softworks pozwala graczom ponownie przenieść się do fantastycznego świata Nirn. "));
            return DataProvider.games;
        }

        public List<IGame> GetAllGames()
        {
            return DataProvider.games;
        }
    }
}
