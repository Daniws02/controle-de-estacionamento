namespace app.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public string Duracao { get; set; } = string.Empty;
        public double Preco { get; set; }
        public bool Estacionado { get; set; }

        public List<Carro> Carros { get; } = new();
    }
}