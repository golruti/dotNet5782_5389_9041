using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject.DalObject;

//הדף של הפונקציות לא מכיר את המחלקות האחרות והמיין לא מכיר את פונקציות ההכנסה\העדכון וכו
namespace ConsoleUI
{
   public class MainFunction
    {
        public static int xx = 10;

       public Station getStation()
        {
            int tempID, tempChargeSlote;
            string tempName;
            double tempLongitude, tempLattitude;
            Station tempStation = new Station();
            Console.Write("Enter id");
            tempID = int.Parse(Console.ReadLine());
            tempStation.Id = tempID;

            Console.Write("Enter name");
            tempName = Console.ReadLine();
            tempStation.Name = tempName;

            Console.Write("Enter longitude");
            tempLongitude = double.Parse(Console.ReadLine());
            tempStation.Longitude = tempLongitude;

            Console.Write("Enter lattitude");
            tempLattitude = double.Parse(Console.ReadLine());
            tempStation.Lattitude = tempLattitude;


            Console.Write("Enter chargeSlote");
            tempChargeSlote = int.Parse(Console.ReadLine());
            tempStation.ChargeSlote = tempChargeSlote;

            return tempStation;
        }
    }
}
