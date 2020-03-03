#RPC Tutorial

� como o exemplo TestSimple, s� que adicionamos um callback, ou sej� o publisher envia a mensagem para uma queue que � consumida por um consumer,
assim que consumida ela � processada e publicada um response em uma queue de callback, e por fim, esta mensagem � consumida pelo publisher.

O client � publisher/consumer assim como o server � publisher/consumer, onde a logica de processamento do nosso cen�rio est� no server (a fun��o fibonacci).

Utilizamos o "basic acknowledgment" no consumer para termos um "server data safety"

No nosso cen�rio teremos 2 servers balancedos (iram fazer um loadbalance das requisi��es) que executam a fun��o fibonacci. Os servers atenderam a uma queue e replicaram para a queue (queue de callback) 
em que o client atende.

Executar o cen�rio:

1 - Entrar na pasta RPCServer: cd RPCServer
2 - Executar o primeiro server: dotnet run
2 - Executar o segundo server: dotnet run
3 - Entrar na pasta do client: cd RPCClient
4 - Executar o client: dotnet run
5 - Para vermos o balanceamento dos servers, s� executar mais de uma vez o client.