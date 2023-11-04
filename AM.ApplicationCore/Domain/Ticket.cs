using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Ticket
    {
        public Double Prix { get; set; }
        public string Siege { get; set; }
        public Boolean VIP { get; set; }
        public virtual Passenger Passenger { get; set; }
        [ForeignKey("Passenger")]
        public string PassengerFK { get; set; }
        public virtual Flight Flight { get; set; }
        [ForeignKey("Flight")]
        public int FlightFK { get; set; }
    }
}
