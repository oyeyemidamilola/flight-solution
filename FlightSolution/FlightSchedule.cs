namespace FlightSolution;

public static class Airport
{
    public const string Montreal = "YUL";
    public const string Toronto = "YYZ";
    public const string Vancouver = "YVR";
    public const string Calgary = "YYC";
}

public class Schedule
{
    private string _destination;
    public ushort Day { get; set; }
    public string Origin => Airport.Montreal;

    public string Destination
    {
        get => _destination;
        set
        {
            if (value == Origin)
            {
                throw new NotSupportedException("Can not have same origin and destination");
            }

            _destination = value;
        }
        
    }
}

public class Flight
{
    public const int MAXCAPACITY = 20;
    public Flight(ushort number, Schedule schedule)
    {
        Number = number;
        Schedule = schedule;
        Orders = new List<Order>();
    }

    public void AddOrder(Order order)
    {
        order.IsProcessed = true;
        Orders.Add(order);
    }

    public bool IsCapacityFull() => Orders.Count == MAXCAPACITY;

    public ushort Number { get; set; }
    public Schedule Schedule { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order
{
    public int Priority { get; set; }
    
    public int FlightNumber { get; set; }
    public string OrderNumber { get; set; }
    public ushort Day { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public bool IsProcessed { get; set; }

    public override string ToString()
    {
        return $"Priority {Priority} Order {OrderNumber} - Destination {Destination}";
    }
}