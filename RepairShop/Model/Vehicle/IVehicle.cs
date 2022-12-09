namespace RepairShop.Model.Vehicle
{
    public interface IVehicle
    {
        Make Make { get; set; }
        Transmission Transmission { get; set; }
        DriveType DriveType { get; set; }
        int Year { get; set; }
        int Millage { get; set; }
    }
}