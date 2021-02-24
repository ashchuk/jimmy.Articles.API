Assignment:
✅ Headless (no UI) CMS for managing articles (CRUD).
- create article
- delete article
- get article by id
- get articles (ascending or descending order + basic limit)
- update article title and body

❗️ Todo: add offset/pagination implementation;

✅ Each article has its ID, title, body and timestamps (on creation and update).
- Id Guid
- Title string 
- Body string
- Created DateTime
- Updating DateTime

✅ Managing articles is the typical set of CRUD calls including reading one and all the articles. Creating/updating/deleting data is possible only if a secret token is provided (can be just one static token).
- Basic auth implemented 

❗️ Todo: set default user/password by env variables in docker-compose configuration file;

✅ For reading all the articles, the endpoint must allow specifying a field to sort by including whether in an ascending or descending order + basic limit/offset pagination.

❗️ Todo: add offset/pagination implementation; response should contains:
- total page count
- current page
- display limit and sort order parameters

❗ The whole client-server communication must be in a JSON format and be ready for extending with other formats (eg. XML).
Todo: add JSON/XML switcher to docker-compose configuration file

Technical Requirements:
* ✅ C#
* ✅ .NET Core 3+
* ✅ ASP.NET Core
* ❗ MSSQL
* ❗ README
* ❗ Docker
* ✅ Automated tests

❗ Todo: 
- add docker/docker-compose support
- add MSSQL container into docker-compose file
- write pretty README file

To run tests
```docker-compose -f .\docker-compose.tests.yml -f .\docker-compose.tests.override.yml up sqldata-test articles-api-test```
To run app
```docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up sqldata articles-api```