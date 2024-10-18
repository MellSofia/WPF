using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика_17._09
{ public enum FuelName
    {
        АИ98,
        АИ95,
        АИ92,
        Метан,
        Водород,
        Дизель_летний,
        Дизель_зимний,

    }
    public class Vehicle
    {
        public  long ID {  get; set; }
        public string Name { get; set; }
        public double AvgSpeed { get; set; }
        public int FuelConsumption { get; set; }
        public string Purpose { get; set; }
        public Fuel Fuel { get; set; }
        public string PurposeFuel { get; set; }
        public Vehicle() { }
        public Vehicle(long iD, string name, int fuelConsumption, string purpose,string purposeFuel)
        {
            ID = iD;
            Name = name;
            FuelConsumption = fuelConsumption;
            Purpose = purpose;
            if(purpose.Contains('Л'))
            {
                AvgSpeed = 75;
            }
            if (purpose.Contains('т'))
            {
                AvgSpeed = 60;
            }
            if (purpose.Contains('Г'))
            {
                AvgSpeed = 45;
            }
            Fuel = new Fuel(purposeFuel);
            PurposeFuel = purposeFuel;
        }
    }
}
