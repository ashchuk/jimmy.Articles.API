## Pipelines

This folder contains MediatR PipelineBehavior implementations, which allow building a pipeline directly inside of MediatR without resolving to use decorators around handlers.

It's a more natural way to enhance your handlers with behavior and better supported in containers.

So, there are two pipelines here:
- ```ValidationBehavior.cs``` - it's a pipeline that checks command`s fields and validates them in ```FluentValidation``` manner.
- ```LoggingBehavior.cs``` - it's a pipeline that collects data that went through CommandHandlers and QueryHandlers. Also, it collects the data that comes from users and sends it to the console.

### Note:

ValidationBehavior throws ```new ValidationException()``` when one of the validators fails.

There is a workaround in ```Startup.cs``` to transform this exception from 500 status code errors to 400 status code messages with a user-friendly response type.
See details in [ValidationExceptionsPreprocessing](https://github.com/ashchuk/jimmy.Articles.API/blob/master/jimmy.Articles.API/Startup.cs#L164) region in ```Startup.cs```
