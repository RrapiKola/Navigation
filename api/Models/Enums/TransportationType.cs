using System.Runtime.Serialization;

namespace api.Models
{
    public enum TransportationType
    {
        [EnumMember(Value = "Car")]
        Car,

        [EnumMember(Value = "Bus")]
        Bus,

        [EnumMember(Value = "Train")]
        Train,

        [EnumMember(Value = "Plane")]
        Plane,

        [EnumMember(Value = "Bicycle")]
        Bicycle,

        [EnumMember(Value = "Walking")]
        Walking
    }
}