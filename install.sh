#!/bin/bash

# navigate to the directory containing the docker-compose file
cd ./Source/

# stop and remove any running instance of the compose file
docker-compose down

# pull the latest images
docker-compose pull

# build and run the compose file
docker-compose up -d --build