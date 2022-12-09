using System;

namespace RepairShop.Model
{
    public class Appointment
    {
        public Automobile Automobile { get; set; }

        public string[] Services { get; set; }

        public string Transportation { get; set; }

        public DateTime Date { get; set; }

        public Appointment()
        {
        }

        public Appointment(Automobile automobile, string[] services, string transportation, DateTime date)
        {
            Automobile = automobile;
            Services = services;
            Transportation = transportation;
            Date = date;
        }

        public override string ToString()
        {
            return "Appointment { \n  Vehicle: " + Automobile.ToString() + "\n  Services: " + Services +
                   "\n  Transportation: " + Transportation + "\n  Date: " + Date + "\n}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Appointment)) return false;
            var appointment = (Appointment)obj;
            return appointment.Automobile.Equals(Automobile) || appointment.Date.Equals(Date);
        }

        /**
         * Automobile and Date should be read-only because the
         * hashcode should not change during runtime.
         *
         * It seems to work fine under the program circumstances.
         */
        public override int GetHashCode()
        {
            return Automobile.GetHashCode() * 17 + Date.GetHashCode();
        }
    }

    public class AppointmentBuilder
    {
        private readonly Appointment _appointment;

        public AppointmentBuilder()
        {
            _appointment = new Appointment();
        }

        public AppointmentBuilder WithVehicle(Automobile vehicle)
        {
            _appointment.Automobile = vehicle;
            return this;
        }

        public AppointmentBuilder WithServices(string[] services)
        {
            _appointment.Services = services;
            return this;
        }

        public AppointmentBuilder WithTransportation(string transportation)
        {
            _appointment.Transportation = transportation;
            return this;
        }


        public AppointmentBuilder OnDate(DateTime date)
        {
            _appointment.Date = date;
            return this;
        }

        public Appointment Build()
        {
            return _appointment;
        }
    }
}