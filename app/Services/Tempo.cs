namespace app.Services;

class Tempo
{
    public static TimeSpan CalcularTempo(DateTime inicial, DateTime final)
    {
        TimeSpan diferenca = final - inicial;

        return diferenca;
    }
    public static int[] TotalHoursMinutes(TimeSpan diferenca)
    {
        int horasTotais = (int)diferenca.TotalHours;
        int minutosRestantes = diferenca.Minutes;

        return [horasTotais, minutosRestantes];
    }
    public static string Duracao(TimeSpan diferenca)
    {
        int horasTotais = (int)diferenca.TotalHours;
        int minutosRestantes = diferenca.Minutes;
        int segundosRestantes = diferenca.Seconds;

        return $"{horasTotais:D2}:{minutosRestantes:D2}:{segundosRestantes:D2}";
    }
}