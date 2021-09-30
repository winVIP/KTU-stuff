using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public enum Level
    {
        One,
        Two,
        Three
    }

    public enum FuelType
    {
        Petrol,
        Diesel,
        Electricity,
        Hybrid
    }

    public enum WindowType
    {
        Electric,
        Mechanical
    }

    public enum BodyType
    {
        Sedan,
        Hatchback,
        Wagon,
        Cabrio,
        SUV
    }

    public enum GearboxType
    {
        Automatic,
        Manual
    }
}