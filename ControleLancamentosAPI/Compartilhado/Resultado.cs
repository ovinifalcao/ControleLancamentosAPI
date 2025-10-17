namespace ControleLancamentosAPI.Compartilhado;

public class Resultado<TResultado, TErro>
{
    public readonly TErro? ValorErro;

    public readonly TResultado? Valor;

    public bool Sucesso { get; }

    public bool Falha => !Sucesso;

    protected internal Resultado(TResultado resultado)
    {
        Sucesso = true;
        Valor = resultado;
        ValorErro = default;
    }

    protected internal Resultado(TErro erro)
    {
        Sucesso = false;
        Valor = default;
        ValorErro = erro;
    }

    public static implicit operator Resultado<TResultado, TErro>(TResultado valor) => new(valor);

    public static implicit operator Resultado<TResultado, TErro>(TErro erro) => new(erro);

    public TResposta Corresponder<TResposta>(Func<TResultado, TResposta> successo, Func<TErro, TResposta> falha)
    {
        if (Sucesso)
        {
            return successo(Valor!);
        }
        return falha(ValorErro!);
    }
}