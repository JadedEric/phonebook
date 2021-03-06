# PhoneBook.Net
For those occasions when your phone's contact list just isn't enough.

The intent behind this repository is to learn something new, expand on already gained knowledge and just having fun as a developer. I saw this as an opportunity to showcase my knowledge of .net 5.0, Angular and micro service architecture and felt that, because I'm not that familiar with GitHub Actions to use this opportunity to push code, build and show the status thereof.

# Build Status
![PhoneBook CI](https://github.com/JadedEric/phonebook/workflows/PhoneBook%20CI/badge.svg)

# Technologies being used
* .Net 5 WebApi
* Angular 11
* Angular Material
* Fontawesome
* SQLite
* Docker
* Github Actions

# Patterns
* Repository Pattern
* Unit of Work Pattern
* Service Pattern
* Micro-service Pattern

# Project Structure
The project is split into a custom-derived mono-repo layout, where the API layer and the WEB layer has been split by two directories of the same name.

* api
	* PhoneBook.Api
		* PhoneBook.Api.DataContext
		* PhoneBook.Api.DomainModels
		* PhoneBook.Api.Repositories
		* PhoneBook.Api.Services
		* PhoneBook.Api.UnitOfWork
		* PhoneBook.Api.UnitTests
		* PhoneBook.Api.ViewModels
* web
	* src
		* app
			* models
			* services

# API Layer
The API layer has been structured to fit into the constructs of a typical micro-service layer, with a bit of customization to fit my views.

I like to think of a solution structure as a filing cabinet. You use the drawers of the cabinet to house your files in order of relevance, for example, the top drawer contains current files, whether it be bills, invoices, degrees/diplomas completed in the year or anything you need to file away for safe keeping. You move down the drawers as time progresses, with the last drawer containing all historical information.

As such, I see the solution structure for this PhoneBook application as follows.

1. **Database Context.** This is where the EF Core context lives and handles everything in regards to the context.
2. **Domain Models.** These are the entities/models between the service layer and the database. These are the exact model structures representing your tables. The entities
3. **Repositories.** The repositories manages the interaction between the user input from the front-end and the data in the database, via the EF Core database context.
4. **Services.** These are the micro-service managers and is the only gateway between the repository and the front-end. The service handles mapping between view models and domain models.
5. **Unit of Work.** Now, EF itself is a Unit of Work pattern and there is technically no need for this, however, it's just an abstraction between the repository, the EF entity set and the persisting of any changes to the database.
6. **Unit Tests.** All relevant NUnit tests for the project
7. **View Models.** These are your typical data transfer objects (DTO's) that stream content back to the front-end. They represent the data that SHOULD be displayed, not the entire entity. Take into account for this, the entity is rather lean, however, they can be more complext, and complexity that you do no want to stream to the front-end, like DateCreated and LastUpdatedDate, for example.

# Angular layer
The Angular layer is a very basic Angular project with a very small footprint.

I have added folders to house the **models** and the **services** needed, as well as entry in the **environments** to point to the API layer, once run as a local instance.

# Getting up and running
There are currently two ways of getting the system to run. Both the API layer and the Angular layer run as individual projects, meaning for the Angular to read the end points, must the API layer be running.

For now, the project has to be run locally, once the repository has been cloned. I am still working on the Docker container for the project and hope to have that task completed within due course.

Simply clone the repository down, then in the **api** folder, run:

```csharp
dotnet run
```

This will launch the API on https://localhost:5001 and load the swagger documentation as the landing page.

Next, inside the **web** folder, execute the following CLI command:

```powershell
npm run start
```

Which will launch Node's server listener on http://localhost:4200

The web application listens for :5001 on localhost for the API and a CORS allowance has been added for :4200 to continue.

# TODO
Please refer to the TODO.md file in the root for progress
