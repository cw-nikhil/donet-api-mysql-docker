This is a simple dotnet core api working as a key value store. It uses MySql for data storage. But since the applications has been containerized we don't need to have dotnet core. Both dotnet api and mysql will run in 2 separate docker containers. For mysql I have used the MySql official docker image.

It has just 2 APIs for now to keep things simple

POST: api/add 
{

    "Key": "name",
    
    "Value": "Nikhil"
    
}

to add a key value pair. Duplicates are not allowed

GET: api/get/key

Steps to run:
1. First clone this repo
 
   **gh repo clone cw-nikhil/donet-api-mysql-docker**
2. Navigate to the donet-api-mysql-docker folder on your local through command line
3. Then you need to run the image associated with this project as well as the MySql official image.

   **sudo docker-compose -f compose.yaml up**
   
   Of course you need to have docker and docker-compose intalled on your machine
   
4. Once the containers are running we need to connect to the mysql docker container to create a table.

      DB Credentials:

      host: localhost

      port: 5001

      username: root

      password: sturdy
  
5. After connection execute the create table command given in queries.sql

Now you can test the APIs using Postman. Also you can change the password of the MySql intance. Just change both the password fields in compose.yaml file
