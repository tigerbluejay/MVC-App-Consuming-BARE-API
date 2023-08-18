# MVC App Consuming Buenos Aires Real Estate (BARE) API

An MVC application consuming the BARE API Service, able to perform CRUD operations on the data retrieved, and able to send data to the API.

The application works with the Buenos Aires Real Estate API, which is a a real state API service exposing Apartment Unit Rental Information in the City of Buenos Aires.

#### Disclaimer

Note: There is another repository containing the standalone implementation of the Buenos Aires Real Estate API. It can be found [here](https://github.com/tigerbluejay/Buenos-Aires-Real-Estate-API). Note that the standalone implemnetation has some differences with the Buenos Aires Real Estate API that is part of this project. This is so, as changes were made during the development of the MVC App in the Buenos Aires Real Estate API (that is part of this Repo) to make the MVC Application compatible with that API. 

Note however that the API in the separate repository has its own database and works perfectly well with Swagger in its intended implementation.
This is to say that the databases from the standalone repo Buenos Aires Real Estate (BARE) API are slightly different from the database generated for the BARE API which is part of this project.

The differences are minor and the structure of the Buenos Aires Real Estate API both in this project and in its standalone repo version are the same. So we'll proceed discussing the characteristics of the MVC App in this readme file.



## Project Structure: MVC App Consuming the BARE API

The MVCApp Consuming the Bare API has three Projects. The Web Project where the Configuration Files, Controllers, View Models and Error View Model, Services and Service Interfaces and Views and Shared Views are located. The Models Project contains the DTOs, Identity DTOs, Models and Static Details. Note the DTOs must coincide with those defined in the API project. a And the Utilities Project only contains the Mapping Configuration for Auto-mapper.


### Components: Configuration Files

When we speak of Configuration Files we refer to the following: Fist, the Program.cs where all the Application Pipeline and Middleware is registered. Here we add services such as Automapper, we register services for dependency injection, we add the HttpContextAccessor for the shared view Layout, we add Authentication, Cookies and Cache, Sessions and register various middlewares.

Second, the launchSettings.json is configured with an eye on not overlapping ports for the joint launch of the BuenosAiresRealEstate.API project and the MVCAppConsumingBAREAPI.Web projects. (Overlap on ports must be checked on the API's launchSettings.json file as well).

Third, the wwwroot folder contains an Images folders with subfolders where the various images used in the MVC App are saved. These images correspond to the ImageUrl Properties of the ApartmentComplexDTO, ApartmentUnitDTO as well. Note that the ApartmentUnitDTO has an ApartmentComplexDTO object from which we retrieve the ImageUrl to display it in the Index view of the ApartmentUnitController.

Finally we have the Project Configuration file which we can access via the contextual menu on each project by selecting "Edit Config File" where we can set the .NET version we are using and whether we want nullable values to be enabled or disabled. (For this project they are disabled).

An additional configuration file we might consider is in the Utilities Project and it is the Mapping.cs file which contains various mappings used in the Services, for translating various types of DTOs.


### Auth

The Auth Component Implementation consists of various parts:

The Auth Controller, which contains implementations for Register, Login and related actions, interfaces with the Auth Service (and IAuthService) which sends the API Request to the Buenos Aires Real Estate API. 

The Auth DTOs called here IdentityDTOs, include LoginRequestDTO, LoginResponseDTO, RegistrationRequestDTO, and UserDTO. These DTOs serve as communication between the Controller and Service components, and are included in the APIRequest object in the Data Property, such that they are sent to the Buenos Aires Real Estate API. 

There are also Login and Registration Views which serve to collect the credentials that are sent. The AuthService communicates with the BaseService which is the one actually requesting and receiving responses from the Buenos Aires Real Estate API.


### Apartment Complex

The Apartment Complex Implementation Structure is similar:

We have an ApartmentComplexController which interfaces with various views and processes the views inputs from the views communicating with the ApartmentComplexService (Which Implements an interface). This service also links into the Base Service for final communication with the API.
The Controller has various methods corresponding to CRUD operations. There are likewise views to display all the Apartment Complexes, Create a new Apartment Complex Entry, Delete a given Apartment Complex and Edit or Update it.

Let's not forget the DTOs (ApartmentComplexDTO (for Read and Delete), ApartmentComplexCreateDTO, and ApartmentComplexUpdateDTO). These DTOs
are parameters in the Controller methods and are passed on to the API Request. These are passed also in the Data Property of the API Request that is sent to the API. But they are also used when we get a response, to deserialize the response Result into an object of type DTO such that it can be displayed in the various views.


### Apartment Unit

The Apartment Unit Implementation Structure is a mirror to the Apartment Complex Implementation with some differences.

We also have an ApartmentUnitController which interfaces with various views and communicates with the ApartmentUnitService which then sends the data to the Base Service to do the communicating with the API.

The are some differences however. Because the views need to display information related to Apartment Complexes since there is a relationship between the tables, there is a special private method defined in the controller called GenerateList, which using LINQ Select statement Projects a List<ApartmentComplexDTO> which is deserialized from the API response the controller receives, into a List<SelectListItems> which are then saved into a View Model (which View Model will depend on the specific method we are working with). This is then saved into the corresponding View Model's ApartmentComplexList property to be displayed in the views. The specifics of the implementation can be seen in the Controller if further clarification were needed.

We also have CRUD style methods in the Controller with the corresponding Views.

The ApartmentUnitService which ultimately communicates with the Base Service, sets up APIRequest information from the various DTOs. Included in this information is not only the DTO data, but also the Url, specific id that is passed on to the Url, the ApiType request and a token. Something similar happens in the ApartmentComplexService.


### Home Component and Shared Views

At the core of the Home Component is the Index view which retrieves the list of Apartment Complexes in the manner previously described: The ApartmentComplexService is invoked, which relies on the BaseService. And the response is processed when the response's Result is deserialized into an ApartmentComplexDTO list which is then pased to the Home View.

Apart from the Home Component we have Shared Views like Layout, but also a Validation Scripts partial which contains jquery validation files on which the various views rely to do field validation.

#### In Sum

This is just a brief description or overview of the main components in the Web App portion of the Application. For a description of the Buenos Aires Real Estate API, [click here](https://github.com/tigerbluejay/Buenos-Aires-Real-Estate-API). 

There may be other minor components to covered in this readme but the readme covers most of them and definitely the core components.

