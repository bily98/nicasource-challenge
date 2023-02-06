[![.NET](https://github.com/damienbod/AspNetCoreIdentityFido2Mfa/workflows/.NET/badge.svg)](https://github.com/bily98/nicasource-challenge/actions/workflows/bilycloud.yml)
# Nicasource Challenge
### This Project is building with .NET 7 and [Metronic](https://keenthemes.com/metronic).

## Description

### This project implements a login with Azure AD B2C, in addition, it makes use of the services of Azure Blob Storage and Azure Cosmos DB to create a personalized cloud for each user that allows the upload and download of files.

### To develop this project, Clean Architecture concepts and Dependency Injection, Repository, and Specification design patterns were implemented.

## Steps to run

### Create your own Azure AD B2C configuration and put into the AzureB2C section inside of [appsettings.json](https://github.com/bily98/nicasource-challenge/blob/master/NicasourceChallenge.Web/appsettings.json).

```json
"AzureAdB2C": {
    "Instance": "your instance",
    "ClientId": "your client id",
    "Domain": "your domain",
    "SignUpSignInPolicyId": "your signup and signin policy",
    "ResetPasswordPolicyId": "your reset password policy",
    "EditProfilePolicyId": "your edit profile policy"
}
```

### Create an account for Azure Cosmos DB, after that create a database and a container with PartitionKey as /PartitionKey.

#### Copy the connection string and put into the CosmosConnectionString section.

```json
"CosmosConnectionString": {
    "ConnectionString": "your connection string",
    "DatabaseName": "your database name"
}
```

#### Copy the container name and update it in the file [CosmosDbContext.cs](https://github.com/bily98/nicasource-challenge/blob/master/NicasourceChallenge.Infrastructure/Data/CosmosDbContext.cs).

```c#
modelBuilder.Entity<File>(e =>
{
    e.ToContainer("your container name");
    e.Property(p => p.Id);
    e.HasPartitionKey<File, string>(p => p.PartitionKey);
});
```

### Create Azure Blob Storage account, after that create a container and copy the connection string and put the values in [appsettings.json](https://github.com/bily98/nicasource-challenge/blob/master/NicasourceChallenge.Web/appsettings.json)

```json
"BlobConnectionString": "your connection string",
"BlobContainerName": "your container name",
```

## Deploy

### To deploy your application you need to create an Azure App Configuration account and add your settings.

### After that you need to set the value for AppUri in [appsettings.json](https://github.com/bily98/nicasource-challenge/blob/master/NicasourceChallenge.Web/appsettings.json)

```json
"AppUri": "your app configuration uri"
```