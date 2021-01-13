using System;
using ApiMongoDB.Data.Collections;
using ApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ApiMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.MongoDB mongoDB;
        IMongoCollection<Infected> infectedCollection;

        // Controller's constructor
        public InfectedController(Data.MongoDB mongoDB)
        {
            this.mongoDB = mongoDB;
            infectedCollection = mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        // Post method to add infected in database
        [HttpPost]
        public ActionResult SaveInfected([FromBody] InfectedDto dto)
        {
            var infected = new Infected(dto.birthDate, dto.gender, dto.latitude, dto.longitude);

            infectedCollection.InsertOne(infected);

            return StatusCode(201, "Infected person added with success!");
        }

        // Get method to return list with infected in database
        [HttpGet]
        public ActionResult GetInfected()
        {
            var infected = infectedCollection.Find(Builders<Infected>.Filter.Empty).ToList();

            return Ok(infected);
        }

        [HttpPut]
        public ActionResult UpdateInfected([FromBody] InfectedDto dto)
        {
            infectedCollection.UpdateOne(Builders<Infected>.Filter.Where(i => i.birthDate == dto.birthDate), Builders<Infected>.Update.Set("gender", dto.gender));

            return Ok("Updated successfully");
        }

        [HttpDelete("{birthDate}")]
        public ActionResult DeleteInfected(DateTime birthDate)
        {
            infectedCollection.DeleteOne(Builders<Infected>.Filter.Where(i => i.birthDate == birthDate));

            return Ok("Deleted infected from database");
        }
    }
}