using System;
namespace ApiMongoDB.Models
{
    public class InfectedDto
    {
        public DateTime birthDate { get; set; }
        public string gender { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}