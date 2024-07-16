using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Controllers;
using app.Data;
using app.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace test
{
    public class CarroControllerTests
    {
        private readonly DbContextOptions<DataContext> _options;
        private readonly DataContext _context;
        private readonly CarroController _controller;

        public CarroControllerTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            _context = new DataContext(_options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _controller = new CarroController(_context);

            _context.Carros.AddRange(GetTestCarros());
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCarros()
        {
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Carro>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task CreateEntrada_AddsCarroToDatabase()
        {
            var carro = new Carro
            {
                Placa = "DEF-5678",
                Duracao = "Carro Estacionado",
                DataEntrada = DateTime.Now
            };

            var result = await _controller.CreateEntrada(carro);

            var carros = _context.Carros.ToList();
            Assert.Contains(carros, c => c.Placa == carro.Placa);
            Assert.IsType<RedirectToActionResult>(result);
        }

        private List<Carro> GetTestCarros()
        {
            return new List<Carro>
            {
                new Carro { Placa = "ABC-1234", DataEntrada = DateTime.Now, Duracao = "1:00:00" },
                new Carro { Placa = "XYZ-5678", DataEntrada = DateTime.Now, Duracao = "1:00:00" }
            };
        }
    }
}