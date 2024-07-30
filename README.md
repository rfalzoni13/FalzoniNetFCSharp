# Projeto de Demonstração por Renato Falzoni

Este projeto é de simples demonstração, desenvolvido com tecnologia .NET Framework em C#

O Projeto está no modelo DDD e consta com as seguintes camadas:

- Domínio: O projeto de Domínio possui as entidades relacionadas às regras de negócio
- Infra Data: O projeto de Infra Data possui as configurações para conexão com banco de dados e os Repositórios de dados
- Infra IoC: Projeto responsável pela injeção das dependências das camadas de aplicação e infra (repositórios)
- Service: Projeto de serviços, responsável pela intermediação entre a aplicação e os repositórios e responsável pelas regras de negócio das requisições.
- Application: Camada responsável pela obtenção dos dados da plataforma e dependências do Idendity
- Presentation: Camada das plataformas (MVC, Web Api, etc).

Atualmente, o projeto suporte os seguintes bancos de dados:
- SQL Server 2022
- MySQL 9.0
