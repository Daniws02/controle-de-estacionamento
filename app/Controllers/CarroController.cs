using Microsoft.AspNetCore.Mvc;
using app.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using app.Models;
using System;
using System.Linq;
using app.Services;

public class CarroController : Controller
{
    private readonly DataContext _context;

    public CarroController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var carros = await _context.Carros.ToListAsync();
        return View(carros);
    }

    public IActionResult CreateEntrada()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntrada(Carro carro)
    {
        var carroExistente = await _context.Carros
            .FirstOrDefaultAsync(i => i.Placa == carro.Placa && i.Estacionado == true);

        if (carroExistente == null)
        {
            carro.DataEntrada = DateTime.Now;
            carro.DataEntrada = carro.DataEntrada.AddTicks(-(carro.DataEntrada.Ticks % TimeSpan.TicksPerSecond));

            int anoEntrada = carro.DataEntrada.Year;

            if (anoEntrada != 2024)
            {
                ModelState.AddModelError("", "A data de entrada deve estar no ano de 2024!");
                return RedirectToAction(nameof(Index));
            }

            carro.DataSaida = DateTime.MinValue;
            carro.Estacionado = true;
            carro.Duracao = "Carro Estacionado";
            carro.Preco = 0;
            _context.Add(carro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult SaidaModal()
    {
        var carrosEstacionados = _context.Carros.Where(i => i.Estacionado).ToList();
        return PartialView(carrosEstacionados);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSaida(Carro carro)
    {
        foreach (var i in _context.Carros)
        {
            if (i.Placa == carro.Placa)
            {
                i.DataSaida = DateTime.Now;
                i.DataSaida = i.DataSaida.AddTicks(-(i.DataSaida.Ticks % TimeSpan.TicksPerSecond));

                int anoEntrada = i.DataEntrada.Year;
                int anoSaida = i.DataSaida.Year;

                if (anoEntrada != 2024 || anoSaida != 2024)
                {
                    ModelState.AddModelError("", "A data de entrada e a de saída devem estar no ano de 2024!");
                    return RedirectToAction(nameof(Index));
                }

                TimeSpan diferenca = Tempo.CalcularTempo(i.DataEntrada, i.DataSaida);
                int[] tempoTotal = Tempo.TotalHoursMinutes(diferenca);
                double preco = Preco.RetornarPreco(tempoTotal[0], tempoTotal[1]);

                i.Duracao = Tempo.Duracao(diferenca);
                i.Preco = preco;
                i.Estacionado = false;
            }
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> LimparHistorico()
    {
        var carros = await _context.Carros.ToListAsync();
        _context.Carros.RemoveRange(carros);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}