# RabbitMQExample
Um mini tutorial de como funciona o RabbitMQ
As aplicações contam com o RabbitMQClient, nesse cenário foi utilizado o server do rabbit com o docker, basta executar a linha abaixo:
```
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```
