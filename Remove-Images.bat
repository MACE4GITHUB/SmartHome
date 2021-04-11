@echo on

docker-compose down

docker image rm --force data-api
docker image rm --force smarthomedataapi
docker image prune --force