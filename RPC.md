#RPC Tutorial

É como o exemplo TestSimple, só que adicionamos um callback, ou sejá o publisher envia a mensagem para uma queue que é consumida por um consumer,
assim que consumida ela é processada e publicada um response em uma queue de callback, e por fim, esta mensagem é consumida pelo publisher.

O client é publisher/consumer assim como o server é publisher/consumer, onde a logica de processamento do nosso cenário está no server (a função fibonacci).

Utilizamos o "basic acknowledgment" no consumer para termos um "server data safety"

No nosso cenário teremos 2 servers balancedos (iram fazer um loadbalance das requisições) que executam a função fibonacci. Os servers atenderam a uma queue e replicaram para a queue (queue de callback) 
em que o client atende.

Executar o cenário:

1 - Entrar na pasta RPCServer: cd RPCServer
2 - Executar o primeiro server: dotnet run
2 - Executar o segundo server: dotnet run
3 - Entrar na pasta do client: cd RPCClient
4 - Executar o client: dotnet run
5 - Para vermos o balanceamento dos servers, só executar mais de uma vez o client.