// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using FlightSolution;

static List<Schedule> GetFlightSchedules()
{
    return new List<Schedule>
    {
        // Day 1
        new Schedule { Day = 1, Destination = Airport.Toronto },
        new Schedule { Day = 1, Destination = Airport.Calgary },
        new Schedule { Day = 1, Destination = Airport.Vancouver },
        
        // Day 2
        new Schedule { Day = 2, Destination = Airport.Toronto },
        new Schedule { Day = 2, Destination = Airport.Calgary },
        new Schedule { Day = 2, Destination = Airport.Vancouver },
    };
}

static List<Flight>? GetFlights()
{
    var schedules = GetFlightSchedules();
    var flights = schedules.Select((sc, index) => new Flight((ushort)(index + 1), sc));
    return flights.ToList();
}

static List<Order> GetAllAvailableOrders()
{
    var jsonFile = File.ReadAllText("orders.json");
    var serializationOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    var ordersDictionary = JsonSerializer.Deserialize<Dictionary<string, Order>>(jsonFile,serializationOptions);
    var orders =  ordersDictionary?.Select((kv, index) => new Order
    {
        Priority = index,
        OrderNumber = kv.Key,
        Destination = kv.Value.Destination,
        IsProcessed = false
    });
    return orders.ToList();
}

var flights = GetFlights();
var orders = GetAllAvailableOrders();
var orderCount = orders.Count();
var counter = 0;
var flightCount = flights.Count();
var flightCounter = 0;



while (flightCounter < flightCount)
{
    var flight = flights[flightCounter];
    counter = 0;
    while (counter < orderCount)
    {
        var order = orders[counter];
        if (!flight.IsCapacityFull() && order.Destination.Equals(flight.Schedule.Destination) && !order.Day.Equals(flight.Schedule.Day) && !order.IsProcessed)
        {
            order.Day = flight.Schedule.Day;
            order.FlightNumber = flight.Number;
            order.Origin = flight.Schedule.Origin;
            order.IsProcessed = true;
            flight.AddOrder(order);
        }

        counter++;
    }
    
    flightCounter++;
}

foreach (var order in orders)
{
    Console.WriteLine(order.IsProcessed
        ? $"order: {order.OrderNumber}, flightNumber: {order.FlightNumber}, departure: {order.Origin}, arrival: {order.Destination}, day: {order.Day}"
        : $"order: {order.OrderNumber}, flightNumber: not-scheduled");
}



