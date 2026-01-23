# FleetManagementApp

A **.NET 9 Blazor Server application** with a **Core library**, **shared components**, and **xUnit tests**. Designed to demonstrate a **modular, reusable structure** where services, models, and UI components are shared across the solution.

---

## Table of Contents

- [Features](#features)  
- [Prerequisites](#prerequisites)  
- [Setup](#setup)  
- [Running the App](#running-the-app)  
- [Running Tests](#running-tests)
- [Build App](#running-tests)  

---

## Features

- Binpacking
- Drag & Drop
- Rotate 90
- No overlapping

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)  

Verify SDK:

```bash
dotnet --version
# Should return 9.x.x
```

---

## Setup

1. **Clone the repository**:

```bash
git clone <https://github.com/taraenis/FleetManagementApp.git>
cd FleetManagementApp
```

2. **Running App**:

```bash
dotnet run --project FleetManagementApp.Web
```

3. **Running Tests**:

```bash
dotnet test FleetManagementApp.Tests
```

4. **Build App**:

```bash
dotnet clean
dotnet build
```

5. **Publish App**:

```bash
dotnet publish -c Release
```
