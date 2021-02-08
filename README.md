# Funda

## Challenges that affected the system design
* Depend on third party API to query the top makelaars. that will require pulling all Funda API data first then query the top makelaars from the returned API data .this operation will take a long time depending on how much data the API has
* Funda API requests are limited to 100 requests per minute
## System design decisions
* Introduce a background service(azure function in our case) that run periodically and persist the Funda API data to a local database
* Create an API that queries the local database to get the top makelaars.
* Introduce retry logic mechanism to solve the Funda API requests limitation
## The system components
* Azure function that should run periodically to persist Funda API data to a local database
* .Net core API that queries the locally persisted data
![alt text](https://github.com/aymansayedmohamed/Funda/blob/Master/System%20Design.png)
## Steps to run the solution
* Clone the repository
* Build the solution to restore the NuGet packages
* Run the solution from Visual Studio
* The Synchronize azure function and the .net core API should start
* Azure function logs should show the persisted data progress
* Please wait for at least one minute or till the azure function pull some data from Funda API and persist it to the local database
* This Get URL get the top 10  makelaar's in Amsterdam have the most object listed for sale "http://localhost:59500/api/Funda?tuin=false" 
* This Get URL get the top 10  makelaar's in Amsterdam have the most object with garden listed for sale "http://localhost:59500/api/Funda?tuin=true" 
