docker-compose:
	docker-compose -f docker-compose.yml -f docker-compose.override.yml down && \
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build --force-recreate --abort-on-container-exit

lint:
	dotnet format MicroServiceDotnet.sln -v diag
