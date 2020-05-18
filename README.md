# Gifter App
Melissa Eenmaa IDDR – ICD0009 Building Distributed Systems – 2020

docker build --tag gifterapp-image .
docker create --name gifterapp-container --publish 80:80/tcp gifterapp-image
docker start gifterapp-container
