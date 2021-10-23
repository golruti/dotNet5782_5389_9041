using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlote { get; set; }
            public override string ToString()
            {
                return ("\nid: " + Id + "\nname: " + Name + "\nLongitude: " + Longitude +
                    "\nLattitude: " + Lattitude + "\nChargeSlote: " + ChargeSlote+"\n");

            }
            public Station Clone()
            {
                return new Station()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Longitude = this.Longitude,
                    Lattitude = this.Lattitude,
                    ChargeSlote = this.ChargeSlote
                };
            }
        }
        
        public override void ToString()
        {
             return "id: "+ base.Id + "name: "+ base.nameId +"logitude: "+ base.Logitude + "lattitude: "+ base.Lattitude +
            "charge slote: "+ base.ChargeSlote ;
         } 
    }
}
