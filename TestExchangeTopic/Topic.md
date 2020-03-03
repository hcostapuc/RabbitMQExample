Topic segue a mesma diretriz do Direct, com um detalhe de diferen�a, suas routingKeys s�o din�micas, isso
faz com que ela atenda a keys com prefixos. Para montar uma routingKey dinamica, utilizamos dois caracteres,
o "*" para dizermos que ele substituirá uma unica palavra e o "#" para substituir uma ou mais palavras.

No nosso cenário, ainda representaremos um fluxo de log, iremos implementar as routingKeys com 2 níveis: <facility>.<severity>
iremos criar 4 receiveis um com as seguintes routingKeys:
	1 - "#" Ou seja ela aceitará qualquer key, de qualquer formato.
	2 - "kern.*" Aceita as keys que tenham o nivel <facility> = kern e de qualquer severidade (lembrando que o "*" bainda apenas uma palavra).
	3 - "*.critical" Aceita as keys que tenham o nivel <severety> = critical e de qualquer facility.
	4 - "kern.*" "*.critical" Aqui estamos criando um multiple binding, estamos dizendo que uma única fila
		recebe mensagens das duas routingKeys, é como agrupar o exemplo 2 e 3 em uma única queue.

Para montar o cenário:

1 - Entramos na pasta do ReceiveLogsTopic: cd ReceiveLogsTopic
2 - Executamos o comando para criar o receive que aceita qualquer key, de qualquer formato,tamanho...: dotnet run "#"
3 - Na mesma pasta executamos o outro receive: dotnet run "kern.*"
4 - Aqui executamos mais um receive: dotnet run "*.critical"
5 - E o ultimo receive: dotnet run "kern.*" "*.critical"
6 - Por ultimo entramos na pasta do publisher: cd EmitLogTopic
7 - Executamos o comando que da match com todas as keys que criamos nos receives: dotnet run "kern.critical" "A critical kernel error"