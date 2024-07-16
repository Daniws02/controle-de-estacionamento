namespace app.Services;

class Preco
{
    public static double RetornarPreco(int hora, int minutos)
    {
        double valor = 0;
        
        if(hora == 0 && minutos <= 30)
        {
            valor = 1;

        } else if(hora == 0 && minutos > 30)
        {
            valor = 2;
        } else 
        {
            valor = CalcularValor(hora, minutos);
        }

        return valor;
    }
    public static double CalcularValor(int hora, int minutos)
    {   
        double valor = 0;

        if(minutos > 10)
        {
            valor = hora + 2;
        } else 
        {
            valor = hora + 1;
        }
        return valor;
    }
}