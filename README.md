# API-Template


## Build

#### Download Docker
#### Start Docker

#### Clone the repository and navigate to the application

```
git clone https://github.com/EnisMulic/CSharp-API-Template
cd Eventi
```

#### Build the Docker Image

```
docker-compose build
```

#### Start the Docker Container
```
docker-compose up
```

## Project Structure

```
.
├───Template.Authorization            # Custom Authorization
│   ├───Constants                     # Constant Names of Roles, Policies, Claims
│   ├───Handlers                      # Requirment Handlers
│   └───Requirements                  # Requirments for a custom Auth Policy
├───Template.Contracts                # Contracts/DTOs Used by the WebApi
│   └───V1                            # Version Folder
│       ├───Requests                  # 
│       └───Responses                 #
├───Template.Core                     # The infrastructure of the project
│   ├───Helpers                       # (e.g. PaginationHelper)
│   ├───Interfaces                    # Service Interfaces
│   └───Settings                      # Settings (e.g. for the EmailService)   
├───Template.Database                 # Database Context   
│   ├───Migrations                    #
├───Template.Domain                   # Domain Models used for the Database
├───Template.EmailService             #
├───Template.Sdk                      # Refit REST API Client
├───Template.Services                 # Implementation of API Services
├───Template.WebAPI                   # RESTful API Project
│   ├───Controllers                   # API Controllers
│   │   └───V1                        # Version Folder
│   ├───Extensions                    #
│   ├───Installers                    # Service registry, Swagger configuration, Automapper profile installatio
│   └───Mappings                      # Mapping Profiles                   
└───Template.WebAPI.IntegrationTests  #
```