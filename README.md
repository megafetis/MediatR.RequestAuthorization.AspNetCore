# MediatR.RequestAuthorization.AspNetCore

[![NuGet](https://img.shields.io/nuget/dt/UTypeExtensions.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization.AspNetCore/) 
[![NuGet](https://img.shields.io/nuget/vpre/UTypeExtensions.svg)](https://www.nuget.org/packages/MediatR.RequestAuthorization.AspNetCore/)

Collection of useful type extensions

## Installing MediatR.RequestAuthorization.AspNetCore

You should install [MediatR.RequestAuthorization.AspNetCore with NuGet](https://www.nuget.org/packages/MediatR.RequestAuthorization.AspNetCore):

    Install-Package MediatR.RequestAuthorization.AspNetCore
    
Or via the .NET Core command line interface:

    dotnet add package MediatR.RequestAuthorization.AspNetCore


##### Common usage (Example):

Check InheritsOrImplements:

```cs 
// Register Authorization Rules classess, that implements IAuthorizationRule
// Also in this method will be registered RequestAuthorizationBehavior, that implements IPipelineBehavior<,>
services.AddMediatRAuthorization([Assembly1,Assembly2]);


// Register IUserContext service with default HttpUserContext or implement your own class
services.AddScoped<IUserContext,HttpUserContext>();


// required for HttpUserContext service
services.AddHttpContextAccessor();

```

To read how to define authorization rules go to [MediatR.RequestAuthorization git repo](https://www.nuget.org/packages/MediatR.RequestAuthorization):