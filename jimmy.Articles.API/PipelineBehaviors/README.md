## Pipelines

This folder contains MediatR PipelineBehavior implementations, which allow to build pipeline directly inside of MediatR without resolving to using decorators around handlers. 

It's a more natural way to enhance your handlers with behavior and better supported in containers.

So, there are two pipelines here:
- ValidationBehavior - it's a pipeline that check command`s fields and validate them in FluentValidation manner.
- LoggingBehavior - it's a pipeline that collects data the data that went through CommandHandlers and QueryHandlers. Also it collects the data that comes from users and send it to console.