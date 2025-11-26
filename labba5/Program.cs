using labba5;

namespace labba5
{
    enum ClassOfService
    {
        Economy,
        Business
    }
    class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNumber { get; set; }

        internal Flight Flight
        {
            get => default;
            set
            {
            }
        }

        public Passenger(string firstName, string lastName, string passportNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PassportNumber = passportNumber;
        }

        public void BookFlight(Flight flight, Ticket ticket)
        {
            Console.WriteLine($"{FirstName} {LastName} забронировал рейс {flight.FlightNumber}.");
            ticket.Status = "Забронирован";
            flight.AddPassenger(this);
        }

        public void PayForTicket(Ticket ticket)
        {
            Console.WriteLine($"{FirstName} {LastName} оплатил билет #{ticket.TicketNumber}.");
            ticket.Status = "Оплачен";
        }
    }

    class Flight
    {
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public List<Passenger> Passengers { get; private set; } = new List<Passenger>();

        internal Ticket Ticket
        {
            get => default;
            set
            {
            }
        }

        public Flight(string flightNumber, string airline, DateTime departure, DateTime arrival,
                     string origin, string destination)
        {
            FlightNumber = flightNumber;
            Airline = airline;
            DepartureTime = departure;
            ArrivalTime = arrival;
            OriginCity = origin;
            DestinationCity = destination;
        }

        public void AddPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
            Console.WriteLine($"Пассажир {passenger.FirstName} {passenger.LastName} добавлен в рейс {FlightNumber}.");
        }

        public void ShowPassengers()
        {
            Console.WriteLine("Пассажиры рейса " + FlightNumber + ":");
            foreach (var p in Passengers)
            {
                Console.WriteLine($"- {p.FirstName} {p.LastName}, паспорт: {p.PassportNumber}");
            }
        }
    }

    class Ticket
    {
        public int TicketNumber { get; set; }
        public string Status { get; set; } = "Не забронирован";
        public double Price { get; set; }
        public ClassOfService Class { get; set; }

        public Ticket(int number, double price, ClassOfService serviceClass)
        {
            TicketNumber = number;
            Price = price;
            Class = serviceClass;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем объекты
            Passenger passenger = new Passenger("Анна", "Иванова", "1234567890");
            Flight flight = new Flight("SU123", "Аэрофлот",
                                     new DateTime(2025, 12, 1, 10, 0, 0),
                                     new DateTime(2025, 12, 1, 13, 30, 0),
                                     "Москва", "Санкт-Петербург");
            Ticket ticket = new Ticket(1001, 5000.0, ClassOfService.Economy);

            // Пассажир бронирует рейс
            passenger.BookFlight(flight, ticket);

            // Пассажир оплачивает билет
            passenger.PayForTicket(ticket);

            // Показываем пассажиров в рейсе
            flight.ShowPassengers();

            // Выводим информацию о билете
            Console.WriteLine($"Билет #{ticket.TicketNumber}, стоимость: {ticket.Price} руб., класс: {ticket.Class}, статус: {ticket.Status}");
        }
    }

}