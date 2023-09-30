##ASP.NET WebAPI Project
This is an example of an ASP.NET WebAPI project that demonstrates working with experiments and data processing. 
The project includes a controller for handling requests, classes for experiments, and database connectivity.

##Configuration
To run the project, you need to configure the connection to the database in the appsettings.json file. 
You can specify the connection string for your SQL Server database in the following section:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ABP_RestAPI;Trusted_Connection=True;"
}
```
##Usage
Run the project.
Use the Swagger UI available at https://localhost:7120/swagger to interact with the API and perform various requests.

##Tests
The project has basic unit tests that validate the logic of the experiments.

##DB diagram

![DB diagram](https://github.com/BIGIMIT/ABP_RestAPI/blob/master/raw/images/diagram.png)

##Sequences diagram

![DB diagram](https://github.com/BIGIMIT/ABP_RestAPI/blob/master/raw/images/sequences-diagram.png)

##Statistical Data
```json
[
  {
    "experimentName": "button-color",
    "count": 600,
    "values": [
      {
        "value": "#FF0000",
        "count": 191,
        "percentage": 31.833333333333336
      },
      {
        "value": "#00FF00",
        "count": 215,
        "percentage": 35.833333333333336
      },
      {
        "value": "#0000FF",
        "count": 194,
        "percentage": 32.33333333333333
      }
    ]
  },
  {
    "experimentName": "price",
    "count": 98,
    "values": [
      {
        "value": "50",
        "count": 6,
        "percentage": 6.122448979591836
      },
      {
        "value": "20",
        "count": 7,
        "percentage": 7.142857142857142
      },
      {
        "value": "5",
        "count": 14,
        "percentage": 14.285714285714285
      },
      {
        "value": "10",
        "count": 71,
        "percentage": 72.44897959183673
      }
    ]
  }
]

```
