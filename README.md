# Enterteniment Manager
![GitHub](https://img.shields.io/github/license/mathfrance/EntertenimentManagementAPI)

## Visão Geral

Este projeto representa o backend de um gerenciador de filmes e jogos, desenvolvido para auxiliar na organização de itens em listas. O objetivo é proporcionar aos usuários um maior controle sobre suas escolhas de filmes para assistir, bem como jogos para jogar. A plataforma possibilita o acompanhamento do que já foi assistido ou jogado, o que está atualmente em andamento e o que está planejado para o futuro.

## Finalidade

Este projeto foi concebido com o objetivo de aplicar conceitos e estudos relacionados a padrões e melhores práticas de desenvolvimento. Sinta-se à vontade para sugerir mudanças e melhorias com foco nesses aspectos. Para obter informações mais detalhadas sobre o código, consulte a Wiki do projeto.,

## Desenvolvimento

- .Net 6.0
- Entity Framework Core 7.0

## Pacotes de Terceiros

- **SecureIdentity (1.0.4):** [![NuGet](https://img.shields.io/nuget/v/SecureIdentity)](https://www.nuget.org/packages/SecureIdentity)
  - Utilizado para gerar senhas e hashes.

- **Swashbuckle.AspNetCore (6.5.0):** [![NuGet](https://img.shields.io/nuget/v/Swashbuckle.AspNetCore)](https://www.nuget.org/packages/Swashbuckle.AspNetCore)
  - Usado para documentar os endpoints através da ferramenta Swagger.

- **Flunt (2.0.5):** [![NuGet](https://img.shields.io/nuget/v/Flunt)](https://www.nuget.org/packages/Flunt)
  - Utilizado para validar e notificar os comandos.

- **Azure.Storage.Blobs (12.17.0):** [![NuGet](https://img.shields.io/nuget/v/Azure.Storage.Blobs)](https://www.nuget.org/packages/Azure.Storage.Blobs)
  - Usado para armazenar imagens no Azure Storage.
 
## Executando o projeto
- Certifique-se de que você tenha o .NET 6 SDK instalado em sua máquina. Você pode verificar a versão do .NET usando o seguinte comando no terminal:
  -   ```shell dotnet --version```
- Clone o repositório do projeto para o seu ambiente local usando o Git:
  -  ```git clone https://github.com/mathfrance/EntertenimentManagementAPI.git``` 
- Configure a **ConectionString** no arquivo **appsettings.json**. Deve ser uma base SQL Server.
- Abra um terminal na raiz do projeto e execute o seguinte comando para restaurar todos os pacotes necessários:
  - ```dotnet retore```
- Após a conclusão da restauração de pacotes, compile a aplicação com o seguinte comando:
  - ```dotnet build```
- Abra um terminal na pasta do projeto **EntertenimentManager.Infra** e execute o seguinte comando para criar o banco de dados e aplicar as migrações:
  - ```dotnet ef database update -s ..\EntertenimentManager.API\```
- Rode o comando abaixo na pasta do projeto **EntertenimentManager.API** para iniciar o projeto:
  - ```dotnet run``` 
- Para mais detalhes sobre o funcionamento, consulte a Wiki do projeto.

## Automação com GitHub Actions

Este projeto inclui um workflow automatizado com o GitHub Actions para simplificar várias tarefas:

- **Compilação:** O projeto é compilado automaticamente em cada push ou pull request.
- **Testes Unitários:** São executados automaticamente após cada compilação para garantir a integridade do código.
- **Atualização do Banco de Dados:** As migrações do Entity Framework Core são aplicadas automaticamente quando necessário.
- **Deploy no Azure Cloud Services:** O projeto pode ser implantado automaticamente no Azure Cloud Services.

### Configurando Segredos

Para usar esse workflow, você deve configurar os [segredos das Actions](https://docs.github.com/pt/actions/security-guides/using-secrets-in-github-actions) no seu repositório com as informações necessárias para o Azure Cloud Services e para acesso ao banco de dados, se aplicável.

Certifique-se de configurar esses segredos de forma segura para manter suas informações confidenciais protegidas.

Consulte o arquivo de workflow [master_enterteniment-manager.yml](https://github.com/mathfrance/EntertenimentManagementAPI/blob/master/.github/workflows/master_enterteniment-manager.yml) para obter detalhes sobre como o workflow está configurado e ajuste conforme necessário.

