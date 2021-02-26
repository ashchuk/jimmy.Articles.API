# Articles REST API application

This is a simple Articles CMS example of a .Net Core application written in CQRS pattern manner. 
It provides a REST API to a EntityFramework-backed article model.
The application written using MediatR, it's a .Net mediator pattern implementation by Jimmy Bogard.

`.env` is a docker-compose environment configuration for application.

## About repo and app structure

Project folders contains it's own README files, you can check it to get more details about some project parts.

- [Controllers](https://github.com/ashchuk/jimmy.Articles.API/tree/master/jimmy.Articles.API/Controllers)
- [Domain](https://github.com/ashchuk/jimmy.Articles.API/tree/master/jimmy.Articles.API/Domain)
- [Infrastructure](https://github.com/ashchuk/jimmy.Articles.API/tree/master/jimmy.Articles.API/Infrastructure)
- [Validation and logging pipelines](https://github.com/ashchuk/jimmy.Articles.API/tree/master/jimmy.Articles.API/PipelineBehaviors)
- [Validation models](https://github.com/ashchuk/jimmy.Articles.API/tree/master/jimmy.Articles.API/Validation)

## Run the app

```docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up sqldata articles-api```

By default API available at ```http://localhost:5102/```

After run you can use SwaggerUI which accessible at ```http://localhost:5102/swagger/index.html``` to make some test queries.

## Run the tests

```docker-compose -f .\docker-compose.tests.yml -f .\docker-compose.tests.override.yml up sqldata-test articles-api-test```

## Details

✅ Headless (no UI) CMS for managing articles (CRUD).
- create article
- delete article
- get article by id
- get articles (ascending or descending order + basic limit and offset parameters)
- update article title and body
- simple offset/limit/sorting implementation;

✅ SwaggerUI for testing

✅ Each article has its ID, title, body and timestamps (on creation and update):
- Guid Id
- Title: from 1 to 100 symbols length
- Body: up to 500 symbols length
- Article created date and time
- Article updating date and time

✅ Creating/updating/deleting data is possible only if a secret token is provided.
- App has basic auth implementation, just provide username and password to use POST/PUT/UPDATE methods. GET methods are accessible without auth.
- Default user/password can be set USER_SERVICE_USERNAME and USER_SERVICE_PASSWORD env variables in .env configuration file.
- By default USER_SERVICE_USERNAME set to 'test' and USER_SERVICE_PASSWORD set to 'test'.
- If USER_SERVICE_USERNAME and USER_SERVICE_PASSWORD are not provided, the username set to 'test ' and password set to 'test'

✅ The whole client-server communication works in a JSON format and can be extended with other formats (eg. XML).


## TODOs
Add offset/pagination implementation; response should contains:
- total page count
- current page
- display limit and sort order parameters

Add JSON/XML switcher to docker-compose configuration file