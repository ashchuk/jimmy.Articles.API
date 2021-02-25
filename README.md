# Articles REST API application

This is a simple Articles CMS example of a .Net Core application written in CQRS pattern manner. 
It provides a REST API to a EntityFramework-backed article model.

`.env` is a docker-compose environment configuration for application.

## About app structure

The application written using MediatR, it's a mediator implementation in .NET by Jimmy Bogard.


## Details

✅ Headless (no UI) CMS for managing articles (CRUD).
- create article
- delete article
- get article by id
- get articles (ascending or descending order + basic limit and offset parameters)
- update article title and body
- simple offset/pagination implementation;

✅ Each article has its ID, title, body and timestamps (on creation and update):
- Guid Id
- Title: from 1 to 100 symbols length
- Body: up to 500 symbols length
- Article created DateTime
- Article updating DateTime

✅ Creating/updating/deleting data is possible only if a secret token is provided.
- Basic auth implemented
- Default user/password can be set USER_SERVICE_USERNAME and USER_SERVICE_PASSWORD env variables in .env configuration file.
- By default USER_SERVICE_USERNAME set to 'test' and USER_SERVICE_PASSWORD set to 'test'.
- If USER_SERVICE_USERNAME and USER_SERVICE_PASSWORD are not provided, the username set to 'test ' and password set to 'test'  

✅ The whole client-server communication works in a JSON format and can be extended with other formats (eg. XML).

## Run the app

```docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up sqldata articles-api```

## Run the tests

```docker-compose -f .\docker-compose.tests.yml -f .\docker-compose.tests.override.yml up sqldata-test articles-api-test```

# REST API

The REST API to the example app is described below.

## Create a specific Article

### Request

`POST /api/v1/Articles`

    curl -X POST "https://localhost:5001/api/v1/Articles" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0" -H  "Content-Type: application/json" -d "{\"title\":\"string\",\"body\":\"string\"}"

### Request body

```
    {
        "title": "string",
        "body": "string"
    }
```

### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 09:21:51 GMT
    server: Kestrel
    transfer-encoding: chunked

    {
        "id": "0642b7b5-26b3-4fd1-a090-d64b53f67792",
        "title": "string",
        "body": "string",
        "creationDate": "2021-02-25T12:21:51.107958+03:00",
        "updatingDate": "2021-02-25T12:21:51.107964+03:00"
    }

## Get a list of Articles

### Request

`GET /api/v1/Articles`

    curl -X GET "https://localhost:5001/api/v1/Articles?Limit=2&Offset=2&OrderByDescending=true" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0"

### Request parameters

    Limit = <number> - list size limit
    Offset = <number> - requested list offset
    OrderByDescending = <boolean> - flag to sort result list in ascending or descending order

### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 09:21:51 GMT
    server: Kestrel
    transfer-encoding: chunked

    [
        {
        "id": "0642b7b5-26b3-4fd1-a090-d64b53f67792",
        "title": "string",
        "body": "string",
        "creationDate": "2021-02-25T12:21:51.107958",
        "updatingDate": "2021-02-25T12:21:51.107964"
        },
        <...>
    ]

## TODOs
Add offset/pagination implementation; response should contains:
- total page count
- current page
- display limit and sort order parameters

Add JSON/XML switcher to docker-compose configuration file