Inicializa o HA configurado

	cd HAProxy
	docker image build -t haproxy-configurated:1.6 .

Sobe os conteiners:
	Pre requisito:executar cmd como admin
	cd ClusterRabbitWithDocker
	start.cmd