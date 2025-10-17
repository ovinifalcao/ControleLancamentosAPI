using AutoBogus;
using ControleLancamentosAPI.Data.Comandos;
using ControleLancamentosAPI.Data.Models;

namespace ControleLancamentosApiTestes.Criadores
{
    public class CriadorRegistrarLancamentoRequest : AutoFaker<RegistrarLancamentoRequest>
    {
        public CriadorRegistrarLancamentoRequest()
        {
            RuleFor(r => r.Descricao, f => f.Random.String2(100));
            RuleFor(r => r.Valor, f => f.Random.Double(0));
            RuleFor(r => r.Natureza, f => f.PickRandom<NaturezaEnum>());
            RuleFor(r => r.Operador, f => $"OP{f.Random.Int(1000, 9999)}");
        }

        public CriadorRegistrarLancamentoRequest ComDescricao(string descricao)
        {
            RuleFor(r => r.Descricao, descricao);
            return this;
        }

        public CriadorRegistrarLancamentoRequest ComValor(double valor)
        {
            RuleFor(r => r.Valor, valor);
            return this;
        }

        public CriadorRegistrarLancamentoRequest ComNatureza(NaturezaEnum natureza)
        {
            RuleFor(r => r.Natureza, natureza);
            return this;
        }

        public CriadorRegistrarLancamentoRequest ComOperador(string operador)
        {
            RuleFor(r => r.Operador, operador);
            return this;
        }
    }
}
