# API-Template

## Build

#### Download Docker
#### Start Docker

#### Clone the repository and navigate to the application

```
git clone https://github.com/EnisMulic/CSharp-API-Template
cd CSharp-API-Template
```

#### Build the Docker Image

```
docker-compose build
```

#### Start the Docker Container
```
docker-compose up
```

* Note:
If you skip the docker part and try to run the api locally it will fail do to it not being able to connect to redis, to fix this execute

```
docer run -p 6379:6379 redis
```


## Project Structure

```
.
├───Template.Authorization            
│   ├───Constants
│   ├───Handlers
│   └───Requirements
├───Template.Contracts
│   ├───HealthChecks
│   └───V1
├───Template.Core
│   ├───Helpers
│   ├───Interfaces
│   └───Settings
├───Template.Database
├───Template.Domain
├───Template.EmailService
├───Template.Sdk
├───Template.Services
├───Template.WebAPI
│   ├───Attributes
│   ├───Controllers
│   │   └───V1
│   ├───Extensions
│   ├───Installers
│   └───Mappings
└───Template.WebAPI.IntegrationTests
```