dockerhub_username=mubbly
dockerhub_reponame_server=gifterapp-server
dockerhub_reponame_client=gifterapp-client

# NOTE: Currently image tag will always default to :latest
server_image_name=$(dockerhub_username)/$(dockerhub_reponame_server)
client_image_name=$(dockerhub_username)/$(dockerhub_reponame_client)

dbuild-image-server: ## Build docker image for server
	docker build --tag $(server_image_name) GifterSolution

dbuild-image-client: ## Build docker image for client
	docker build --tag $(client_image_name) GifterClient

dbuild-images: ## Build docker images for server and client
	make dbuild-image-server
	make dbuild-image-client

dpush-image-server: ## Push server docker image to dockerhub
	docker push $(server_image_name)

dpush-image-client: ## Push client docker image to dockerhub
	docker push $(client_image_name)

dpush-images: ## Push client docker image to dockerhub
	docker push $(server_image_name)
	docker push $(client_image_name)

# NOTE: For side by side usage in local env mapping frontend port 80 to 8080 to avoid conflic with backend on port 80.
# 		In azure both will run on port 80
dcreate-containers: ## Create docker containers from server and client for local
	docker create \
		--name gifterapp-server \
		--publish 80:80/tcp \
		$(server_image_name)
	docker create  \
		--name gifterapp-client \
		--publish 8080:80/tcp \
		$(client_image_name)

dremove-containers: ## Stop and remove the containers if they are running and exist
	docker container stop gifterapp-server
	docker container rm gifterapp-server
	docker container stop gifterapp-client
	docker container rm gifterapp-client

dstart-containers: ## Start docker containers for server and client
	docker start gifterapp-server
	docker start gifterapp-client

az-create-resource-group: ## Ran only once to create azure resource group
	az group create \
		--name gifterappResourceGroup \
		--location northeurope

az-create-plan: ## Ran only once to create azure subscription plan
	az appservice plan create \
		--resource-group gifterappResourceGroup \
		--sku B1 \
		--is-linux \
		--name basic1

# NOTE: Server will run on gifterapp.azurewebsites.net (user doesn't see this in normal operations)
az-create-webapp-server: ## Create server container in azure
	az webapp create \
		--resource-group gifterappResourceGroup \
		--name gifterapp-server \
		--deployment-container-image-name $(server_image_name) \
		--plan basic1

# NOTE: Client will run on gifterapp.azurewebsites.net
az-create-webapp-client: ## Create client container in azure
	az webapp create \
		--resource-group gifterappResourceGroup \
		--name gifterapp \
		--deployment-container-image-name $(client_image_name) \
		--plan basic1

az-webapp-restart: ## Restart azure webapp will update images
	az webapp restart \
		--name gifterapp \
		--resource-group gifterappResourceGroup
	az webapp restart \
		--name gifterapp-server \
		--resource-group gifterappResourceGroup

az-update: ## Build and push docker images, restart azure webapp
	make dbuild-images \
		&& make dpush-images \
		&& make az-webapp-restart
		
help: ## Show this help message
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) \
	| awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'
	