namespace ControleLancamentosAPI.Compartilhado.Erros
{
    public static partial class EndpointErros
    {
        [LoggerMessage(Level = LogLevel.Error, Message = "Registro de Lançamento Inválido. Detalhes: {description}")]
        public static partial void RegistroInvalido(this ILogger logger, string description);
    }
}
