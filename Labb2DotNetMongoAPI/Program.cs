using Labb2DotNetMongoAPI.Models;
using Labb2DotNetMongoAPI.Data;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Labb2DotNetMongoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            MongoDbContext db = new MongoDbContext("FootballPlayers");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           // app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAll");


            //CREATE PLAYER
            app.MapPost("/player", async (Players newPlayer) =>
            {
                var player = await db.AddPlayer("Players", newPlayer);
                return Results.Ok(player);
            });

            //GET ALL
            app.MapGet("/players", async () =>
            {
                var players = await db.GetAllPlayers("Players");
                return Results.Ok(players);
            });

            //GET PLAYER BY ID
            app.MapGet("/player/{id}", async (string id) =>
            {
                var player = await db.GetPlayerById("Players", id);
                return Results.Ok(player);
            });

            //UPDATE PLAYER
            app.MapPut("/player/{id}", async (string id, Players updatedPlayer) =>
            {
                var player = await db.UpdatePlayerById("Players", id, updatedPlayer);

                if (player == null)
                {
                    return Results.NotFound("Could not find player");
                }
                else
                {
                    return Results.Ok(player);
                }
            });

            //DELETE PLAYER
            app.MapDelete("/player/{id}", async (string id) =>
            {
                var message = await db.DeletePlayer("Players", id);
                return "Player deleted";
            });

            app.Run();
        }
    }
}
