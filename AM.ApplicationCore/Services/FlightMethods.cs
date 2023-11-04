using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Plane = AM.ApplicationCore.Domain.Plane;

namespace AM.ApplicationCore.Services
{
    public class FlightMethods : IFlightMethods
    {
        public Action<Plane> ShowFlightDetailsDel;
        public Func<string, double> DurationAverageDel;
        public FlightMethods()
        {
            ShowFlightDetailsDel = pl=> {
                var req = from f in Flights
                          where f.Plane == pl
                          select new { f.Destination, f.FlightDate };
                foreach (var item in req)
                    Console.WriteLine(item);
            };
            DurationAverageDel = destination => {
                {
                    var req = from f in Flights
                              where f.Destination == destination
                              select f.EstimatedDuration;
                    return req.Average();
                }
            };
        }
        public List<Flight> Flights { get; set; }=new List<Flight>();

        public IEnumerable<IGrouping<string, Flight>> DestinationGroupedFlights()
        {
            //var req = from f in Flights
            //          group f by f.Destination;
            var req = Flights.GroupBy(f => f.Destination);
            foreach(var g in req)
            {
                Console.WriteLine(g.Key);
                foreach(var f in g)
                    Console.WriteLine(f);
            }

            return req;
        }

        public double DurationAverage(string destination)
        {
            //var req= from f in Flights
            //         where f.Destination == destination
            //         select f.EstimatedDuration;
            var req= Flights.Where(f=> f.Destination == destination)
                .Average(f => f.EstimatedDuration);
            return req;
        }

        public IEnumerable<DateTime> GetFlightDates(string destination)
        {
            IEnumerable<DateTime> dates = new List<DateTime>();

            //foreach (Flight f in Flights)
            //{
            //    if (f.Destination == destination)
            //    {
            //        dates.Add(f.FlightDate);
            //        Console.WriteLine(f.FlightDate);
            //    }
            //}
            //dates=from f in Flights 
            //      where f.Destination == destination
            //      select f.FlightDate;
            dates = Flights.Where(f => f.Destination == destination)
                .Select(f => f.FlightDate);
            return dates;

        }

        public void GetFlights(string filterType, string filterValue)
        {
           switch(filterType)
            {
                case "Destination":
                foreach (Flight f in Flights)
                        if(f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                break;
                case "Departure":
                    foreach (Flight f in Flights)
                        if (f.Departure.Equals(filterValue))
                            Console.WriteLine(f);
                    break;
                case "estimatedDuration":
                    foreach (Flight f in Flights)
                        if (f.EstimatedDuration.Equals(int.Parse(filterValue)))
                            Console.WriteLine(f);
                    break;
                case "FlightDate":
                    foreach (Flight f in Flights)
                        if (f.FlightDate.Equals(DateTime.Parse(filterValue))) 
                            Console.WriteLine(f);
                    break;

            }

        }

        public IEnumerable<Flight> OrderedDurationFlights()
        {
            //var req = from f in Flights
            //          orderby f.EstimatedDuration descending
            //          select f;
            //return req;
            return Flights.OrderByDescending(f => f.EstimatedDuration);
        }

        public int ProgrammedFlightNumber(DateTime startDate)
        {
            //var req = from f in Flights
            //          where (f.FlightDate - startDate).TotalDays >= 0
            //          && (f.FlightDate - startDate).TotalDays < 7
            //          select f;
            //            return req.Count();

           return Flights.Where(f => (f.FlightDate - startDate).TotalDays >= 0
                      && (f.FlightDate - startDate).TotalDays < 7)
                    .Count();
        }

        //public IEnumerable<Traveller> SeniorTravellers(Flight flight)
        //{
        //    //var req= from t in flight.Passengers.OfType<Traveller>()
        //    //         orderby t.BirthDate
        //    //         select t;
        //    //return req.Take(3);
        //    return flight.Passengers.OfType<Traveller>()
        //        .OrderBy(t => t.BirthDate).Take(3);
        //    //Skip(3) pour ignorer les 3 premiers
        // }

        public void ShowFlightDetails(Plane plane)
        {
            //var req = from f in Flights
            //                          where f.Plane == plane
            //                          select new { f.Destination, f.FlightDate };
            var req = Flights.Where(f => f.Plane == plane)
                 .Select(f => new { f.Destination, f.FlightDate });
            foreach(var item in req)
                Console.WriteLine(item);
        }
    }
}
