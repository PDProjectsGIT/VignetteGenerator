# Image Vignetting Application
This application is designed for applying a vignette effect to an image with additional RGB correction. 
## Table of Contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
## General info
It follows a layered architecture, separating the view and model components. The view is implemented using the .NET framework, providing a user-friendly interface. The model encompasses the processing and data management functionalities, featuring two dynamically linked libraries, both containing nearly identical vignetting algorithms implemented in different technologies. Both algorithms calculate the vignetting coefficient using an exponential function. The high-level algorithm is implemented in VignetteProcessor.cs, while the low-level assembly language version is in VignetteASM/VignetteASM.asm. The application allows measuring the execution time for both algorithms. Additionally, it includes a feature to generate time reports for a selected set of files, thread count, and execution attempts.
## Technologies
* .NET Framework
* Assembly
## Setup
To set up the application, follow these steps:
* Clone the repository to your local machine.
* Ensure that the .NET Framework is installed.
* Open the solution in Visual Studio.
* Build and run the application.
