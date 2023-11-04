// See https://aka.ms/new-console-template for more information

using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.Infrastructure;

Console.WriteLine("Hello, World!");
Plane plane = new Plane();
plane.Capacity = 100;
Console.WriteLine(plane.Capacity);
plane.ManufactureDate = DateTime.Now;
plane.PlaneType = PlaneType.Airbus;
//Initialiseur d'objet
Plane plane1 = new Plane { Capacity=200,
                        ManufactureDate=new DateTime(2023,9,8)};
Console.WriteLine(plane1);
//Tester la méthode CheckProfile
Passenger p1 = 
    new Passenger {FullName=new FullName { FirstName = "amiNa", LastName = "aOun" }, EmailAddress = "Amina.aoun@esprit.tn" };
Console.WriteLine(p1.CheckProfile("Amina", "Aoun"));
Console.WriteLine(p1.CheckProfile("Amina", "Aoun", "abc"));
//Tester PassengerType
Staff s1 = new Staff();
Traveller t1 = new Traveller();
p1.PassengerType();
s1.PassengerType();
t1.PassengerType();
Console.WriteLine( "******GetFlightDates********");
FlightMethods fm=new FlightMethods();
fm.Flights = TestData.listFlights;
fm.GetFlightDates("Madrid");
Console.WriteLine("******GetFlights********");
fm.GetFlights("FlightDate", "2022/02/01 21:10:10");
Console.WriteLine("***********ShowFlightDetails**********");
fm.ShowFlightDetails(TestData.BoingPlane);
fm.ShowFlightDetailsDel(TestData.BoingPlane);
Console.WriteLine("**********ProgrammedFlightNumber****");
Console.WriteLine(fm.ProgrammedFlightNumber(new DateTime(2022, 01, 31)));
Console.WriteLine("**********DurationAverage****");
Console.WriteLine(fm.DurationAverage("Madrid"));
Console.WriteLine(fm.DurationAverageDel("Madrid"));
Console.WriteLine("**********OrderedDuration****");
foreach(var f in fm.OrderedDurationFlights())
    Console.WriteLine(f);
//Console.WriteLine("**********SeniorTravellers****");
//foreach(var t in fm.SeniorTravellers(TestData.flight1))
//    Console.WriteLine(t.BirthDate);
Console.WriteLine("**********DestinationGroupedFlight****");
fm.DestinationGroupedFlights();
Console.WriteLine("**********UpperFullname****");
p1.UpperFullName();
Console.WriteLine(p1.FullName.FirstName + " " + p1.FullName.LastName);
//Inserer 2 planes
AMContext ctx=new AMContext();
IUnitOfWork uow =new UnitOfWork(ctx);
IServicePlane sp = new ServicePlane(uow);
IServiceFlight sf = new ServiceFlight(uow);
sp.Add(TestData.BoingPlane);
sp.Add(TestData.Airbusplane);
////Inserer 2 flights
sf.Add(TestData.flight1);
sf.Add(TestData.flight2);
////Persistance
sp.Commit();
Console.WriteLine("Ajout avec succès");
//Afficher le contenu de la table Flights
foreach(Flight f in sf.GetMany())
    Console.WriteLine("destination: "+f.Destination
        +" Flight date: "+f.FlightDate+" Capacity: "+f.Plane.Capacity);



