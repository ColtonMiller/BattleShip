using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Point
    {
        public enum PointStatus
        {
            Empty,
            Ship,
            Hit,
            Miss
        }
        //properties
        int X { get; set; }
        int Y { get; set; }
        public PointStatus Status { get; set; }
        //constructor
        public Point(int x, int y, PointStatus p)
        {
            this.X = x;
            this.Y = y;
            this.Status = p;
        }
    }
    public class Ship 
    {
        public enum ShipType
        {
            Carrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }
        //properties
        ShipType Type { get; set; }
        public List<Point> OccpiedPoints { get; set; }
        int Length { get; set; }
        bool IsDestroyed
        {
            get
            {
                return Length == OccpiedPoints.Where(x => x.Status == Point.PointStatus.Hit).Count();
            }
        }
        //constructor
        public Ship(ShipType typeOfShip)
        {
            List<Point> OccupiedPoints = new List<Point> { };
            switch (typeOfShip)
            {
                case ShipType.Carrier:
                    Length = 5;
                    break;
                case ShipType.Battleship:
                    Length = 4;
                    break;
                case ShipType.Cruiser:
                    Length = 3;
                    break;
                case ShipType.Submarine:
                    Length = 3;
                    break;
                case ShipType.Destroyer:
                    Length = 2;
                    break;
            }
        }
        class Grid
        {
            //enum
            enum PlaceShipDirection
            {
                Horizontal,
                Vertical
            }
            //properties
            Point[,] Ocean { get; set; }
            List<Ship> ListOfShips { get; set; }
            bool AllShipsDestroyed
            {
                get
                {
                    return ListOfShips.Count() == ListOfShips.Where(x => x.IsDestroyed).Count();
                }
            }
            int CombatRound { get; set; }
            //methods
            void PlaceShip(Ship shipToPlace, PlaceShipDirection direction, int startX, int startY)
            {
                for (int i = 1; i <= shipToPlace.Length; i++)
                {
                    Ocean[startX, startY].Status = Point.PointStatus.Ship;
                    shipToPlace.OccpiedPoints.Add(Ocean[startX, startY]);
                    if (direction == PlaceShipDirection.Horizontal)
                    {
                        startX++;
                    }
                    else
                    {
                        startY++;
                    }
                }
            }
            void DisplayOcean()
            {
                for (int x = 0; x < Ocean.GetLength(0); x++)
                {
                    for (int y = 0; y < Ocean.GetLength(1); y++)
                    {
                        if (Ocean[x,y].Status == Point.PointStatus.Empty)
                        {
                            Console.Write("[ ]");
                        }
                        else if (Ocean[x,y].Status == Point.PointStatus.Hit)
                        {
                            Console.Write("[X]");
                        }
                        else if (Ocean[x,y].Status == Point.PointStatus.Miss)
                        {
                            Console.Write("[O]");
                        }
                        Console.Write("\n");
                        {

                        }
                    }
                }
            }
            bool Target(int x, int y)
            {
                int destroyed = ListOfShips.Where(i => i.IsDestroyed).Count();
                if (Ocean[x,y].Status == Point.PointStatus.Ship)
                {
                    Ocean[x, y].Status = Point.PointStatus.Hit;
                }
                else
                {
                    Ocean[x, y].Status = Point.PointStatus.Miss;
                }
                int newDestroyed = ListOfShips.Where(i => i.IsDestroyed).Count();
                if (newDestroyed > destroyed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            void PlayGame()
            {

            }
            //constructor
            public Grid()
            {
                Point[,] Ocean = new Point[10,10];
                for (int x = 0; x < Ocean.GetLength(0); x++)
                {
                    for (int y = 0; y < Ocean.GetLength(1); y++)
                    {
                        Point point = new Point(x, y, Point.PointStatus.Empty);
                    }
                }
                List<Ship> ListOfShips = new List<Ship>();
                //make one of each ship
                Ship carrier = new Ship(ShipType.Carrier);
                Ship battleship = new Ship(ShipType.Battleship);
                Ship cruiser = new Ship(ShipType.Cruiser);
                Ship submarine = new Ship(ShipType.Submarine);
                Ship destroyer = new Ship(ShipType.Destroyer);
                ListOfShips.Add(carrier);
                ListOfShips.Add(battleship);
                ListOfShips.Add(cruiser);
                ListOfShips.Add(submarine);
                ListOfShips.Add(destroyer);
            }
        }
    }
    
}
