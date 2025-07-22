# Haus Design Culture App

A custom software made for Haus Design Culture. </br>
The software was design to track orders, development process and ensure that the employees productivity and accuracy.
Designed to enhance operational efficiency, and offer a comprehensive suite of features to streamline business processes and facilitate informed decision-making.

## Key Features

- Tracking the current stage of the curtain's building stage (Sew, Iron, QC).
- Add, Delete, Bill and ship orders.
- Add, Delete employees.
- Track employee's work and productivity.

## Technologies Used

- C# and WPF (Windows Presentation Foundation)
- .Net Framework 4.7.2
- Material Design
- Rest API

## Project Structure

The project is split into two main components:

- HausManagementLibrary - Responsible for the logic and connection with the server.
- HausManagementUI - Responsible for displaying the data properly.

## How to Run

> ⚠️ **Important:** Before running the app you need to have the [server](https://github.com/noxi-tech/haus-backend) available and working.

### Development environment:

To open the project in development enviroment use visual studio to run the solution (HausApp.sln) located in the root folder.

### Production app:

Check the releses download the latest relese, install and run the app.

## How to use:

The project was made for Haus Design Culture, so the content and data are according to the owner's request, that being said start by.

### 1. Connect to the server:

When you run the server you will get the hostname and port (usually 8000) the server is running on.

when you got your hostname and port connect it to the app by opening the connection settings and put in the following:

```code
http://{Hostname}:{Port}
```

then press the connect button.

### 2. Choose app role:

Now you need to choose between Manager or Employee, each one serves a different purpose.
after choosing you will have connected and locked the role of the app.

### Notes:

> The default password for the manager is **admin**

to unlock the app role you will have to right click on the three vertical points next to the connection textbox in the connection settings.
you will have to enter **1234** in the textbox that appears.

## Acknowledgments

External plugins / libraries:

- Material Design
- Newtonsoft

Contributers:

- [Ibraheem Assad](https://github.com/Ibraheem-Asaad)

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
