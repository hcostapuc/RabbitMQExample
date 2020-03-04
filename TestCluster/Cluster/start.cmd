REM Script to manage the containers execution
 
REM Variables for images build.
set JOIN_RABBIT2_RABBIT1="rabbitmqctl stop_app; rabbitmqctl join_cluster rabbit@rabbitmq1; rabbitmqctl start_app"
set JOIN_RABBIT3_RABBIT1="rabbitmqctl stop_app; rabbitmqctl join_cluster rabbit@rabbitmq1; rabbitmqctl start_app"
set JOIN_RABBIT4_RABBIT1="rabbitmqctl stop_app; rabbitmqctl join_cluster rabbit@rabbitmq1; rabbitmqctl start_app"
set OPTIONAL_COMMAND="rabbitmqctl set_policy ha-all '' '{\"ha-mode\":\"all\", \"ha-sync-mode\":\"automatic\"}'"

REM Subindo os container's do rabbitmq

docker-compose down
docker-compose up -d

timeout 15
docker exec -ti rabbitmq2 bash -c %JOIN_RABBIT2_RABBIT1%
docker exec -ti rabbitmq3 bash -c %JOIN_RABBIT3_RABBIT1%
docker exec -ti rabbitmq4 bash -c %JOIN_RABBIT4_RABBIT1%
docker exec -ti rabbitmq1 bash -c %OPTIONAL_COMMAND%