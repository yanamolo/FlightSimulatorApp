# FlightSimulatorApp

Third Mile-Stone main achievements:

    1) Using the .NET Framework to create a GUI App for FlightGear.
    2) Using MVVM architecture in a multi-threaded environment.
    3) Implementing a TCP Client to send/receive and parse data from FlightGear.

## About the implementation process

This App has three main parts that run it, each part with its own designated responsibilities. 
The **Model** interacts with The server (the Flight-Gear app or server script) via TCP connection, continuously send and read data 
requests and notifies the relevant **ViewModel** when it's data changed. Next, the **ViewModels** process the changed data and notifies 
the **Views** about the changed data. the **Views** reacts to the changed data accordingly, the data flows both from and to 
the **Model**.

## Added Fetures

- Home screen that allows you to connect by default, or enter your own ip and port addresses.
- Main screen with a map that shows where is the airplain right now in the world, joystick that moves the airplain, sliders of the
throttle and rudder that you can change as you wish.
- Button that change the pin of the plane. You can choose an icon as you like :)
- Button that can show you the LATITUDE and LONGITUDE of the airplain at each moment.
- Button that can remove any error that jumps on the screen.

## Compiling and Running
We provide a _batch script_ to compile this project, you should use it!
The batch file is called _build_and_run_ and is added to the repository.
To properly use this _batch script_ you should download the repository content without changing any files location and have Visual Studio 2019 installed, just double click the _batch script_ and you're good to go :)
(note that _build_and_run_ should reside in the same folder as the FlightSimulatorApp.zip you downloaded).

## Python Script
We provide a python script called_dummyServer_ which acts as a FlightGear server. We used this _dummyServer_ while building the project to speed up development.

![FlightGearApp](https://github.com/yanamolo/FlightSimulatorApp/blob/master/BackgroundHome.png)
<!--stackedit_data:
eyJoaXN0b3J5IjpbLTIwNDE0Njk4NTZdfQ==
-->
