using ControleLancamentosApiTestes.Configuracoes.Base;

namespace ControleLancamentosApiTestes.Configuracoes.Fixtures;

[CollectionDefinition("DatabaseCollection", DisableParallelization = true)]
public class DatabaseFixture : IClassFixture<DatabaseTestBase>
{
}