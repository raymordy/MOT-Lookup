using System;
using System.Collections.Generic;
using System.Text;

namespace MOT_Lookup.Models
{
    public class Vehicle
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FirstUsedDate { get; set; }
        public string FuelType { get; set; }
        public string PrimaryColour { get; set; }
        public string MotTestExpiryDate { get; set; }
        public IEnumerable<MotTest> MotTests { get; set; }
    }
}
