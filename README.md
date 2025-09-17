# New Project Name TBD
> Formerly *Festiv* – A reimagined personal project, born from a chaotic group assignment and rebuilt with care, intention, and functionality.

## Overview

This project began as a group capstone assignment for LaunchCode. Originally intended as a large-scale party planning platform with ambitious social features, the group effort ultimately fell short due to over-scoping, misalignment, technical inexperience and a harsh timeline. Many key features were incomplete or broken, and the codebase was fragmented and inconsistent.

This repository represents a **personal reclamation** of that original idea. I've taken ownership of the project, plan to clean up the architecture, refactor broken functionality, and iterate toward a clean, minimal MVP I can proudly showcase as a reflection of my current skills and growth as a developer.

## Vision

A lightweight event planner for game night or similar small group events, rebuilt with a focus on:

- Usability over novelty
- Clear and maintainable code structure
- Respecting technical boundaries while learning and iterating
- Deployment-ready architecture

### In Progress: New Name & Branding
The project is currently titled **Festiv**, but will be renamed upon completion to reflect its new identity as a personal project.

---

## Tech Stack

- **JavaScript** for frontend behavior
- **C#** with **ASP.NET Core MVC**
- **Razor Views** for server-rendered pages
- **Entity Framework Core** for ORM
- **MySQL** for relational data storage

---

## Project Structure

This is a **monolithic** application with no frontend-backend separation. Server-side rendering is handled through Razor Views, with backend logic and data access managed through ASP.NET Core and Entity Framework.

---

## Running the Project Locally

> This project targets **.NET 8.0** and is built with **ASP.NET Core MVC**, which supports cross-platform development.  
> However, some setup steps may currently assume a Windows-based environment, so non-Windows users might need to adjust configurations.

### Requirements:
- [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Visual Studio, Visual Studio Code, or any IDE supporting ASP.NET Core development

### Setup:
1. Clone the repository:
   git clone https://github.com/loganrains/Festiv.git

2. Set up your MySQL server and update the connection string in appsettings.json.

3. Run database migrations (if applicable).

4. Build and run the project:
    dotnet build
    dotnet run
