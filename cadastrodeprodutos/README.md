# CRUD simples de produto

## Framework utilizado

.NET Core 3.0 SDK (ASPNET Core 3.0)

### Como compilar o projeto?

Para compilar o projeto, basta rodar `dotnet build CadastroProdutos.sln`.

### Configuração Nuget

É necessário adicionar uma nova fonte Nuget em *Manage Nuget Packages -> Sources*:

    Name = Nuget - AmbevDevs
    URL = https://nexus.ambevdevs.com.br/nexus/repository/nuget-hosted/
    Usuário = ********
    Senha = ********

### Como rodar os testes?

Este projeto tem uma *peculiaridade* nos testes: há duas categorias de testes: **testes de unidade**, e **testes de 
integração**.

Os testes de integração dependem de uma instância do MongoDB rodando com sistema de arquivos em memória. Felizmente, 
temos um arquivo *Docker Compose* especialmente preparado para isso. Para subir o MongoDB, rode o comando 
`docker-compose -f docker-compose-mongo.yml up`.

Feito isso, utilize o comando `dotnet test` para executar os testes.

## Eventos

Tanto os eventos publicados pelo microserviço de trocas, quanto aqueles consumidos de outros serviços utilizam o
protocolo [_Protocol Buffers_](https://developers.google.com/protocol-buffers/docs/csharptutorial).

O _Protocol Buffers_, mais conhecido como *Protobuf*, garante que os eventos publicados pelo microserviço sejam 
possíveis de ser consumidos por outros implementados em diferentes linguagens de programação.

### Gerando fontes C# para os eventos do microserviço

Por convenção, os arquivos *.proto* ficam na raiz do projeto `CadastroProdutos.Eventos`, com nome no formato 
**nome_evento.proto**.

Para gerar o arquivo fonte C# relativo a um arquivo *.proto*, siga os seguintes passos:

1) Acessar o diretório do projeto `CadastroProdutos.Eventos` num terminal
2) Rodar o comando `protoc -I=.  --csharp_out=. nome_evento.proto`
3) Verificar que foi gerado um arquivo `NomeEvento.cs`

**Observações:** para instalar o *Protobuf* na sua máquina, siga o [tutorial no OneNote](https://hbsis-my.sharepoint.com/personal/rodrigo_linhares_hbsis_com_br/_layouts/OneNote.aspx?id=%2Fpersonal%2Frodrigo_linhares_hbsis_com_br%2FDocuments%2FDesenvolvimento%2FCadernos%2FHercules%2FHercules&wd=target%28Ferramentas.one%7C32B56415-1CE9-44DF-A3B5-C7).

## Configurações do serviços

Algumas funcionalidades do serviço podem ser configuradas via variáveis de ambiente.

### Feature Toggles

Para ativar um feature toggle, basta inserir um novo registro na collection *feature_toggle* do MongoDB, no seguinte formato:

```JSON
{
  "Codigo": 1,
  "Valor": "true",
  "Descricao": "descricao opcional"
}
```

#### Desabilitar as críticas

```JSON
{
  "Codigo": 1,
  "Valor": "78,106",
  "Descricao": "Desabilita as críticas informadas"
}
```

