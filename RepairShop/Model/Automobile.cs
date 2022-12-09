using System.Runtime.CompilerServices;
using RepairShop.Model.Vehicle;

namespace RepairShop.Model
{
    public class Automobile : IVehicle
    {
        public Make Make { get; set; }
        public Transmission Transmission { get; set; }
        public DriveType DriveType { get; set; }
        public int Year { get; set; }
        public int Millage { get; set; }

        public Automobile() { }

        public Automobile(Make make, Transmission transmission, DriveType driveType, int year, int millage)
        {
            Make = make;
            Transmission = transmission;
            DriveType = driveType;
            Year = year;
            Millage = millage;
        }

        public override string ToString()
        {
            return "Vehicle { \n  Make: " + Make + "\n  Transmission: " + Transmission + "\n  DriveType: " + DriveType +
                   "\n  Year: " + Year + "\n  Millage: " + Millage + "\n}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Appointment)) return false;
            var vehicle = (Automobile)obj;
            return Make == vehicle.Make && Transmission == vehicle.Transmission && DriveType == vehicle.DriveType &&
                   Year == vehicle.Year && Millage == vehicle.Millage;
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }
    }

    public class VehicleBuilder
    {
        private readonly Automobile _vehicle;

        public VehicleBuilder()
        {
            _vehicle = new Automobile();
        }

        public VehicleBuilder WithMake(Make make)
        {
            _vehicle.Make = make;
            return this;
        }

        public VehicleBuilder WithTransmission(Transmission transmission)
        {
            _vehicle.Transmission = transmission;
            return this;
        }

        public VehicleBuilder WithDriveType(DriveType driveType)
        {
            _vehicle.DriveType = driveType;
            return this;
        }

        public VehicleBuilder WithYear(int year)
        {
            _vehicle.Year = year;
            return this;
        }

        public VehicleBuilder WithMillage(int millage)
        {
            _vehicle.Millage = millage;
            return this;
        }

        public Automobile Build()
        {
            return _vehicle;
        }
    }
}