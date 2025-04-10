# Hi, this is Jesse :)
## This is going to be the documentation for how to setup this robot project!

#### The point of this project is to implement minimal api, low latency with MQTT, and to try out native AOT

#### the nuget packages associated will be as follows:
#### For API:
- dotnet add package MQTTnet.AspNetCore
- dotnet add package

#### For INFRA layer:
- dotnet add package Dapper #Db access
- dotnet add pacakge Npgsql #PostgreSQL
- dotnet add package MQTTnet.AspNetCore --version 4.2.0.706
- dotnet add package MQTTnet --version 4.2.0.706

### To run project:
- need dotnet 8
- go to api folder ( i.e. cd TexistenceRobot.Api)
- dotnet run 


$ curl -X POST http://localhost:5085/robots/TX-010/commands   -H "Content-Type: application/json"   -d '{"command":"move
_forward", "robot":"TX-010", "user":"operator1"}'


$ curl http://localhost:5085/robots/TX-010/status


$ curl -X PUT http://localhost:5085/robots/TX-010/commands/cmd123 \
  -H "Content-Type: application/json" \
  -d '{"command":"stop"}'


$ curl http://localhost:5085/robots/TX-010/status