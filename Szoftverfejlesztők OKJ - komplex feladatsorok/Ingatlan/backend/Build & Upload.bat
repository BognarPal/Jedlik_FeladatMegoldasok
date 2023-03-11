docker build . --file Dockerfile -t ghcr.io/bognarpal/ingatlan-webapi:1.0.3 -t ghcr.io/bognarpal/ingatlan-webapi:latest
docker push ghcr.io/bognarpal/ingatlan-webapi:1.0.3
docker push ghcr.io/bognarpal/ingatlan-webapi:latest
docker rmi ghcr.io/bognarpal/ingatlan-webapi:1.0.3
docker rmi ghcr.io/bognarpal/ingatlan-webapi:latest

