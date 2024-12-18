# MovieMate Web Application
Information Systems Project

Author name: Sofía Martínez Pastor. Student ID: 

This project is a Movie Catalog built using ASP.NET Core MVC. It intends to be a web app where users can save the movies they have watched, rate them and leave comments. It is my first project using these technologies so I intend to keep it basic and functioning.

## Final Goal

The ultimate vision of the project is to use Kaggle data (https://www.kaggle.com/datasets/rounakbanik/the-movies-dataset) to integrate Artificial Intelligence algorithms to match users based on their movie preferencies and ratings.

## Project Requirements:

Web Application (40%):
 
- Develop a web application using the .NET framework.
- The application must support user login and registration.
- The application must store data in a database.
- Use functionalities and concepts covered in practical exercises during the course.
- The solution must be publicly accessible on the web (hosted on Microsoft Azure cloud, another provider, or your own infrastructure). Include the URL in the report.
 
Database (10%):
 
The database must contain at least 5 tables.
Use a SQL server available in the Microsoft Azure cloud (or another provider or your own infrastructure).
The same database must be used for both the web application and the web service.
 
Web Service (20%):
 
- Develop a .NET REST web service that accepts and sends data in JSON format.
- The web service must be accessible via a public URL (Azure or an alternative).
- The service must support all CRUD operations.
- The service must be documented (e.g., using Swagger UI).
- The service must support authentication/authorization for requests.
 
Android Client (20%): ¿?
 
- Develop an Android mobile application that connects to your web service.
- Use the Volley library in the app for communication with the REST service.
- The application must use at least one "Create" and one "Read" operation from your REST service.
 
Report and Source Code (10%):

The report must be submitted as a README.md file within the GitHub repository (for both the web application and the API), and include:
- project title,
- author names and their student ID numbers,
- screenshots of the graphical interface of your mobile application and web application (approximately 5 screenshots in total),
- a brief description of the entire system's functionality,
- a description of tasks completed by each student, and
- an image of the database model with a description (you can create the diagram using SQL Server Management Studio (SSMS) or similar tools).
 
Additional for Extra Credit:

- Enhanced appearance of the web application (CSS + HTML).
- Integration with an LLM (OpenAI API, ChatGPT, GPT4All, etc.).
- Innovative solutions.
- Source Code Requirements:
    The source code must be published on GitHub (alternatively, you may use other platforms like GitLab, BitBucket, etc.):
    - one repository for the web application (+API),
    - one repository for the Android application.
    The repositories must show that more than one commit was made.
    It must be evident that you used at least one pull request that was closed by merging branches.



## What I've done so far

- Create the web app using .NET and Visual Studio Code.
- Integrate user login and registration features using ASP.NET Core Identity.
- Certain pages are accessible only to logged-in users.
- Connect the web app to a Microsoft Azure SQL database, where both user data and movie-related information are stored.
- Publicate the web app in Microsoft Azure
- Implementing all CRUD operations for full data management (with scaffolding)


## Future tasks
1. Swager UI
2. Integrating Kaggle data in the database
3. AI algorithm
4. Enhanced appearance of the web application (CSS + HTML)