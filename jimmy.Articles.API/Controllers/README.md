
# REST API

The REST API to the app is described below.

## Create a specific Article

### Request

`POST /api/v1/Articles`

    curl -X POST "https://localhost:5001/api/v1/Articles" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0" -H  "Content-Type: application/json" -d "{\"title\":\"string\",\"body\":\"string\"}"

### Request body

    Title = <string> - article title
    Body = <string> - article body

    {
        "title": "string",
        "body": "string"
    }

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

## Get Article by ID

### Request

`GET /api/v1/Articles/{id}`

    curl -X GET "http://localhost:5102/api/v1/Articles/<Article ID>" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0"

### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 09:21:51 GMT
    server: Kestrel
    transfer-encoding: chunked

    {
        "id": "c4fbd6a6-dc99-4806-908c-cef423687936",
        "title": "string",
        "body": "string",
        "creationDate": "2021-02-25T15:06:03.4472804",
        "updatingDate": "2021-02-25T15:06:03.4473232"
    }

## Delete Article

### Request

`DELETE /api/v1/Articles/{id}`

    curl -X DELETE "http://localhost:5102/api/v1/Articles/<Article ID>" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0"

### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 09:21:51 GMT
    server: Kestrel
    transfer-encoding: chunked

    {
        "id": "c4fbd6a6-dc99-4806-908c-cef423687936",
        "title": "string",
        "body": "string",
        "creationDate": "2021-02-25T15:06:03.4472804",
        "updatingDate": "2021-02-25T15:06:03.4473232"
    }

## Update Article

### Request

`PUT /api/v1/Articles/{id}`

    curl -X PUT "http://localhost:5102/api/v1/Articles/<Article ID>" -H  "accept: */*" -H  "Authorization: Basic dGVzdDp0ZXN0" -H  "Content-Type: application/json" -d "{\"title\":\"asd\",\"body\":\"asd\"}"

### Request parameters

    Title = <string> - article title
    Body = <string> - article body

    {
        "title": "string",
        "body": "string"
    }

### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 09:21:51 GMT
    server: Kestrel
    transfer-encoding: chunked

    {
        "id": "c4fbd6a6-dc99-4806-908c-cef423687936",
        "title": "string",
        "body": "string",
        "creationDate": "2021-02-25T15:06:03.4472804",
        "updatingDate": "2021-02-25T15:06:03.4473232"
    }

## Authenticate user

### Request

`POST /api/v1/Users/authenticate`

    curl -X POST "http://localhost:5102/api/v1/Users/authenticate" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"username\":\"test\",\"password\":\"test\"}"

### Request parameters

    Username = <string> - user name
    Password = <string> - user password

    {
        "username": "string",
        "password": "string"
    }
    
### Response

    content-type: application/json; charset=utf-8
    date: Thu,25 Feb 2021 15:11:33 GMT
    server: Kestrel
    transfer-encoding: chunked

    {
        "id": 1,
        "username": "test",
        "password": null
    }
