## Description [Identity] https://learn.microsoft.com/pt-br/aspnet/core/security/?view=aspnetcore-6.0

Esse projeto foi criado junto ao curso .NET 6 e Identity: Implementando controle de usuario

Est5e é um projeto de controle de usuario, onde usamos politicas de autorização para o usuario, foi criada a entidade 
usuario extendida do Identityuser, além disso tambem usamos as secrets para guardar operações sensiveis da aplicação;

## Rodar APP no VSCode
Antes de rodar é preciso configurar os seus secrets com as informações de banco de dados e SymmetricSecurityKey
para gerar o token do usuario.

Para rodar o projeto use o terminal e rode o seguinte comando:

``` bash
#Rodar Projeto
 
$ dotnet watch run

##Rodar APP no Visual Studio
Para rodar o projeto aperte as tecals:

``` bash
#Rodar Projeto 
Crtl + F5
O porjeto irá rodar nas portas https:7060 e http: 5206