using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    public class Dwelling : IMapObject //жилье
    {
        public int Owner { get; set; }
        public void Act(Player player)
        {
            Owner = player.Id;
        }
    }

    public class Mine : IMapObject
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }

        public void Act(Player player)
        {
            if (player.CanBeat(Army))
            {
                Owner = player.Id;
                player.Consume(Treasure);
            }
            else player.Die();

        }
    }

    public class Creeps : IMapObject
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }

        public void Act(Player player)
        {
            if (player.CanBeat(Army))
                player.Consume(Treasure);
            else
                player.Die();
        }
    }

    public class Wolfs : IMapObject
    {
        public Army Army { get; set; }

        public void Act(Player player)
        {
            if (!player.CanBeat(Army))
                player.Die();
        }
    }

    public class ResourcePile : IMapObject
    {
        public Treasure Treasure { get; set; }

        public void Act(Player player)
        {
            player.Consume(Treasure);
        }
    }

    public interface IMapObject
    {
        void Act(Player player);
    }

    public static class Interaction
    {
        public static void Make(Player player, IMapObject mapObject)
        {
            mapObject.Act(player);
        }
    }
}


//В компьютерной игре, персонаж игрока взаимодействует с различными объектами на карте.Есть всего три способа взаимодействовать:
//Сражаться с армией.
//Собирать сокровища.
//Присваивать объект себе.
//А вот различных видов объектов на карте уже 5, а будет ещё больше.
//Выделите все поля, необходимую для каждого взаимодействия, в свой интерфейс.Отрефакторьте программу, избавившись от повторяющихся участков 
//кода в Interaction.Make.
//В итоговом решении Interaction.Make должен работать только с интерфейсами, и не должен содержать упоминаний конкретных классов.